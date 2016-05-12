<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormButton.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.FormButton" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script type="text/javascript">
        function buttonLoading() {
            var btn = $("#fatbtn");
            btn.button('loading')
            setTimeout(function () {
                btn.button('reset')
            }, 3000)
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>按钮</h1>
            <h4><small>1.使用.btn-lg、.btn-sm、.btn-xs可以获得不同尺寸的按钮.</small></h4>
            <h4><small>2.通过给按钮添加.btn-block可以使其充满父节点100%的宽度，而且按钮也变为了块级（block）元素。</small></h4>
            <button type="button" class="btn btn-default">Default</button>
            <button type="button" class="btn btn-primary">Primary[主要，提供额外的视觉感，可在一系列按钮中指出主要操作]</button>
            <button type="button" class="btn btn-success">Success</button>
            <button type="button" class="btn btn-info">Info</button>
            <button type="button" class="btn btn-warning">Warning</button>
            <button type="button" class="btn btn-danger">Danger</button>
            <button type="button" class="btn btn-link">链接</button>
            <h1>活动状态 </h1>
            <button type="button" class="btn btn-danger active">活动状态</button>
            <h1>禁用状态 </h1>
            <button type="button" class="btn btn-lg btn-primary" disabled="disabled">Primary button</button>
            <button type="button" class="btn btn-default btn-lg" disabled="disabled">Button</button>
            <h1>链接元素</h1>
            <a href="#" class="btn btn-primary btn-lg disabled" role="button">Primary link</a>
            <a href="#" class="btn btn-default btn-lg disabled" role="button">Link</a>
            <h1>可做按钮使用的Html标签 </h1>
            <h4><small>1.作为最佳实践，我们强烈建议尽可能使用'button'元素以确保跨浏览器的一致性样式。除去其它原因，这个Firefox的bug让我们无法为基于'input'标签的按钮设置line-height，这导致在Firefox上，他们与其它按钮的高度不一致。 </small></h4>
            <a class="btn btn-default" href="#" role="button">Link</a>
            <button class="btn btn-default" type="submit">Button</button>
            <input class="btn btn-default" type="button" value="Input">
            <input class="btn btn-default" type="submit" value="Submit">
            <h1>状态</h1>
            <button type="button" id="fatbtn" data-loading-text="正在加载..." class="btn btn-primary" onclick="buttonLoading()">Loading state </button>
        </div>
    </form>
</body>
</html>
