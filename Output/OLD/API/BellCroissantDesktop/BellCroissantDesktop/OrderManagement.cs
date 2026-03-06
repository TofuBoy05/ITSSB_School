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
    public partial class OrderManagement : Form
    {
        private List<Order> masterOrderList = new List<Order>();
        private bool sortAscending = true;
        public OrderManagement()
        {
            InitializeComponent();
            dgvProducts.AutoGenerateColumns = false;

        }

        private async Task RefreshData()
        {
            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                // Fetch data from API
                var list = await client.GetFromJsonAsync<List<Order>>("api/Orders");

                // save to master Order

                masterOrderList = list ?? new List<Order>();


                // Show in Grid
                dgvProducts.DataSource = masterOrderList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to auto-load data.");
            }
        }

        private async void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var selectedOrder = dgvProducts.Rows[e.RowIndex].DataBoundItem as Order;

            if (selectedOrder == null) return;

            string columnName = dgvProducts.Columns[e.ColumnIndex].Name;
            if (columnName == "btnOrderDetail")
            {
                using (var f = new OrderDetails(selectedOrder))
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        await RefreshData();
                    }
            }
        }

        private async void OrderManagement_Load(object sender, EventArgs e)
        {
            await RefreshData();
        }

        private void dgvProducts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string propertyName = dgvProducts.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(propertyName) || masterOrderList == null)
            {
                return;
            }

            sortAscending = !sortAscending;

            List<Order> sortedList;

            var currentList = dgvProducts.DataSource as List<Order>;
            if (currentList == null) return;

            if (sortAscending)
            {
                sortedList = currentList.OrderBy(p => p.GetType().GetProperty(propertyName).GetValue(p, null)).ToList();
            }
            else
            {
                sortedList = currentList.OrderByDescending(p => p.GetType().GetProperty(propertyName).GetValue(p, null)).ToList();
            }

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = sortedList;

            dgvProducts.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = sortAscending ? SortOrder.Ascending : SortOrder.Descending;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                dgvProducts.DataSource = new List<Order>();
            }

            var filtered = masterOrderList.Where(p =>
                p.TransactionId.ToString() == keyword ||
                p.CustomerName != null && p.CustomerName.ToLower().Contains(keyword) ||
                p.OrderDate.ToString("MM/dd/yyyy").Contains(keyword) ||
                p.OrderDate.ToString("yyyy/MM/dd").Contains(keyword)
            ).ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = filtered;
        }
    }
}
