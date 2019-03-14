namespace Window
{
    partial class MainWindow
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
            this.KingdomPanel = new System.Windows.Forms.Panel();
            this.KingdomLabel = new System.Windows.Forms.Label();
            this.SetKingdom = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.Panel();
            this.StartGameBtn = new System.Windows.Forms.Button();
            this.GamePanel = new System.Windows.Forms.Panel();
            this.LogPanel = new System.Windows.Forms.Panel();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.LogLabel = new System.Windows.Forms.Label();
            this.PlayAreaPanel = new System.Windows.Forms.Panel();
            this.PhaseDescription = new System.Windows.Forms.Label();
            this.PhaseLabel = new System.Windows.Forms.Label();
            this.CoinLabel = new System.Windows.Forms.Label();
            this.BuyLabel = new System.Windows.Forms.Label();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.PlayAreaLabel = new System.Windows.Forms.Label();
            this.KingdomPanel.SuspendLayout();
            this.Header.SuspendLayout();
            this.GamePanel.SuspendLayout();
            this.LogPanel.SuspendLayout();
            this.PlayAreaPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // KingdomPanel
            // 
            this.KingdomPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.KingdomPanel.Controls.Add(this.KingdomLabel);
            this.KingdomPanel.Location = new System.Drawing.Point(2, 35);
            this.KingdomPanel.Margin = new System.Windows.Forms.Padding(2);
            this.KingdomPanel.Name = "KingdomPanel";
            this.KingdomPanel.Size = new System.Drawing.Size(150, 402);
            this.KingdomPanel.TabIndex = 0;
            // 
            // KingdomLabel
            // 
            this.KingdomLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KingdomLabel.AutoSize = true;
            this.KingdomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KingdomLabel.Location = new System.Drawing.Point(38, 8);
            this.KingdomLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.KingdomLabel.Name = "KingdomLabel";
            this.KingdomLabel.Size = new System.Drawing.Size(78, 20);
            this.KingdomLabel.TabIndex = 0;
            this.KingdomLabel.Text = "Kingdom";
            // 
            // SetKingdom
            // 
            this.SetKingdom.Location = new System.Drawing.Point(2, 2);
            this.SetKingdom.Margin = new System.Windows.Forms.Padding(2);
            this.SetKingdom.Name = "SetKingdom";
            this.SetKingdom.Size = new System.Drawing.Size(150, 24);
            this.SetKingdom.TabIndex = 1;
            this.SetKingdom.Text = "Set Kingdom";
            this.SetKingdom.UseVisualStyleBackColor = true;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Header.Controls.Add(this.StartGameBtn);
            this.Header.Controls.Add(this.SetKingdom);
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Margin = new System.Windows.Forms.Padding(2);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(704, 30);
            this.Header.TabIndex = 2;
            // 
            // StartGameBtn
            // 
            this.StartGameBtn.Location = new System.Drawing.Point(157, 2);
            this.StartGameBtn.Margin = new System.Windows.Forms.Padding(2);
            this.StartGameBtn.Name = "StartGameBtn";
            this.StartGameBtn.Size = new System.Drawing.Size(150, 24);
            this.StartGameBtn.TabIndex = 2;
            this.StartGameBtn.Text = "Play";
            this.StartGameBtn.UseVisualStyleBackColor = true;
            this.StartGameBtn.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // GamePanel
            // 
            this.GamePanel.Controls.Add(this.LogPanel);
            this.GamePanel.Controls.Add(this.PlayAreaPanel);
            this.GamePanel.Controls.Add(this.PhaseDescription);
            this.GamePanel.Controls.Add(this.PhaseLabel);
            this.GamePanel.Controls.Add(this.CoinLabel);
            this.GamePanel.Controls.Add(this.BuyLabel);
            this.GamePanel.Controls.Add(this.ActionLabel);
            this.GamePanel.Location = new System.Drawing.Point(157, 35);
            this.GamePanel.Margin = new System.Windows.Forms.Padding(2);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(547, 405);
            this.GamePanel.TabIndex = 3;
            this.GamePanel.Visible = false;
            // 
            // LogPanel
            // 
            this.LogPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LogPanel.Controls.Add(this.LogTextBox);
            this.LogPanel.Controls.Add(this.LogLabel);
            this.LogPanel.Location = new System.Drawing.Point(295, 52);
            this.LogPanel.Margin = new System.Windows.Forms.Padding(2);
            this.LogPanel.Name = "LogPanel";
            this.LogPanel.Size = new System.Drawing.Size(250, 350);
            this.LogPanel.TabIndex = 7;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LogTextBox.Location = new System.Drawing.Point(6, 31);
            this.LogTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(238, 318);
            this.LogTextBox.TabIndex = 4;
            // 
            // LogLabel
            // 
            this.LogLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LogLabel.Location = new System.Drawing.Point(2, 8);
            this.LogLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LogLabel.Name = "LogLabel";
            this.LogLabel.Size = new System.Drawing.Size(245, 20);
            this.LogLabel.TabIndex = 3;
            this.LogLabel.Text = "Log";
            this.LogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayAreaPanel
            // 
            this.PlayAreaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayAreaPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PlayAreaPanel.Controls.Add(this.PlayAreaLabel);
            this.PlayAreaPanel.Location = new System.Drawing.Point(2, 52);
            this.PlayAreaPanel.Margin = new System.Windows.Forms.Padding(2);
            this.PlayAreaPanel.Name = "PlayAreaPanel";
            this.PlayAreaPanel.Size = new System.Drawing.Size(290, 350);
            this.PlayAreaPanel.TabIndex = 6;
            // 
            // PhaseDescription
            // 
            this.PhaseDescription.AutoSize = true;
            this.PhaseDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PhaseDescription.Location = new System.Drawing.Point(3, 31);
            this.PhaseDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PhaseDescription.Name = "PhaseDescription";
            this.PhaseDescription.Size = new System.Drawing.Size(79, 17);
            this.PhaseDescription.TabIndex = 5;
            this.PhaseDescription.Text = "Description";
            // 
            // PhaseLabel
            // 
            this.PhaseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PhaseLabel.AutoSize = true;
            this.PhaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PhaseLabel.Location = new System.Drawing.Point(2, 8);
            this.PhaseLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PhaseLabel.Name = "PhaseLabel";
            this.PhaseLabel.Size = new System.Drawing.Size(59, 20);
            this.PhaseLabel.TabIndex = 4;
            this.PhaseLabel.Text = "Phase";
            // 
            // CoinLabel
            // 
            this.CoinLabel.AutoSize = true;
            this.CoinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CoinLabel.Location = new System.Drawing.Point(484, 32);
            this.CoinLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CoinLabel.Name = "CoinLabel";
            this.CoinLabel.Size = new System.Drawing.Size(59, 17);
            this.CoinLabel.TabIndex = 2;
            this.CoinLabel.Text = "Coins: 0";
            // 
            // BuyLabel
            // 
            this.BuyLabel.AutoSize = true;
            this.BuyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BuyLabel.Location = new System.Drawing.Point(488, 16);
            this.BuyLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BuyLabel.Name = "BuyLabel";
            this.BuyLabel.Size = new System.Drawing.Size(55, 17);
            this.BuyLabel.TabIndex = 1;
            this.BuyLabel.Text = "Buys: 1";
            // 
            // ActionLabel
            // 
            this.ActionLabel.AutoSize = true;
            this.ActionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ActionLabel.Location = new System.Drawing.Point(475, 0);
            this.ActionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(70, 17);
            this.ActionLabel.TabIndex = 0;
            this.ActionLabel.Text = "Actions: 1";
            // 
            // PlayAreaLabel
            // 
            this.PlayAreaLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayAreaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlayAreaLabel.Location = new System.Drawing.Point(2, 8);
            this.PlayAreaLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PlayAreaLabel.Name = "PlayAreaLabel";
            this.PlayAreaLabel.Size = new System.Drawing.Size(286, 20);
            this.PlayAreaLabel.TabIndex = 3;
            this.PlayAreaLabel.Text = "Hand";
            this.PlayAreaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 442);
            this.Controls.Add(this.GamePanel);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.KingdomPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.Text = "Muj nazev aplikace";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KingdomPanel.ResumeLayout(false);
            this.KingdomPanel.PerformLayout();
            this.Header.ResumeLayout(false);
            this.GamePanel.ResumeLayout(false);
            this.GamePanel.PerformLayout();
            this.LogPanel.ResumeLayout(false);
            this.LogPanel.PerformLayout();
            this.PlayAreaPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel KingdomPanel;
        private System.Windows.Forms.Label KingdomLabel;
        private System.Windows.Forms.Button SetKingdom;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Button StartGameBtn;
        private System.Windows.Forms.Panel GamePanel;
        private System.Windows.Forms.Label CoinLabel;
        private System.Windows.Forms.Label BuyLabel;
        private System.Windows.Forms.Label ActionLabel;
        private System.Windows.Forms.Label PhaseLabel;
        private System.Windows.Forms.Label PhaseDescription;
        private System.Windows.Forms.Panel PlayAreaPanel;
        private System.Windows.Forms.Panel LogPanel;
        private System.Windows.Forms.Label LogLabel;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.Label PlayAreaLabel;
    }
}

