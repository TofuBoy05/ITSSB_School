using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BellCroissantDesktop
{
    public partial class FormAddEdit : Form
    {
        public Product CurrentProduct { get; set; }
        public FormAddEdit(Product product = null)
        {
            InitializeComponent();
            this.CurrentProduct = product;

            if (product != null)
            {
                txtProductName.Text = product.ProductName;
                txtCategory.Text = product.Category;
                numPrice.Value = product.Price;
                numCost.Value = product.Cost;
                dtPickerIntroductionDate.Value = product.IntroducedDate.ToDateTime(TimeOnly.MinValue);
                cbActive.Checked = product.Active;
                cbSeasonal.Checked = product.Seasonal;
                this.Text = "Edit Product";
            }
            else
            {
                this.Text = "Add New Product";
            }
        }

        private void formAddEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
