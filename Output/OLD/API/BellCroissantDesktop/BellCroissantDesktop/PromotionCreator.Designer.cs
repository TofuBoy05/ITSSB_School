namespace BellCroissantDesktop
{
    partial class PromotionCreator
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
            txtPromotionName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtApplicableProducts = new TextBox();
            txtDiscountType = new ComboBox();
            label5 = new Label();
            dateStartDate = new DateTimePicker();
            dateEndDate = new DateTimePicker();
            label6 = new Label();
            label7 = new Label();
            numDiscountValue = new NumericUpDown();
            numMinimumOrderValue = new NumericUpDown();
            numPriorityValue = new NumericUpDown();
            label8 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numDiscountValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinimumOrderValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPriorityValue).BeginInit();
            SuspendLayout();
            // 
            // txtPromotionName
            // 
            txtPromotionName.Location = new Point(12, 38);
            txtPromotionName.Name = "txtPromotionName";
            txtPromotionName.Size = new Size(385, 23);
            txtPromotionName.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 1;
            label1.Text = "Promotion Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 69);
            label2.Name = "label2";
            label2.Size = new Size(82, 15);
            label2.TabIndex = 3;
            label2.Text = "Discount Type";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 118);
            label3.Name = "label3";
            label3.Size = new Size(85, 15);
            label3.TabIndex = 5;
            label3.Text = "Discount Value";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 167);
            label4.Name = "label4";
            label4.Size = new Size(222, 15);
            label4.TabIndex = 7;
            label4.Text = "Applicable Products (Comma Separated)";
            // 
            // txtApplicableProducts
            // 
            txtApplicableProducts.Location = new Point(12, 185);
            txtApplicableProducts.Name = "txtApplicableProducts";
            txtApplicableProducts.Size = new Size(385, 23);
            txtApplicableProducts.TabIndex = 6;
            // 
            // txtDiscountType
            // 
            txtDiscountType.DropDownStyle = ComboBoxStyle.DropDownList;
            txtDiscountType.FormattingEnabled = true;
            txtDiscountType.Items.AddRange(new object[] { "Percentage", "FixedAmount" });
            txtDiscountType.Location = new Point(12, 87);
            txtDiscountType.Name = "txtDiscountType";
            txtDiscountType.Size = new Size(385, 23);
            txtDiscountType.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 216);
            label5.Name = "label5";
            label5.Size = new Size(58, 15);
            label5.TabIndex = 10;
            label5.Text = "Start Date";
            // 
            // dateStartDate
            // 
            dateStartDate.Location = new Point(12, 234);
            dateStartDate.Name = "dateStartDate";
            dateStartDate.Size = new Size(385, 23);
            dateStartDate.TabIndex = 11;
            // 
            // dateEndDate
            // 
            dateEndDate.Location = new Point(12, 283);
            dateEndDate.Name = "dateEndDate";
            dateEndDate.Size = new Size(385, 23);
            dateEndDate.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 265);
            label6.Name = "label6";
            label6.Size = new Size(54, 15);
            label6.TabIndex = 12;
            label6.Text = "End Date";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 314);
            label7.Name = "label7";
            label7.Size = new Size(124, 15);
            label7.TabIndex = 15;
            label7.Text = "Minimum Order Value";
            // 
            // numDiscountValue
            // 
            numDiscountValue.Location = new Point(12, 136);
            numDiscountValue.Name = "numDiscountValue";
            numDiscountValue.Size = new Size(385, 23);
            numDiscountValue.TabIndex = 16;
            // 
            // numMinimumOrderValue
            // 
            numMinimumOrderValue.Location = new Point(12, 332);
            numMinimumOrderValue.Name = "numMinimumOrderValue";
            numMinimumOrderValue.Size = new Size(385, 23);
            numMinimumOrderValue.TabIndex = 17;
            // 
            // numPriorityValue
            // 
            numPriorityValue.Location = new Point(12, 381);
            numPriorityValue.Name = "numPriorityValue";
            numPriorityValue.Size = new Size(385, 23);
            numPriorityValue.TabIndex = 19;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 363);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 18;
            label8.Text = "Priority Value";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 420);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(185, 23);
            btnSave.TabIndex = 23;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(212, 420);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(185, 23);
            btnCancel.TabIndex = 24;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // PromotionCreator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(409, 470);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(numPriorityValue);
            Controls.Add(label8);
            Controls.Add(numMinimumOrderValue);
            Controls.Add(numDiscountValue);
            Controls.Add(label7);
            Controls.Add(dateEndDate);
            Controls.Add(label6);
            Controls.Add(dateStartDate);
            Controls.Add(label5);
            Controls.Add(txtDiscountType);
            Controls.Add(label4);
            Controls.Add(txtApplicableProducts);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPromotionName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "PromotionCreator";
            Text = "PromotionCreator";
            ((System.ComponentModel.ISupportInitialize)numDiscountValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinimumOrderValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPriorityValue).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPromotionName;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtApplicableProducts;
        private ComboBox txtDiscountType;
        private Label label5;
        private DateTimePicker dateStartDate;
        private DateTimePicker dateEndDate;
        private Label label6;
        private Label label7;
        private NumericUpDown numDiscountValue;
        private NumericUpDown numMinimumOrderValue;
        private NumericUpDown numPriorityValue;
        private Label label8;
        private Button btnSave;
        private Button btnCancel;
    }
}