using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Molin_CRM.Business;
using Molin_CRM.Helper;
using Molin_CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet2.Utilities.Models;

namespace Molin_CRM
{
    public partial class winMain : XtraForm
    {
        public winMain()
        {
            InitializeComponent();
        }

        public void LoadAllProductList(string name)
        {
            List<Product> _productList = ProductBus.Instance.GetAllProduct(name);
            gcProductList.DataSource = _productList;
            gvProductList.SetSummaryItem("Number", DevExpress.Data.SummaryItemType.Sum, "合计= {0:n2}");
            gvProductList.SetSummaryItem("Price", DevExpress.Data.SummaryItemType.Sum, "合计={0:n2} 元");
            if (!string.IsNullOrWhiteSpace(name))
            {
                _productList = ProductBus.Instance.GetAllProduct(string.Empty);
            }
            if (_productList != null)
            {
                List<ListItem> _itemList = new List<ListItem>();
                _productList.ForEach(p =>
                {
                    _itemList.Add(new ListItem() { Key = p.Name, Value = p.Name });
                });
                cbSellProductName.Properties.Items.Clear();
                cbSellProductName.SetDataSource(_itemList, "----查询所有品名----");
            }
        }

        protected override void WndProc(ref   Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                if (DevMessageBoxHelper.ShowYesNoAndWarning("确认退出本Molin-CRM客户端？") != System.Windows.Forms.DialogResult.Yes)
                    return;
            }
            base.WndProc(ref m);
        }

        private void BasicInit()
        {
            deSellStartTime.DateTime = DateTime.Now.AddDays(-1);
            deSellEndTime.DateTime = DateTime.Now;
        }

        private void BindCustomerListToCombox(List<Customer> customeList)
        {
            if (customeList != null)
            {
                cbCustomer.Properties.Items.Clear();
                List<ListItem> _itemList = new List<ListItem>();
                customeList.ForEach(c =>
                {
                    _itemList.Add(new ListItem(c.ID, c.Name) { });
                });
                cbCustomer.SetDataSource(_itemList, "-----请选择客户-----");
                cbSellCustomer.Properties.Items.Clear();
                cbSellCustomer.SetDataSource(_itemList, "-----查询所有客户-----");
            }
        }

        /// <summary>
        /// 购物品类
        /// </summary>
        private void btnBuyOpt_Click(object sender, EventArgs e)
        {
            string _productName = this.txtProductName.Text.Trim();
            decimal _productNumber = this.spinProductNumber.Value,
                    _prodcutPrice = this.spinProductPrice.Value;
            if (CheckBuyOptParamter(_productName))
            {
                Product _product = new Product();
                _product.Name = _productName;
                _product.Number = _productNumber.ToIntOrDefault(0);
                _product.Price = _prodcutPrice;
                bool _result = ProductBus.Instance.Add(_product);
                string _optMessage = string.Format("添加品名『{0}』{1}", _productName, _result == true ? "成功！" : "失败。");
                DevMessageBoxHelper.ShowInfo(_optMessage);
                LoadAllProductList(string.Empty);
            }
        }

        private void btnBuyOut_Click(object sender, EventArgs e)
        {
            string _name = this.txtBuyOutName.Text.Trim(),
                   _customerName = this.cbCustomer.Text.Trim();
            int _number = (int)this.spinBuyOutPNumber.Value,
                _customerIndex = this.cbCustomer.SelectedIndex,
                _totalNumber = this.txtBuyOutName.Tag.ToIntOrDefault(0);
            decimal _price = this.spinBuyOutPPrice.Value,
                    _totalPrice = Math.Round(_price * _number, 2);
            if (CheckBuyOutParamter(_customerIndex))
            {
                try
                {
                    string _promptMessage = BuilderBuyOutPromptMsg(_name, _customerName, _number, _price, _totalPrice);
                    if (DevMessageBoxHelper.ShowYesNoAndTips(_promptMessage) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Sell _sell = new Sell();
                        _sell.Name = _name;
                        _sell.Number = _number;
                        _sell.Price = _price;
                        _sell.Customer = _customerName;
                        bool _result = SellBus.Instance.Add(_sell, _totalNumber);
                        DevMessageBoxHelper.ShowInfo(string.Format("售出品名『{0}』{1}", _name, _result == true ? "成功" : "失败"));
                    }
                }
                catch (Exception ex)
                {
                    DevMessageBoxHelper.ShowError(string.Format("响应售出操作异常，原因:{0}", ex.Message));
                }
                finally
                {
                    LoadAllProductList(string.Empty);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportToXls_Click(object sender, EventArgs e)
        {
            gcProductList.ToXls(string.Format("{0}_营养素库存", DateTime.Now.ToString("yyyyMMdd")));
        }

        private void btnItemAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            winAbout _about = new winAbout();
            _about.ShowDialog();
        }

        private void btnItemBasicSet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tbcMain.SelectedTabPage = tpBasictSet;
        }

        private void btnItemBuyIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tbcMain.SelectedTabPage = tpBuyIn;
        }

