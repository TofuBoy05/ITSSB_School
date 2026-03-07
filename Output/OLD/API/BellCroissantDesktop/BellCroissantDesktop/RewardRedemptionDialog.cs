using System;
using System.Drawing;
using System.Windows.Forms;

namespace BellCroissantDesktop
{
    public class RewardRedemptionDialog : Form
    {
        private Label lblTitle;
        private Label lblInfo;
        private RadioButton rbFiveEuro;
        private RadioButton rbTenPercent;
        private RadioButton rbTierUpgrade;
        private Panel pnlOptions;
        private Button btnRedeem;
        private Button btnCancel;

        public RewardType SelectedReward { get; private set; }

        public RewardRedemptionDialog(CustomerWithLoyalty customer)
        {
            InitializeComponent();
            LoadCustomerInfo(customer);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblInfo = new Label();
            pnlOptions = new Panel();
            rbFiveEuro = new RadioButton();
            rbTenPercent = new RadioButton();
            rbTierUpgrade = new RadioButton();
            btnRedeem = new Button();
            btnCancel = new Button();

            pnlOptions.SuspendLayout();
            SuspendLayout();

            // lblTitle
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(460, 30);
            lblTitle.Text = "Redeem Rewards (1,000 Points)";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblInfo
            lblInfo.Location = new Point(12, 50);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(460, 40);
            lblInfo.Text = "You have enough points to redeem a reward! Select one of the following options:";
            lblInfo.TextAlign = ContentAlignment.MiddleLeft;

            // pnlOptions
            pnlOptions.BorderStyle = BorderStyle.FixedSingle;
            pnlOptions.Controls.Add(rbFiveEuro);
            pnlOptions.Controls.Add(rbTenPercent);
            pnlOptions.Controls.Add(rbTierUpgrade);
            pnlOptions.Location = new Point(12, 100);
            pnlOptions.Name = "pnlOptions";
            pnlOptions.Size = new Size(460, 150);

            // rbFiveEuro
            rbFiveEuro.AutoSize = false;
            rbFiveEuro.Checked = true;
            rbFiveEuro.Location = new Point(15, 15);
            rbFiveEuro.Name = "rbFiveEuro";
            rbFiveEuro.Size = new Size(430, 35);
            rbFiveEuro.TabIndex = 0;
            rbFiveEuro.TabStop = true;
            rbFiveEuro.Text = "€5 Discount on next purchase\n(Valid for 30 days, applicable to all products)";
            rbFiveEuro.UseVisualStyleBackColor = true;

            // rbTenPercent
            rbTenPercent.AutoSize = false;
            rbTenPercent.Location = new Point(15, 55);
            rbTenPercent.Name = "rbTenPercent";
            rbTenPercent.Size = new Size(430, 35);
            rbTenPercent.TabIndex = 1;
            rbTenPercent.Text = "10% Discount on next purchase\n(Valid for 30 days, applicable to all products)";
            rbTenPercent.UseVisualStyleBackColor = true;

            // rbTierUpgrade
            rbTierUpgrade.AutoSize = false;
            rbTierUpgrade.Location = new Point(15, 95);
            rbTierUpgrade.Name = "rbTierUpgrade";
            rbTierUpgrade.Size = new Size(430, 40);
            rbTierUpgrade.TabIndex = 2;
            rbTierUpgrade.Text = "Upgrade Membership Tier\n(Basic → Silver → Gold, earn more points per purchase)";
            rbTierUpgrade.UseVisualStyleBackColor = true;

            // btnRedeem
            btnRedeem.Location = new Point(297, 265);
            btnRedeem.Name = "btnRedeem";
            btnRedeem.Size = new Size(85, 30);
            btnRedeem.TabIndex = 3;
            btnRedeem.Text = "Redeem";
            btnRedeem.UseVisualStyleBackColor = true;
            btnRedeem.Click += btnRedeem_Click;

            // btnCancel
            btnCancel.Location = new Point(387, 265);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 30);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 311);
            Controls.Add(btnCancel);
            Controls.Add(btnRedeem);
            Controls.Add(pnlOptions);
            Controls.Add(lblInfo);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RewardRedemptionDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Redeem Rewards";
            pnlOptions.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void LoadCustomerInfo(CustomerWithLoyalty customer)
        {
            lblInfo.Text = $"You have {customer.Points} points. Select a reward to redeem (costs 1,000 points):";

            // Disable tier upgrade if already at max tier
            if (customer.MembershipStatus == "Gold")
            {
                rbTierUpgrade.Enabled = false;
                rbTierUpgrade.Text += "\n(Already at highest tier)";
            }
        }

        private void btnRedeem_Click(object sender, EventArgs e)
        {
            if (rbFiveEuro.Checked)
            {
                SelectedReward = RewardType.FiveEuroDiscount;
            }
            else if (rbTenPercent.Checked)
            {
                SelectedReward = RewardType.TenPercentDiscount;
            }
            else if (rbTierUpgrade.Checked)
            {
                SelectedReward = RewardType.TierUpgrade;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to redeem this reward? 1,000 points will be deducted.",
                "Confirm Redemption",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
