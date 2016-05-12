<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="YanZhiwei.DotNet.Core.RBACExample.Admin.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>角色管理</title>
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
        
        function CheckAll(cbAllId) {
            var keys = $("#txtKeys").val("").val();
            $(":checkbox").each(function () {
                $(this).prop("checked", $("#cbAll").prop("checked"));
                var cbValue = "'" + $(this).val() + "',";
                if ($("#cbAll").prop("checked") && $(this).attr("id") != "cbAll") {
                    keys += cbValue;
                }
            });
            $("#txtKeys").val(keys);
        }
        function CheckRow(cb) {
            var keys = $("#txtKeys").val();
            var cbValue = "'" + $(cb).val() + "',";
            if (!$(cb).prop("checked")) {
                keys = keys.replace(cbValue, "");
            } else if (keys.indexOf(cbValue) == -1) {
                keys += cbValue;
            }
            $("#txtKeys").val(keys);
        }
        function ConfirmDel() {
            if ($("#txtKeys").val().length == 0) {
                alert("请先选择要删除的数据");
                return false;
            } else {
                return confirm("确定要删除选中的数据吗?");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        请输入角色名称：<asp:TextBox ID="txtKeyword" runat="server" Width="301px"></asp:TextBox>
        <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" />
        <asp:HiddenField ID="txtKeys" runat="server" EnableViewState="false" />
        <div>
            <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="False"
                Width="100%" AllowPaging="True" DataKeyNames="Code" OnPageIndexChanging="gvRole_PageIndexChanging"
                OnRowDataBound="gvRole_RowDataBound" PageSize="4" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input id="cbAll" type="checkbox" onclick="CheckAll();" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input id="cbRow" value='<%#Eval("Code") %>' type="checkbox" onclick="CheckRow(this);" />
                        </ItemTemplate>
                        <ItemStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="序号" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle Width="50px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="角色名称" />
                    <asp:BoundField DataField="Code" HeaderText="角色编码" />
                    <asp:HyperLinkField Text="编辑" HeaderStyle-Width="50px" ItemStyle-Width="50px" DataNavigateUrlFields="Code,Name"
                        DataNavigateUrlFormatString="Role/FormRoleEdit.aspx?RoleCode={0}&RoleName={1}">
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle Width="50px"></ItemStyle>
                    </asp:HyperLinkField>
                </Columns>
                <EmptyDataTemplate>
                    没有数据。
                </EmptyDataTemplate>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </div>
        <asp:HyperLink ID="hlNew" runat="server" NavigateUrl="Role/FormRoleEdit.aspx">新增</asp:HyperLink>&nbsp;|&nbsp;
    <asp:LinkButton ID="lbDelete" runat="server"
        OnClientClick="return ConfirmDel();" OnClick="lbDelete_Click">删除</asp:LinkButton>
    </form>
</body>
</html>
