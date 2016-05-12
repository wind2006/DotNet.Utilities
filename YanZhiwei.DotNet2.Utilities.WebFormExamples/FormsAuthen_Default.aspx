<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormsAuthen_Default.aspx.cs" Inherits="YanZhiwei.DotNet2.Utilities.WebForm.Examples.FormsAuthen_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <fieldset>
        <legend>用户状态</legend>
        <form action="<%= Request.RawUrl %>" method="post">
            <% if (Request.IsAuthenticated)
               { %>
        当前用户已登录，登录名：<%= Context.User.Identity.Name %>
            <br />
            <% var user = Context.User;   %>
            <input type="submit" name="Logon" value="退出" />
            <% }
               else
               { %>
            <b>当前用户还未登录。</b>
            <% } %>
        </form>
    </fieldset>
</body>
</html>
