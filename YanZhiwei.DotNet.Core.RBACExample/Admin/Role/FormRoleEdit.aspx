<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormRoleEdit.aspx.cs" Inherits="YanZhiwei.DotNet.Core.RBACExample.Admin.RoleModule.FormRoleEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑角色</title>
    <script src="../../Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //全部模块全选或全取消
        function cbAllCheck(obj) {
            $("#txtPermissions").val("");
            $(":checkbox").each(function () {
                $(this).prop("checked", $(obj).prop("checked"));
                if ($(obj).prop("checked")) {
                    if ($(this).val() != 'on') {
                        $("#txtPermissions").val($("#txtPermissions").val() + "," + $(this).val() + ",");
                    }
                }
            });
        }

        //当前模块全选或全取消
        function cbModuleCheck(obj, cblID) {
            $("#" + cblID).find(":input").each(function () {
                $(this).prop("checked", $(obj).prop("checked"));
                if ($(obj).prop("checked")) {
                    if ($("#txtPermissions").val().indexOf("," + $(this).val() + ",") == -1) {
                        $("#txtPermissions").val($("#txtPermissions").val() + "," + $(this).val() + ",");
                    }
                } else {
                    $("#txtPermissions").val(
                        $("#txtPermissions").val().replace("," + $(this).val() + ",", ""));
                }
            });
        }
        //当前权限选中或取消
        function cbPermissionCheck(obj) {
            if ($(obj).prop("checked")) {
                $("#txtPermissions").val($("#txtPermissions").val() + "," + $(obj).val() + ",");
            } else {
                $("#txtPermissions").val(
                        $("#txtPermissions").val().replace("," + $(obj).val() + ",", ""));
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="txtPermissions" runat="server" />
        <div>
            角色名称：<asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                ControlToValidate="txtRoleName" Display="Dynamic" ErrorMessage="&lt;---请填写角色名称"></asp:RequiredFieldValidator>
        </div>
        <div>
            角色编码：<asp:TextBox ID="txtRoleCode" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                ControlToValidate="txtRoleCode" Display="Dynamic" ErrorMessage="&lt;---请填写角色编码"></asp:RequiredFieldValidator>
        </div>
        <div>
            <input id="cbAll" type="checkbox" onclick="cbAllCheck(this)" />全选
        </div>
        <div>
            <asp:GridView runat="server" ID="gvParent" AutoGenerateColumns="False"
                DataKeyNames="Code" ShowHeader="False"
                OnRowDataBound="gvParent_RowDataBound" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblTopModule" runat="server" Font-Bold="true" Text='<%# Eval("Name") %>'></asp:Label>
                            <br />
                            <asp:GridView runat="server" ID="gvChild" AutoGenerateColumns="False" EnableModelValidation="True"
                                DataKeyNames="Code" ShowHeader="false"
                                OnRowDataBound="gvChild_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMoudle" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            <asp:CheckBox ID="cbModule" runat="server" />全选
                                        <br />
                                            <asp:Label ID="lblPermissions" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                <SortedDescendingHeaderStyle BackColor="#7E0000" />
            </asp:GridView>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_Click" />
        </div>
        <a href="../Index.aspx">../Index.aspx</a>
    </form>
</body>
</html>
