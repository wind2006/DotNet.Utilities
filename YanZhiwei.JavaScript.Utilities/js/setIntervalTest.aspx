<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setIntervalTest.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.js.setIntervalTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>setInterval demo</title>
    <script type="text/javascript">
        var _interval = null;
        window.onload = function () {

            console.log('loaded.');
            demo2();
        }
        function demo1() {
            setInterval(function () {
                var _data = new Date();
                console.log(_data.toLocaleDateString() + ' ' + _data.toLocaleTimeString());
            }, 1000);
        }
        function demo2() {

            _interval = setInterval(function () {
                var _data = new Date();
                console.log(_data.toLocaleDateString() + ' ' + _data.toLocaleTimeString());

            }, 1000);
            return _interval;
        }
        function demo2_stop() {
            console.log('clearInterval');
            window.clearInterval(_interval);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="Button1" type="button" value="stop" onclick="demo2_stop()" />
        </div>
    </form>
</body>
</html>
