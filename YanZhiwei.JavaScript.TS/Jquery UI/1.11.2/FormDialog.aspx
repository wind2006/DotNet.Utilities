<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormDialog.aspx.cs" Inherits="YanZhiwei.JavaScript.Learn.Jquery_UI._1._11._2.FormDialog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="jquery-ui-1.11.2.custom/external/jquery/jquery.js" type="text/javascript"></script>
    <script src="jquery-ui-1.11.2.custom/jquery-ui.js" type="text/javascript"></script>
    <link href="jquery-ui-1.11.2.custom/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        /*
        *参考：
        *jQuery UI Dialog常用的参数有：
        1.autoOpen：默认true，即dialog方法创建就显示对话框
        2.buttons：默认无，用于设置显示的按钮，可以是JSON和Array形式:
        1.{"确定":function(){},"取消":function(){}}
        2.[{text:"确定", click: function(){}},{text:"取消",click:function(){}}]
        3.modal：默认false，是否模态对话框，如果设置为true则会创建一个遮罩层把页面其他元素遮住
        4.title：标题
        5.draggable：是否可手动，默认true
        6.resizable：是否可调整大小，默认true
        7.width：宽度，默认300
        8.height：高度，默认"auto"
        */

        function openDialog() {
            // $("#somediv").load('FormDialogTemplate.aspx').dialog({ modal: true });
            // $("#somediv").dialog({ modal: true });
            $("#somediv").load('FormDialogTemplate.aspx').dialog({
                resizable: false,
                height: 600,
                width: 1000,
                modal: true,
                buttons: {
                    '关闭': function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="somediv">Some text to display</div>
        <a href="#" onclick="openDialog();">Dialog</a>
    </form>
</body>
</html>
