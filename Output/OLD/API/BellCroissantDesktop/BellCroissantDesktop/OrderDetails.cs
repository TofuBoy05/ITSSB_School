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
    public partial class OrderDetails : Form
    {
        private Order currentOrder;

        public OrderDetails(Order selectedOrder)
        {
            InitializeComponent();
            currentOrder = selectedOrder;

            if (selectedOrder != null)
            {
                labOrderId.Text = selectedOrder.TransactionId.ToString();
                labCustomerName.Text = selectedOrder.CustomerName;
                labOrderDate.Text = selectedOrder.OrderDate.ToString("dd/MM/yyyy");
                cmbOrderStatus.Text = selectedOrder.Status;

                // FIX 1: Set to false so your custom design columns work properly!
                dgvOrderItems.AutoGenerateColumns = false;
            }
        }

        private async void OrderDetails_Load(object sender, EventArgs e)
        {
            // FIX 2: Stop the code if the order has no items, preventing a fatal crash.
            if (currentOrder?.OrderItems == null) return;

            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                var products = await client.GetFromJsonAsync<List<Product>>("api/products");

                var displayItems = currentOrder.OrderItems.Select(item => new
                {
                    ProductName = products?.FirstOrDefault(p => p.ProductId == item.ProductId)?.ProductName ?? "Unknown",
                    Quantity = item.Quantity,
                    Price = item.Price, // Just ensure your OrderItem class uses 'Price' and not 'UnitPrice'
                }).ToList();

                dgvOrderItems.DataSource = displayItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load item details: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            currentOrder.Status = cmbOrderStatus.Text;
            currentOrder.Customer = null;
            foreach (var item in currentOrder.OrderItems)
            {
                item.Product = null;
                item.Transaction = null;
            }
            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                string action;
                if (cmbOrderStatus.Text.ToLower() == "cancelled") { action = "cancel"; }
                else if (cmbOrderStatus.Text.ToLower() == "pending") { action = "pending"; }
                else  { action = "complete"; }
                // 3. Send the cleaned-up Order
                var res = await client.PutAsync($"api/orders/{currentOrder.TransactionId}/{action}", null);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Successfully updated order.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Closes form and tells grid to refresh
                }
                else
                {
                    // Read the exact error message from the API so you can see it!
                    string errorText = await res.Content.ReadAsStringAsync();
                    MessageBox.Show($"API Error: {errorText}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}");
            }


        }
    }
}