        private void btnItemBuyOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tbcMain.SelectedTabPage = tpBuyOut;
        }

        private void btnItemCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            tbcMain.SelectedTabPage = tpBasictSet;
            tbcBasicSet.SelectedTabPage = tpBasicSet_Customer;
        }

        private void btnProductFuzzySearch_Click(object sender, EventArgs e)
        {
            LoadAllProductList(this.txtFSProdcutName.Text.Trim());
        }

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string _name = this.txtCus_Name.Text.Trim(),
                       _phone = this.txtCus_Phone.Text.Trim(),
                       _address = this.txtCus_Address.Text.Trim(),
                       _remark = this.txtCus_Remaker.Text.Trim(),
                       _id = txtCus_Name.Tag.ToStringOrDefault("");

                if (CheckCustomerParamter(_name))
                {
                    Customer _customer = CustomerBus.Instance.Get(_name);
                    bool _add = _customer == null;
                    _customer = BuilderCustomer(_customer, _name, _phone, _address, _remark, _id);
                    bool _result = _add == true ? CustomerBus.Instance.Add(_customer) : CustomerBus.Instance.Update(_customer);
                    DevMessageBoxHelper.ShowInfo(string.Format("保存客户『{0}』{1}.", _name, _result == true ? "成功" : "失败"));
                }
            }
            catch (Exception ex)
            {
                DevMessageBoxHelper.ShowError(string.Format("响应保存操作异常，原因:{0}", ex.Message));
            }
            finally
            {
                LoadAllCustomerList();
                SetCustomerControl(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            }
        }

        private string BuilderBuyOutPromptMsg(string _name, string _customerName, int _number, decimal _price, decimal _totalPrice)
        {
            StringBuilder _builder = new StringBuilder();
            _builder.AppendFormat("确认售出:{0}", Environment.NewLine);
            _builder.AppendFormat("品名：{0}{1}", _name, Environment.NewLine);
            _builder.AppendFormat("数量：{0}{1}", _number, Environment.NewLine);
            _builder.AppendFormat("单价：{0}{1}", _price, Environment.NewLine);
            _builder.AppendFormat("总价：{0}{1}", _totalPrice, Environment.NewLine);
            _builder.AppendFormat("============{0}", Environment.NewLine);
            _builder.AppendFormat("客户：{0}", _customerName);
            return _builder.ToString();
        }

        private Customer BuilderCustomer(Customer customer, string name, string phone, string address, string remark, string id)
        {
            if (customer == null)
                customer = new Customer();
            else
                customer.ID = id;
            customer.Name = name;
            customer.Phone = phone;
            customer.Address = address;
            customer.Remark = remark;
            return customer;
        }

        private string BuilderPromptMessage(Product findedProduct, Product curProduct, out bool update)
        {
            StringBuilder _builder = new StringBuilder();
            update = false;
            _builder.AppendFormat("确定对品名:『{0}』进行如下更新：{1}", findedProduct.Name, Environment.NewLine);
            if (findedProduct.Price != curProduct.Price)
            {
                _builder.AppendFormat("价格:{0}更新为：{1}{2}", findedProduct.Price, curProduct.Price, Environment.NewLine);
                update = true;
            }
            if (findedProduct.Number != curProduct.Number)
            {
                _builder.AppendFormat("数量:{0}更新为：{1}{2}", findedProduct.Number, curProduct.Number, Environment.NewLine);
                update = true;
            }
            return _builder.ToString();
        }

        /// <summary>
        /// 购物品类——参数检查
        /// </summary>
        /// <param name="productName">参数检查</param>
        /// <returns></returns>
        private bool CheckBuyOptParamter(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                txtProductName.ShowToolTip<TextEdit>(toolTipController, "请输入品名！");
                return false;
            }

            if (ProductBus.Instance.Get(productName) != null)
            {
                txtProductName.ShowToolTip<TextEdit>(toolTipController, "该品名已经存在，请修改！");
                return false;
            }
            return true;
        }

        private bool CheckBuyOutParamter(int customerIndex)
        {
            bool _result = !(customerIndex <= 0);
            if (!_result)
                cbCustomer.ShowToolTip<ComboBoxEdit>(toolTipController, "请选择客户！");
            return _result;
        }

        private bool CheckCustomerParamter(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                txtCus_Name.ShowToolTip<TextEdit>(toolTipController, "请输入客户名称！");
                return false;
            }
            return true;
        }

        private void DateTimeTask()
        {
            ThreadPool.QueueUserWorkItem((arg) =>
            {
                while (true)
                {
                    barItemTimeNow.Caption = string.Format("当前时间: {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Thread.Sleep(1000);
                }
            });
        }

        private void gvCustomer_Click(object sender, EventArgs e)
        {
            int _rowIndex = gvCustomer.FocusedRowHandle;
            if (_rowIndex >= 0)
            {
                Customer _customer = (Customer)gvCustomer.GetRow(_rowIndex);
                if (_customer == null) return;
                SetCustomerControl(_customer.Name, _customer.Phone, _customer.Address, _customer.Remark, _customer.ID);
            }
        }

        private void LoadAllCustomerList()
        {
            List<Customer> _customerList = CustomerBus.Instance.GetAll(); ;
            gcCustomer.DataSource = _customerList;
            BindCustomerListToCombox(_customerList);
        }

        private void repositoryItemBtnBuyOut_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Product _curProduct = gvProductList.GetEntityByFocusedRowHandle<Product>();
            if (_curProduct != null)
            {
                tbcBuyInOutMain.SelectedTabPage = tpBuyOutOp;
                txtBuyOutName.Text = _curProduct.Name;
                txtBuyOutName.Tag = _curProduct.Number;
                spinBuyOutPNumber.Properties.MaxValue = _curProduct.Number == 0 ? 1 : _curProduct.Number;
                spinBuyOutPNumber.Properties.MinValue = 1;
                spinBuyOutPNumber.Value = 1;
                btnBuyOut.Enabled = !(_curProduct.Number == 0);
            }
        }

        private void repositoryItembtnDelCustome_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Customer _curCustomer = gvCustomer.GetEntityByFocusedRowHandle<Customer>();
            if (_curCustomer != null)
            {
                if (DevMessageBoxHelper.ShowYesNoAndWarning(string.Format("确认删除客户『{0}』?", _curCustomer.Name)) == System.Windows.Forms.DialogResult.Yes)
                {
                    bool _result = CustomerBus.Instance.Delete(_curCustomer);
                    DevMessageBoxHelper.ShowInfo(string.Format("删除客户『{0}』{1}.", _curCustomer.Name, _result == true ? "成功" : "失败"));
                    LoadAllCustomerList();
                }
            }
        }

        private void repositoryItemBtnUpdateProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int _rowIndex = gvProductList.FocusedRowHandle;
                if (_rowIndex >= 0)
                {
                    Product _curProduct = (Product)gvProductList.GetRow(_rowIndex);
                    if (_curProduct != null)
                    {
                        Product _findedProduct = ProductBus.Instance.Get(_curProduct.Name);
                        UpdateProduct(_findedProduct, _curProduct);
                    }
                }
            }
            catch (Exception ex)
            {
                DevMessageBoxHelper.ShowError(string.Format("响应更新操作异常，原因:{0}", ex.Message));
            }
        }

        private void rgSellDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _index = rgSellDate.SelectedIndex;
            if (_index == 0)
            {
                deSellEndTime.Visible = false;
                deSellStartTime.Visible = false;
                lblSellTimeRange.Visible = false;
            }
            else if (_index == 1)
            {
                deSellEndTime.Visible = false;
                deSellStartTime.Visible = true;
                lblSellTimeRange.Visible = false;
            }
            else if (_index == 2)
            {
                deSellEndTime.Visible = true;
                deSellStartTime.Visible = true;
                lblSellTimeRange.Visible = true;
            }
        }

        private void SetCustomerControl(string name, string phone, string address, string remark, string id)
        {
            txtCus_Address.Text = address;
            txtCus_Name.Text = name;
            txtCus_Name.Tag = id;
            txtCus_Phone.Text = phone;
            txtCus_Remaker.Text = remark;
        }

        private void UpdateProduct(Product findedProduct, Product curProduct)
        {
            bool _updated = false;
            string _promptMessage = BuilderPromptMessage(findedProduct, curProduct, out _updated);
            if (!_updated)
            {
                DevMessageBoxHelper.ShowWarning("未更新任何信息！");
                return;
            }
            if (DevMessageBoxHelper.ShowYesNoAndWarning(_promptMessage) == System.Windows.Forms.DialogResult.Yes)
            {
                bool _result = ProductBus.Instance.Update(curProduct.Name, curProduct.Number, curProduct.Price);
                LoadAllProductList(string.Empty);
                DevMessageBoxHelper.ShowInfo(string.Format("更新品名『{0}』{1}", curProduct.Number, _result == true ? "成功" : "失败"));
            }
        }

        private void winMain_Load(object sender, EventArgs e)
        {
            LoadAllProductList(string.Empty);
            LoadAllCustomerList();
            BasicInit();
            DateTimeTask();
        }

        private void btnQuerySell_Click(object sender, EventArgs e)
        {
            string _customerName = this.cbSellCustomer.Text.Trim(),
                   _productName = this.cbSellProductName.Text.Trim();
            int _customerIndex = this.cbSellCustomer.SelectedIndex,
                _productIndex = this.cbSellProductName.SelectedIndex,
                _queryTimeIndex = this.rgSellDate.SelectedIndex;
        }
    }
}