using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using YanZhiwei.DotNet.Core.RBAC.Model;

namespace YanZhiwei.DotNet.Core.RBACExample.Admin.RoleModule
{
    public partial class FormRoleEdit : System.Web.UI.Page
    {
        private StringBuilder RolePermission { get; set; }

        private StringBuilder GetPermissionByRole(string roleCode)
        {
            List<RolePermission> _result = CacheRBACContext.Current.GetRolePermission<RolePermission>(roleCode);
            if (_result != null)
            {
                StringBuilder _role = new StringBuilder();
                foreach (RolePermission rolep in _result)
                {
                    _role.Append("," + rolep.ModulePermissionId + ",");
                }
                return _role;
            }
            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string _roleName = Request.QueryString["RoleName"],
                       _roleCode = Request.QueryString["RoleCode"];
                txtRoleName.Text = _roleName;
                txtRoleCode.Text = _roleCode;
                if (!string.IsNullOrEmpty(_roleCode))
                {
                    ViewState["RoleCode"] = _roleCode;
                    RolePermission = GetPermissionByRole(Request.QueryString["RoleCode"]);
                    txtPermissions.Value = RolePermission.ToString();
                }
                BindTopAllModulePermissionsToView();
            }
        }

        /// <summary>
        /// 绑定所有顶级模块到界面展示
        /// </summary>
        /// <param name="table">The table.</param>
        /// 时间：2015-11-24 14:01
        /// 备注：
        private void BindTopAllModulePermissionsToView()
        {
            gvParent.DataSource = CacheRBACContext.Current.AllModules.Where(c => c.ParentCode == "0").ToList();
            gvParent.DataBind();
        }

        protected void gvParent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView _gridView = (GridView)e.Row.FindControl("gvChild");
                if (_gridView != null)
                {
                    string _parentCode = gvParent.DataKeys[e.Row.RowIndex].Value.ToString().Trim();
                    List<Module> _result = CacheRBACContext.Current.AllModules.Where(c => c.ParentCode == _parentCode).ToList();
                    _gridView.DataSource = _result;
                    _gridView.DataBind();
                }
            }
        }

        protected void gvChild_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPermissions = (Label)e.Row.FindControl("lblPermissions");
                string _moduleCode = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
                List<ModulePermission> _result = CacheRBACContext.Current.AllModulePermissions.Where(c => c.ModuleCode == _moduleCode).ToList();
                if (_result != null)
                {
                    StringBuilder appendCheckBox = new StringBuilder();
                    int index = 0;
                    foreach (ModulePermission moduleOp in _result)
                    {
                        appendCheckBox.Append(
                            string.Format("<input type='checkbox' id='{0}' value='{1}' onclick='cbPermissionCheck(this)' {3} />{2} ",
                            "cb" + e.Row.RowIndex.ToString("00") + index.ToString("00"),
                            moduleOp.Id,
                            moduleOp.PermissionName,
                            RolePermission != null && RolePermission.ToString().IndexOf("," + moduleOp.Id + ",") >= 0 ? "checked" : ""
                            )
                            );

                        index++;
                    }
                    lblPermissions.Text = appendCheckBox.ToString();
                    ((CheckBox)e.Row.FindControl("cbModule")).Attributes.Add("onclick", "cbModuleCheck(this,'" + lblPermissions.ClientID + "')");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string roleName = txtRoleName.Text.Trim();
                string roleCode = txtRoleCode.Text.Trim();
                string[] permissions = txtPermissions.Value
                    .TrimStart(',')
                    .TrimEnd(',')
                    .Replace(",,", ",")
                    .Split(',');
                bool _result = false;
                if (ViewState["RoleCode"] != null)
                {
                    _result = CacheRBACContext.Current.UpdateRole(roleName, ViewState["RoleCode"].ToString(), permissions);
                }
                else
                {
                    _result = CacheRBACContext.Current.CreateRole(roleName, roleCode, permissions);
                }
                if (!_result)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Infomation", "alert('操作失败!');", true);
                }
            }
        }
    }
}