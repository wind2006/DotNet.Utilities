using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using YanZhiwei.DotNet.Core.RBAC.Model;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet.Core.RBACExample.Admin
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRoleToView();
            }
        }

        private void BindRoleToView()
        {
            gvRole.DataSource = LoadRowInfo();
            gvRole.DataBind();
        }

        private object LoadRowInfo()
        {
            string _searchKey = this.txtKeyword.Text.Trim();
            return CacheRBACContext.Current.FuzzySearchRolesByName<Role>(_searchKey);
        }

        protected void gvRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRole.PageIndex = e.NewPageIndex;
            BindRoleToView();
        }

        protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text =
                    Convert.ToString(gvRole.PageSize * gvRole.PageIndex + e.Row.RowIndex + 1);
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindRoleToView();
        }

        protected void lbDelete_Click(object sender, EventArgs e)
        {
            string _keys = txtKeys.Value.Trim();
            if (CacheRBACContext.Current.DeleteRoleByCode(_keys.SubStringFromLast(',')))
            {
                BindRoleToView();
                txtKeys.Value = string.Empty;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Infomation", "alert('删除失败!');", true);
            }
        }
    }
}