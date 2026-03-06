namespace BellCroissantDesktop
{
    partial class OrderManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAddOrder = new Button();
            txtSearch = new TextBox();
            dgvProducts = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colCustomerName = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colTotalAmount = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            btnOrderDetail = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // btnAddOrder
            // 
            btnAddOrder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddOrder.Location = new Point(666, 3);
            btnAddOrder.Name = "btnAddOrder";
            btnAddOrder.Size = new Size(122, 23);
            btnAddOrder.TabIndex = 5;
            btnAddOrder.Text = "Add new Order";
            btnAddOrder.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(12, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search on ID/Customer Name/Date";
            txtSearch.Size = new Size(301, 23);
            txtSearch.TabIndex = 4;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // dgvProducts
            // 
            dgvProducts.AccessibleName = "dgvProducts";
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { colId, colCustomerName, colDate, colTotalAmount, colStatus, btnOrderDetail });
            dgvProducts.Location = new Point(12, 33);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.Size = new Size(776, 405);
            dgvProducts.TabIndex = 3;
            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
            dgvProducts.ColumnHeaderMouseClick += dgvProducts_ColumnHeaderMouseClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "TransactionId";
            colId.HeaderText = "Order ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colCustomerName
            // 
            colCustomerName.DataPropertyName = "CustomerName";
            colCustomerName.HeaderText = "Customer Name";
            colCustomerName.Name = "colCustomerName";
            colCustomerName.ReadOnly = true;
            // 
            // colDate
            // 
            colDate.DataPropertyName = "OrderDate";
            colDate.HeaderText = "Date";
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            // 
            // colTotalAmount
            // 
            colTotalAmount.DataPropertyName = "TotalAmount";
            colTotalAmount.HeaderText = "Total Amount";
            colTotalAmount.Name = "colTotalAmount";
            colTotalAmount.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.DataPropertyName = "Status";
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // btnOrderDetail
            // 
            btnOrderDetail.HeaderText = "Order Detail";
            btnOrderDetail.Name = "btnOrderDetail";
            btnOrderDetail.ReadOnly = true;
            btnOrderDetail.Text = "Detail View";
            btnOrderDetail.UseColumnTextForButtonValue = true;
            // 
            // OrderManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAddOrder);
            Controls.Add(txtSearch);
            Controls.Add(dgvProducts);
            Name = "OrderManagement";
            Text = "Form2";
            Load += OrderManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAddOrder;
        private TextBox txtSearch;
        private DataGridView dgvProducts;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colCustomerName;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colTotalAmount;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewButtonColumn btnOrderDetail;
    }
}