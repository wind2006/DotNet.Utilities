<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dataTables_example3.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.jquery.datatables._1._10._2.dataTables_example3__" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="media/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="media/js/jquery.js" type="text/javascript"></script>
    <script src="media/js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="datatablesUtils.js" type="text/javascript"></script>
    <script type="text/javascript">


        $(document).ready(function () {


            var _columnDefs = [{
                "targets": -1,
                "data": null,
                "defaultContent": "<button>Click!</button>"
            },
                    {
                        'targets': 0,
                        'searchable': false,
                        'orderable': false,
                        'className': 'dt-body-center',
                        'render': function (data, type, full, meta) {
                            return '<input type="checkbox">';
                        }
                    }]



            var table = $('#example').initByColumnDefs(_columnDefs, "arrays.txt");
            table.applySyncCheckstate('ckAll');
            $('#example tbody').on('click', 'button', function () {
                var data = table.DataTable().row($(this).parents('tr')).data();
                alert(data[0] + "'s salary is: " + data[5]);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="example" class="display" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <th>
                        <input name="select_all" id="ckAll" value="1" type="checkbox">
                    </th>
                    <th>Name</th>
                    <th>Position</th>
                    <th>Office</th>
                    <th>Extn.</th>
                    <th>Start date</th>
                    <th>Salary</th>
                </tr>
            </thead>


        </table>
    </form>
</body>
</html>
