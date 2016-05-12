#if (RUNNING_ON_2 || RUNNING_ON_3 || RUNNING_ON_3_5)


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

/// <summary>
/// ExpressionVisitor
/// </summary>
public abstract class ExpressionVisitor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionVisitor"/> class.
    /// </summary>
    protected ExpressionVisitor()
    {
    }

    /// <summary>
    /// Visits the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    /// <exception cref="System.Exception"></exception>
    public virtual Expression Visit(Expression expression)
    {
        if (expression == null)
            return expression;
        switch (expression.NodeType)
        {
            case ExpressionType.Negate:
            case ExpressionType.NegateChecked:
            case ExpressionType.Not:
            case ExpressionType.Convert:
            case ExpressionType.ConvertChecked:
            case ExpressionType.ArrayLength:
            case ExpressionType.Quote:
            case ExpressionType.TypeAs:
                return this.VisitUnary((UnaryExpression)expression);

            case ExpressionType.Add:
            case ExpressionType.AddChecked:
            case ExpressionType.Subtract:
            case ExpressionType.SubtractChecked:
            case ExpressionType.Multiply:
            case ExpressionType.MultiplyChecked:
            case ExpressionType.Divide:
            case ExpressionType.Modulo:
            case ExpressionType.And:
            case ExpressionType.AndAlso:
            case ExpressionType.Or:
            case ExpressionType.OrElse:
            case ExpressionType.LessThan:
            case ExpressionType.LessThanOrEqual:
            case ExpressionType.GreaterThan:
            case ExpressionType.GreaterThanOrEqual:
            case ExpressionType.Equal:
            case ExpressionType.NotEqual:
            case ExpressionType.Coalesce:
            case ExpressionType.ArrayIndex:
            case ExpressionType.RightShift:
            case ExpressionType.LeftShift:
            case ExpressionType.ExclusiveOr:
                return this.VisitBinary((BinaryExpression)expression);

            case ExpressionType.TypeIs:
                return this.VisitTypeIs((TypeBinaryExpression)expression);

            case ExpressionType.Conditional:
                return this.VisitConditional((ConditionalExpression)expression);

            case ExpressionType.Constant:
                return this.VisitConstant((ConstantExpression)expression);

            case ExpressionType.Parameter:
                return this.VisitParameter((ParameterExpression)expression);

            case ExpressionType.MemberAccess:
                return this.VisitMember((MemberExpression)expression);

            case ExpressionType.Call:
                return this.VisitMethodCall((MethodCallExpression)expression);

            case ExpressionType.Lambda:
                return this.VisitLambda((LambdaExpression)expression);

            case ExpressionType.New:
                return this.VisitNew((NewExpression)expression);

            case ExpressionType.NewArrayInit:
            case ExpressionType.NewArrayBounds:
                return this.VisitNewArray((NewArrayExpression)expression);

            case ExpressionType.Invoke:
                return this.VisitInvocation((InvocationExpression)expression);

            case ExpressionType.MemberInit:
                return this.VisitMemberInit((MemberInitExpression)expression);

            case ExpressionType.ListInit:
                return this.VisitListInit((ListInitExpression)expression);

            default:
                throw new Exception(string.Format("Unhandled expression type: '{0}'", expression.NodeType));
        }
    }

    /// <summary>
    /// Visits the binding.
    /// </summary>
    /// <param name="binding">The binding.</param>
    /// <returns>MemberBinding</returns>
    /// <exception cref="System.Exception"></exception>
    protected virtual MemberBinding VisitBinding(MemberBinding binding)
    {
        switch (binding.BindingType)
        {
            case MemberBindingType.Assignment:
                return this.VisitMemberAssignment((MemberAssignment)binding);

            case MemberBindingType.MemberBinding:
                return this.VisitMemberMemberBinding((MemberMemberBinding)binding);

            case MemberBindingType.ListBinding:
                return this.VisitMemberListBinding((MemberListBinding)binding);

            default:
                throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
        }
    }

    /// <summary>
    /// Visits the element initializer.
    /// </summary>
    /// <param name="initializer">The initializer.</param>
    /// <returns>ElementInit</returns>
    protected virtual ElementInit VisitElementInitializer(ElementInit initializer)
    {
        ReadOnlyCollection<Expression> _arguments = this.VisitExpressionList(initializer.Arguments);
        if (_arguments != initializer.Arguments)
        {
            return Expression.ElementInit(initializer.AddMethod, _arguments);
        }
        return initializer;
    }

    /// <summary>
    /// Visits the unary.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitUnary(UnaryExpression expression)
    {
        Expression _expression = this.Visit(expression.Operand);
        if (_expression != expression.Operand)
        {
            return Expression.MakeUnary(expression.NodeType, _expression, expression.Type, expression.Method);
        }
        return expression;
    }

    /// <summary>
    /// Visits the binary.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitBinary(BinaryExpression expression)
    {
        Expression _left = this.Visit(expression.Left);
        Expression _right = this.Visit(expression.Right);
        Expression _expression = this.Visit(expression.Conversion);
        if (_left != expression.Left || _right != expression.Right || _expression != expression.Conversion)
        {
            if (expression.NodeType == ExpressionType.Coalesce && expression.Conversion != null)
                return Expression.Coalesce(_left, _right, _expression as LambdaExpression);
            else
                return Expression.MakeBinary(expression.NodeType, _left, _right, expression.IsLiftedToNull, expression.Method);
        }
        return expression;
    }

    /// <summary>
    /// Visits the type is.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitTypeIs(TypeBinaryExpression expression)
    {
        Expression _expression = this.Visit(expression.Expression);
        if (_expression != expression.Expression)
        {
            return Expression.TypeIs(_expression, expression.TypeOperand);
        }
        return expression;
    }

    /// <summary>
    /// Visits the constant.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitConstant(ConstantExpression expression)
    {
        return expression;
    }

    /// <summary>
    /// Visits the conditional.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitConditional(ConditionalExpression expression)
    {
        Expression _expression = this.Visit(expression.Test);
        Expression _ifTrue = this.Visit(expression.IfTrue);
        Expression _ifFalse = this.Visit(expression.IfFalse);
        if (_expression != expression.Test || _ifTrue != expression.IfTrue || _ifFalse != expression.IfFalse)
        {
            return Expression.Condition(_expression, _ifTrue, _ifFalse);
        }
        return expression;
    }

    /// <summary>
    /// Visits the parameter.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitParameter(ParameterExpression expression)
    {
        return expression;
    }

    /// <summary>
    /// Visits the member.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitMember(MemberExpression expression)
    {
        Expression _expression = this.Visit(expression.Expression);
        if (_expression != expression.Expression)
        {
            return Expression.MakeMemberAccess(_expression, expression.Member);
        }
        return expression;
    }

    /// <summary>
    /// Visits the method call.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitMethodCall(MethodCallExpression expression)
    {
        Expression _expression = this.Visit(expression.Object);
        IEnumerable<Expression> _args = this.VisitExpressionList(expression.Arguments);
        if (_expression != expression.Object || _args != expression.Arguments)
        {
            return Expression.Call(_expression, expression.Method, _args);
        }
        return expression;
    }

    /// <summary>
    /// Visits the expression list.
    /// </summary>
    /// <param name="original">The original.</param>
    /// <returns>ReadOnlyCollection</returns>
    protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
    {
        List<Expression> _list = null;
        for (int i = 0, n = original.Count; i < n; i++)
        {
            Expression p = this.Visit(original[i]);
            if (_list != null)
            {
                _list.Add(p);
            }
            else if (p != original[i])
            {
                _list = new List<Expression>(n);
                for (int j = 0; j < i; j++)
                {
                    _list.Add(original[j]);
                }
                _list.Add(p);
            }
        }
        if (_list != null)
        {
            return _list.AsReadOnly();
        }
        return original;
    }

    /// <summary>
    /// Visits the member assignment.
    /// </summary>
    /// <param name="assignment">The assignment.</param>
    /// <returns>MemberAssignment</returns>
    protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
    {
        Expression _expression = this.Visit(assignment.Expression);
        if (_expression != assignment.Expression)
        {
            return Expression.Bind(assignment.Member, _expression);
        }
        return assignment;
    }

    /// <summary>
    /// Visits the member member binding.
    /// </summary>
    /// <param name="binding">The binding.</param>
    /// <returns>MemberMemberBinding</returns>
    protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
    {
        IEnumerable<MemberBinding> _bindings = this.VisitBindingList(binding.Bindings);
        if (_bindings != binding.Bindings)
        {
            return Expression.MemberBind(binding.Member, _bindings);
        }
        return binding;
    }

    /// <summary>
    /// Visits the member list binding.
    /// </summary>
    /// <param name="binding">The binding.</param>
    /// <returns>MemberListBinding</returns>
    protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
    {
        IEnumerable<ElementInit> _initializers = this.VisitElementInitializerList(binding.Initializers);
        if (_initializers != binding.Initializers)
        {
            return Expression.ListBind(binding.Member, _initializers);
        }
        return binding;
    }

    /// <summary>
    /// Visits the binding list.
    /// </summary>
    /// <param name="original">The original.</param>
    /// <returns>IEnumerable</returns>
    protected virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
    {
        List<MemberBinding> _list = null;
        for (int i = 0, n = original.Count; i < n; i++)
        {
            MemberBinding _binding = this.VisitBinding(original[i]);
            if (_list != null)
            {
                _list.Add(_binding);
            }
            else if (_binding != original[i])
            {
                _list = new List<MemberBinding>(n);
                for (int j = 0; j < i; j++)
                {
                    _list.Add(original[j]);
                }
                _list.Add(_binding);
            }
        }
        if (_list != null)
            return _list;
        return original;
    }

    /// <summary>
    /// Visits the element initializer list.
    /// </summary>
    /// <param name="original">The original.</param>
    /// <returns>IEnumerable</returns>
    protected virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
    {
        List<ElementInit> _list = null;
        for (int i = 0, n = original.Count; i < n; i++)
        {
            ElementInit _init = this.VisitElementInitializer(original[i]);
            if (_list != null)
            {
                _list.Add(_init);
            }
            else if (_init != original[i])
            {
                _list = new List<ElementInit>(n);
                for (int j = 0; j < i; j++)
                {
                    _list.Add(original[j]);
                }
                _list.Add(_init);
            }
        }
        if (_list != null)
            return _list;
        return original;
    }

    /// <summary>
    /// Visits the lambda.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitLambda(LambdaExpression expression)
    {
        Expression _expression = this.Visit(expression.Body);
        if (_expression != expression.Body)
        {
            return Expression.Lambda(expression.Type, _expression, expression.Parameters);
        }
        return expression;
    }

    /// <summary>
    /// Visits the new.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>NewExpression</returns>
    protected virtual NewExpression VisitNew(NewExpression expression)
    {
        IEnumerable<Expression> _args = this.VisitExpressionList(expression.Arguments);
        if (_args != expression.Arguments)
        {
            if (expression.Members != null)
                return Expression.New(expression.Constructor, _args, expression.Members);
            else
                return Expression.New(expression.Constructor, _args);
        }
        return expression;
    }

    /// <summary>
    /// Visits the member initialize.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitMemberInit(MemberInitExpression expression)
    {
        NewExpression _expression = this.VisitNew(expression.NewExpression);
        IEnumerable<MemberBinding> _bindings = this.VisitBindingList(expression.Bindings);
        if (_expression != expression.NewExpression || _bindings != expression.Bindings)
        {
            return Expression.MemberInit(_expression, _bindings);
        }
        return expression;
    }

    /// <summary>
    /// Visits the list initialize.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitListInit(ListInitExpression expression)
    {
        NewExpression _expression = this.VisitNew(expression.NewExpression);
        IEnumerable<ElementInit> _initializers = this.VisitElementInitializerList(expression.Initializers);
        if (_expression != expression.NewExpression || _initializers != expression.Initializers)
        {
            return Expression.ListInit(_expression, _initializers);
        }
        return expression;
    }

    /// <summary>
    /// Visits the new array.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitNewArray(NewArrayExpression expression)
    {
        IEnumerable<Expression> _exprs = this.VisitExpressionList(expression.Expressions);
        if (_exprs != expression.Expressions)
        {
            if (expression.NodeType == ExpressionType.NewArrayInit)
            {
                return Expression.NewArrayInit(expression.Type.GetElementType(), _exprs);
            }
            else
            {
                return Expression.NewArrayBounds(expression.Type.GetElementType(), _exprs);
            }
        }
        return expression;
    }

    /// <summary>
    /// Visits the invocation.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Expression</returns>
    protected virtual Expression VisitInvocation(InvocationExpression expression)
    {
        IEnumerable<Expression> _args = this.VisitExpressionList(expression.Arguments);
        Expression _expr = this.Visit(expression.Expression);
        if (_args != expression.Arguments || _expr != expression.Expression)
        {
            return Expression.Invoke(_expr, _args);
        }
        return expression;
    }
}
#endif