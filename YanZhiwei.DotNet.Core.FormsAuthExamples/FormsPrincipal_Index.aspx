<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormsPrincipal_Index.aspx.cs" Inherits="YanZhiwei.DotNet.Core.FormsAuthExamples.FormsPrincipal_Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label">当前用户已登录，登录名：</asp:Label>
            <asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label><br />
            <asp:Label ID="Label2" runat="server" Text="Label">用户权限：</asp:Label>
            <asp:Label ID="lblRoles" runat="server" Text="Label"></asp:Label><br />
        </div>
        <a href="WebUploadFileDemo.aspx">WebUploadFileDemo.aspx</a><br />
        <a href="WebUploadImageDemo.aspx">WebUploadImageDemo.aspx</a><br />
        <a href="ValidateImgDemo.aspx">ValidateImgDemo.aspx</a>
    </form>
</body>
</html>
