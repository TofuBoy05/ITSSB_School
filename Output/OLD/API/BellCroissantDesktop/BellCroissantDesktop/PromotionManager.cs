using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Json;

namespace BellCroissantDesktop
{
    public partial class PromotionManager : Form
    {
        private List<Promotion> masterPromotionList = new List<Promotion>();
        private bool sortAscending = true;
        public PromotionManager()
        {
            InitializeComponent();
            dgvPromotions.AutoGenerateColumns = false;
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            dgvPromotions.Columns.Clear();
            dgvPromotions.Columns.Add(new DataGridViewTextBoxColumn { Name = "PromotionId", HeaderText = "ID", DataPropertyName = "PromotionId", Width = 50 });
            dgvPromotions.Columns.Add(new DataGridViewTextBoxColumn { Name = "PromotionName", HeaderText = "Name", DataPropertyName = "PromotionName", Width = 150 });
            dgvPromotions.Columns.Add(new DataGridViewTextBoxColumn { Name = "DiscountType", HeaderText = "Type", DataPropertyName = "DiscountType", Width = 100 });
            dgvPromotions.Columns.Add(new DataGridViewTextBoxColumn { Name = "DiscountValue", HeaderText = "Value", DataPropertyName = "DiscountValue", Width = 80 });
            dgvPromotions.Columns.Add(new DataGridViewTextBoxColumn { Name = "StartDate", HeaderText = "Start Date", DataPropertyName = "StartDate", Width = 100 });
            dgvPromotions.Columns.Add(new DataGridViewTextBoxColumn { Name = "EndDate", HeaderText = "End Date", DataPropertyName = "EndDate", Width = 100 });
            dgvPromotions.Columns.Add(new DataGridViewTextBoxColumn { Name = "MinimumOrderValue", HeaderText = "Min Order", DataPropertyName = "MinimumOrderValue", Width = 80 });
            dgvPromotions.Columns.Add(new DataGridViewTextBoxColumn { Name = "Priority", HeaderText = "Priority", DataPropertyName = "Priority", Width = 70 });

            var btnView = new DataGridViewButtonColumn { Name = "btnView", HeaderText = "View", Text = "View", UseColumnTextForButtonValue = true, Width = 80 };
            dgvPromotions.Columns.Add(btnView);
            var btnEdit = new DataGridViewButtonColumn { Name = "btnEdit", HeaderText = "Edit", Text = "Edit", UseColumnTextForButtonValue = true, Width = 80 };
            dgvPromotions.Columns.Add(btnEdit);
            var btnDelete = new DataGridViewButtonColumn { Name = "btnDelete", HeaderText = "Delete", Text = "Delete", UseColumnTextForButtonValue = true, Width = 80 };
            dgvPromotions.Columns.Add(btnDelete);
        }

        private async Task RefreshData()
        {
            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                var list = await client.GetFromJsonAsync<List<Promotion>>("api/promotions");
                masterPromotionList = list ?? new List<Promotion>();
                dgvPromotions.DataSource = masterPromotionList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load promotions.");
            }
        }

        private async void btnAddPromotion_Click(object sender, EventArgs e)
        {
            using (var promotionCreator = new PromotionCreator())
            {
                if (promotionCreator.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the data after adding a new promotion
                    await RefreshData();
                }
            }
        }

        private async void dgvPromotions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var selectedPromotion = dgvPromotions.Rows[e.RowIndex].DataBoundItem as Promotion;
            if (selectedPromotion == null) return;

            string columnName = dgvPromotions.Columns[e.ColumnIndex].Name;
            if (columnName == "btnView")
            {
                ShowPromotionDetails(selectedPromotion);
            }
            else if (columnName == "btnEdit")
            {
                using var editor = new PromotionCreator(selectedPromotion);
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    await RefreshData();
                }
            }
            else if (columnName == "btnDelete")
            {
                var confirm = MessageBox.Show($"Are you sure you want to delete promotion '{selectedPromotion.PromotionName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
                client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

                try
                {
                    var resp = await client.DeleteAsync($"api/Promotions/{selectedPromotion.PromotionId}");
                    if (resp.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Promotion deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await RefreshData();
                    }
                    else
                    {
                        var body = await resp.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to delete promotion: {resp.StatusCode}\n{body}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting promotion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowPromotionDetails(Promotion promotion)
        {
            var details = new StringBuilder();
            details.AppendLine($"Promotion ID: {promotion.PromotionId}");
            details.AppendLine($"Name: {promotion.PromotionName}");
            details.AppendLine($"Discount Type: {promotion.DiscountType}");
            details.AppendLine($"Discount Value: {promotion.DiscountValue}");
            details.AppendLine($"Start Date: {promotion.StartDate}");
            details.AppendLine($"End Date: {promotion.EndDate}");
            details.AppendLine($"Minimum Order Value: {promotion.MinimumOrderValue?.ToString() ?? "N/A"}");
            details.AppendLine($"Priority: {promotion.Priority}");
            details.AppendLine($"Applicable Products: {promotion.ApplicableProducts ?? "All Products"}");

            if (!string.IsNullOrEmpty(promotion.QuantityBasedRules))
            {
                details.AppendLine($"\nQuantity-Based Rules:\n{promotion.QuantityBasedRules}");
            }

            MessageBox.Show(details.ToString(), "Promotion Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSearchPromotions_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchPromotions.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                dgvPromotions.DataSource = null;
                dgvPromotions.DataSource = masterPromotionList;
                return;
            }

            var filtered = masterPromotionList.Where(p =>
                p.PromotionId.ToString().Contains(keyword) ||
                (p.PromotionName != null && p.PromotionName.ToLower().Contains(keyword)) ||
                (p.DiscountType != null && p.DiscountType.ToLower().Contains(keyword))
            ).ToList();

            dgvPromotions.DataSource = null;
            dgvPromotions.DataSource = filtered;
        }

        private async void PromotionManager_Load(object sender, EventArgs e)
        {
            await RefreshData();
        }
    }
}
