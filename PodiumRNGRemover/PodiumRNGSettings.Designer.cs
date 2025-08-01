namespace PodiumRNGRemover
{
    partial class PodiumRNGSettings
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblKey1 = new System.Windows.Forms.Label();
            this.lblKey2 = new System.Windows.Forms.Label();
            this.lblKey3 = new System.Windows.Forms.Label();
            this.lblKey4 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCredit = new System.Windows.Forms.Label();
            this.txtKey1 = new System.Windows.Forms.TextBox();
            this.txtKey2 = new System.Windows.Forms.TextBox();
            this.txtKey3 = new System.Windows.Forms.TextBox();
            this.txtKey4 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 20);
            this.lblTitle.Text = "Podium RNG Remover Settings";
            
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(15, 40);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(250, 13);
            this.lblInfo.Text = "Key 1: No deduction | Key 2: -2.3s | Key 3: -3.3s";
            
            this.lblKey1.AutoSize = true;
            this.lblKey1.Location = new System.Drawing.Point(15, 70);
            this.lblKey1.Name = "lblKey1";
            this.lblKey1.Size = new System.Drawing.Size(120, 13);
            this.lblKey1.Text = "Key 1 (No deduction):";
            
            this.txtKey1.Location = new System.Drawing.Point(140, 67);
            this.txtKey1.Name = "txtKey1";
            this.txtKey1.Size = new System.Drawing.Size(100, 20);
            
            this.lblKey2.AutoSize = true;
            this.lblKey2.Location = new System.Drawing.Point(15, 100);
            this.lblKey2.Name = "lblKey2";
            this.lblKey2.Size = new System.Drawing.Size(100, 13);
            this.lblKey2.Text = "Key 2 (-2.3s):";
            
            this.txtKey2.Location = new System.Drawing.Point(140, 97);
            this.txtKey2.Name = "txtKey2";
            this.txtKey2.Size = new System.Drawing.Size(100, 20);
            
            this.lblKey3.AutoSize = true;
            this.lblKey3.Location = new System.Drawing.Point(15, 130);
            this.lblKey3.Name = "lblKey3";
            this.lblKey3.Size = new System.Drawing.Size(100, 13);
            this.lblKey3.Text = "Key 3 (-3.3s):";
            
            this.txtKey3.Location = new System.Drawing.Point(140, 127);
            this.txtKey3.Name = "txtKey3";
            this.txtKey3.Size = new System.Drawing.Size(100, 20);
            
            this.lblKey4.AutoSize = true;
            this.lblKey4.Location = new System.Drawing.Point(15, 160);
            this.lblKey4.Name = "lblKey4";
            this.lblKey4.Size = new System.Drawing.Size(100, 13);
            this.lblKey4.Text = "Key 4 (Cancel):";
            
            this.txtKey4.Location = new System.Drawing.Point(140, 157);
            this.txtKey4.Name = "txtKey4";
            this.txtKey4.Size = new System.Drawing.Size(100, 20);
            
            this.lblDescription.Location = new System.Drawing.Point(15, 200);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(270, 120);
            this.lblDescription.Text = "This LiveSplit extension allows you to deduct time from the timer " +
                "based on Crash Team Racing: Nitro-Fueled podium RNG." +
                "During the podium, press the configured keys to indicate " +
                "the podium RNG:\n\n" +
                "• Good Podium: No time deduction\n" +
                "• Medium Podium: -2.3 seconds\n" +
                "• Bad Podium: -3.3 seconds\n\n" +
                "Time will be deducted when you split or skip split. " +
                "You can cancel the selection with the Cancel key.";
            
            this.lblCredit.AutoSize = true;
            this.lblCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblCredit.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblCredit.Location = new System.Drawing.Point(220, 370);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(60, 13);
            this.lblCredit.Text = "By Jasu14";
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblKey1);
            this.Controls.Add(this.txtKey1);
            this.Controls.Add(this.lblKey2);
            this.Controls.Add(this.txtKey2);
            this.Controls.Add(this.lblKey3);
            this.Controls.Add(this.txtKey3);
            this.Controls.Add(this.lblKey4);
            this.Controls.Add(this.txtKey4);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCredit);
            this.Name = "PodiumRNGSettings";
            this.Size = new System.Drawing.Size(350, 400);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}