<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bootboxDemo.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.bs.v3.bootboxDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="../../jquery/jquery-1.9.1.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="bootbox/bootbox.min.js"></script>
    <script src="bootbox/bootboxUtils.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            bootboxUtils.showAutoCloseDialog('测试', '内容', 5000);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
