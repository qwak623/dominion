namespace Window
{
    partial class GameForm
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
            this.DuelButton = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.Button();
            this.StartGame = new System.Windows.Forms.Button();
            this.GamePanel = new System.Windows.Forms.Panel();
            this.PhasePanel = new System.Windows.Forms.Panel();
            this.BuyLabel = new System.Windows.Forms.Label();
            this.PhaseLabel = new System.Windows.Forms.Label();
            this.PhaseDescription = new System.Windows.Forms.Label();
            this.CoinLabel = new System.Windows.Forms.Label();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.LogPanel = new System.Windows.Forms.Panel();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.LogLabel = new System.Windows.Forms.Label();
            this.PlayAreaPanel = new System.Windows.Forms.Panel();
            this.PlayAreaLabel = new System.Windows.Forms.Label();
            this.CurrentKingdomPanel = new System.Windows.Forms.Panel();
            this.CurrentKingdomLabel = new System.Windows.Forms.Label();
            this.ExtensionsCardsPanel = new System.Windows.Forms.Panel();
            this.ExtensionsCardsLabel = new System.Windows.Forms.Label();
            this.SetKingdomPanel = new System.Windows.Forms.Panel();
            this.ExtensionsPanel = new System.Windows.Forms.Panel();
            this.ExtensionsLabel = new System.Windows.Forms.Label();
            this.KingdomPanel.SuspendLayout();
            this.Header.SuspendLayout();
            this.GamePanel.SuspendLayout();
            this.PhasePanel.SuspendLayout();
            this.LogPanel.SuspendLayout();
            this.PlayAreaPanel.SuspendLayout();
            this.CurrentKingdomPanel.SuspendLayout();
            this.ExtensionsCardsPanel.SuspendLayout();
            this.SetKingdomPanel.SuspendLayout();
            this.ExtensionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // KingdomPanel
            // 
            this.KingdomPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.KingdomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KingdomPanel.Controls.Add(this.KingdomLabel);
            this.KingdomPanel.Location = new System.Drawing.Point(3, 0);
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
            this.SetKingdom.Location = new System.Drawing.Point(209, 3);
            this.SetKingdom.Name = "SetKingdom";
            this.SetKingdom.Size = new System.Drawing.Size(200, 30);
            this.SetKingdom.TabIndex = 2;
            this.SetKingdom.Text = "Set Kingdom";
            this.SetKingdom.UseVisualStyleBackColor = true;
            this.SetKingdom.Click += new System.EventHandler(this.SetKingdom_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Header.Controls.Add(this.DuelButton);
            this.Header.Controls.Add(this.Settings);
            this.Header.Controls.Add(this.StartGame);
            this.Header.Controls.Add(this.SetKingdom);
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(938, 37);
            this.Header.TabIndex = 2;
            // 
            // DuelButton
            // 
            this.DuelButton.Location = new System.Drawing.Point(735, 4);
            this.DuelButton.Name = "DuelButton";
            this.DuelButton.Size = new System.Drawing.Size(200, 30);
            this.DuelButton.TabIndex = 4;
            this.DuelButton.Text = "Duel";
            this.DuelButton.UseVisualStyleBackColor = true;
            this.DuelButton.Click += new System.EventHandler(this.Duel_Click);
            // 
            // Settings
            // 
            this.Settings.Location = new System.Drawing.Point(415, 3);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(200, 30);
            this.Settings.TabIndex = 3;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // StartGame
            // 
            this.StartGame.Location = new System.Drawing.Point(3, 3);
            this.StartGame.Name = "StartGame";
            this.StartGame.Size = new System.Drawing.Size(200, 30);
            this.StartGame.TabIndex = 1;
            this.StartGame.Text = "Start";
            this.StartGame.UseVisualStyleBackColor = true;
            this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // GamePanel
            // 
            this.GamePanel.Controls.Add(this.PhasePanel);
            this.GamePanel.Controls.Add(this.LogPanel);
            this.GamePanel.Controls.Add(this.KingdomPanel);
            this.GamePanel.Controls.Add(this.PlayAreaPanel);
            this.GamePanel.Location = new System.Drawing.Point(0, 43);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(938, 498);
            this.GamePanel.TabIndex = 3;
            this.GamePanel.Visible = false;
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
            this.PhasePanel.Location = new System.Drawing.Point(209, 0);
            this.PhasePanel.Name = "PhasePanel";
            this.PhasePanel.Size = new System.Drawing.Size(726, 71);
            this.PhasePanel.TabIndex = 8;
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
            // LogPanel
            // 
            this.LogPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LogPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogPanel.Controls.Add(this.LogTextBox);
            this.LogPanel.Controls.Add(this.LogLabel);
            this.LogPanel.Location = new System.Drawing.Point(601, 74);
            this.LogPanel.Name = "LogPanel";
            this.LogPanel.Size = new System.Drawing.Size(334, 421);
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
            this.LogLabel.Size = new System.Drawing.Size(326, 25);
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
            this.PlayAreaPanel.Location = new System.Drawing.Point(209, 74);
            this.PlayAreaPanel.Name = "PlayAreaPanel";
            this.PlayAreaPanel.Size = new System.Drawing.Size(390, 421);
            this.PlayAreaPanel.TabIndex = 0;
            // 
            // PlayAreaLabel
            // 
            this.PlayAreaLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayAreaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlayAreaLabel.Location = new System.Drawing.Point(3, 10);
            this.PlayAreaLabel.Name = "PlayAreaLabel";
            this.PlayAreaLabel.Size = new System.Drawing.Size(382, 25);
            this.PlayAreaLabel.TabIndex = 3;
            this.PlayAreaLabel.Text = "Hand";
            this.PlayAreaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurrentKingdomPanel
            // 
            this.CurrentKingdomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentKingdomPanel.Controls.Add(this.CurrentKingdomLabel);
            this.CurrentKingdomPanel.Location = new System.Drawing.Point(3, 4);
            this.CurrentKingdomPanel.Name = "CurrentKingdomPanel";
            this.CurrentKingdomPanel.Size = new System.Drawing.Size(196, 500);
            this.CurrentKingdomPanel.TabIndex = 4;
            // 
            // CurrentKingdomLabel
            // 
            this.CurrentKingdomLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentKingdomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CurrentKingdomLabel.Location = new System.Drawing.Point(0, 10);
            this.CurrentKingdomLabel.Name = "CurrentKingdomLabel";
            this.CurrentKingdomLabel.Size = new System.Drawing.Size(194, 25);
            this.CurrentKingdomLabel.TabIndex = 1;
            this.CurrentKingdomLabel.Text = "Kingdom";
            this.CurrentKingdomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DifferentCardsPanel
            // 
            this.ExtensionsCardsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExtensionsCardsPanel.Controls.Add(this.ExtensionsCardsLabel);
            this.ExtensionsCardsPanel.Location = new System.Drawing.Point(205, 5);
            this.ExtensionsCardsPanel.Name = "DifferentCardsPanel";
            this.ExtensionsCardsPanel.Size = new System.Drawing.Size(529, 499);
            this.ExtensionsCardsPanel.TabIndex = 5;
            // 
            // DifferentCardsLabel
            // 
            this.ExtensionsCardsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtensionsCardsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ExtensionsCardsLabel.Location = new System.Drawing.Point(3, 8);
            this.ExtensionsCardsLabel.Name = "DifferentCardsLabel";
            this.ExtensionsCardsLabel.Size = new System.Drawing.Size(525, 25);
            this.ExtensionsCardsLabel.TabIndex = 2;
            this.ExtensionsCardsLabel.Text = "Base cards";
            this.ExtensionsCardsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SetKingdomPanel
            // 
            this.SetKingdomPanel.Controls.Add(this.ExtensionsPanel);
            this.SetKingdomPanel.Controls.Add(this.CurrentKingdomPanel);
            this.SetKingdomPanel.Controls.Add(this.ExtensionsCardsPanel);
            this.SetKingdomPanel.Location = new System.Drawing.Point(0, 39);
            this.SetKingdomPanel.Name = "SetKingdomPanel";
            this.SetKingdomPanel.Size = new System.Drawing.Size(938, 507);
            this.SetKingdomPanel.TabIndex = 4;
            this.SetKingdomPanel.Visible = false;
            // 
            // ExtensionsPanel
            // 
            this.ExtensionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExtensionsPanel.Controls.Add(this.ExtensionsLabel);
            this.ExtensionsPanel.Location = new System.Drawing.Point(739, 5);
            this.ExtensionsPanel.Name = "ExtensionsPanel";
            this.ExtensionsPanel.Size = new System.Drawing.Size(196, 500);
            this.ExtensionsPanel.TabIndex = 6;
            // 
            // ExtensionsLabel
            // 
            this.ExtensionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtensionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ExtensionsLabel.Location = new System.Drawing.Point(0, 10);
            this.ExtensionsLabel.Name = "ExtensionsLabel";
            this.ExtensionsLabel.Size = new System.Drawing.Size(194, 25);
            this.ExtensionsLabel.TabIndex = 1;
            this.ExtensionsLabel.Text = "Extensions";
            this.ExtensionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 544);
            this.Controls.Add(this.SetKingdomPanel);
            this.Controls.Add(this.GamePanel);
            this.Controls.Add(this.Header);
            this.Name = "GameForm";
            this.Text = "Dominex";
            this.KingdomPanel.ResumeLayout(false);
            this.KingdomPanel.PerformLayout();
            this.Header.ResumeLayout(false);
            this.GamePanel.ResumeLayout(false);
            this.PhasePanel.ResumeLayout(false);
            this.PhasePanel.PerformLayout();
            this.LogPanel.ResumeLayout(false);
            this.LogPanel.PerformLayout();
            this.PlayAreaPanel.ResumeLayout(false);
            this.CurrentKingdomPanel.ResumeLayout(false);
            this.ExtensionsCardsPanel.ResumeLayout(false);
            this.SetKingdomPanel.ResumeLayout(false);
            this.ExtensionsPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Panel CurrentKingdomPanel;
        private System.Windows.Forms.Label CurrentKingdomLabel;
        private System.Windows.Forms.Panel ExtensionsCardsPanel;
        private System.Windows.Forms.Panel SetKingdomPanel;
        private System.Windows.Forms.Button DuelButton;
        private System.Windows.Forms.Label ExtensionsCardsLabel;
        private System.Windows.Forms.Panel ExtensionsPanel;
        private System.Windows.Forms.Label ExtensionsLabel;
    }
}

