using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductStore
{
    public partial class frmMaintainProducts : Form
    {
        ProductData bm = new ProductData();
        DataTable dtProduct;
        public frmMaintainProducts()
        {
            InitializeComponent();
        }

        private void frmMaintainProducts_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            dtProduct = bm.getProducts();
            dtProduct.PrimaryKey = new DataColumn[] { dtProduct.Columns["ProductID"] };
            dtProduct.Columns.Add("SubTotal", typeof(double), "Quantity * UnitPrice");
            bsProducts.DataSource = dtProduct;
            txtID.DataBindings.Clear();
            txtName.DataBindings.Clear();
            txtPrice.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();

            txtID.DataBindings.Add("Text", bsProducts, "ProductID");
            txtName.DataBindings.Add("Text", bsProducts, "ProductName");
            txtPrice.DataBindings.Add("Text", bsProducts, "UnitPrice");
            txtQuantity.DataBindings.Add("Text", bsProducts, "Quantity");

            dgvProductList.DataSource = bsProducts;
            bsProducts.Sort = "ProductID DESC";
            bnProductList.BindingSource = bsProducts;
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            int ID = 1;
            string name = string.Empty;
            double price = 0;
            int proQuantity = 0;
            if (dtProduct.Rows.Count > 0)
            {
                ID = int.Parse(dtProduct.Compute("MAX(ProductID)", "").ToString()) + 1;
            }
            Product pro = new Product
            {
                productID = ID,
                productName = Name,
                unitPrice = price,
                quantity = proQuantity
            };
            frmProductDetails ProductDetail = new frmProductDetails(true, pro);
            DialogResult r = ProductDetail.ShowDialog();
            if (r == DialogResult.OK)
            {
                pro = ProductDetail.ProductAddOrEdit;
                dtProduct.Rows.Add(pro.productID, pro.productName, pro.unitPrice, pro.quantity);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtID.Text);
            string name = txtName.Text;
            double price = float.Parse(txtPrice.Text);
            int proQuantity = int.Parse(txtQuantity.Text);
            Product pro = new Product
            {
                productID = ID,
                productName = Name,
                unitPrice = price,
                quantity = proQuantity
            };
            frmProductDetails ProductDetail = new frmProductDetails(false, pro);
            DialogResult r = ProductDetail.ShowDialog();
            if (r == DialogResult.OK)
            {
                DataRow row = dtProduct.Rows.Find(pro.productID);
                row["ProductName"] = pro.productName;
                row["UnitPrice"] = pro.unitPrice;
                row["Quantity"] = pro.quantity;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtID.Text);
            if (bm.DeleteProduct(ID))
            {
                DataRow row = dtProduct.Rows.Find(ID);
                dtProduct.Rows.Remove(row);
                MessageBox.Show("Successful.");
            }
            else
            {
                MessageBox.Show("Fail.");
            }
        }
    }
}
