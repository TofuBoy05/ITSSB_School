
using System.Drawing.Text;
using System.Net.Http.Json;



namespace BellCroissantDesktop
{
    public partial class MainForm : Form
    {
        private List<Product> masterProductList = new List<Product>();
        private bool sortAscending = true;
        public MainForm()
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
                var list = await client.GetFromJsonAsync<List<Product>>("api/Products");

                // save to master product

                masterProductList = list ?? new List<Product>();


                // Show in Grid
                dgvProducts.DataSource = masterProductList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to auto-load data.");
            }
        }

        // 3. This is the event that runs when the app starts
        private async void Form1_Load(object sender, EventArgs e)
        {
            await RefreshData();
        }

        private void dgvProducts_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                dgvProducts.DataSource = new List<Product>();
            }

            var filtered = masterProductList.Where(p =>
                p.ProductName != null && p.ProductName.ToLower().Contains(keyword) ||
                p.Category != null && p.Category.ToLower().Contains(keyword)
            ).ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = filtered;
        }

        private async void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var selectedProduct = dgvProducts.Rows[e.RowIndex].DataBoundItem as Product;

            if (selectedProduct == null) return;

            string columnName = dgvProducts.Columns[e.ColumnIndex].Name;
            if (columnName == "btnEdit")
            {
                using (var f = new FormAddEdit(selectedProduct))
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        await RefreshData();
                    }
            }

            //else if (columnName == "btnDelete")
            //{
            //    // DELETE LOGIC
            //    var confirm = MessageBox.Show($"Delete {selectedProduct.ProductName}?", "Confirm", MessageBoxButtons.YesNo);
            //    if (confirm == DialogResult.Yes)
            //    {
            //        MessageBox.Show($"Deleted: {selectedProduct.ProductName}");
            //    }
            //}


        }

        private void dgvProducts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string propertyName = dgvProducts.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrEmpty(propertyName) || masterProductList == null)
            {
                return;
            }

            sortAscending = !sortAscending;

            List<Product> sortedList;

            var currentList = dgvProducts.DataSource as List<Product>;
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

        private async void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Pass null because we are adding new
            using (var f = new FormAddEdit(null))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    await RefreshData(); // Reload grid after saving
                }
            }

        }

        private void btnManageOrders_Click(object sender, EventArgs e)
        {
            using (var f = new OrderManagement())
            {
                f.ShowDialog();
            }
        }

        private void btnManagePromotions_Click(object sender, EventArgs e)
        {
            using (var f = new PromotionManager())
            {
                f.ShowDialog();
            }
        }

        private void btnManageLoyalty_Click(object sender, EventArgs e)
        {
            using (var f = new LoyaltyManagement())
            {
                f.ShowDialog();
            }
        }
    }
}