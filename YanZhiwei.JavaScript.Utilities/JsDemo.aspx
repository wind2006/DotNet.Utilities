<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JsDemo.aspx.cs" Inherits="YanZhiwei.JavaScript.Utilities.JsDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Js Demo</title>
    <script src="jsUtils.js"></script>
    <script type="text/javascript">
        function getFriendlyStringDemo() {
            var _time = '2011-10-12 09:14:12';
            _time = jsUtils.datetime.parseDateTime(_time);
            console.log("getFriendlyString：" + jsUtils.datetime.getFriendlyString(_time));
        }
        function getUrlParamterDemo() {
            var _url = window.location.href;
            var _value = jsUtils.url.get(_url, 'keyid');
            alert(_value);

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="Button1" type="button" value="getFriendlyString" onclick="getFriendlyStringDemo()" /><br />
            <input id="Button2" type="button" value="getUrl" onclick="getUrlParamterDemo()" />
        </div>
    </form>
</body>
</html>
