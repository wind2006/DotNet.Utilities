<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxBaseTable.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.DataTables.V1.DataTables_1._10._6.AjaxBaseTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="media/css/jquery.dataTables.css" rel="stylesheet" />
    <script src="media/js/jquery.js" type="text/javascript"></script>
    <script src="media/js/jquery.dataTables.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var baseTable = $("#ajaxBaseTable");
            baseTable.dataTable({
                language: {
                    "sProcessing": "处理中...",
                    "sLengthMenu": "显示 _MENU_ 项结果",
                    "sZeroRecords": "没有匹配结果",
                    "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
                    "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
                    "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
                    "sInfoPostFix": "",
                    "sSearch": "搜索:",
                    "sUrl": "",
                    "sEmptyTable": "表中数据为空",
                    "sLoadingRecords": "载入中...",
                    "sInfoThousands": ",",
                    "oPaginate": {
                        "sFirst": "首页",
                        "sPrevious": "上页",
                        "sNext": "下页",
                        "sLast": "末页"
                    },
                    "oAria": {
                        "sSortAscending": ": 以升序排列此列",
                        "sSortDescending": ": 以降序排列此列"
                    }
                },
                columns: [
                    { "data": "ProductID" },
                    { "data": "ProductName" },
                    { "data": "UnitPrice" }
                ],
                ajax: {
                    url: '../../../BackHandler/BaseHandler.ashx?action=initDataTable',
                    type:'post'
                }
            });

            //$.ajax({
            //    url: '../../../BackHandler/BaseHandler.ashx?action=initDataTable',
            //    dataType: 'json',
            //    success: function (s) {
            //        baseTable.DataTable({
            //            data: $.parseJSON(s)
            //        });
            //    },
            //    error: function (e) {
            //        console.log(e.responseText);
            //    }
            //});

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" class="display" id="ajaxBaseTable" cellspacing="0">
            <thead>
                <tr>
                    <th>ProductID</th>
                    <th>ProductName</th>
                    <th>UnitPrice</th>
                </tr>
            </thead>
        </table>
    </form>
</body>
</html>
