namespace Window
{
    partial class MyForm
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
            this.StartGame = new System.Windows.Forms.Button();
            this.GamePanel = new System.Windows.Forms.Panel();
            this.LogPanel = new System.Windows.Forms.Panel();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.LogLabel = new System.Windows.Forms.Label();
            this.PlayAreaPanel = new System.Windows.Forms.Panel();
            this.PlayAreaLabel = new System.Windows.Forms.Label();
            this.PhaseDescription = new System.Windows.Forms.Label();
            this.PhaseLabel = new System.Windows.Forms.Label();
            this.CoinLabel = new System.Windows.Forms.Label();
            this.BuyLabel = new System.Windows.Forms.Label();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.PhasePanel = new System.Windows.Forms.Panel();
            this.KingdomPanel.SuspendLayout();
            this.Header.SuspendLayout();
            this.GamePanel.SuspendLayout();
            this.LogPanel.SuspendLayout();
            this.PlayAreaPanel.SuspendLayout();
            this.PhasePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // KingdomPanel
            // 
            this.KingdomPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.KingdomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KingdomPanel.Controls.Add(this.KingdomLabel);
            this.KingdomPanel.Location = new System.Drawing.Point(3, 43);
            this.KingdomPanel.Name = "KingdomPanel";
            this.KingdomPanel.Size = new System.Drawing.Size(200, 495);
            this.KingdomPanel.TabIndex = 0;
            // 
            // KingdomLabel
            // 
            this.KingdomLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KingdomLabel.AutoSize = true;
            this.KingdomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KingdomLabel.Location = new System.Drawing.Point(51, 9);
            this.KingdomLabel.Name = "KingdomLabel";
            this.KingdomLabel.Size = new System.Drawing.Size(97, 25);
            this.KingdomLabel.TabIndex = 0;
            this.KingdomLabel.Text = "Kingdom";
            // 
            // SetKingdom
            // 
            this.SetKingdom.Location = new System.Drawing.Point(3, 4);
            this.SetKingdom.Name = "SetKingdom";
            this.SetKingdom.Size = new System.Drawing.Size(200, 30);
            this.SetKingdom.TabIndex = 1;
            this.SetKingdom.Text = "Set Kingdom";
            this.SetKingdom.UseVisualStyleBackColor = true;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Header.Controls.Add(this.StartGame);
            this.Header.Controls.Add(this.SetKingdom);
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(938, 37);
            this.Header.TabIndex = 2;
            // 
            // StartGame
            // 
            this.StartGame.Location = new System.Drawing.Point(209, 4);
            this.StartGame.Name = "StartGame";
            this.StartGame.Size = new System.Drawing.Size(200, 30);
            this.StartGame.TabIndex = 2;
            this.StartGame.Text = "Start";
            this.StartGame.UseVisualStyleBackColor = true;
            this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // GamePanel
            // 
            this.GamePanel.Controls.Add(this.PhasePanel);
            this.GamePanel.Controls.Add(this.LogPanel);
            this.GamePanel.Controls.Add(this.PlayAreaPanel);
            this.GamePanel.Location = new System.Drawing.Point(209, 43);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(729, 498);
            this.GamePanel.TabIndex = 3;
            this.GamePanel.Visible = false;
            // 
            // LogPanel
            // 
            this.LogPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LogPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogPanel.Controls.Add(this.LogTextBox);
            this.LogPanel.Controls.Add(this.LogLabel);
            this.LogPanel.Location = new System.Drawing.Point(393, 74);
            this.LogPanel.Name = "LogPanel";
            this.LogPanel.Size = new System.Drawing.Size(333, 421);
            this.LogPanel.TabIndex = 7;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LogTextBox.Location = new System.Drawing.Point(8, 38);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(316, 390);
            this.LogTextBox.TabIndex = 4;
            // 
            // LogLabel
            // 
            this.LogLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LogLabel.Location = new System.Drawing.Point(3, 10);
            this.LogLabel.Name = "LogLabel";
            this.LogLabel.Size = new System.Drawing.Size(325, 25);
            this.LogLabel.TabIndex = 3;
            this.LogLabel.Text = "Log";
            this.LogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayAreaPanel
            // 
            this.PlayAreaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayAreaPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PlayAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayAreaPanel.Controls.Add(this.PlayAreaLabel);
            this.PlayAreaPanel.Location = new System.Drawing.Point(3, 74);
            this.PlayAreaPanel.Name = "PlayAreaPanel";
            this.PlayAreaPanel.Size = new System.Drawing.Size(387, 421);
            this.PlayAreaPanel.TabIndex = 6;
            // 
            // PlayAreaLabel
            // 
            this.PlayAreaLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayAreaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlayAreaLabel.Location = new System.Drawing.Point(3, 10);
            this.PlayAreaLabel.Name = "PlayAreaLabel";
            this.PlayAreaLabel.Size = new System.Drawing.Size(379, 25);
            this.PlayAreaLabel.TabIndex = 3;
            this.PlayAreaLabel.Text = "Hand";
            this.PlayAreaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PhaseDescription
            // 
            this.PhaseDescription.AutoSize = true;
            this.PhaseDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PhaseDescription.Location = new System.Drawing.Point(4, 34);
            this.PhaseDescription.Name = "PhaseDescription";
            this.PhaseDescription.Size = new System.Drawing.Size(95, 20);
            this.PhaseDescription.TabIndex = 5;
            this.PhaseDescription.Text = "Description";
            // 
            // PhaseLabel
            // 
            this.PhaseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PhaseLabel.AutoSize = true;
            this.PhaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PhaseLabel.Location = new System.Drawing.Point(3, 9);
            this.PhaseLabel.Name = "PhaseLabel";
            this.PhaseLabel.Size = new System.Drawing.Size(73, 25);
            this.PhaseLabel.TabIndex = 4;
            this.PhaseLabel.Text = "Phase";
            // 
            // CoinLabel
            // 
            this.CoinLabel.AutoSize = true;
            this.CoinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CoinLabel.Location = new System.Drawing.Point(643, 45);
            this.CoinLabel.Name = "CoinLabel";
            this.CoinLabel.Size = new System.Drawing.Size(71, 20);
            this.CoinLabel.TabIndex = 2;
            this.CoinLabel.Text = "Coins: 0";
            // 
            // BuyLabel
            // 
            this.BuyLabel.AutoSize = true;
            this.BuyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BuyLabel.Location = new System.Drawing.Point(648, 25);
            this.BuyLabel.Name = "BuyLabel";
            this.BuyLabel.Size = new System.Drawing.Size(66, 20);
            this.BuyLabel.TabIndex = 1;
            this.BuyLabel.Text = "Buys: 1";
            // 
            // ActionLabel
            // 
            this.ActionLabel.AutoSize = true;
            this.ActionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ActionLabel.Location = new System.Drawing.Point(630, 5);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(84, 20);
            this.ActionLabel.TabIndex = 0;
            this.ActionLabel.Text = "Actions: 1";
            // 
            // PhasePanel
            // 
            this.PhasePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PhasePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PhasePanel.Controls.Add(this.BuyLabel);
            this.PhasePanel.Controls.Add(this.PhaseLabel);
            this.PhasePanel.Controls.Add(this.PhaseDescription);
            this.PhasePanel.Controls.Add(this.CoinLabel);
            this.PhasePanel.Controls.Add(this.ActionLabel);
            this.PhasePanel.Location = new System.Drawing.Point(3, 0);
            this.PhasePanel.Name = "PhasePanel";
            this.PhasePanel.Size = new System.Drawing.Size(723, 71);
            this.PhasePanel.TabIndex = 8;
            // 
            // MyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 544);
            this.Controls.Add(this.GamePanel);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.KingdomPanel);
            this.Name = "MyForm";
            this.Text = "Muj nazev aplikace";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KingdomPanel.ResumeLayout(false);
            this.KingdomPanel.PerformLayout();
            this.Header.ResumeLayout(false);
            this.GamePanel.ResumeLayout(false);
            this.LogPanel.ResumeLayout(false);
            this.LogPanel.PerformLayout();
            this.PlayAreaPanel.ResumeLayout(false);
            this.PhasePanel.ResumeLayout(false);
            this.PhasePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel KingdomPanel;
        private System.Windows.Forms.Label KingdomLabel;
        private System.Windows.Forms.Button SetKingdom;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Button StartGame;
        private System.Windows.Forms.Panel GamePanel;
        private System.Windows.Forms.Label PlayAreaLabel;
        private System.Windows.Forms.Label CoinLabel;
        private System.Windows.Forms.Label BuyLabel;
        private System.Windows.Forms.Label ActionLabel;
        private System.Windows.Forms.Label PhaseLabel;
        private System.Windows.Forms.Label PhaseDescription;
        private System.Windows.Forms.Panel PlayAreaPanel;
        private System.Windows.Forms.Panel LogPanel;
        private System.Windows.Forms.Label LogLabel;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.Panel PhasePanel;
    }
}

