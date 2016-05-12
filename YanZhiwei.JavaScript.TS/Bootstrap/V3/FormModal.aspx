<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormModal.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Bootstrap.V3.FormModal" %>

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
        $(document).ready(function () {
            $('#myModal').on('hide.bs.modal', function () {
                alert('hide.bs.modal event');
            });
            $('#myModal').on('hiden.bs.modal', function () {
                alert('hiden.bs.modal event');
            });
            $('#myModal').on('show.bs.modal', function () {
                alert('show.bs.modal event');
            });
            $('#myModal').on('shown.bs.modal', function () {
                alert('shown.bs.modal event');
            });
        });
        function modelShow() {
            $('#myModal').modal('show');
        }
        function modelHide() {
            $('#myModal').modal('hide');
        }
        function modelToggle() {
            $('#myModal').modal('toggle');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>模态框</h1>
            <h4><small>增强模态框的可访问性 请确保为.modal添加了role="dialog"；aria-labelledby="myModalLabel"属性指向模态框标题；aria-hidden="true"告诉辅助性工具略过模态框的DOM元素。</small></h4>
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Modal title</h4>
                        </div>
                        <div class="modal-body">One fine body&hellip; </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-primary">Save changes</button>
                        </div>
                    </div>
                </div>
            </div>
            <h1>用法--通过data属性</h1>
            <h4><small>通过在一个起控制器作用的页面元素（例如，按钮）上设置data-toggle="modal"，并使用data-target="#foo"或href="#foo"指向特定的模态框即可.</small></h4>
            <button class="btn btn-primary" type="button" data-toggle="modal" data-target="#myModal">弹出模态框方式一</button>
            <h1>用法--通过JavaScript</h1>
            <button class="btn btn-primary" type="button" onclick="modelShow()">打开</button>
            <button class="btn btn-primary" type="button" onclick="modelHide()">隐藏</button>
            <button class="btn btn-primary" type="button" onclick="modelToggle()">启动或隐藏模态框</button>
        </div>
    </form>
</body>
</html>
