<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidateImgDemo.aspx.cs" Inherits="YanZhiwei.DotNet3._5.Utilities.WebFormExamples.ValidateImgDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>验证码测试</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img src="ValidateImg.aspx" onclick="this.src='ValidateImg.aspx?qs='+Math.random();"  style="cursor:pointer"/>
        </div>
    </form>
</body>
</html>
