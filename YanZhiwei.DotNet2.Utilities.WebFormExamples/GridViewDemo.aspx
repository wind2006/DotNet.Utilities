<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridViewDemo.aspx.cs" Inherits="YanZhiwei.DotNet2.Utilities.WebFormExamples.GridViewDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>GirdView Demo</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div ></div>
        <div class="container">
            <asp:GridView ID="gvDemo" runat="server" CssClass="table table-hover table-striped" GridLines="None" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" SortExpression="ProductID" />
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                    <asp:BoundField DataField="QuantityPerUnit" HeaderText="QuantityPerUnit" SortExpression="QuantityPerUnit" />
                    <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" SortExpression="UnitPrice" />
                    <asp:BoundField DataField="UnitsOnOrder" HeaderText="UnitsOnOrder" SortExpression="UnitsOnOrder" />
                    <asp:BoundField DataField="Discontinued" HeaderText="Discontinued" SortExpression="Discontinued" />
                </Columns>
                <RowStyle CssClass="cursor-pointer" />
                <PagerStyle CssClass="active" />
                <PagerTemplate>
                    <asp:LinkButton ID="lbtnFirst" runat="server" Font-Overline="false" CommandName="Page"
                        CommandArgument="1">首页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnPrev" runat="server" Font-Overline="false">上一页</asp:LinkButton>
                    <asp:PlaceHolder ID="phdPageNumber" runat="server"></asp:PlaceHolder>
                    <asp:LinkButton ID="lbtnNext" runat="server" Font-Overline="false">下一页</asp:LinkButton>
                    <asp:LinkButton ID="lbtnLast" runat="server" Font-Overline="false" CommandName="Page"
                        CommandArgument='<%# gvDemo.PageCount %>'>尾页</asp:LinkButton>
                    <asp:DropDownList ID="drpShowCount" CssClass="selectpicker" runat="server" AutoPostBack="True">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList>
                </PagerTemplate>
            </asp:GridView>

            <asp:GridView ID="gvPage" runat="server" CssClass="table table-hover table-striped" GridLines="None" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" SortExpression="ProductID" />
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                    <asp:BoundField DataField="QuantityPerUnit" HeaderText="QuantityPerUnit" SortExpression="QuantityPerUnit" />
                    <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" SortExpression="UnitPrice" />
                    <asp:BoundField DataField="UnitsOnOrder" HeaderText="UnitsOnOrder" SortExpression="UnitsOnOrder" />
                    <asp:BoundField DataField="Discontinued" HeaderText="Discontinued" SortExpression="Discontinued" />
                </Columns>
                <RowStyle CssClass="cursor-pointer" />
                <PagerStyle CssClass="active" />
            </asp:GridView>
            <div id="page">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblPCurIndex" runat="server" Text="当前页:"></asp:Label>&nbsp;
                            <asp:Label ID="lblPCurIndexValue" runat="server" Text="0"></asp:Label>
                            <asp:Label ID="lblPSymbol" runat="server" Text="/"></asp:Label>
                            <asp:Label ID="lblPCount" runat="server"></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblPTotalCount" runat="server" Text="总条数:"></asp:Label>
                            <asp:Label ID="lblPTotalCountValue" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnPFirst" CssClass="btn btn-default btn-md" runat="server" CommandName="first" Text="首页" />
                            <asp:Button ID="btnPPre" CssClass="btn btn-default btn-md" runat="server" CommandName="prev" Text="上一页" />
                            <asp:Button ID="btnPNext" CssClass="btn btn-default btn-md" runat="server" CommandName="next" Text="下一页" />
                            <asp:Button ID="btnPLast" CssClass="btn btn-default btn-md" runat="server" CommandName="last" Text="尾页" />&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="drpPShowCount" CssClass="selectpicker" runat="server" AutoPostBack="True">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
