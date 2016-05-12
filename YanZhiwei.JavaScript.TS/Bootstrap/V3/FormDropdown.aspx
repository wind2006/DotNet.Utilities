<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormDropdown.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.FormDropdown" %>

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
            <h1>下拉框</h1>
            <h4><small>1.button按钮中有个dropdown-toggle，还有一个data-toggle属性，根据这个属性来弹出下来列表。</small></h4>
            <h4><small>2.ul标签的dropdown-menu应该是和上面button按钮的样式类dropdown-toggle联合使用，在通过aria-labelledby绑定上面的button按钮。</small></h4>
            <h4><small>3.divider是一个分割线的样式类。 </small></h4>
            <h4><small>4.dropdown-header通过添加标题来标明一组动作</small></h4>
            <div class="dropdown">
                <button type="button" id="dropdown1" class="btn dropdown-toggle" data-toggle="dropdown">
                    Dropdown <span class="caret" />
                </button>
                <ul class="dropdown-menu text-center" role="menu" aria-labelledby="dropdown1">
                    <li role="presentation" class="dropdown-header">时事</li>
                    <li role="presentation"><a role="menuitem" tabindex="-1" href="#">体育</a></li>
                    <li role="presentation"><a role="menuitem" tabindex="-1" href="#">娱乐</a></li>
                    <li role="presentation" class="divider">教育</li>
                    <li class="disabled" role="presentation"><a role="menuitem" tabindex="-1" href="#">Something else here</a></li>
                    <li role="presentation"><a role="menuitem" tabindex="-1" href="#">科技</a></li>
                </ul>
            </div>
            <h1>按钮组</h1>
            <div class="btn-group">
                <button type="button" class="btn btn-default">中超</button>
                <button type="button" class="btn btn-default">国足</button>
                <button type="button" class="btn btn-default">亚冠</button>
            </div>
            <h1>按钮工具栏</h1>
            <h4><small>1.只要给.btn-group加上.btn-group-*，而不是给组中每个按钮都应用大小类。</small></h4>
            <div class="btn-toolbar" role="toolbar">
                <div class="btn-group">
                    <button type="button" class="btn btn-default">1</button>
                    <button type="button" class="btn btn-default">2</button>
                    <button type="button" class="btn btn-default">3</button>
                    <button type="button" class="btn btn-default">4</button>
                    <button type="button" class="btn btn-default">5</button>
                    <button type="button" class="btn btn-default">6</button>
                </div>
                <div class="btn-group">
                    <button type="button" class="btn btn-default">7</button>
                    <button type="button" class="btn btn-default">8</button>
                </div>
                <div class="btn-group">
                    <button type="button" class="btn btn-default">9</button>
                </div>
            </div>
            <h1>嵌套</h1>
            <div class="btn-group">
                <button type="button" class="btn btn-default">时事</button>
                <button type="button" class="btn btn-default">科技</button>
                <div class="btn-group">
                    <button id="dropdown2" type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                        足球
                        <span class="caret" />
                    </button>
                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropdown2">
                        <li><a href="#">国足</a></li>
                        <li><a href="#">中超</a></li>
                        <li><a href="#">亚冠</a></li>
                    </ul>
                </div>
            </div>
            <h1>两端对齐的链接排列</h1>
            <div class="btn-group btn-group-justified">
                <button type="button" class="btn btn-default">Left</button>
                <button type="button" class="btn btn-default">Middle</button>
                <button type="button" class="btn btn-default">Right</button>
            </div>
        </div>
    </form>
</body>
</html>
