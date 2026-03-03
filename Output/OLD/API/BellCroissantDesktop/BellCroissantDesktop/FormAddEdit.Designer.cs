namespace BellCroissantDesktop
{
    partial class FormAddEdit
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
            cbActive = new CheckBox();
            cbSeasonal = new CheckBox();
            txtCategory = new TextBox();
            txtProductName = new TextBox();
            dtPickerIntroductionDate = new DateTimePicker();
            label6 = new Label();
            txtDescription = new RichTextBox();
            btnSave = new Button();
            btnCancel = new Button();
            numPrice = new NumericUpDown();
            numCost = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCost).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 15);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "Category:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 53);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 1;
            label2.Text = "Product Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 91);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 2;
            label3.Text = "Price:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 131);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 3;
            label4.Text = "Cost:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 171);
            label5.Name = "label5";
            label5.Size = new Size(103, 15);
            label5.TabIndex = 4;
            label5.Text = "Introduction Date:";
            // 
            // cbActive
            // 
            cbActive.AutoSize = true;
            cbActive.Location = new Point(14, 220);
            cbActive.Name = "cbActive";
            cbActive.Size = new Size(59, 19);
            cbActive.TabIndex = 5;
            cbActive.Text = "Active";
            cbActive.UseVisualStyleBackColor = true;
            // 
            // cbSeasonal
            // 
            cbSeasonal.AutoSize = true;
            cbSeasonal.Location = new Point(134, 220);
            cbSeasonal.Name = "cbSeasonal";
            cbSeasonal.Size = new Size(72, 19);
            cbSeasonal.TabIndex = 6;
            cbSeasonal.Text = "Seasonal";
            cbSeasonal.UseVisualStyleBackColor = true;
            // 
            // txtCategory
            // 
            txtCategory.Location = new Point(134, 12);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(254, 23);
            txtCategory.TabIndex = 7;
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(134, 50);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(254, 23);
            txtProductName.TabIndex = 8;
            // 
            // dtPickerIntroductionDate
            // 
            dtPickerIntroductionDate.Location = new Point(134, 167);
            dtPickerIntroductionDate.Name = "dtPickerIntroductionDate";
            dtPickerIntroductionDate.Size = new Size(255, 23);
            dtPickerIntroductionDate.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 264);
            label6.Name = "label6";
            label6.Size = new Size(70, 15);
            label6.TabIndex = 12;
            label6.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(134, 264);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(254, 131);
            txtDescription.TabIndex = 13;
            txtDescription.Text = "";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 481);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(194, 23);
            btnSave.TabIndex = 14;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(214, 481);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(175, 23);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // numPrice
            // 
            numPrice.Location = new Point(134, 89);
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(254, 23);
            numPrice.TabIndex = 16;
            // 
            // numCost
            // 
            numCost.Location = new Point(134, 129);
            numCost.Name = "numCost";
            numCost.Size = new Size(254, 23);
            numCost.TabIndex = 17;
            // 
            // FormAddEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(404, 517);
            Controls.Add(numCost);
            Controls.Add(numPrice);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtDescription);
            Controls.Add(label6);
            Controls.Add(dtPickerIntroductionDate);
            Controls.Add(txtProductName);
            Controls.Add(txtCategory);
            Controls.Add(cbSeasonal);
            Controls.Add(cbActive);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FormAddEdit";
            Text = "Add/Edit Product";
            Load += formAddEdit_Load;
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCost).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private CheckBox cbActive;
        private CheckBox cbSeasonal;
        private TextBox txtCategory;
        private TextBox txtProductName;
        private DateTimePicker dtPickerIntroductionDate;
        private Label label6;
        private RichTextBox txtDescription;
        private Button btnSave;
        private Button btnCancel;
        private NumericUpDown numPrice;
        private NumericUpDown numCost;
    }
}