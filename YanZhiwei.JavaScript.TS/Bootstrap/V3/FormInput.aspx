<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormInput.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.FormInput" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>输入框</h1>
            <h4><small>1.通过在基于文本的输入框前面，后面或是两边加上文字或按钮，可以扩展对表单的控制。用带有.input-group-addon的.input-group，可以给.form-control前面或后面追加元素。这里请避免使用'select' 元素，因为 WebKit 浏览器不能完全支持它的样式。</small></h4>
            <h4><small>2.不要直接将.input-group和.form-group混合使用，因为.input-group是一个独立的组件。</small></h4>
            <div class="input-group">
                <span class="input-group-addon">@</span>
                <input type="text" class="form-control" placeholder="Username">
            </div>
            <div class="input-group">
                <input type="text" class="form-control">
                <span class="input-group-addon">.00</span>
            </div>
            <div class="input-group">
                <span class="input-group-addon">$</span>
                <input type="text" class="form-control">
                <span class="input-group-addon">.00</span>
            </div>
            <h1>附加按钮 </h1>
            <div class="input-group">
                <span class="input-group-btn">
                    <button class="btn btn-default" type="button">Go!</button>
                </span>
                <input type="text" class="form-control">
            </div>
            <div class="input-group">
                <input type="text" class="form-control">
                <span class="input-group-btn">
                    <button class="btn btn-default" type="button">Go!</button>
                </span>
            </div>
        </div>
    </form>
</body>
</html>
