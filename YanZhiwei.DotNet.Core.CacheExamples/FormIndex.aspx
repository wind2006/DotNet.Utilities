<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormIndex.aspx.cs" Inherits="YanZhiwei.DotNet.Core.Cache.Examples.FormIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="CacheSet" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Cache Get" />
            <asp:Button ID="Button5" runat="server" Text="ToPageCache" OnClick="Button5_Click" />
            <asp:Button ID="Button3" runat="server" Text="Log Text Set" OnClick="Button3_Click" />
            <asp:Button ID="Button4" runat="server" Text="Log Db Set" OnClick="Button4_Click" />
        </div>
    </form>
</body>
</html>
