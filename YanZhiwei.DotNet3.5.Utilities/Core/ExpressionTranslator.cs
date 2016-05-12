namespace YanZhiwei.DotNet3._5.Utilities.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    /// <summary>
    /// ExpressionTranslator
    /// </summary>
    /// 时间：2016-01-08 9:53
    /// 备注：
    public class ExpressionTranslator : ExpressionVisitor
    {
        #region Fields

        private StringBuilder builder;
        private int? skip = null;
        private int? take = null;
        private string whereClause = string.Empty;
        private string _orderBy = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTranslator"/> class.
        /// </summary>
        public ExpressionTranslator()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the order by.
        /// </summary>
        public string OrderBy
        {
            get
            {
                return _orderBy;
            }
        }

        /// <summary>
        /// Gets the skip.
        /// </summary>
        public int? Skip
        {
            get
            {
                return skip;
            }
        }

        /// <summary>
        /// Gets the take.
        /// </summary>
        public int? Take
        {
            get
            {
                return take;
            }
        }

        /// <summary>
        /// Gets the where clause.
        /// </summary>
        public string WhereClause
        {
            get
            {
                return whereClause;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Translates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>KeySelector表达式</returns>
        public string Translate(Expression expression)
        {
            this.builder = new StringBuilder();
            this.Visit(expression);
            whereClause = this.builder.ToString();
            return whereClause;
        }

        /// <summary>
        /// Determines whether [is null constant] [the specified expression].
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>bool</returns>
        protected bool IsNullConstant(Expression expression)
        {
            return (expression.NodeType == ExpressionType.Constant && ((ConstantExpression)expression).Value == null);
        }

        /// <summary>
        /// Visits the binary.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// Expression
        /// </returns>
        /// <exception cref="System.NotSupportedException"></exception>
        protected override Expression VisitBinary(BinaryExpression expression)
        {
            builder.Append("(");
            this.Visit(expression.Left);

            switch (expression.NodeType)
            {
                case ExpressionType.And:
                    builder.Append(" AND ");
                    break;

                case ExpressionType.AndAlso:
                    builder.Append(" AND ");
                    break;

                case ExpressionType.Or:
                    builder.Append(" OR ");
                    break;

                case ExpressionType.OrElse:
                    builder.Append(" OR ");
                    break;

                case ExpressionType.Equal:
                    if (IsNullConstant(expression.Right))
                    {
                        builder.Append(" IS ");
                    }
                    else
                    {
                        builder.Append(" = ");
                    }
                    break;

                case ExpressionType.NotEqual:
                    if (IsNullConstant(expression.Right))
                    {
                        builder.Append(" IS NOT ");
                    }
                    else
                    {
                        builder.Append(" <> ");
                    }
                    break;

                case ExpressionType.LessThan:
                    builder.Append(" < ");
                    break;

                case ExpressionType.LessThanOrEqual:
                    builder.Append(" <= ");
                    break;

                case ExpressionType.GreaterThan:
                    builder.Append(" > ");
                    break;

                case ExpressionType.GreaterThanOrEqual:
                    builder.Append(" >= ");
                    break;

                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", expression.NodeType));
            }

            this.Visit(expression.Right);
            builder.Append(")");
            return expression;
        }

        /// <summary>
        /// Visits the constant.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// Expression
        /// </returns>
        /// <exception cref="System.NotSupportedException"></exception>
        protected override Expression VisitConstant(ConstantExpression expression)
        {
            IQueryable _queryable = expression.Value as IQueryable;

            if (_queryable == null && expression.Value == null)
            {
                builder.Append("NULL");
            }
            else if (_queryable == null)
            {
                switch (Type.GetTypeCode(expression.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        builder.Append(((bool)expression.Value) ? 1 : 0);
                        break;

                    case TypeCode.String:
                        builder.Append("'");
                        builder.Append(expression.Value);
                        builder.Append("'");
                        break;

                    case TypeCode.DateTime:
                        builder.Append("'");
                        builder.Append(expression.Value);
                        builder.Append("'");
                        break;

                    case TypeCode.Object:
                        throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", expression.Value));

                    default:
                        builder.Append(expression.Value);
                        break;
                }
            }

            return expression;
        }

        /// <summary>
        /// Visits the member.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// Expression
        /// </returns>
        /// <exception cref="System.NotSupportedException"></exception>
        protected override Expression VisitMember(MemberExpression expression)
        {
            if (expression.Expression != null && expression.Expression.NodeType == ExpressionType.Parameter)
            {
                builder.Append(expression.Member.Name);
                return expression;
            }

            throw new NotSupportedException(string.Format("The member '{0}' is not supported", expression.Member.Name));
        }

        /// <summary>
        /// Visits the method call.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// Expression
        /// </returns>
        /// <exception cref="System.NotSupportedException"></exception>
        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.DeclaringType == typeof(Queryable) && expression.Method.Name == "Where")
            {
                this.Visit(expression.Arguments[0]);
                LambdaExpression lambda = (LambdaExpression)StripQuotes(expression.Arguments[1]);
                this.Visit(lambda.Body);
                return expression;
            }
            else if (expression.Method.Name == "Take")
            {
                if (this.ParseTakeExpression(expression))
                {
                    Expression nextExpression = expression.Arguments[0];
                    return this.Visit(nextExpression);
                }
            }
            else if (expression.Method.Name == "Skip")
            {
                if (this.ParseSkipExpression(expression))
                {
                    Expression nextExpression = expression.Arguments[0];
                    return this.Visit(nextExpression);
                }
            }
            else if (expression.Method.Name == "OrderBy")
            {
                if (this.ParseOrderByExpression(expression, "ASC"))
                {
                    Expression nextExpression = expression.Arguments[0];
                    return this.Visit(nextExpression);
                }
            }
            else if (expression.Method.Name == "OrderByDescending")
            {
                if (this.ParseOrderByExpression(expression, "DESC"))
                {
                    Expression nextExpression = expression.Arguments[0];
                    return this.Visit(nextExpression);
                }
            }

            throw new NotSupportedException(string.Format("The method '{0}' is not supported", expression.Method.Name));
        }

        /// <summary>
        /// Visits the unary.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// Expression
        /// </returns>
        /// <exception cref="System.NotSupportedException"></exception>
        protected override Expression VisitUnary(UnaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Not:
                    builder.Append(" NOT ");
                    this.Visit(expression.Operand);
                    break;

                case ExpressionType.Convert:
                    this.Visit(expression.Operand);
                    break;

                default:
                    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", expression.NodeType));
            }
            return expression;
        }

        private static Expression StripQuotes(Expression expression)
        {
            while (expression.NodeType == ExpressionType.Quote)
            {
                expression = ((UnaryExpression)expression).Operand;
            }
            return expression;
        }

        private bool ParseOrderByExpression(MethodCallExpression expression, string order)
        {
            UnaryExpression unary = (UnaryExpression)expression.Arguments[1];
            LambdaExpression lambdaExpression = (LambdaExpression)unary.Operand;

            lambdaExpression = (LambdaExpression)ExpressionEvaluator.PartialEval(lambdaExpression);

            MemberExpression body = lambdaExpression.Body as MemberExpression;
            if (body != null)
            {
                if (string.IsNullOrEmpty(_orderBy))
                {
                    _orderBy = string.Format("{0} {1}", body.Member.Name, order);
                }
                else
                {
                    _orderBy = string.Format("{0}, {1} {2}", _orderBy, body.Member.Name, order);
                }

                return true;
            }

            return false;
        }

        private bool ParseSkipExpression(MethodCallExpression expression)
        {
            ConstantExpression _sizeExpression = (ConstantExpression)expression.Arguments[1];

            int _size;
            if (int.TryParse(_sizeExpression.Value.ToString(), out _size))
            {
                skip = _size;
                return true;
            }

            return false;
        }

        private bool ParseTakeExpression(MethodCallExpression expression)
        {
            ConstantExpression _sizeExpression = (ConstantExpression)expression.Arguments[1];

            int _size;
            if (int.TryParse(_sizeExpression.Value.ToString(), out _size))
            {
                take = _size;
                return true;
            }

            return false;
        }

        #endregion Methods
    }
}