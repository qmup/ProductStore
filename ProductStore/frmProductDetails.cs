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
    public partial class frmProductDetails : Form
    {
        private bool addOrEdit;
        public Product ProductAddOrEdit { get; set; }
        public frmProductDetails()
        {
            InitializeComponent();
        }

        public frmProductDetails(bool flag, Product p) : this()
        {
            addOrEdit = flag;
            ProductAddOrEdit = p;
            InitData();
        }

        private void InitData()
        {
            txtID.Text = ProductAddOrEdit.productID.ToString();
            txtName.Text = ProductAddOrEdit.productName;
            txtPrice.Text = ProductAddOrEdit.unitPrice.ToString();
            txtQuantity.Text = ProductAddOrEdit.quantity.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag;
            ProductAddOrEdit.productID = int.Parse(txtID.Text);
            ProductAddOrEdit.productName = txtName.Text;
            ProductAddOrEdit.unitPrice = int.Parse(txtPrice.Text);
            ProductAddOrEdit.quantity = int.Parse(txtQuantity.Text);
            ProductData proData = new ProductData();
            if (addOrEdit == true)
            {
                flag = proData.addProduct(ProductAddOrEdit);
            }
            else
            {
                flag = proData.updateProduct(ProductAddOrEdit);
            }
            if (flag == true)
            {
                MessageBox.Show("Save successful.");
            }
            else
            {
                MessageBox.Show("Save fail.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
