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
            this.SetKingdomButton = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.Panel();
            this.StopButton = new System.Windows.Forms.Button();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.StartGameButton = new System.Windows.Forms.Button();
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
            this.SettingsPanel = new System.Windows.Forms.Panel();
            this.PlayerNameTextBox = new System.Windows.Forms.TextBox();
            this.PlayerNameLabel = new System.Windows.Forms.Label();
            this.ThreesRadio = new System.Windows.Forms.RadioButton();
            this.FivesRadio = new System.Windows.Forms.RadioButton();
            this.SelectAITypeLabel = new System.Windows.Forms.Label();
            this.TensRadio = new System.Windows.Forms.RadioButton();
            this.ExtensionsPanel = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            this.SettingsPanel.SuspendLayout();
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
            this.SetKingdomButton.Location = new System.Drawing.Point(239, 3);
            this.SetKingdomButton.Name = "SetKingdom";
            this.SetKingdomButton.Size = new System.Drawing.Size(230, 30);
            this.SetKingdomButton.TabIndex = 2;
            this.SetKingdomButton.Text = "Set Kingdom";
            this.SetKingdomButton.UseVisualStyleBackColor = true;
            this.SetKingdomButton.Click += new System.EventHandler(this.SetKingdom_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Header.Controls.Add(this.StopButton);
            this.Header.Controls.Add(this.SettingsButton);
            this.Header.Controls.Add(this.StartGameButton);
            this.Header.Controls.Add(this.SetKingdomButton);
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(938, 37);
            this.Header.TabIndex = 2;
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(711, 3);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(224, 30);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(475, 3);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(230, 30);
            this.SettingsButton.TabIndex = 3;
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.OpenSettings);
            // 
            // StartGameButton
            // 
            this.StartGameButton.Location = new System.Drawing.Point(3, 3);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(230, 30);
            this.StartGameButton.TabIndex = 1;
            this.StartGameButton.Text = "Start";
            this.StartGameButton.UseVisualStyleBackColor = true;
            this.StartGameButton.Click += new System.EventHandler(this.StartButton_Click);
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
            this.CurrentKingdomPanel.Location = new System.Drawing.Point(3, 5);
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
            // ExtensionsCardsPanel
            // 
            this.ExtensionsCardsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExtensionsCardsPanel.Controls.Add(this.ExtensionsCardsLabel);
            this.ExtensionsCardsPanel.Location = new System.Drawing.Point(205, 5);
            this.ExtensionsCardsPanel.Name = "ExtensionsCardsPanel";
            this.ExtensionsCardsPanel.Size = new System.Drawing.Size(529, 499);
            this.ExtensionsCardsPanel.TabIndex = 5;
            // 
            // ExtensionsCardsLabel
            // 
            this.ExtensionsCardsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtensionsCardsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ExtensionsCardsLabel.Location = new System.Drawing.Point(3, 8);
            this.ExtensionsCardsLabel.Name = "ExtensionsCardsLabel";
            this.ExtensionsCardsLabel.Size = new System.Drawing.Size(525, 25);
            this.ExtensionsCardsLabel.TabIndex = 2;
            this.ExtensionsCardsLabel.Text = "Base cards";
            this.ExtensionsCardsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SetKingdomPanel
            // 
            this.SetKingdomPanel.Controls.Add(this.SettingsPanel);
            this.SetKingdomPanel.Controls.Add(this.ExtensionsPanel);
            this.SetKingdomPanel.Controls.Add(this.CurrentKingdomPanel);
            this.SetKingdomPanel.Controls.Add(this.ExtensionsCardsPanel);
            this.SetKingdomPanel.Location = new System.Drawing.Point(0, 39);
            this.SetKingdomPanel.Name = "SetKingdomPanel";
            this.SetKingdomPanel.Size = new System.Drawing.Size(938, 507);
            this.SetKingdomPanel.TabIndex = 4;
            this.SetKingdomPanel.Visible = false;
            // 
            // SettingsPanel
            // 
            this.SettingsPanel.Controls.Add(this.PlayerNameTextBox);
            this.SettingsPanel.Controls.Add(this.PlayerNameLabel);
            this.SettingsPanel.Controls.Add(this.ThreesRadio);
            this.SettingsPanel.Controls.Add(this.FivesRadio);
            this.SettingsPanel.Controls.Add(this.SelectAITypeLabel);
            this.SettingsPanel.Controls.Add(this.TensRadio);
            this.SettingsPanel.Location = new System.Drawing.Point(3, 3);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Size = new System.Drawing.Size(938, 507);
            this.SettingsPanel.TabIndex = 7;
            this.SettingsPanel.Visible = false;
            // 
            // PlayerNameTextBox
            // 
            this.PlayerNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PlayerNameTextBox.Location = new System.Drawing.Point(472, 238);
            this.PlayerNameTextBox.Name = "PlayerNameTextBox";
            this.PlayerNameTextBox.Size = new System.Drawing.Size(214, 30);
            this.PlayerNameTextBox.TabIndex = 8;
            this.PlayerNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PlayerNameTextBox.TextChanged += new System.EventHandler(this.PlayerNameTextBox_TextChanged);
            // 
            // PlayerNameLabel
            // 
            this.PlayerNameLabel.AutoSize = true;
            this.PlayerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlayerNameLabel.Location = new System.Drawing.Point(263, 243);
            this.PlayerNameLabel.Name = "PlayerNameLabel";
            this.PlayerNameLabel.Size = new System.Drawing.Size(127, 25);
            this.PlayerNameLabel.TabIndex = 7;
            this.PlayerNameLabel.Text = "Player name:";
            // 
            // ThreesRadio
            // 
            this.ThreesRadio.AutoSize = true;
            this.ThreesRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ThreesRadio.Location = new System.Drawing.Point(762, 135);
            this.ThreesRadio.Name = "ThreesRadio";
            this.ThreesRadio.Size = new System.Drawing.Size(95, 29);
            this.ThreesRadio.TabIndex = 6;
            this.ThreesRadio.TabStop = true;
            this.ThreesRadio.Text = "Threes";
            this.ThreesRadio.UseVisualStyleBackColor = true;
            this.ThreesRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // FivesRadio
            // 
            this.FivesRadio.AutoSize = true;
            this.FivesRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FivesRadio.Location = new System.Drawing.Point(532, 135);
            this.FivesRadio.Name = "FivesRadio";
            this.FivesRadio.Size = new System.Drawing.Size(80, 29);
            this.FivesRadio.TabIndex = 5;
            this.FivesRadio.TabStop = true;
            this.FivesRadio.Text = "Fives";
            this.FivesRadio.UseVisualStyleBackColor = true;
            this.FivesRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // SelectAITypeLabel
            // 
            this.SelectAITypeLabel.AutoSize = true;
            this.SelectAITypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SelectAITypeLabel.Location = new System.Drawing.Point(41, 137);
            this.SelectAITypeLabel.Name = "SelectAITypeLabel";
            this.SelectAITypeLabel.Size = new System.Drawing.Size(139, 25);
            this.SelectAITypeLabel.TabIndex = 4;
            this.SelectAITypeLabel.Text = "Select AI type:";
            // 
            // TensRadio
            // 
            this.TensRadio.AutoSize = true;
            this.TensRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TensRadio.Location = new System.Drawing.Point(296, 135);
            this.TensRadio.Name = "TensRadio";
            this.TensRadio.Size = new System.Drawing.Size(78, 29);
            this.TensRadio.TabIndex = 1;
            this.TensRadio.TabStop = true;
            this.TensRadio.Text = "Tens";
            this.TensRadio.UseVisualStyleBackColor = true;
            this.TensRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // ExtensionsPanel
            // 
            this.ExtensionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExtensionsPanel.Controls.Add(this.button8);
            this.ExtensionsPanel.Controls.Add(this.button7);
            this.ExtensionsPanel.Controls.Add(this.button6);
            this.ExtensionsPanel.Controls.Add(this.button5);
            this.ExtensionsPanel.Controls.Add(this.button4);
            this.ExtensionsPanel.Controls.Add(this.button3);
            this.ExtensionsPanel.Controls.Add(this.button2);
            this.ExtensionsPanel.Controls.Add(this.button1);
            this.ExtensionsPanel.Controls.Add(this.ExtensionsLabel);
            this.ExtensionsPanel.Location = new System.Drawing.Point(739, 5);
            this.ExtensionsPanel.Name = "ExtensionsPanel";
            this.ExtensionsPanel.Size = new System.Drawing.Size(196, 500);
            this.ExtensionsPanel.TabIndex = 6;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(3, 44);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(188, 30);
            this.button8.TabIndex = 12;
            this.button8.Tag = "";
            this.button8.Text = "Precomputed";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.SetPrecomputedRandomGame);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(3, 294);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(188, 30);
            this.button7.TabIndex = 11;
            this.button7.Tag = "6";
            this.button7.Text = "Trash heap";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.SetPresetGame);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(3, 258);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(188, 30);
            this.button6.TabIndex = 10;
            this.button6.Tag = "5";
            this.button6.Text = "Village square";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.SetPresetGame);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(3, 222);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(188, 30);
            this.button5.TabIndex = 9;
            this.button5.Tag = "4";
            this.button5.Text = "Size distortion";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.SetPresetGame);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(3, 186);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(188, 30);
            this.button4.TabIndex = 8;
            this.button4.Tag = "3";
            this.button4.Text = "Interaction";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.SetPresetGame);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 150);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(188, 30);
            this.button3.TabIndex = 7;
            this.button3.Tag = "2";
            this.button3.Text = "Big money";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.SetPresetGame);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(188, 30);
            this.button2.TabIndex = 6;
            this.button2.Tag = "1";
            this.button2.Text = "First game";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SetPresetGame);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 30);
            this.button1.TabIndex = 5;
            this.button1.Tag = "";
            this.button1.Text = "Random";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SetRandomGame);
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
            this.ExtensionsLabel.Text = "Preset games";
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
            this.SettingsPanel.ResumeLayout(false);
            this.SettingsPanel.PerformLayout();
            this.ExtensionsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel KingdomPanel;
        private System.Windows.Forms.Label KingdomLabel;
        private System.Windows.Forms.Button SetKingdomButton;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Button StartGameButton;
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
        private System.Windows.Forms.Panel CurrentKingdomPanel;
        private System.Windows.Forms.Label CurrentKingdomLabel;
        private System.Windows.Forms.Panel ExtensionsCardsPanel;
        private System.Windows.Forms.Panel SetKingdomPanel;
        private System.Windows.Forms.Label ExtensionsCardsLabel;
        private System.Windows.Forms.Panel ExtensionsPanel;
        private System.Windows.Forms.Label ExtensionsLabel;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Panel SettingsPanel;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.RadioButton ThreesRadio;
        private System.Windows.Forms.RadioButton FivesRadio;
        private System.Windows.Forms.Label SelectAITypeLabel;
        private System.Windows.Forms.RadioButton TensRadio;
        private System.Windows.Forms.Label PlayerNameLabel;
        private System.Windows.Forms.TextBox PlayerNameTextBox;
    }
}

