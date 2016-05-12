namespace YanZhiwei.DotNet3._5.Utilities.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal static class ExpressionEvaluator
    {
        #region Methods

        public static Expression PartialEval(Expression expression, Func<Expression, bool> fnCanBeEvaluated)
        {
            return new SubtreeEvaluator(new Nominator(fnCanBeEvaluated).Nominate(expression)).Eval(expression);
        }

        public static Expression PartialEval(Expression expression)
        {
            return PartialEval(expression, ExpressionEvaluator.CanBeEvaluatedLocally);
        }

        private static bool CanBeEvaluatedLocally(Expression expression)
        {
            return expression.NodeType != ExpressionType.Parameter;
        }

        #endregion Methods

        #region Nested Types

        private class Nominator : ExpressionVisitor
        {
            #region Fields

            private HashSet<Expression> _candidates;
            private bool _cannotBeEvaluated;
            private Func<Expression, bool> _fnCanBeEvaluated;

            #endregion Fields

            #region Constructors

            internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
            {
                this._fnCanBeEvaluated = fnCanBeEvaluated;
            }

            #endregion Constructors

            #region Methods

            /// <summary>
            /// Visits the specified expression.
            /// </summary>
            /// <param name="expression">The expression.</param>
            /// <returns>Expression</returns>
            /// 时间：2016-01-08 10:02
            /// 备注：
            public override Expression Visit(Expression expression)
            {
                if (expression != null)
                {
                    bool _saveCannotBeEvaluated = this._cannotBeEvaluated;
                    this._cannotBeEvaluated = false;
                    base.Visit(expression);
                    if (!this._cannotBeEvaluated)
                    {
                        if (this._fnCanBeEvaluated(expression))
                        {
                            this._candidates.Add(expression);
                        }
                        else {
                            this._cannotBeEvaluated = true;
                        }
                    }
                    this._cannotBeEvaluated |= _saveCannotBeEvaluated;
                }
                return expression;
            }

            internal HashSet<Expression> Nominate(Expression expression)
            {
                this._candidates = new HashSet<Expression>();
                this.Visit(expression);
                return this._candidates;
            }

            #endregion Methods
        }

        private class SubtreeEvaluator : ExpressionVisitor
        {
            #region Fields

            private HashSet<Expression> _candidates;

            #endregion Fields

            #region Constructors

            internal SubtreeEvaluator(HashSet<Expression> candidates)
            {
                this._candidates = candidates;
            }

            #endregion Constructors

            #region Methods

            public override Expression Visit(Expression expression)
            {
                if (expression == null)
                {
                    return null;
                }
                if (this._candidates.Contains(expression))
                {
                    return this.Evaluate(expression);
                }
                return base.Visit(expression);
            }

            internal Expression Eval(Expression expression)
            {
                return this.Visit(expression);
            }

            private Expression Evaluate(Expression expression)
            {
                if (expression.NodeType == ExpressionType.Constant)
                {
                    return expression;
                }
                LambdaExpression _lambda = Expression.Lambda(expression);
                Delegate _fn = _lambda.Compile();
                return Expression.Constant(_fn.DynamicInvoke(null), expression.Type);
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}