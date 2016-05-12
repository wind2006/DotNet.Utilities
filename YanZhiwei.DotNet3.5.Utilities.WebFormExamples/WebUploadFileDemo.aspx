<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebUploadFileDemo.aspx.cs" Inherits="YanZhiwei.DotNet3._5.Utilities.WebForm.Examples.WebUploadFileDemo" %>

&nbsp;

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" method="post" enctype="multipart/form-data" action="WebUploadFileDemo.aspx">
        <input id="File1" type="file" name="File1"/>
        <input id="Submit1" type="submit" value="submit" />
    </form>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</body>
</html>
