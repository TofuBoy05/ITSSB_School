namespace BellCroissantDesktop
{
    partial class ConflictResolutionWizard
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
            pnlStep1 = new Panel();
            lstConflicts = new ListBox();
            lblStep1Title = new Label();
            lblStep1Desc = new Label();
            pnlStep2 = new Panel();
            txtConflictDetails = new TextBox();
            lblStep2Title = new Label();
            lblStep2Desc = new Label();
            pnlTimeline = new Panel();
            pnlStep3 = new Panel();
            cmbPriority = new ComboBox();
            lblPriorityWarning = new Label();
            lblPriorityExplanation = new Label();
            lblStep3Title = new Label();
            lblCurrentPriority = new Label();
            btnNext = new Button();
            btnBack = new Button();
            btnFinish = new Button();
            btnCancel = new Button();
            lblStepIndicator = new Label();
            pnlStep1.SuspendLayout();
            pnlStep2.SuspendLayout();
            pnlStep3.SuspendLayout();
            SuspendLayout();
            // 
            // pnlStep1
            // 
            pnlStep1.Controls.Add(lstConflicts);
            pnlStep1.Controls.Add(lblStep1Desc);
            pnlStep1.Controls.Add(lblStep1Title);
            pnlStep1.Location = new Point(12, 40);
            pnlStep1.Name = "pnlStep1";
            pnlStep1.Size = new Size(760, 400);
            pnlStep1.TabIndex = 0;
            // 
            // lstConflicts
            // 
            lstConflicts.FormattingEnabled = true;
            lstConflicts.ItemHeight = 15;
            lstConflicts.Location = new Point(15, 70);
            lstConflicts.Name = "lstConflicts";
            lstConflicts.Size = new Size(730, 319);
            lstConflicts.TabIndex = 2;
            lstConflicts.SelectedIndexChanged += lstConflicts_SelectedIndexChanged;
            // 
            // lblStep1Title
            // 
            lblStep1Title.AutoSize = true;
            lblStep1Title.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStep1Title.Location = new Point(15, 10);
            lblStep1Title.Name = "lblStep1Title";
            lblStep1Title.Size = new Size(224, 25);
            lblStep1Title.TabIndex = 0;
            lblStep1Title.Text = "Step 1: List of Conflicts";
            // 
            // lblStep1Desc
            // 
            lblStep1Desc.AutoSize = true;
            lblStep1Desc.Location = new Point(15, 40);
            lblStep1Desc.Name = "lblStep1Desc";
            lblStep1Desc.Size = new Size(450, 15);
            lblStep1Desc.TabIndex = 1;
            lblStep1Desc.Text = "The following conflicts were detected. Select a conflict to view details and resolve.";
            // 
            // pnlStep2
            // 
            pnlStep2.Controls.Add(pnlTimeline);
            pnlStep2.Controls.Add(txtConflictDetails);
            pnlStep2.Controls.Add(lblStep2Desc);
            pnlStep2.Controls.Add(lblStep2Title);
            pnlStep2.Location = new Point(12, 40);
            pnlStep2.Name = "pnlStep2";
            pnlStep2.Size = new Size(760, 400);
            pnlStep2.TabIndex = 1;
            pnlStep2.Visible = false;
            // 
            // txtConflictDetails
            // 
            txtConflictDetails.Font = new Font("Consolas", 9F);
            txtConflictDetails.Location = new Point(15, 70);
            txtConflictDetails.Multiline = true;
            txtConflictDetails.Name = "txtConflictDetails";
            txtConflictDetails.ReadOnly = true;
            txtConflictDetails.ScrollBars = ScrollBars.Vertical;
            txtConflictDetails.Size = new Size(730, 200);
            txtConflictDetails.TabIndex = 3;
            // 
            // lblStep2Title
            // 
            lblStep2Title.AutoSize = true;
            lblStep2Title.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStep2Title.Location = new Point(15, 10);
            lblStep2Title.Name = "lblStep2Title";
            lblStep2Title.Size = new Size(283, 25);
            lblStep2Title.TabIndex = 0;
            lblStep2Title.Text = "Step 2: Conflict Notification";
            // 
            // lblStep2Desc
            // 
            lblStep2Desc.AutoSize = true;
            lblStep2Desc.Location = new Point(15, 40);
            lblStep2Desc.Name = "lblStep2Desc";
            lblStep2Desc.Size = new Size(480, 15);
            lblStep2Desc.TabIndex = 1;
            lblStep2Desc.Text = "Review the detailed explanation of the selected conflict, including overlapping dates and products.";
            // 
            // pnlTimeline
            // 
            pnlTimeline.BorderStyle = BorderStyle.FixedSingle;
            pnlTimeline.Location = new Point(15, 280);
            pnlTimeline.Name = "pnlTimeline";
            pnlTimeline.Size = new Size(730, 110);
            pnlTimeline.TabIndex = 4;
            pnlTimeline.Paint += pnlTimeline_Paint;
            // 
            // pnlStep3
            // 
            pnlStep3.Controls.Add(lblCurrentPriority);
            pnlStep3.Controls.Add(cmbPriority);
            pnlStep3.Controls.Add(lblPriorityWarning);
            pnlStep3.Controls.Add(lblPriorityExplanation);
            pnlStep3.Controls.Add(lblStep3Title);
            pnlStep3.Location = new Point(12, 40);
            pnlStep3.Name = "pnlStep3";
            pnlStep3.Size = new Size(760, 400);
            pnlStep3.TabIndex = 2;
            pnlStep3.Visible = false;
            // 
            // cmbPriority
            // 
            cmbPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPriority.FormattingEnabled = true;
            cmbPriority.Location = new Point(15, 180);
            cmbPriority.Name = "cmbPriority";
            cmbPriority.Size = new Size(300, 23);
            cmbPriority.TabIndex = 4;
            cmbPriority.SelectedIndexChanged += cmbPriority_SelectedIndexChanged;
            // 
            // lblPriorityWarning
            // 
            lblPriorityWarning.ForeColor = Color.OrangeRed;
            lblPriorityWarning.Location = new Point(15, 220);
            lblPriorityWarning.Name = "lblPriorityWarning";
            lblPriorityWarning.Size = new Size(730, 60);
            lblPriorityWarning.TabIndex = 3;
            lblPriorityWarning.Text = "";
            // 
            // lblPriorityExplanation
            // 
            lblPriorityExplanation.Location = new Point(15, 70);
            lblPriorityExplanation.Name = "lblPriorityExplanation";
            lblPriorityExplanation.Size = new Size(730, 80);
            lblPriorityExplanation.TabIndex = 1;
            lblPriorityExplanation.Text = "Priority determines which promotion is applied when multiple promotions are eligible for the same product. Higher priority promotions are applied first. However, changing priority alone will not resolve conflicts where products are active in multiple promotions with the same priority simultaneously. Consider adjusting date ranges or removing overlapping products instead.";
            // 
            // lblStep3Title
            // 
            lblStep3Title.AutoSize = true;
            lblStep3Title.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStep3Title.Location = new Point(15, 10);
            lblStep3Title.Name = "lblStep3Title";
            lblStep3Title.Size = new Size(234, 25);
            lblStep3Title.TabIndex = 0;
            lblStep3Title.Text = "Step 3: Priority Change";
            // 
            // lblCurrentPriority
            // 
            lblCurrentPriority.AutoSize = true;
            lblCurrentPriority.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCurrentPriority.Location = new Point(15, 155);
            lblCurrentPriority.Name = "lblCurrentPriority";
            lblCurrentPriority.Size = new Size(150, 19);
            lblCurrentPriority.TabIndex = 5;
            lblCurrentPriority.Text = "Current Priority: 0";
            // 
            // btnNext
            // 
            btnNext.Location = new Point(597, 450);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(85, 30);
            btnNext.TabIndex = 3;
            btnNext.Text = "Next >";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnBack
            // 
            btnBack.Enabled = false;
            btnBack.Location = new Point(506, 450);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(85, 30);
            btnBack.TabIndex = 4;
            btnBack.Text = "< Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnFinish
            // 
            btnFinish.Location = new Point(688, 450);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(85, 30);
            btnFinish.TabIndex = 5;
            btnFinish.Text = "Finish";
            btnFinish.UseVisualStyleBackColor = true;
            btnFinish.Visible = false;
            btnFinish.Click += btnFinish_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(12, 450);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 30);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblStepIndicator
            // 
            lblStepIndicator.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStepIndicator.Location = new Point(12, 9);
            lblStepIndicator.Name = "lblStepIndicator";
            lblStepIndicator.Size = new Size(760, 23);
            lblStepIndicator.TabIndex = 7;
            lblStepIndicator.Text = "Step 1 of 3";
            lblStepIndicator.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ConflictResolutionWizard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 491);
            Controls.Add(lblStepIndicator);
            Controls.Add(btnCancel);
            Controls.Add(btnFinish);
            Controls.Add(btnBack);
            Controls.Add(btnNext);
            Controls.Add(pnlStep1);
            Controls.Add(pnlStep2);
            Controls.Add(pnlStep3);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConflictResolutionWizard";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Promotion Conflict Resolution Wizard";
            pnlStep1.ResumeLayout(false);
            pnlStep1.PerformLayout();
            pnlStep2.ResumeLayout(false);
            pnlStep2.PerformLayout();
            pnlStep3.ResumeLayout(false);
            pnlStep3.PerformLayout();
            ResumeLayout(false);
        }

        private Panel pnlStep1;
        private Panel pnlStep2;
        private Panel pnlStep3;
        private Label lblStep1Title;
        private Label lblStep1Desc;
        private ListBox lstConflicts;
        private Label lblStep2Title;
        private Label lblStep2Desc;
        private TextBox txtConflictDetails;
        private Panel pnlTimeline;
        private Label lblStep3Title;
        private Label lblPriorityExplanation;
        private Label lblPriorityWarning;
        private ComboBox cmbPriority;
        private Label lblCurrentPriority;
        private Button btnNext;
        private Button btnBack;
        private Button btnFinish;
        private Button btnCancel;
        private Label lblStepIndicator;
    }
}
