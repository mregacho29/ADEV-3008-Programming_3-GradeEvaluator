namespace ADEV3008Windows
{
    partial class FileIOExample
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
            this.btnElementLambda = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnElements = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.rtxtData = new System.Windows.Forms.RichTextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnAttributes = new System.Windows.Forms.Button();
            this.btnHeader = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.btnError = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnElementLambda
            // 
            this.btnElementLambda.Location = new System.Drawing.Point(414, 117);
            this.btnElementLambda.Name = "btnElementLambda";
            this.btnElementLambda.Size = new System.Drawing.Size(131, 28);
            this.btnElementLambda.TabIndex = 20;
            this.btnElementLambda.Text = "Element Lambda";
            this.btnElementLambda.UseVisualStyleBackColor = true;
            this.btnElementLambda.Click += new System.EventHandler(this.btnElementLambda_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(414, 81);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(131, 28);
            this.btnFilter.TabIndex = 19;
            this.btnFilter.Text = "Filtered Elements";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnElements
            // 
            this.btnElements.Location = new System.Drawing.Point(255, 117);
            this.btnElements.Margin = new System.Windows.Forms.Padding(4);
            this.btnElements.Name = "btnElements";
            this.btnElements.Size = new System.Drawing.Size(131, 28);
            this.btnElements.TabIndex = 18;
            this.btnElements.Text = "All Elements";
            this.btnElements.UseVisualStyleBackColor = true;
            this.btnElements.Click += new System.EventHandler(this.btnElements_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(334, 409);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(131, 28);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rtxtData
            // 
            this.rtxtData.Location = new System.Drawing.Point(264, 197);
            this.rtxtData.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtData.Name = "rtxtData";
            this.rtxtData.Size = new System.Drawing.Size(269, 117);
            this.rtxtData.TabIndex = 16;
            this.rtxtData.Text = "";
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(334, 153);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(4);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(131, 28);
            this.btnWrite.TabIndex = 15;
            this.btnWrite.Text = "Write Data";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnAttributes
            // 
            this.btnAttributes.Location = new System.Drawing.Point(255, 81);
            this.btnAttributes.Margin = new System.Windows.Forms.Padding(4);
            this.btnAttributes.Name = "btnAttributes";
            this.btnAttributes.Size = new System.Drawing.Size(131, 28);
            this.btnAttributes.TabIndex = 14;
            this.btnAttributes.Text = "Attributes";
            this.btnAttributes.UseVisualStyleBackColor = true;
            this.btnAttributes.Click += new System.EventHandler(this.btnAttributes_Click);
            // 
            // btnHeader
            // 
            this.btnHeader.Location = new System.Drawing.Point(162, 346);
            this.btnHeader.Name = "btnHeader";
            this.btnHeader.Size = new System.Drawing.Size(131, 28);
            this.btnHeader.TabIndex = 21;
            this.btnHeader.Text = "Header";
            this.btnHeader.UseVisualStyleBackColor = true;
            this.btnHeader.Click += new System.EventHandler(this.btnHeader_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(334, 346);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(131, 28);
            this.btnDetails.TabIndex = 22;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // btnError
            // 
            this.btnError.Location = new System.Drawing.Point(506, 346);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(131, 28);
            this.btnError.TabIndex = 23;
            this.btnError.Text = "Error";
            this.btnError.UseVisualStyleBackColor = true;
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // FileIOExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.btnHeader);
            this.Controls.Add(this.btnElementLambda);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnElements);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.rtxtData);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnAttributes);
            this.Name = "FileIOExample";
            this.Text = "FileIOExample";
            this.Load += new System.EventHandler(this.FileIOExample_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnElementLambda;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnElements;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RichTextBox rtxtData;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnAttributes;
        private System.Windows.Forms.Button btnHeader;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Button btnError;
    }
}