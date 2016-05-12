<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dataTables_example5.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.jquery.datatables._1._10._2.dataTables_example5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="media/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="media/js/jquery.js" type="text/javascript"></script>
    <script src="media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="datatablesUtils.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //1
            //$('#example').dataTable({
            //    "sPaginationType": "full_numbers",
            //    "bProcessing": true,
            //    "bServerSide": true,
            //    'sAjaxSource': '../../../BackHandler/BaseHandler.ashx'
            //});

            //2
            //var table = $('#example');
            //table.dataTable({
            //    "bPaginate": true,
            //    "bLengthChange": false,
            //    "bFilter": false,
            //    "bSort": true,
            //    "bInfo": false,
            //    'bProcessing': true,
            //    'bServerSide': true,
            //    'sAjaxSource': '../../../BackHandler/BaseHandler.ashx',
            //    "autoWidth": false,
            //    "bStateSave": true,
            //    "oLanguage": {
            //        "sZeroRecords": "抱歉， 没有找到",
            //        "sInfoEmpty": "表中数据为空",
            //        "sZeroRecords": "没有检索到数据",
            //        "sProcessing": "处理中..."
            //    },
            //    "columns": [
            //      { "data": "Id", "title": "用户Id", "class": "center", "width": "50%" },
            //      { "data": "Name", "title": "用户名称", "class": "center", "width": "50%" }
            //    ],
            //    "order": [
            //        [0, "desc"]
            //    ],
            //    "fnServerData": function (sSource, aoData, fnCallback) {
            //        $.ajax({
            //            "dataType": 'json',
            //            "contentType": "application/json; charset=utf-8",
            //            "type": "GET",
            //            "url": sSource,
            //            "data": aoData,
            //            "success": function (msg) {
            //                var json = msg.Message;
            //                fnCallback(json);
            //                $("#example").show();
            //            },
            //            error: function (xhr, textStatus, error) {
            //                if (typeof console == "object") {
            //                    console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
            //                }
            //            }
            //        });

            //    }
            //});
            var _columns = [
                  { "data": "Id", "title": "用户Id", "class": "center", "width": "50%" },
                  { "data": "Name", "title": "用户名称", "class": "center", "width": "50%" }
            ];
            $('#example').executePageQuery(_columns, '../../../BackHandler/BaseHandler.ashx');

        });
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <table border="0" class="display" id="example">
            <%--            <thead>
                <tr class="gridStyle">
                    <th>UserId</th>
                    <th>Name</th>
                </tr>
            </thead>--%>
        </table>
    </form>
</body>
</html>
