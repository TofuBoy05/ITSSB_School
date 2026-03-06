namespace BellCroissantDesktop
{
    partial class OrderDetails
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            dgvOrderItems = new DataGridView();
            colItemName = new DataGridViewTextBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            labOrderId = new Label();
            labCustomerName = new Label();
            labOrderDate = new Label();
            labTotalAmount = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            cmbOrderStatus = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvOrderItems).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 26);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 0;
            label1.Text = "Order ID:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 65);
            label2.Name = "label2";
            label2.Size = new Size(97, 15);
            label2.TabIndex = 1;
            label2.Text = "Customer Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 104);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 2;
            label3.Text = "Order Date:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 142);
            label4.Name = "label4";
            label4.Size = new Size(80, 15);
            label4.TabIndex = 3;
            label4.Text = "Total Amount";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 180);
            label5.Name = "label5";
            label5.Size = new Size(75, 15);
            label5.TabIndex = 4;
            label5.Text = "Order Status:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(30, 215);
            label6.Name = "label6";
            label6.Size = new Size(74, 15);
            label6.TabIndex = 5;
            label6.Text = "List of Items:";
            // 
            // dgvOrderItems
            // 
            dgvOrderItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrderItems.Columns.AddRange(new DataGridViewColumn[] { colItemName, colQuantity, colPrice });
            dgvOrderItems.Location = new Point(30, 244);
            dgvOrderItems.Name = "dgvOrderItems";
            dgvOrderItems.Size = new Size(342, 220);
            dgvOrderItems.TabIndex = 6;
            // 
            // colItemName
            // 
            colItemName.DataPropertyName = "ProductName";
            colItemName.HeaderText = "Items";
            colItemName.Name = "colItemName";
            // 
            // colQuantity
            // 
            colQuantity.DataPropertyName = "Quantity";
            colQuantity.HeaderText = "Quantity";
            colQuantity.Name = "colQuantity";
            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "Price";
            colPrice.HeaderText = "Price";
            colPrice.Name = "colPrice";
            // 
            // labOrderId
            // 
            labOrderId.AutoSize = true;
            labOrderId.Location = new Point(167, 26);
            labOrderId.Name = "labOrderId";
            labOrderId.Size = new Size(54, 15);
            labOrderId.TabIndex = 7;
            labOrderId.Text = "(orderID)";
            // 
            // labCustomerName
            // 
            labCustomerName.AutoSize = true;
            labCustomerName.Location = new Point(167, 65);
            labCustomerName.Name = "labCustomerName";
            labCustomerName.Size = new Size(97, 15);
            labCustomerName.TabIndex = 8;
            labCustomerName.Text = "(customerName)";
            // 
            // labOrderDate
            // 
            labOrderDate.AutoSize = true;
            labOrderDate.Location = new Point(167, 104);
            labOrderDate.Name = "labOrderDate";
            labOrderDate.Size = new Size(54, 15);
            labOrderDate.TabIndex = 9;
            labOrderDate.Text = "(orderID)";
            // 
            // labTotalAmount
            // 
            labTotalAmount.AutoSize = true;
            labTotalAmount.Location = new Point(167, 142);
            labTotalAmount.Name = "labTotalAmount";
            labTotalAmount.Size = new Size(54, 15);
            labTotalAmount.TabIndex = 10;
            labTotalAmount.Text = "(orderID)";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(42, 488);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(156, 23);
            btnSave.TabIndex = 12;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(204, 488);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(156, 23);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // cmbOrderStatus
            // 
            cmbOrderStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrderStatus.FormattingEnabled = true;
            cmbOrderStatus.Items.AddRange(new object[] { "Pending", "Completed", "Cancelled" });
            cmbOrderStatus.Location = new Point(167, 177);
            cmbOrderStatus.Name = "cmbOrderStatus";
            cmbOrderStatus.Size = new Size(121, 23);
            cmbOrderStatus.TabIndex = 14;
            // 
            // OrderDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(403, 523);
            Controls.Add(cmbOrderStatus);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(labTotalAmount);
            Controls.Add(labOrderDate);
            Controls.Add(labCustomerName);
            Controls.Add(labOrderId);
            Controls.Add(dgvOrderItems);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "OrderDetails";
            Text = "OrderDetails";
            Load += OrderDetails_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOrderItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private DataGridView dgvOrderItems;
        private Label labOrderId;
        private Label labCustomerName;
        private Label labOrderDate;
        private Label labTotalAmount;
        private Button btnSave;
        private Button btnCancel;
        private ComboBox cmbOrderStatus;
        private DataGridViewTextBoxColumn colItemName;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewTextBoxColumn colPrice;
    }
}