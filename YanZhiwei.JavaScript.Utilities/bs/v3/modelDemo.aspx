<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modelDemo.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.bs.v3.modelDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="../../jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#myModal').on('show.bs.modal', function (e) {
                var _cc = $(e.relatedTarget).attr('data-id');
                $('#divDemo').text(_cc);
            });

             $('#myModal').on('hide.bs.modal', function () {
               alert('嘿，我听说您喜欢模态框...');
              });


            $('#btnSubmit').click("click", function (e) {
                var cc = $('#divDemo').text();
                $('#myModal').modal('hide');
                //  alert(cc);
            })
        });


    </script>
</head>
<body>

    <h2>创建模态框（Modal）</h2>
    <!-- 按钮触发模态框 -->
    <button class="btn btn-primary btn-lg" data-id="yanzhiwei" data-toggle="modal"
        data-target="#myModal">
        开始演示模态框
    </button>

    <a href="#myModal" data-id="yanzhiwei" data-toggle="modal">myModel</a>
    <!-- 模态框（Modal） -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close"
                        data-dismiss="modal" aria-hidden="true">
                        &times;
                    </button>
                    <h4 class="modal-title" id="myModalLabel">模态框（Modal）标题
                    </h4>
                </div>
                <div class="modal-body" id="divDemo">
                    在这里添加一些文本
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default"
                        data-dismiss="modal">
                        关闭
                    </button>
                    <button type="button" id="btnSubmit" class="btn btn-primary">
                        提交更改
                    </button>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
