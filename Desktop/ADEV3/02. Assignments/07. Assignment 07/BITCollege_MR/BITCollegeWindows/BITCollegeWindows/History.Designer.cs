namespace BITCollegeWindows
{
    partial class History
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label studentNumberLabel;
            System.Windows.Forms.Label fullNameLabel;
            System.Windows.Forms.Label descriptionLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.descriptionLabels = new System.Windows.Forms.Label();
            this.studentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fullNameLabels = new System.Windows.Forms.Label();
            this.studentNumberMaskedLabel = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.lnkReturn = new System.Windows.Forms.LinkLabel();
            this.registrationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.registrationDataGridView = new System.Windows.Forms.DataGridView();
            this.RegistrationNumberDataGridView = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateDataGridView = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CourseDataGridView = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GradeDataGridView = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotesDataGridView = new System.Windows.Forms.DataGridViewTextBoxColumn();
            studentNumberLabel = new System.Windows.Forms.Label();
            fullNameLabel = new System.Windows.Forms.Label();
            descriptionLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.registrationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // studentNumberLabel
            // 
            studentNumberLabel.AutoSize = true;
            studentNumberLabel.Location = new System.Drawing.Point(43, 36);
            studentNumberLabel.Name = "studentNumberLabel";
            studentNumberLabel.Size = new System.Drawing.Size(106, 16);
            studentNumberLabel.TabIndex = 0;
            studentNumberLabel.Text = "Student Number:";
            // 
            // fullNameLabel
            // 
            fullNameLabel.AutoSize = true;
            fullNameLabel.Location = new System.Drawing.Point(394, 35);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new System.Drawing.Size(71, 16);
            fullNameLabel.TabIndex = 2;
            fullNameLabel.Text = "Full Name:";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new System.Drawing.Point(43, 81);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(62, 16);
            descriptionLabel.TabIndex = 4;
            descriptionLabel.Text = "Program:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(descriptionLabel);
            this.groupBox1.Controls.Add(this.descriptionLabels);
            this.groupBox1.Controls.Add(fullNameLabel);
            this.groupBox1.Controls.Add(this.fullNameLabels);
            this.groupBox1.Controls.Add(studentNumberLabel);
            this.groupBox1.Controls.Add(this.studentNumberMaskedLabel);
            this.groupBox1.Location = new System.Drawing.Point(49, 38);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(865, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Student Data";
            // 
            // descriptionLabels
            // 
            this.descriptionLabels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionLabels.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "AcademicProgram.Description", true));
            this.descriptionLabels.Location = new System.Drawing.Point(185, 81);
            this.descriptionLabels.Name = "descriptionLabels";
            this.descriptionLabels.Size = new System.Drawing.Size(594, 23);
            this.descriptionLabels.TabIndex = 5;
            // 
            // studentBindingSource
            // 
            this.studentBindingSource.DataSource = typeof(BITCollege_MR.Models.Student);
            // 
            // fullNameLabels
            // 
            this.fullNameLabels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fullNameLabels.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "FullName", true));
            this.fullNameLabels.Location = new System.Drawing.Point(506, 35);
            this.fullNameLabels.Name = "fullNameLabels";
            this.fullNameLabels.Size = new System.Drawing.Size(295, 23);
            this.fullNameLabels.TabIndex = 3;
            // 
            // studentNumberMaskedLabel
            // 
            this.studentNumberMaskedLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.studentNumberMaskedLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentBindingSource, "StudentNumber", true));
            this.studentNumberMaskedLabel.Location = new System.Drawing.Point(185, 36);
            this.studentNumberMaskedLabel.Mask = "0000-0000";
            this.studentNumberMaskedLabel.Name = "studentNumberMaskedLabel";
            this.studentNumberMaskedLabel.Size = new System.Drawing.Size(148, 23);
            this.studentNumberMaskedLabel.TabIndex = 1;
            this.studentNumberMaskedLabel.Text = "    -";
            // 
            // lnkReturn
            // 
            this.lnkReturn.AutoSize = true;
            this.lnkReturn.Location = new System.Drawing.Point(404, 508);
            this.lnkReturn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkReturn.Name = "lnkReturn";
            this.lnkReturn.Size = new System.Drawing.Size(140, 16);
            this.lnkReturn.TabIndex = 1;
            this.lnkReturn.TabStop = true;
            this.lnkReturn.Text = "Return to Student Data";
            this.lnkReturn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReturn_LinkClicked);
            // 
            // registrationBindingSource
            // 
            this.registrationBindingSource.DataSource = typeof(BITCollege_MR.Models.Registration);
            // 
            // registrationDataGridView
            // 
            this.registrationDataGridView.AllowUserToOrderColumns = true;
            this.registrationDataGridView.AutoGenerateColumns = false;
            this.registrationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.registrationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RegistrationNumberDataGridView,
            this.DateDataGridView,
            this.CourseDataGridView,
            this.GradeDataGridView,
            this.NotesDataGridView});
            this.registrationDataGridView.DataSource = this.registrationBindingSource;
            this.registrationDataGridView.Location = new System.Drawing.Point(49, 226);
            this.registrationDataGridView.Name = "registrationDataGridView";
            this.registrationDataGridView.RowHeadersWidth = 51;
            this.registrationDataGridView.RowTemplate.Height = 24;
            this.registrationDataGridView.Size = new System.Drawing.Size(865, 243);
            this.registrationDataGridView.TabIndex = 2;
            // 
            // RegistrationNumberDataGridView
            // 
            this.RegistrationNumberDataGridView.DataPropertyName = "RegistrationNumber";
            this.RegistrationNumberDataGridView.HeaderText = "Registration Number";
            this.RegistrationNumberDataGridView.MinimumWidth = 6;
            this.RegistrationNumberDataGridView.Name = "RegistrationNumberDataGridView";
            this.RegistrationNumberDataGridView.Width = 160;
            // 
            // DateDataGridView
            // 
            this.DateDataGridView.DataPropertyName = "RegistrationDate";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.DateDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.DateDataGridView.HeaderText = "Date";
            this.DateDataGridView.MinimumWidth = 6;
            this.DateDataGridView.Name = "DateDataGridView";
            this.DateDataGridView.Width = 165;
            // 
            // CourseDataGridView
            // 
            this.CourseDataGridView.DataPropertyName = "Title";
            this.CourseDataGridView.HeaderText = "Course";
            this.CourseDataGridView.MinimumWidth = 6;
            this.CourseDataGridView.Name = "CourseDataGridView";
            this.CourseDataGridView.Width = 220;
            // 
            // GradeDataGridView
            // 
            this.GradeDataGridView.DataPropertyName = "Grade";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "00.00%";
            dataGridViewCellStyle2.NullValue = null;
            this.GradeDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.GradeDataGridView.HeaderText = "Grade";
            this.GradeDataGridView.MinimumWidth = 6;
            this.GradeDataGridView.Name = "GradeDataGridView";
            this.GradeDataGridView.Width = 50;
            // 
            // NotesDataGridView
            // 
            this.NotesDataGridView.DataPropertyName = "Notes";
            this.NotesDataGridView.HeaderText = "Notes";
            this.NotesDataGridView.MinimumWidth = 6;
            this.NotesDataGridView.Name = "NotesDataGridView";
            this.NotesDataGridView.Width = 220;
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 554);
            this.Controls.Add(this.registrationDataGridView);
            this.Controls.Add(this.lnkReturn);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "History";
            this.Text = "History";
            this.Load += new System.EventHandler(this.History_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.registrationDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkReturn;
        private System.Windows.Forms.BindingSource studentBindingSource;
        private EWSoftware.MaskedLabelControl.MaskedLabel studentNumberMaskedLabel;
        private System.Windows.Forms.Label fullNameLabels;
        private System.Windows.Forms.Label descriptionLabels;
        private System.Windows.Forms.BindingSource registrationBindingSource;
        private System.Windows.Forms.DataGridView registrationDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegistrationNumberDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn GradeDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotesDataGridView;
    }
}