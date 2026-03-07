using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Net.Http.Json;

namespace BellCroissantDesktop
{
    public partial class PromotionCreator : Form
    {
        private bool isEdit = false;
        private int editingId = 0;
        public Promotion CreatedPromotion { get; private set; }

        public PromotionCreator()
        {
            InitializeComponent();
            // initialize controls
            txtDiscountType.Items.Clear();
            txtDiscountType.Items.AddRange(new object[] { "Percentage", "FixedAmount" });
            txtDiscountType.SelectedIndex = 0;

            numDiscountValue.DecimalPlaces = 2; // Discount value
            numMinimumOrderValue.DecimalPlaces = 2; // Minimum order
            numPriorityValue.Minimum = 0;
        }

        public PromotionCreator(Promotion promotion) : this()
        {
            if (promotion == null) return;
            isEdit = true;
            editingId = promotion.PromotionId;
            CreatedPromotion = promotion;

            txtPromotionName.Text = promotion.PromotionName;
            // try to select discount type
            var idx = txtDiscountType.Items.IndexOf(promotion.DiscountType);
            txtDiscountType.SelectedIndex = idx >= 0 ? idx : 0;
            numDiscountValue.Value = promotion.DiscountValue;
            txtApplicableProducts.Text = promotion.ApplicableProducts;
            try
            {
                dateStartDate.Value = promotion.StartDate.ToDateTime(new TimeOnly(0, 0));
                dateEndDate.Value = promotion.EndDate.ToDateTime(new TimeOnly(0, 0));
            }
            catch
            {
                // ignore invalid DateOnly -> DateTime conversion
            }
            numMinimumOrderValue.Value = promotion.MinimumOrderValue ?? 0;
            numPriorityValue.Value = promotion.Priority;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private async Task<List<PromotionConflict>> DetectConflicts(Promotion promotion)
        {
            var conflicts = new List<PromotionConflict>();

            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                // Get all promotions from API
                var allPromotions = await client.GetFromJsonAsync<List<Promotion>>("api/Promotions");
                if (allPromotions == null) return conflicts;

                // Parse applicable products for current promotion
                var currentProducts = ParseProductIds(promotion.ApplicableProducts);
                if (currentProducts.Count == 0) return conflicts; // No specific products = no conflict

                // Find conflicting promotions
                foreach (var existingPromo in allPromotions)
                {
                    // Skip self when editing
                    if (isEdit && existingPromo.PromotionId == editingId)
                        continue;

                    // Check if same priority
                    if (existingPromo.Priority != promotion.Priority)
                        continue;

                    // Check date overlap
                    if (!HasDateOverlap(promotion.StartDate, promotion.EndDate, existingPromo.StartDate, existingPromo.EndDate))
                        continue;

                    // Check product overlap
                    var existingProducts = ParseProductIds(existingPromo.ApplicableProducts);
                    var overlappingProducts = currentProducts.Intersect(existingProducts).ToList();

                    if (overlappingProducts.Count > 0)
                    {
                        // Calculate overlap dates
                        var overlapStart = promotion.StartDate > existingPromo.StartDate ? promotion.StartDate : existingPromo.StartDate;
                        var overlapEnd = promotion.EndDate < existingPromo.EndDate ? promotion.EndDate : existingPromo.EndDate;

                        // Find or create conflict group
                        var existingConflict = conflicts.FirstOrDefault(c =>
                            c.SharedPriority == promotion.Priority &&
                            c.OverlapStartDate == overlapStart &&
                            c.OverlapEndDate == overlapEnd &&
                            c.ConflictingProductIds.Intersect(overlappingProducts).Any());

                        if (existingConflict != null)
                        {
                            existingConflict.ConflictingPromotions.Add(existingPromo);
                            existingConflict.ConflictingProductIds = existingConflict.ConflictingProductIds.Union(overlappingProducts).ToList();
                        }
                        else
                        {
                            conflicts.Add(new PromotionConflict
                            {
                                ConflictingPromotions = new List<Promotion> { promotion, existingPromo },
                                ConflictingProductIds = overlappingProducts,
                                OverlapStartDate = overlapStart,
                                OverlapEndDate = overlapEnd,
                                SharedPriority = promotion.Priority
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error detecting conflicts: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return conflicts;
        }

        private List<string> ParseProductIds(string? applicableProducts)
        {
            if (string.IsNullOrWhiteSpace(applicableProducts))
                return new List<string>();

            return applicableProducts
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToList();
        }

        private bool HasDateOverlap(DateOnly start1, DateOnly end1, DateOnly start2, DateOnly end2)
        {
            return start1 <= end2 && start2 <= end1;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // basic validation
            if (string.IsNullOrWhiteSpace(txtPromotionName.Text) || txtPromotionName.Text.Length > 100)
            {
                MessageBox.Show("Promotion Name is required and must be under 100 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dateEndDate.Value.Date < dateStartDate.Value.Date)
            {
                MessageBox.Show("End date cannot be earlier than start date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // build promotion
            var p = new Promotion();
            // PromotionId left as default for creation
            p.PromotionName = txtPromotionName.Text.Trim();
            p.DiscountType = txtDiscountType.SelectedItem as string ?? "Percentage";
            p.DiscountValue = numDiscountValue.Value;
            p.ApplicableProducts = txtApplicableProducts.Text?.Trim();
            p.StartDate = DateOnly.FromDateTime(dateStartDate.Value);
            p.EndDate = DateOnly.FromDateTime(dateEndDate.Value);
            p.MinimumOrderValue = numMinimumOrderValue.Value;
            p.Priority = (int)numPriorityValue.Value;
            p.QuantityBasedRules = null;

            // Detect conflicts before saving
            var conflicts = await DetectConflicts(p);
            if (conflicts.Count > 0)
            {
                var conflictResult = MessageBox.Show(
                    $"{conflicts.Count} conflict(s) detected with existing promotions.\n\n" +
                    "Would you like to use the Conflict Resolution Wizard to review and resolve them?",
                    "Conflicts Detected",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (conflictResult == DialogResult.Yes)
                {
                    // Launch wizard
                    using var wizard = new ConflictResolutionWizard(p, conflicts);
                    if (wizard.ShowDialog() == DialogResult.OK)
                    {
                        if (wizard.PriorityChanged)
                        {
                            p.Priority = wizard.NewPriority;
                            numPriorityValue.Value = wizard.NewPriority;

                            // Re-detect conflicts with new priority
                            var newConflicts = await DetectConflicts(p);
                            if (newConflicts.Count > 0)
                            {
                                MessageBox.Show(
                                    $"Warning: {newConflicts.Count} conflict(s) still exist after priority change.\n" +
                                    "You may need to adjust date ranges or applicable products.\n\n" +
                                    "Proceeding with save...",
                                    "Conflicts Still Exist",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        // User cancelled wizard
                        return;
                    }
                }
                else if (conflictResult == DialogResult.Cancel)
                {
                    // User cancelled save
                    return;
                }
                // DialogResult.No = proceed with save despite conflicts
            }

            // POST or PUT to API
            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                HttpResponseMessage response;
                if (isEdit)
                {
                    p.PromotionId = editingId;
                    response = await client.PutAsJsonAsync($"api/Promotions/{p.PromotionId}", p);
                }
                else
                {
                    response = await client.PostAsJsonAsync("api/Promotions", p);
                }

                if (response.IsSuccessStatusCode)
                {
                    // Some APIs return an empty body (204 No Content) on successful PUT/POST.
                    // Guard against attempting to deserialize empty content which throws the JSON exception.
                    Promotion? created = null;
                    var contentString = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(contentString))
                    {
                        try
                        {
                            created = JsonSerializer.Deserialize<Promotion>(contentString);
                        }
                        catch
                        {
                            // ignore deserialization errors and fallback to request object
                        }
                    }

                    CreatedPromotion = created ?? p;
                    MessageBox.Show(isEdit ? "Promotion updated successfully!" : "Promotion created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to save promotion: {response.StatusCode}\n{errorContent}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving promotion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
