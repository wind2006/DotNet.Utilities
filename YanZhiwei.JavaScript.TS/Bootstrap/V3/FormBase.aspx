<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormBase.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.FormTitle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Bootstrap3 基本模板</title>
    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="js/html5shiv.min.js"></script>
      <script src="js/respond.min.js"></script>
    <![endif]-->
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="js/jquery-1.11.1.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1 class="page-header">标题<small>副标题</small></h1>
            <strong>强调</strong>
            <em>斜体</em>
            <p class="text-left">文本内容居左</p>
            <p class="text-center">文本内容居中</p>
            <p class="text-right">文本内容居右</p>
            <h1>强调色</h1>
            <p class="text-muted">这个是text-muted属性的强调色！</p>
            <p class="text-primary">这个是text-primary属性的强调色！</p>
            <p class="text-success">这个是text-success属性的强调色！</p>
            <p class="text-info">这个是text-info属性的强调色！</p>
            <p class="text-warning">这个是text-warning属性的强调色！</p>
            <p class="text-danger">这个是text-danger属性的强调色！</p>
            <h1>基本缩略语</h1>
            <abbr title="你好，世界">abbr</abbr>
            <abbr title="你好，世界" class="initialism">abbr</abbr>
            <h1>地址</h1>
            <address>
                <strong>LH Inc</strong><br />
                上海市卢湾区蒙自路<br />
                新大楼3F11<br />
                <abbr title="联系方式">Phone:</abbr>200111
            </address>
            <h1>引用<small> 将任何HTML裹在'blockquote'之中即可表现为引用。对于直接引用，我们建议用'p'标签。</small></h1>
            <blockquote>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.</p>
            </blockquote>
            <blockquote>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.</p>
                <small>参考来源：<cite title="维基百科">维基百科</cite></small>
            </blockquote>
            <blockquote class="pull-right">
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.</p>
            </blockquote>
            <h1>内联代码</h1>
            <code>
                <pre class="pre-scrollable">
        public static IQueryable<T> OrderBy<T, TProperty>(this IQueryable<T> queryable, Expression<Func<T, TProperty>> expression, bool desc = false)
        {
            MemberExpression _memberExpression = expression.Body as MemberExpression;
            string _propertyName = _memberExpression.Member.Name;
            dynamic keySelector = GetLambdaExpression<T>(_propertyName);
            return desc ? Queryable.OrderByDescending(queryable, keySelector) : Queryable.OrderBy(queryable, keySelector);
        }
                </pre>
            </code>
        </div>
    </form>
</body>
</html>
