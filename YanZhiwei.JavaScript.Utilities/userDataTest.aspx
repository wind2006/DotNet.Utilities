<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userDataTest.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.userDataTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>userData Demo</title>
    <script src="userData.js" type="text/javascript"></script>
    <script type="text/javascript">
        function save() {
            userData.save('yanzhiwei', 'test');
        }
        function load() {
            alert(userData.load('yanzhiwei'));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="Button1" type="button" value="save" onclick="save()" />
            <input id="Button2" type="button" value="load" onclick="load()" />
        </div>
    </form>
</body>
</html>
