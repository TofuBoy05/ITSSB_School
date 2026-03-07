namespace BellCroissantDesktop
{
    partial class LoyaltyManagement
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvCustomers = new DataGridView();
            colCustomerId = new DataGridViewTextBoxColumn();
            colFirstName = new DataGridViewTextBoxColumn();
            colLastName = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colMembershipStatus = new DataGridViewTextBoxColumn();
            colLoyaltyPoints = new DataGridViewTextBoxColumn();
            colTotalSpending = new DataGridViewTextBoxColumn();
            txtSearch = new TextBox();
            lblSearch = new Label();
            pnlCustomerDetails = new Panel();
            btnRedeemRewards = new Button();
            btnRecalculatePoints = new Button();
            btnCancel = new Button();
            btnSaveChanges = new Button();
            btnPopulateTestData = new Button();
            numLoyaltyPoints = new NumericUpDown();
            txtMembershipStatus = new TextBox();
            txtEmail = new TextBox();
            txtLastName = new TextBox();
            txtFirstName = new TextBox();
            txtCustomerId = new TextBox();
            lblLoyaltyPoints = new Label();
            lblMembershipStatus = new Label();
            lblEmail = new Label();
            lblLastName = new Label();
            lblFirstName = new Label();
            lblCustomerId = new Label();
            lblCustomerDetails = new Label();
            pnlPagination = new Panel();
            lblPageInfo = new Label();
            btnNextPage = new Button();
            btnPrevPage = new Button();
            cmbPageSize = new ComboBox();
            lblPageSize = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            pnlCustomerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numLoyaltyPoints).BeginInit();
            pnlPagination.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Columns.AddRange(new DataGridViewColumn[] { colCustomerId, colFirstName, colLastName, colEmail, colMembershipStatus, colLoyaltyPoints, colTotalSpending });
            dgvCustomers.Location = new Point(12, 45);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(760, 300);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
            dgvCustomers.ColumnHeaderMouseClick += dgvCustomers_ColumnHeaderMouseClick;
            // 
            // colCustomerId
            // 
            colCustomerId.DataPropertyName = "CustomerId";
            colCustomerId.HeaderText = "Customer ID";
            colCustomerId.Name = "colCustomerId";
            colCustomerId.ReadOnly = true;
            colCustomerId.Width = 80;
            // 
            // colFirstName
            // 
            colFirstName.DataPropertyName = "FirstName";
            colFirstName.HeaderText = "First Name";
            colFirstName.Name = "colFirstName";
            colFirstName.ReadOnly = true;
            colFirstName.Width = 100;
            // 
            // colLastName
            // 
            colLastName.DataPropertyName = "LastName";
            colLastName.HeaderText = "Last Name";
            colLastName.Name = "colLastName";
            colLastName.ReadOnly = true;
            colLastName.Width = 100;
            // 
            // colEmail
            // 
            colEmail.DataPropertyName = "Email";
            colEmail.HeaderText = "Email";
            colEmail.Name = "colEmail";
            colEmail.ReadOnly = true;
            colEmail.Width = 150;
            // 
            // colMembershipStatus
            // 
            colMembershipStatus.DataPropertyName = "MembershipStatus";
            colMembershipStatus.HeaderText = "Membership";
            colMembershipStatus.Name = "colMembershipStatus";
            colMembershipStatus.ReadOnly = true;
            colMembershipStatus.Width = 90;
            // 
            // colLoyaltyPoints
            // 
            colLoyaltyPoints.DataPropertyName = "Points";
            colLoyaltyPoints.HeaderText = "Loyalty Points";
            colLoyaltyPoints.Name = "colLoyaltyPoints";
            colLoyaltyPoints.ReadOnly = true;
            colLoyaltyPoints.Width = 90;
            // 
            // colTotalSpending
            // 
            colTotalSpending.DataPropertyName = "TotalSpending";
            colTotalSpending.HeaderText = "Total Spending";
            colTotalSpending.Name = "colTotalSpending";
            colTotalSpending.ReadOnly = true;
            colTotalSpending.Width = 100;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(70, 12);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search by name, ID, or email...";
            txtSearch.Size = new Size(350, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(12, 15);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "Search:";
            // 
            // btnPopulateTestData
            // 
            btnPopulateTestData.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPopulateTestData.BackColor = Color.Orange;
            btnPopulateTestData.Location = new Point(592, 12);
            btnPopulateTestData.Name = "btnPopulateTestData";
            btnPopulateTestData.Size = new Size(180, 23);
            btnPopulateTestData.TabIndex = 5;
            btnPopulateTestData.Text = "⚠ Populate Test Data (250)";
            btnPopulateTestData.UseVisualStyleBackColor = false;
            btnPopulateTestData.Click += btnPopulateTestData_Click;
            // 
            // pnlCustomerDetails
            // 
            pnlCustomerDetails.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlCustomerDetails.BorderStyle = BorderStyle.FixedSingle;
            pnlCustomerDetails.Controls.Add(btnRedeemRewards);
            pnlCustomerDetails.Controls.Add(btnRecalculatePoints);
            pnlCustomerDetails.Controls.Add(btnCancel);
            pnlCustomerDetails.Controls.Add(btnSaveChanges);
            pnlCustomerDetails.Controls.Add(numLoyaltyPoints);
            pnlCustomerDetails.Controls.Add(txtMembershipStatus);
            pnlCustomerDetails.Controls.Add(txtEmail);
            pnlCustomerDetails.Controls.Add(txtLastName);
            pnlCustomerDetails.Controls.Add(txtFirstName);
            pnlCustomerDetails.Controls.Add(txtCustomerId);
            pnlCustomerDetails.Controls.Add(lblLoyaltyPoints);
            pnlCustomerDetails.Controls.Add(lblMembershipStatus);
            pnlCustomerDetails.Controls.Add(lblEmail);
            pnlCustomerDetails.Controls.Add(lblLastName);
            pnlCustomerDetails.Controls.Add(lblFirstName);
            pnlCustomerDetails.Controls.Add(lblCustomerId);
            pnlCustomerDetails.Controls.Add(lblCustomerDetails);
            pnlCustomerDetails.Enabled = false;
            pnlCustomerDetails.Location = new Point(12, 391);
            pnlCustomerDetails.Name = "pnlCustomerDetails";
            pnlCustomerDetails.Size = new Size(760, 180);
            pnlCustomerDetails.TabIndex = 3;
            // 
            // btnRedeemRewards
            // 
            btnRedeemRewards.Location = new Point(630, 140);
            btnRedeemRewards.Name = "btnRedeemRewards";
            btnRedeemRewards.Size = new Size(120, 30);
            btnRedeemRewards.TabIndex = 16;
            btnRedeemRewards.Text = "Redeem Rewards";
            btnRedeemRewards.UseVisualStyleBackColor = true;
            btnRedeemRewards.Visible = false;
            btnRedeemRewards.Click += btnRedeemRewards_Click;
            // 
            // btnRecalculatePoints
            // 
            btnRecalculatePoints.Location = new Point(240, 140);
            btnRecalculatePoints.Name = "btnRecalculatePoints";
            btnRecalculatePoints.Size = new Size(140, 30);
            btnRecalculatePoints.TabIndex = 15;
            btnRecalculatePoints.Text = "Recalculate Points";
            btnRecalculatePoints.UseVisualStyleBackColor = true;
            btnRecalculatePoints.Click += btnRecalculatePoints_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(130, 140);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 30);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.Location = new Point(15, 140);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(110, 30);
            btnSaveChanges.TabIndex = 13;
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.UseVisualStyleBackColor = true;
            btnSaveChanges.Click += btnSaveChanges_Click;
            // 
            // numLoyaltyPoints
            // 
            numLoyaltyPoints.Location = new Point(500, 95);
            numLoyaltyPoints.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numLoyaltyPoints.Name = "numLoyaltyPoints";
            numLoyaltyPoints.Size = new Size(240, 23);
            numLoyaltyPoints.TabIndex = 12;
            // 
            // txtMembershipStatus
            // 
            txtMembershipStatus.Location = new Point(500, 60);
            txtMembershipStatus.Name = "txtMembershipStatus";
            txtMembershipStatus.ReadOnly = true;
            txtMembershipStatus.Size = new Size(240, 23);
            txtMembershipStatus.TabIndex = 11;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(500, 30);
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = true;
            txtEmail.Size = new Size(240, 23);
            txtEmail.TabIndex = 10;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(115, 95);
            txtLastName.Name = "txtLastName";
            txtLastName.ReadOnly = true;
            txtLastName.Size = new Size(240, 23);
            txtLastName.TabIndex = 9;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(115, 60);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.ReadOnly = true;
            txtFirstName.Size = new Size(240, 23);
            txtFirstName.TabIndex = 8;
            // 
            // txtCustomerId
            // 
            txtCustomerId.Location = new Point(115, 30);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(240, 23);
            txtCustomerId.TabIndex = 7;
            // 
            // lblLoyaltyPoints
            // 
            lblLoyaltyPoints.AutoSize = true;
            lblLoyaltyPoints.Location = new Point(400, 97);
            lblLoyaltyPoints.Name = "lblLoyaltyPoints";
            lblLoyaltyPoints.Size = new Size(84, 15);
            lblLoyaltyPoints.TabIndex = 6;
            lblLoyaltyPoints.Text = "Loyalty Points:";
            // 
            // lblMembershipStatus
            // 
            lblMembershipStatus.AutoSize = true;
            lblMembershipStatus.Location = new Point(400, 63);
            lblMembershipStatus.Name = "lblMembershipStatus";
            lblMembershipStatus.Size = new Size(76, 15);
            lblMembershipStatus.TabIndex = 5;
            lblMembershipStatus.Text = "Membership:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(400, 33);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email:";
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(15, 97);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(66, 15);
            lblLastName.TabIndex = 3;
            lblLastName.Text = "Last Name:";
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(15, 63);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(67, 15);
            lblFirstName.TabIndex = 2;
            lblFirstName.Text = "First Name:";
            // 
            // lblCustomerId
            // 
            lblCustomerId.AutoSize = true;
            lblCustomerId.Location = new Point(15, 33);
            lblCustomerId.Name = "lblCustomerId";
            lblCustomerId.Size = new Size(76, 15);
            lblCustomerId.TabIndex = 1;
            lblCustomerId.Text = "Customer ID:";
            // 
            // lblCustomerDetails
            // 
            lblCustomerDetails.AutoSize = true;
            lblCustomerDetails.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCustomerDetails.Location = new Point(10, 5);
            lblCustomerDetails.Name = "lblCustomerDetails";
            lblCustomerDetails.Size = new Size(123, 19);
            lblCustomerDetails.TabIndex = 0;
            lblCustomerDetails.Text = "Customer Details";
            // 
            // pnlPagination
            // 
            pnlPagination.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlPagination.Controls.Add(lblPageInfo);
            pnlPagination.Controls.Add(btnNextPage);
            pnlPagination.Controls.Add(btnPrevPage);
            pnlPagination.Controls.Add(cmbPageSize);
            pnlPagination.Controls.Add(lblPageSize);
            pnlPagination.Location = new Point(12, 351);
            pnlPagination.Name = "pnlPagination";
            pnlPagination.Size = new Size(760, 35);
            pnlPagination.TabIndex = 4;
            // 
            // lblPageInfo
            // 
            lblPageInfo.Anchor = AnchorStyles.Top;
            lblPageInfo.Location = new Point(280, 8);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(200, 23);
            lblPageInfo.TabIndex = 4;
            lblPageInfo.Text = "Page 1 of 1";
            lblPageInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnNextPage
            // 
            btnNextPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNextPage.Location = new Point(665, 5);
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new Size(85, 25);
            btnNextPage.TabIndex = 3;
            btnNextPage.Text = "Next >";
            btnNextPage.UseVisualStyleBackColor = true;
            btnNextPage.Click += btnNextPage_Click;
            // 
            // btnPrevPage
            // 
            btnPrevPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPrevPage.Location = new Point(574, 5);
            btnPrevPage.Name = "btnPrevPage";
            btnPrevPage.Size = new Size(85, 25);
            btnPrevPage.TabIndex = 2;
            btnPrevPage.Text = "< Previous";
            btnPrevPage.UseVisualStyleBackColor = true;
            btnPrevPage.Click += btnPrevPage_Click;
            // 
            // cmbPageSize
            // 
            cmbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPageSize.FormattingEnabled = true;
            cmbPageSize.Items.AddRange(new object[] { "10", "25", "50", "100" });
            cmbPageSize.Location = new Point(90, 6);
            cmbPageSize.Name = "cmbPageSize";
            cmbPageSize.Size = new Size(75, 23);
            cmbPageSize.TabIndex = 1;
            cmbPageSize.SelectedIndexChanged += cmbPageSize_SelectedIndexChanged;
            // 
            // lblPageSize
            // 
            lblPageSize.AutoSize = true;
            lblPageSize.Location = new Point(10, 9);
            lblPageSize.Name = "lblPageSize";
            lblPageSize.Size = new Size(74, 15);
            lblPageSize.TabIndex = 0;
            lblPageSize.Text = "Items/Page:";
            // 
            // LoyaltyManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 581);
            Controls.Add(btnPopulateTestData);
            Controls.Add(pnlPagination);
            Controls.Add(pnlCustomerDetails);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(dgvCustomers);
            Name = "LoyaltyManagement";
            Text = "Loyalty Program Management";
            Load += LoyaltyManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            pnlCustomerDetails.ResumeLayout(false);
            pnlCustomerDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numLoyaltyPoints).EndInit();
            pnlPagination.ResumeLayout(false);
            pnlPagination.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvCustomers;
        private TextBox txtSearch;
        private Label lblSearch;
        private Panel pnlCustomerDetails;
        private Label lblCustomerDetails;
        private Label lblCustomerId;
        private Label lblFirstName;
        private Label lblLastName;
        private Label lblEmail;
        private Label lblMembershipStatus;
        private Label lblLoyaltyPoints;
        private TextBox txtCustomerId;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtEmail;
        private TextBox txtMembershipStatus;
        private NumericUpDown numLoyaltyPoints;
        private Button btnSaveChanges;
        private Button btnCancel;
        private Button btnRecalculatePoints;
        private Button btnRedeemRewards;
        private Panel pnlPagination;
        private Label lblPageSize;
        private ComboBox cmbPageSize;
        private Button btnPrevPage;
        private Button btnNextPage;
        private Label lblPageInfo;
        private DataGridViewTextBoxColumn colCustomerId;
        private DataGridViewTextBoxColumn colFirstName;
        private DataGridViewTextBoxColumn colLastName;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colMembershipStatus;
        private DataGridViewTextBoxColumn colLoyaltyPoints;
        private DataGridViewTextBoxColumn colTotalSpending;
        private Button btnPopulateTestData;
    }
}
