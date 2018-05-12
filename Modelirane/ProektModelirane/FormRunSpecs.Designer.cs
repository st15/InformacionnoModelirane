namespace ProektModelirane
{
    partial class FormRunSpecs
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.tbStep = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Step";
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(71, 26);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(98, 20);
            this.tbTime.TabIndex = 0;
            this.tbTime.Text = "10";
            // 
            // tbStep
            // 
            this.tbStep.Location = new System.Drawing.Point(71, 80);
            this.tbStep.Name = "tbStep";
            this.tbStep.Size = new System.Drawing.Size(98, 20);
            this.tbStep.TabIndex = 1;
            this.tbStep.Text = "2";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(33, 128);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(136, 38);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormRunSpecs
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 188);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbStep);
            this.Controls.Add(this.tbTime);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRunSpecs";
            this.ShowInTaskbar = false;
            this.Text = "Run Specs";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.TextBox tbStep;
        private System.Windows.Forms.Button btnOK;
    }
}