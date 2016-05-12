<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheDemo.aspx.cs" Inherits="YanZhiwei.DotNet2.Utilities.WebFormExamples.CacheDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Set Cache By AbsoluteExpiration" Width="265px" />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Set Cache By SlidingExpiration" Width="265px" />
    
    </div>
    </form>
</body>
</html>
