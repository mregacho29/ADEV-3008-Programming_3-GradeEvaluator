namespace ADEV3008Windows
{
    partial class LinqExample
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
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLambda = new System.Windows.Forms.Button();
            this.btnLet = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this.btnWhere = new System.Windows.Forms.Button();
            this.lblValue = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnDatabase = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(639, 364);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(81, 36);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLambda
            // 
            this.btnLambda.Location = new System.Drawing.Point(429, 119);
            this.btnLambda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLambda.Name = "btnLambda";
            this.btnLambda.Size = new System.Drawing.Size(98, 36);
            this.btnLambda.TabIndex = 12;
            this.btnLambda.Text = "LAMBDA";
            this.btnLambda.UseVisualStyleBackColor = true;
            this.btnLambda.Click += new System.EventHandler(this.btnLambda_Click);
            // 
            // btnLet
            // 
            this.btnLet.Location = new System.Drawing.Point(429, 50);
            this.btnLet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLet.Name = "btnLet";
            this.btnLet.Size = new System.Drawing.Size(98, 36);
            this.btnLet.TabIndex = 11;
            this.btnLet.Text = "LET";
            this.btnLet.UseVisualStyleBackColor = true;
            this.btnLet.Click += new System.EventHandler(this.btnLet_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(285, 190);
            this.btnJoin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(103, 36);
            this.btnJoin.TabIndex = 10;
            this.btnJoin.Text = "JOIN";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // btnWhere
            // 
            this.btnWhere.Location = new System.Drawing.Point(285, 119);
            this.btnWhere.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWhere.Name = "btnWhere";
            this.btnWhere.Size = new System.Drawing.Size(98, 36);
            this.btnWhere.TabIndex = 9;
            this.btnWhere.Text = "WHERE";
            this.btnWhere.UseVisualStyleBackColor = true;
            this.btnWhere.Click += new System.EventHandler(this.btnWhere_Click);
            // 
            // lblValue
            // 
            this.lblValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblValue.Location = new System.Drawing.Point(201, 258);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(399, 142);
            this.lblValue.TabIndex = 8;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(285, 50);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(98, 36);
            this.btnSelect.TabIndex = 7;
            this.btnSelect.Text = "SELECT";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnDatabase
            // 
            this.btnDatabase.Location = new System.Drawing.Point(429, 190);
            this.btnDatabase.Name = "btnDatabase";
            this.btnDatabase.Size = new System.Drawing.Size(103, 36);
            this.btnDatabase.TabIndex = 14;
            this.btnDatabase.Text = "DATABASE";
            this.btnDatabase.UseVisualStyleBackColor = true;
            this.btnDatabase.Click += new System.EventHandler(this.btnDatabase_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(285, 50);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 36);
            this.button1.TabIndex = 7;
            this.button1.Text = "SELECT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(285, 119);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 36);
            this.button2.TabIndex = 9;
            this.button2.Text = "WHERE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnWhere_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(429, 50);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 36);
            this.button3.TabIndex = 11;
            this.button3.Text = "LET";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnLet_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(429, 119);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(103, 36);
            this.button4.TabIndex = 12;
            this.button4.Text = "LAMBDA";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnLambda_Click);
            // 
            // LinqExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDatabase);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnLambda);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnLet);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnWhere);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSelect);
            this.Name = "LinqExample";
            this.Text = "Linq Examples";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLambda;
        private System.Windows.Forms.Button btnLet;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Button btnWhere;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnDatabase;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}