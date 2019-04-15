namespace Window
{
    partial class SettingsForm
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
            this.Header = new System.Windows.Forms.Panel();
            this.DominexButton = new System.Windows.Forms.Button();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.ProvincialButton = new System.Windows.Forms.Button();
            this.ProvincialPanel = new System.Windows.Forms.Panel();
            this.ProvincialRun = new System.Windows.Forms.Button();
            this.Header.SuspendLayout();
            this.ProvincialPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Header.Controls.Add(this.DominexButton);
            this.Header.Controls.Add(this.ReturnButton);
            this.Header.Controls.Add(this.ProvincialButton);
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(938, 37);
            this.Header.TabIndex = 3;
            // 
            // DominexButton
            // 
            this.DominexButton.Location = new System.Drawing.Point(415, 3);
            this.DominexButton.Name = "DominexButton";
            this.DominexButton.Size = new System.Drawing.Size(200, 30);
            this.DominexButton.TabIndex = 3;
            this.DominexButton.Text = "Dominex";
            this.DominexButton.UseVisualStyleBackColor = true;
            // 
            // ReturnButton
            // 
            this.ReturnButton.Location = new System.Drawing.Point(3, 3);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(200, 30);
            this.ReturnButton.TabIndex = 1;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.Return);
            // 
            // ProvincialButton
            // 
            this.ProvincialButton.Location = new System.Drawing.Point(209, 3);
            this.ProvincialButton.Name = "ProvincialButton";
            this.ProvincialButton.Size = new System.Drawing.Size(200, 30);
            this.ProvincialButton.TabIndex = 2;
            this.ProvincialButton.Text = "Provincial";
            this.ProvincialButton.UseVisualStyleBackColor = true;
            this.ProvincialButton.Click += new System.EventHandler(this.ProvincialShow);
            // 
            // ProvincialPanel
            // 
            this.ProvincialPanel.Controls.Add(this.ProvincialRun);
            this.ProvincialPanel.Location = new System.Drawing.Point(0, 39);
            this.ProvincialPanel.Name = "ProvincialPanel";
            this.ProvincialPanel.Size = new System.Drawing.Size(938, 413);
            this.ProvincialPanel.TabIndex = 4;
            // 
            // ProvincialRun
            // 
            this.ProvincialRun.Location = new System.Drawing.Point(726, 369);
            this.ProvincialRun.Name = "ProvincialRun";
            this.ProvincialRun.Size = new System.Drawing.Size(200, 30);
            this.ProvincialRun.TabIndex = 4;
            this.ProvincialRun.Text = "Run";
            this.ProvincialRun.UseVisualStyleBackColor = true;
            this.ProvincialRun.Click += new System.EventHandler(this.Run);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 450);
            this.Controls.Add(this.ProvincialPanel);
            this.Controls.Add(this.Header);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Header.ResumeLayout(false);
            this.ProvincialPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Button DominexButton;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Button ProvincialButton;
        private System.Windows.Forms.Panel ProvincialPanel;
        private System.Windows.Forms.Button ProvincialRun;
    }
}