using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace BellCroissantDesktop
{
    public partial class ConflictResolutionWizard : Form
    {
        private int currentStep = 1;
        private List<PromotionConflict> conflicts = new List<PromotionConflict>();
        private PromotionConflict? selectedConflict = null;
        private Promotion currentPromotion;
        private int newPriority;
        private bool priorityChanged = false;

        public bool PriorityChanged => priorityChanged;
        public int NewPriority => newPriority;

        public ConflictResolutionWizard(Promotion promotion, List<PromotionConflict> detectedConflicts)
        {
            InitializeComponent();
            currentPromotion = promotion;
            conflicts = detectedConflicts;
            newPriority = promotion.Priority;

            LoadStep1();
        }

        private void LoadStep1()
        {
            lstConflicts.Items.Clear();
            lstConflicts.DisplayMember = "Summary";

            foreach (var conflict in conflicts)
            {
                lstConflicts.Items.Add(conflict);
            }

            if (conflicts.Count > 0)
            {
                lstConflicts.SelectedIndex = 0;
            }
        }

        private void lstConflicts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstConflicts.SelectedItem is PromotionConflict conflict)
            {
                selectedConflict = conflict;
            }
        }

        private void ShowStep(int step)
        {
            currentStep = step;

            pnlStep1.Visible = (step == 1);
            pnlStep2.Visible = (step == 2);
            pnlStep3.Visible = (step == 3);

            btnBack.Enabled = (step > 1);
            btnNext.Visible = (step < 3);
            btnFinish.Visible = (step == 3);

            lblStepIndicator.Text = $"Step {step} of 3";

            if (step == 2)
            {
                LoadStep2();
            }
            else if (step == 3)
            {
                LoadStep3();
            }
        }

        private void LoadStep2()
        {
            if (selectedConflict == null)
            {
                MessageBox.Show("Please select a conflict to review.", "No Conflict Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowStep(1);
                return;
            }

            var details = new System.Text.StringBuilder();
            details.AppendLine("=== CONFLICT DETAILS ===");
            details.AppendLine();
            details.AppendLine("CONFLICTING PROMOTIONS:");
            foreach (var promo in selectedConflict.ConflictingPromotions)
            {
                details.AppendLine($"  - {promo.PromotionName} (ID: {promo.PromotionId})");
                details.AppendLine($"    Discount: {promo.DiscountType} - {promo.DiscountValue}");
                details.AppendLine($"    Date Range: {promo.StartDate} to {promo.EndDate}");
                details.AppendLine($"    Priority: {promo.Priority}");
                details.AppendLine();
            }

            details.AppendLine("CONFLICTING PRODUCTS:");
            details.AppendLine($"  {string.Join(", ", selectedConflict.ConflictingProductIds)}");
            details.AppendLine();

            details.AppendLine("OVERLAP PERIOD:");
            details.AppendLine($"  {selectedConflict.OverlapStartDate} to {selectedConflict.OverlapEndDate}");
            details.AppendLine();

            details.AppendLine("SHARED PRIORITY:");
            details.AppendLine($"  Priority Level: {selectedConflict.SharedPriority}");
            details.AppendLine();

            details.AppendLine("ISSUE:");
            details.AppendLine($"  Multiple promotions with the same priority ({selectedConflict.SharedPriority}) are");
            details.AppendLine($"  active for the same products during overlapping dates. This creates");
            details.AppendLine($"  ambiguity in which promotion should be applied.");

            txtConflictDetails.Text = details.ToString();
            pnlTimeline.Invalidate(); // Trigger timeline redraw
        }

        private void pnlTimeline_Paint(object sender, PaintEventArgs e)
        {
            if (selectedConflict == null || selectedConflict.ConflictingPromotions.Count == 0)
                return;

            var g = e.Graphics;
            var panel = pnlTimeline;

            // Find overall date range
            var allDates = selectedConflict.ConflictingPromotions
                .SelectMany(p => new[] { p.StartDate, p.EndDate })
                .ToList();
            var minDate = allDates.Min();
            var maxDate = allDates.Max();
            var totalDays = (maxDate.ToDateTime(TimeOnly.MinValue) - minDate.ToDateTime(TimeOnly.MinValue)).Days;

            if (totalDays <= 0) totalDays = 1;

            // Drawing settings
            int margin = 20;
            int rowHeight = 25;
            int timelineWidth = panel.Width - 2 * margin;
            int startY = 10;

            // Draw title
            g.DrawString("Timeline Visualization:", new Font("Segoe UI", 9, FontStyle.Bold), Brushes.Black, margin, startY);
            startY += 20;

            // Draw each promotion's timeline
            int promoIndex = 0;
            foreach (var promo in selectedConflict.ConflictingPromotions)
            {
                int y = startY + (promoIndex * rowHeight);

                // Calculate bar position
                var startDays = (promo.StartDate.ToDateTime(TimeOnly.MinValue) - minDate.ToDateTime(TimeOnly.MinValue)).Days;
                var endDays = (promo.EndDate.ToDateTime(TimeOnly.MinValue) - minDate.ToDateTime(TimeOnly.MinValue)).Days;

                int barX = margin + (int)((startDays / (double)totalDays) * timelineWidth);
                int barWidth = (int)(((endDays - startDays) / (double)totalDays) * timelineWidth);
                if (barWidth < 5) barWidth = 5;

                // Draw bar
                var color = promoIndex % 2 == 0 ? Color.CornflowerBlue : Color.LightCoral;
                g.FillRectangle(new SolidBrush(color), barX, y, barWidth, 20);
                g.DrawRectangle(Pens.Black, barX, y, barWidth, 20);

                // Draw label
                var label = promo.PromotionName.Length > 20 ? promo.PromotionName.Substring(0, 17) + "..." : promo.PromotionName;
                g.DrawString(label, new Font("Segoe UI", 8), Brushes.Black, barX, y + 3);

                promoIndex++;
            }

            // Draw overlap highlight
            var overlapStart = (selectedConflict.OverlapStartDate.ToDateTime(TimeOnly.MinValue) - minDate.ToDateTime(TimeOnly.MinValue)).Days;
            var overlapEnd = (selectedConflict.OverlapEndDate.ToDateTime(TimeOnly.MinValue) - minDate.ToDateTime(TimeOnly.MinValue)).Days;
            int overlapX = margin + (int)((overlapStart / (double)totalDays) * timelineWidth);
            int overlapWidth = (int)(((overlapEnd - overlapStart) / (double)totalDays) * timelineWidth);

            int overlapY = startY + (selectedConflict.ConflictingPromotions.Count * rowHeight) + 5;
            g.FillRectangle(new SolidBrush(Color.FromArgb(100, 255, 0, 0)), overlapX, startY, overlapWidth, selectedConflict.ConflictingPromotions.Count * rowHeight);
            g.DrawString("Overlap", new Font("Segoe UI", 8, FontStyle.Bold), Brushes.Red, overlapX, overlapY);
        }

        private void LoadStep3()
        {
            lblCurrentPriority.Text = $"Current Priority: {currentPromotion.Priority}";

            cmbPriority.Items.Clear();
            for (int i = 0; i <= 10; i++)
            {
                cmbPriority.Items.Add($"Priority {i}" + (i == currentPromotion.Priority ? " (Current)" : ""));
            }

            // Select current priority
            cmbPriority.SelectedIndex = currentPromotion.Priority;
        }

        private void cmbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedPriority = cmbPriority.SelectedIndex;

            if (selectedPriority != currentPromotion.Priority)
            {
                lblPriorityWarning.Text = $"⚠ Warning: Changing priority from {currentPromotion.Priority} to {selectedPriority} may affect how this promotion interacts with other promotions. " +
                    $"This change alone may not fully resolve conflicts if products still overlap with other promotions of priority {selectedPriority}. " +
                    $"Consider also adjusting date ranges or applicable products.";
                newPriority = selectedPriority;
                priorityChanged = true;
            }
            else
            {
                lblPriorityWarning.Text = "";
                newPriority = currentPromotion.Priority;
                priorityChanged = false;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentStep == 1 && selectedConflict == null)
            {
                MessageBox.Show("Please select a conflict to review.", "No Conflict Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShowStep(currentStep + 1);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ShowStep(currentStep - 1);
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (priorityChanged)
            {
                var result = MessageBox.Show(
                    $"You have changed the priority to {newPriority}. This will be applied when you save the promotion.\n\n" +
                    $"Note: This may not fully resolve all conflicts. You may need to adjust date ranges or products as well.\n\n" +
                    $"Do you want to proceed?",
                    "Confirm Priority Change",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                var result = MessageBox.Show(
                    "No changes were made. The conflicts still exist.\n\n" +
                    "You can:\n" +
                    "- Adjust the promotion's date range\n" +
                    "- Modify applicable products\n" +
                    "- Change priority (Step 3)\n\n" +
                    "Do you want to close the wizard without making changes?",
                    "No Changes Made",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
