<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebUploadImageDemo.aspx.cs" Inherits="YanZhiwei.DotNet3._5.Utilities.WebFormExamples.WebUploadImageDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" method="post" enctype="multipart/form-data" action="WebUploadImageDemo.aspx">
        <input id="File1" type="file" name="File1"/>
        <input id="Submit1" type="submit" value="submit" />
    </form>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</body>
</html>
