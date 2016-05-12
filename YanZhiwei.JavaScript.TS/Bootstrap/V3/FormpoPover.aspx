<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPopover.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.FormpoPover" %>

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
    <style type="text/css">
        .bs-example-popover .popover {
            position: relative;
            display: block;
            float: left;
            width: 240px;
            margin: 20px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#a2").popover();
            $("[data-toggle=popover]").popover();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>弹出框</h1>
            <div class="bs-example-popover .popover">
                <div class="popover top">
                    <div class="arrow"></div>
                    <h3 class="popover-title">Popover top</h3>
                    <div class="popover-content">
                        <p>Sed posuere consectetur est at lobortis. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.</p>
                    </div>
                </div>

                <div class="popover right">
                    <div class="arrow"></div>
                    <h3 class="popover-title">Popover right</h3>
                    <div class="popover-content">
                        <p>Sed posuere consectetur est at lobortis. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.</p>
                    </div>
                </div>

                <div class="popover bottom">
                    <div class="arrow"></div>
                    <h3 class="popover-title">Popover bottom</h3>

                    <div class="popover-content">
                        <p>Sed posuere consectetur est at lobortis. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.</p>
                    </div>
                </div>

                <div class="popover left">
                    <div class="arrow"></div>
                    <h3 class="popover-title">Popover left</h3>
                    <div class="popover-content">
                        <p>Sed posuere consectetur est at lobortis. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum.</p>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>
            <h1>动态演示</h1>
            <h4><small>就一个a标签，但是赋予了按钮的样式类，然后给与几个属性，主要用于展示弹出框的： 
                       第一个：data-original-title ——标题 
                       第二个：data-content——内容 
                       第三个：data-placement——位置（上下左右top、bottom、left、right） 
            </small></h4>
            <a id="a2" class="btn btn-lg btn-danger" data-placement="right" data-content="Bootstrap是Twitter推出的一个开源的用于前端开发的工具包。它由Twitter的设计师Mark Otto和Jacob Thornton合作开发，是一个CSS/HTML框架。Bootstrap提供了优雅的HTML和CSS规范，它即是由动态CSS语言Less写成。Bootstrap一经推出后颇受欢迎，一直是GitHub上的热门开源项目，包括NASA的MSNBC（微软全国广播公司）的Breaking News都使用了该项目。" title="" href="#" data-original-title="Bootstrap">Bootstrap百度百科</a>
            <h1>四个方向 </h1>
            <div style="margin-left: 200px; margin-top: 100px; margin-bottom: 200px;" class="bs-example tooltip-demo">
                <div class="bs-example-tooltips">
                    <button type="button" class="btn btn-default" data-container="body" data-toggle="popover" data-placement="left" data-content="Vivamus sagittis lacus vel augue laoreet rutrum faucibus.">左侧弹框 </button>
                    <button type="button" class="btn btn-default" data-container="body" data-toggle="popover" data-placement="top" data-content="Vivamus sagittis lacus vel augue laoreet rutrum faucibus.">上方弹框 </button>
                    <button type="button" class="btn btn-default" data-container="body" data-toggle="popover" data-placement="bottom" data-content="Vivamus sagittis lacus vel augue laoreet rutrum faucibus.">下方弹框 </button>
                    <button type="button" class="btn btn-default" data-container="body" data-toggle="popover" data-placement="right" data-content="Vivamus sagittis lacus vel augue laoreet rutrum faucibus.">右侧弹框 </button>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
