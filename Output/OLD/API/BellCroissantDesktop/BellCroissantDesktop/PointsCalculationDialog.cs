using System;
using System.Drawing;
using System.Windows.Forms;

namespace BellCroissantDesktop
{
    public class PointsCalculationDialog : Form
    {
        private DataGridView dgvBreakdown;
        private Label lblTitle;
        private Label lblAnniversary;
        private Label lblTotal;
        private Button btnConfirm;
        private Button btnCancel;

        public PointsCalculationDialog(PointsCalculation calculation)
        {
            InitializeComponent();
            LoadCalculation(calculation);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            dgvBreakdown = new DataGridView();
            lblAnniversary = new Label();
            lblTotal = new Label();
            btnConfirm = new Button();
            btnCancel = new Button();
            
            ((System.ComponentModel.ISupportInitialize)dgvBreakdown).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(760, 25);
            lblTitle.Text = "Points Calculation Breakdown";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // dgvBreakdown
            dgvBreakdown.AllowUserToAddRows = false;
            dgvBreakdown.AllowUserToDeleteRows = false;
            dgvBreakdown.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBreakdown.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBreakdown.Location = new Point(12, 45);
            dgvBreakdown.Name = "dgvBreakdown";
            dgvBreakdown.ReadOnly = true;
            dgvBreakdown.Size = new Size(760, 300);
            dgvBreakdown.TabIndex = 0;

            // lblAnniversary
            lblAnniversary.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAnniversary.ForeColor = Color.Green;
            lblAnniversary.Location = new Point(12, 355);
            lblAnniversary.Name = "lblAnniversary";
            lblAnniversary.Size = new Size(760, 23);
            lblAnniversary.Text = "";

            // lblTotal
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotal.Location = new Point(12, 385);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(760, 25);
            lblTotal.Text = "Total Points: 0";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;

            // btnConfirm
            btnConfirm.Location = new Point(597, 420);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(85, 30);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += (s, e) => DialogResult = DialogResult.OK;

            // btnCancel
            btnCancel.Location = new Point(687, 420);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 30);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(lblTotal);
            Controls.Add(lblAnniversary);
            Controls.Add(dgvBreakdown);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PointsCalculationDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Points Calculation";
            ((System.ComponentModel.ISupportInitialize)dgvBreakdown).EndInit();
            ResumeLayout(false);
        }

        private void LoadCalculation(PointsCalculation calculation)
        {
            lblTitle.Text = $"Points Calculation for {calculation.CustomerName}";

            dgvBreakdown.Columns.Clear();
            dgvBreakdown.Columns.Add("OrderId", "Order ID");
            dgvBreakdown.Columns.Add("OrderDate", "Order Date");
            dgvBreakdown.Columns.Add("TotalAmount", "Order Amount");
            dgvBreakdown.Columns.Add("BasePoints", "Base Points");
            dgvBreakdown.Columns.Add("PromotionBonus", "Promo Bonus");
            dgvBreakdown.Columns.Add("TotalPoints", "Total Points");

            foreach (var order in calculation.OrderBreakdown)
            {
                dgvBreakdown.Rows.Add(
                    order.OrderId,
                    order.OrderDate.ToString("yyyy-MM-dd"),
                    $"€{order.TotalAmount:F2}",
                    order.BasePoints,
                    order.PromotionBonus,
                    order.TotalPoints
                );
            }

            if (calculation.AnniversaryBonus > 0)
            {
                lblAnniversary.Text = $"🎉 Anniversary Bonus: +{calculation.AnniversaryBonus} points!";
            }

            lblTotal.Text = $"Total Points: {calculation.TotalPoints}";
        }
    }
}
