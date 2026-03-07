namespace BellCroissantDesktop
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvProducts = new DataGridView();
            colActive = new DataGridViewCheckBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colCat = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colCost = new DataGridViewTextBoxColumn();
            btnEdit = new DataGridViewButtonColumn();
            btnDelete = new DataGridViewButtonColumn();
            txtSearch = new TextBox();
            btnAddProduct = new Button();
            btnManageOrders = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.AccessibleName = "dgvProducts";
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { colActive, colName, colCat, colPrice, colCost, btnEdit, btnDelete });
            dgvProducts.Location = new Point(12, 41);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.Size = new Size(776, 406);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
            dgvProducts.ColumnHeaderMouseClick += dgvProducts_ColumnHeaderMouseClick;
            dgvProducts.DataError += dgvProducts_DataError;
            // 
            // colActive
            // 
            colActive.DataPropertyName = "Active";
            colActive.HeaderText = "Active";
            colActive.Name = "colActive";
            colActive.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "ProductName";
            colName.HeaderText = "Product Name";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colCat
            // 
            colCat.DataPropertyName = "Category";
            colCat.HeaderText = "Category";
            colCat.Name = "colCat";
            colCat.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "Price";
            colPrice.HeaderText = "Price";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colCost
            // 
            colCost.DataPropertyName = "Cost";
            colCost.HeaderText = "Cost";
            colCost.Name = "colCost";
            colCost.ReadOnly = true;
            // 
            // btnEdit
            // 
            btnEdit.HeaderText = "Action";
            btnEdit.Name = "btnEdit";
            btnEdit.ReadOnly = true;
            btnEdit.Text = "Edit";
            btnEdit.UseColumnTextForButtonValue = true;
            // 
            // btnDelete
            // 
            btnDelete.HeaderText = "";
            btnDelete.Name = "btnDelete";
            btnDelete.ReadOnly = true;
            btnDelete.Text = "Delete";
            btnDelete.UseColumnTextForButtonValue = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(12, 12);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search on products/categories";
            txtSearch.Size = new Size(301, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(666, 11);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(122, 23);
            btnAddProduct.TabIndex = 2;
            btnAddProduct.Text = "Add new Product";
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // btnManageOrders
            // 
            btnManageOrders.Location = new Point(682, 457);
            btnManageOrders.Name = "btnManageOrders";
            btnManageOrders.Size = new Size(106, 23);
            btnManageOrders.TabIndex = 3;
            btnManageOrders.Text = "Manage Orders";
            btnManageOrders.UseVisualStyleBackColor = true;
            btnManageOrders.Click += btnManageOrders_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 492);
            Controls.Add(btnManageOrders);
            Controls.Add(btnAddProduct);
            Controls.Add(txtSearch);
            Controls.Add(dgvProducts);
            Name = "MainForm";
            Text = "Product management";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProducts;
        private TextBox txtSearch;
        private Button btnAddProduct;
        private DataGridViewCheckBoxColumn colActive;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colCat;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colCost;
        private DataGridViewButtonColumn btnEdit;
        private DataGridViewButtonColumn btnDelete;
        private Button btnManageOrders;
    }
}
