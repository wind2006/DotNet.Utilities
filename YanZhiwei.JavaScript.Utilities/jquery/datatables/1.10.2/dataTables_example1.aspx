<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dataTables_example1.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.jquery.datatables._1._10._2.dataTables" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>jQuery DataTables – How to add a checkbox column</title>
    <link href="media/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="media/js/jquery.js" type="text/javascript"></script>
    <script src="media/js/jquery.dataTables.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#example').DataTable({
                //language: {
                //    search: "_INPUT_",
                //    searchPlaceholder: "Search..."
                //},
                'ajax': {
                    'url': 'ids-arrays.txt'
                },
                'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                }],
                'order': [1, 'asc']
            });
            //------增加搜索框水印文字提示-----------
            //$('#example').removeClass('display').addClass('table table-striped table-bordered');
            //$('.dataTables_filter input[type="search"]').attr('placeholder', 'Type in customer name, date or amount').css({ 'width': '250px', 'display': 'inline-block' });
            // Handle click on "Select all" control
            $('#example-select-all').on('click', function () {
                // Get all rows with search applied
                var rows = table.rows({ 'search': 'applied' }).nodes();
                // Check/uncheck checkboxes for all rows in the table
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });

            // Handle click on checkbox to set state of "Select all" control
            $('#example tbody').on('change', 'input[type="checkbox"]', function () {
                // If checkbox is not checked
                if (!this.checked) {
                    var el = $('#example-select-all').get(0);
                    // If "Select all" control is checked and has 'indeterminate' property
                    if (el && el.checked && ('indeterminate' in el)) {
                        // Set visual state of "Select all" control 
                        // as 'indeterminate'
                        el.indeterminate = true;
                    }
                }
            });


            //----------增加单独列搜索------------------
            var table = $('#example').DataTable();
            $('#example tfoot th').each(function () {
                var title = $(this).text();
                if (title != '') {
                    $(this).html('<input type="text" placeholder="Search ' + title + '" />').css({
                        'width': '100%',
                        'padding': '3px',
                        'box-sizing': 'border-box'
                    });
                }
            });
            table.columns().each(function () {
                var that = this;
                $('input', this.footer()).on('keyup change', function () {
                    if (that.search() !== this.value) {
                        that.search(this.value)
                            .draw();
                    }
                });
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="example" class="display select" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <th>
                        <input name="select_all" value="1" id="example-select-all" type="checkbox"></th>
                    <th>Name</th>
                    <th>Position</th>
                    <th>Office</th>
                    <th>Extn.</th>
                    <th>Start date</th>
                    <th>Salary</th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <th></th>
                    <th>Name</th>
                    <th>Position</th>
                    <th>Office</th>
                    <th>Extn.</th>
                    <th>Start date</th>
                    <th>Salary</th>
                </tr>
            </tfoot>
        </table>
    </form>
</body>
</html>
