namespace BellCroissantDesktop
{
    partial class PromotionManager
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
            dgvPromotions = new DataGridView();
            btnAddPromotion = new Button();
            txtSearchPromotions = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvPromotions).BeginInit();
            SuspendLayout();
            // 
            // dgvPromotions
            // 
            dgvPromotions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPromotions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPromotions.Location = new Point(12, 46);
            dgvPromotions.Name = "dgvPromotions";
            dgvPromotions.Size = new Size(938, 392);
            dgvPromotions.TabIndex = 0;
            dgvPromotions.CellContentClick += dgvPromotions_CellContentClick;
            // 
            // btnAddPromotion
            // 
            btnAddPromotion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddPromotion.Location = new Point(831, 12);
            btnAddPromotion.Name = "btnAddPromotion";
            btnAddPromotion.Size = new Size(119, 23);
            btnAddPromotion.TabIndex = 1;
            btnAddPromotion.Text = "Add Promotion";
            btnAddPromotion.UseVisualStyleBackColor = true;
            btnAddPromotion.Click += btnAddPromotion_Click;
            // 
            // txtSearchPromotions
            // 
            txtSearchPromotions.Location = new Point(12, 12);
            txtSearchPromotions.Name = "txtSearchPromotions";
            txtSearchPromotions.Size = new Size(258, 23);
            txtSearchPromotions.TabIndex = 2;
            txtSearchPromotions.Text = "Search Promotions";
            txtSearchPromotions.TextChanged += txtSearchPromotions_TextChanged;
            // 
            // PromotionManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(962, 450);
            Controls.Add(txtSearchPromotions);
            Controls.Add(btnAddPromotion);
            Controls.Add(dgvPromotions);
            Name = "PromotionManager";
            Text = "PromotionManager";
            Load += PromotionManager_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPromotions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPromotions;
        private Button btnAddPromotion;
        private TextBox txtSearchPromotions;
    }
}