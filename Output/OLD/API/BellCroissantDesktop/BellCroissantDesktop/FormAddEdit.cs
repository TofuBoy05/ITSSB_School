using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
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

            // Populate category choices
            cmbCategory.Items.AddRange(new[] { "Bread", "Pastry", "Tarte", "Viennoiserie" });

            if (product != null)
            {
                txtProductName.Text = product.ProductName;
                // select category if it exists in the list
                if (!string.IsNullOrEmpty(product.Category) && cmbCategory.Items.Contains(product.Category))
                {
                    cmbCategory.SelectedItem = product.Category;
                }
                else
                {
                    cmbCategory.SelectedIndex = -1;
                }
                numPrice.Value = product.Price;
                numCost.Value = product.Cost;
                dtPickerIntroductionDate.Value = product.IntroducedDate.ToDateTime(TimeOnly.MinValue);
                cbActive.Checked = product.Active;
                cbSeasonal.Checked = product.Seasonal;
                this.Text = "Edit Product";
            }
            else
            {
                // default selection for new product
                cmbCategory.SelectedIndex = 0;
                this.Text = "Add New Product";
            }
        }

        private void formAddEdit_Load(object sender, EventArgs e)
        {

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || txtProductName.Text.Length > 100)
            {
                MessageBox.Show("Product Name is required and must be under 100 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var selectedCategory = cmbCategory.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedCategory) || selectedCategory.Length > 100)
            {
                MessageBox.Show("Category is required and must be under 100 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (numPrice.Value < 0)
            {
                MessageBox.Show("Price must be a positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (numCost.Value < 0)
            {
                MessageBox.Show("Cost must be a positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            bool isNewProduct = CurrentProduct == null;
            var p = CurrentProduct ?? new Product();

            p.ProductName = txtProductName.Text;
            p.Category = cmbCategory.SelectedItem as string ?? string.Empty;
            p.Price = numPrice.Value;
            p.Cost = numCost.Value;
            p.Description = txtDescription.Text;
            p.Seasonal = cbSeasonal.Checked;
            p.Active = cbActive.Checked;
            p.IntroducedDate = DateOnly.FromDateTime(dtPickerIntroductionDate.Value);

            try
            {
                using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };

                client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

                HttpResponseMessage response;

                if (isNewProduct)
                {
                    response = await client.PostAsJsonAsync("api/products", p);

                }
                else
                {
                    response = await client.PutAsJsonAsync($"api/products/{p.ProductId}", p);
                }

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Product saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show($"Failed to save product. API returned: {response.ReasonPhrase}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not finish task. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
