namespace SHARModChecklist;

partial class FrmMain
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
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
        GBLevel = new GroupBox();
        CBLargeFont = new CheckBox();
        BtnLevel7 = new Button();
        CBDarkMode = new CheckBox();
        BtnLevel6 = new Button();
        BtnLevel5 = new Button();
        BtnLevel4 = new Button();
        BtnLevel3 = new Button();
        BtnLevel2 = new Button();
        BtnLevel1 = new Button();
        CBLevel = new ComboBox();
        GBStats = new GroupBox();
        SCGagsWasps = new SplitContainer();
        GBGags = new GroupBox();
        CLBGags = new CheckedListBox();
        GBWasps = new GroupBox();
        CLBWasps = new CheckedListBox();
        GBRewards = new GroupBox();
        CLBRewards = new CheckedListBox();
        GBCards = new GroupBox();
        CLBCards = new CheckedListBox();
        CMSCards = new ContextMenuStrip(components);
        TSMIShowCardLocationSpoilers = new ToolStripMenuItem();
        GBMissions = new GroupBox();
        LblBonusMission = new Label();
        LblMissions = new Label();
        LblStreetRaces = new Label();
        TmrUpdater = new System.Windows.Forms.Timer(components);
        GBLevel.SuspendLayout();
        GBStats.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)SCGagsWasps).BeginInit();
        SCGagsWasps.Panel1.SuspendLayout();
        SCGagsWasps.Panel2.SuspendLayout();
        SCGagsWasps.SuspendLayout();
        GBGags.SuspendLayout();
        GBWasps.SuspendLayout();
        GBRewards.SuspendLayout();
        GBCards.SuspendLayout();
        CMSCards.SuspendLayout();
        GBMissions.SuspendLayout();
        SuspendLayout();
        // 
        // GBLevel
        // 
        GBLevel.Controls.Add(CBLargeFont);
        GBLevel.Controls.Add(BtnLevel7);
        GBLevel.Controls.Add(CBDarkMode);
        GBLevel.Controls.Add(BtnLevel6);
        GBLevel.Controls.Add(BtnLevel5);
        GBLevel.Controls.Add(BtnLevel4);
        GBLevel.Controls.Add(BtnLevel3);
        GBLevel.Controls.Add(BtnLevel2);
        GBLevel.Controls.Add(BtnLevel1);
        GBLevel.Dock = DockStyle.Top;
        GBLevel.Location = new Point(4, 3);
        GBLevel.Margin = new Padding(4, 3, 4, 3);
        GBLevel.Name = "GBLevel";
        GBLevel.Padding = new Padding(4, 3, 4, 3);
        GBLevel.Size = new Size(727, 46);
        GBLevel.TabIndex = 99;
        GBLevel.TabStop = false;
        GBLevel.Text = "Level";
        // 
        // CBLargeFont
        // 
        CBLargeFont.AutoSize = true;
        CBLargeFont.Dock = DockStyle.Right;
        CBLargeFont.Location = new Point(557, 19);
        CBLargeFont.Name = "CBLargeFont";
        CBLargeFont.Size = new Size(82, 24);
        CBLargeFont.TabIndex = 100;
        CBLargeFont.Text = "Large Font";
        CBLargeFont.UseVisualStyleBackColor = true;
        CBLargeFont.CheckedChanged += CBTheming_CheckedChanged;
        // 
        // BtnLevel7
        // 
        BtnLevel7.Dock = DockStyle.Left;
        BtnLevel7.FlatStyle = FlatStyle.Flat;
        BtnLevel7.Location = new Point(454, 19);
        BtnLevel7.Name = "BtnLevel7";
        BtnLevel7.Size = new Size(75, 24);
        BtnLevel7.TabIndex = 7;
        BtnLevel7.Tag = "6";
        BtnLevel7.Text = "Level 7";
        BtnLevel7.UseVisualStyleBackColor = true;
        BtnLevel7.Click += BtnLevel_Click;
        // 
        // CBDarkMode
        // 
        CBDarkMode.AutoSize = true;
        CBDarkMode.Dock = DockStyle.Right;
        CBDarkMode.Location = new Point(639, 19);
        CBDarkMode.Name = "CBDarkMode";
        CBDarkMode.Size = new Size(84, 24);
        CBDarkMode.TabIndex = 2;
        CBDarkMode.Text = "Dark Mode";
        CBDarkMode.UseVisualStyleBackColor = true;
        CBDarkMode.CheckedChanged += CBTheming_CheckedChanged;
        // 
        // BtnLevel6
        // 
        BtnLevel6.Dock = DockStyle.Left;
        BtnLevel6.FlatStyle = FlatStyle.Flat;
        BtnLevel6.Location = new Point(379, 19);
        BtnLevel6.Name = "BtnLevel6";
        BtnLevel6.Size = new Size(75, 24);
        BtnLevel6.TabIndex = 6;
        BtnLevel6.Tag = "5";
        BtnLevel6.Text = "Level 6";
        BtnLevel6.UseVisualStyleBackColor = true;
        BtnLevel6.Click += BtnLevel_Click;
        // 
        // BtnLevel5
        // 
        BtnLevel5.Dock = DockStyle.Left;
        BtnLevel5.FlatStyle = FlatStyle.Flat;
        BtnLevel5.Location = new Point(304, 19);
        BtnLevel5.Name = "BtnLevel5";
        BtnLevel5.Size = new Size(75, 24);
        BtnLevel5.TabIndex = 5;
        BtnLevel5.Tag = "4";
        BtnLevel5.Text = "Level 5";
        BtnLevel5.UseVisualStyleBackColor = true;
        BtnLevel5.Click += BtnLevel_Click;
        // 
        // BtnLevel4
        // 
        BtnLevel4.Dock = DockStyle.Left;
        BtnLevel4.FlatStyle = FlatStyle.Flat;
        BtnLevel4.Location = new Point(229, 19);
        BtnLevel4.Name = "BtnLevel4";
        BtnLevel4.Size = new Size(75, 24);
        BtnLevel4.TabIndex = 4;
        BtnLevel4.Tag = "3";
        BtnLevel4.Text = "Level 4";
        BtnLevel4.UseVisualStyleBackColor = true;
        BtnLevel4.Click += BtnLevel_Click;
        // 
        // BtnLevel3
        // 
        BtnLevel3.Dock = DockStyle.Left;
        BtnLevel3.FlatStyle = FlatStyle.Flat;
        BtnLevel3.Location = new Point(154, 19);
        BtnLevel3.Name = "BtnLevel3";
        BtnLevel3.Size = new Size(75, 24);
        BtnLevel3.TabIndex = 3;
        BtnLevel3.Tag = "2";
        BtnLevel3.Text = "Level 3";
        BtnLevel3.UseVisualStyleBackColor = true;
        BtnLevel3.Click += BtnLevel_Click;
        // 
        // BtnLevel2
        // 
        BtnLevel2.Dock = DockStyle.Left;
        BtnLevel2.FlatStyle = FlatStyle.Flat;
        BtnLevel2.Location = new Point(79, 19);
        BtnLevel2.Name = "BtnLevel2";
        BtnLevel2.Size = new Size(75, 24);
        BtnLevel2.TabIndex = 2;
        BtnLevel2.Tag = "1";
        BtnLevel2.Text = "Level 2";
        BtnLevel2.UseVisualStyleBackColor = true;
        BtnLevel2.Click += BtnLevel_Click;
        // 
        // BtnLevel1
        // 
        BtnLevel1.Dock = DockStyle.Left;
        BtnLevel1.FlatStyle = FlatStyle.Flat;
        BtnLevel1.Location = new Point(4, 19);
        BtnLevel1.Name = "BtnLevel1";
        BtnLevel1.Size = new Size(75, 24);
        BtnLevel1.TabIndex = 1;
        BtnLevel1.Tag = "0";
        BtnLevel1.Text = "Level 1";
        BtnLevel1.UseVisualStyleBackColor = true;
        BtnLevel1.Click += BtnLevel_Click;
        // 
        // CBLevel
        // 
        CBLevel.Dock = DockStyle.Bottom;
        CBLevel.DropDownStyle = ComboBoxStyle.DropDownList;
        CBLevel.Enabled = false;
        CBLevel.FormattingEnabled = true;
        CBLevel.Items.AddRange(new object[] { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7" });
        CBLevel.Location = new Point(4, 493);
        CBLevel.Margin = new Padding(4, 3, 4, 3);
        CBLevel.Name = "CBLevel";
        CBLevel.Size = new Size(727, 23);
        CBLevel.TabIndex = 0;
        CBLevel.Visible = false;
        CBLevel.SelectedIndexChanged += CBLevel_SelectedIndexChanged;
        // 
        // GBStats
        // 
        GBStats.Controls.Add(SCGagsWasps);
        GBStats.Controls.Add(GBRewards);
        GBStats.Controls.Add(GBCards);
        GBStats.Controls.Add(GBMissions);
        GBStats.Dock = DockStyle.Fill;
        GBStats.Location = new Point(4, 49);
        GBStats.Margin = new Padding(4, 3, 4, 3);
        GBStats.Name = "GBStats";
        GBStats.Padding = new Padding(4, 3, 4, 3);
        GBStats.Size = new Size(727, 467);
        GBStats.TabIndex = 1;
        GBStats.TabStop = false;
        GBStats.Text = "Stats";
        // 
        // SCGagsWasps
        // 
        SCGagsWasps.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        SCGagsWasps.Location = new Point(247, 22);
        SCGagsWasps.Margin = new Padding(4, 3, 4, 3);
        SCGagsWasps.Name = "SCGagsWasps";
        // 
        // SCGagsWasps.Panel1
        // 
        SCGagsWasps.Panel1.Controls.Add(GBGags);
        // 
        // SCGagsWasps.Panel2
        // 
        SCGagsWasps.Panel2.Controls.Add(GBWasps);
        SCGagsWasps.Size = new Size(473, 438);
        SCGagsWasps.SplitterDistance = 234;
        SCGagsWasps.SplitterWidth = 5;
        SCGagsWasps.TabIndex = 8;
        // 
        // GBGags
        // 
        GBGags.BackColor = SystemColors.Window;
        GBGags.Controls.Add(CLBGags);
        GBGags.Dock = DockStyle.Fill;
        GBGags.Location = new Point(0, 0);
        GBGags.Margin = new Padding(4, 3, 4, 3);
        GBGags.Name = "GBGags";
        GBGags.Padding = new Padding(4, 3, 4, 3);
        GBGags.Size = new Size(234, 438);
        GBGags.TabIndex = 6;
        GBGags.TabStop = false;
        GBGags.Text = "Gags";
        // 
        // CLBGags
        // 
        CLBGags.BorderStyle = BorderStyle.None;
        CLBGags.Dock = DockStyle.Fill;
        CLBGags.FormattingEnabled = true;
        CLBGags.Location = new Point(4, 19);
        CLBGags.Margin = new Padding(4, 3, 4, 3);
        CLBGags.Name = "CLBGags";
        CLBGags.Size = new Size(226, 416);
        CLBGags.TabIndex = 0;
        CLBGags.ItemCheck += CLB_DisableCheck;
        // 
        // GBWasps
        // 
        GBWasps.BackColor = SystemColors.Window;
        GBWasps.Controls.Add(CLBWasps);
        GBWasps.Dock = DockStyle.Fill;
        GBWasps.Location = new Point(0, 0);
        GBWasps.Margin = new Padding(4, 3, 4, 3);
        GBWasps.Name = "GBWasps";
        GBWasps.Padding = new Padding(4, 3, 4, 3);
        GBWasps.Size = new Size(234, 438);
        GBWasps.TabIndex = 7;
        GBWasps.TabStop = false;
        GBWasps.Text = "Wasps";
        // 
        // CLBWasps
        // 
        CLBWasps.BorderStyle = BorderStyle.None;
        CLBWasps.Dock = DockStyle.Fill;
        CLBWasps.FormattingEnabled = true;
        CLBWasps.Location = new Point(4, 19);
        CLBWasps.Margin = new Padding(4, 3, 4, 3);
        CLBWasps.Name = "CLBWasps";
        CLBWasps.Size = new Size(226, 416);
        CLBWasps.TabIndex = 0;
        CLBWasps.ItemCheck += CLB_DisableCheck;
        // 
        // GBRewards
        // 
        GBRewards.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        GBRewards.BackColor = SystemColors.Window;
        GBRewards.Controls.Add(CLBRewards);
        GBRewards.Location = new Point(7, 254);
        GBRewards.Margin = new Padding(4, 3, 4, 3);
        GBRewards.Name = "GBRewards";
        GBRewards.Padding = new Padding(4, 3, 4, 3);
        GBRewards.Size = new Size(233, 206);
        GBRewards.TabIndex = 5;
        GBRewards.TabStop = false;
        GBRewards.Text = "Rewards";
        // 
        // CLBRewards
        // 
        CLBRewards.BorderStyle = BorderStyle.None;
        CLBRewards.Dock = DockStyle.Fill;
        CLBRewards.FormattingEnabled = true;
        CLBRewards.Location = new Point(4, 19);
        CLBRewards.Margin = new Padding(4, 3, 4, 3);
        CLBRewards.Name = "CLBRewards";
        CLBRewards.Size = new Size(225, 184);
        CLBRewards.TabIndex = 0;
        CLBRewards.ItemCheck += CLB_DisableCheck;
        // 
        // GBCards
        // 
        GBCards.BackColor = SystemColors.Window;
        GBCards.Controls.Add(CLBCards);
        GBCards.Location = new Point(7, 99);
        GBCards.Margin = new Padding(4, 3, 4, 3);
        GBCards.Name = "GBCards";
        GBCards.Padding = new Padding(4, 3, 4, 3);
        GBCards.Size = new Size(233, 148);
        GBCards.TabIndex = 4;
        GBCards.TabStop = false;
        GBCards.Text = "Cards";
        // 
        // CLBCards
        // 
        CLBCards.BorderStyle = BorderStyle.None;
        CLBCards.ContextMenuStrip = CMSCards;
        CLBCards.Dock = DockStyle.Fill;
        CLBCards.FormattingEnabled = true;
        CLBCards.Items.AddRange(new object[] { "Card 1", "Card 2", "Card 3", "Card 4", "Card 5", "Card 6", "Card 7" });
        CLBCards.Location = new Point(4, 19);
        CLBCards.Margin = new Padding(4, 3, 4, 3);
        CLBCards.Name = "CLBCards";
        CLBCards.Size = new Size(225, 126);
        CLBCards.TabIndex = 0;
        CLBCards.ItemCheck += CLB_DisableCheck;
        // 
        // CMSCards
        // 
        CMSCards.Items.AddRange(new ToolStripItem[] { TSMIShowCardLocationSpoilers });
        CMSCards.Name = "CMSCards";
        CMSCards.Size = new Size(197, 26);
        // 
        // TSMIShowCardLocationSpoilers
        // 
        TSMIShowCardLocationSpoilers.CheckOnClick = true;
        TSMIShowCardLocationSpoilers.Name = "TSMIShowCardLocationSpoilers";
        TSMIShowCardLocationSpoilers.Size = new Size(196, 22);
        TSMIShowCardLocationSpoilers.Text = "Show Location Spoilers";
        TSMIShowCardLocationSpoilers.CheckedChanged += TSMIShowCardLocationSpoilers_CheckedChanged;
        // 
        // GBMissions
        // 
        GBMissions.Controls.Add(LblBonusMission);
        GBMissions.Controls.Add(LblMissions);
        GBMissions.Controls.Add(LblStreetRaces);
        GBMissions.Location = new Point(7, 22);
        GBMissions.Margin = new Padding(4, 3, 4, 3);
        GBMissions.Name = "GBMissions";
        GBMissions.Padding = new Padding(4, 3, 4, 3);
        GBMissions.Size = new Size(233, 70);
        GBMissions.TabIndex = 3;
        GBMissions.TabStop = false;
        GBMissions.Text = "Missions";
        // 
        // LblBonusMission
        // 
        LblBonusMission.AutoSize = true;
        LblBonusMission.Location = new Point(7, 48);
        LblBonusMission.Margin = new Padding(4, 0, 4, 0);
        LblBonusMission.Name = "LblBonusMission";
        LblBonusMission.Size = new Size(107, 15);
        LblBonusMission.TabIndex = 2;
        LblBonusMission.Text = "Bonus Mission: 0/1";
        // 
        // LblMissions
        // 
        LblMissions.AutoSize = true;
        LblMissions.Location = new Point(7, 18);
        LblMissions.Margin = new Padding(4, 0, 4, 0);
        LblMissions.Name = "LblMissions";
        LblMissions.Size = new Size(76, 15);
        LblMissions.TabIndex = 0;
        LblMissions.Text = "Missions: 0/7";
        // 
        // LblStreetRaces
        // 
        LblStreetRaces.AutoSize = true;
        LblStreetRaces.Location = new Point(7, 33);
        LblStreetRaces.Margin = new Padding(4, 0, 4, 0);
        LblStreetRaces.Name = "LblStreetRaces";
        LblStreetRaces.Size = new Size(93, 15);
        LblStreetRaces.TabIndex = 1;
        LblStreetRaces.Text = "Street Races: 0/3";
        // 
        // TmrUpdater
        // 
        TmrUpdater.Enabled = true;
        TmrUpdater.Interval = 1000;
        TmrUpdater.Tick += TmrUpdater_Tick;
        // 
        // FrmMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.Window;
        ClientSize = new Size(735, 519);
        Controls.Add(CBLevel);
        Controls.Add(GBStats);
        Controls.Add(GBLevel);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(4, 3, 4, 3);
        MinimumSize = new Size(751, 558);
        Name = "FrmMain";
        Padding = new Padding(4, 3, 4, 3);
        Text = "SHAR Mod Checklist";
        FormClosing += FrmMain_FormClosing;
        Load += FrmMain_Load;
        GBLevel.ResumeLayout(false);
        GBLevel.PerformLayout();
        GBStats.ResumeLayout(false);
        SCGagsWasps.Panel1.ResumeLayout(false);
        SCGagsWasps.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)SCGagsWasps).EndInit();
        SCGagsWasps.ResumeLayout(false);
        GBGags.ResumeLayout(false);
        GBWasps.ResumeLayout(false);
        GBRewards.ResumeLayout(false);
        GBCards.ResumeLayout(false);
        CMSCards.ResumeLayout(false);
        GBMissions.ResumeLayout(false);
        GBMissions.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.GroupBox GBLevel;
    private System.Windows.Forms.ComboBox CBLevel;
    private System.Windows.Forms.GroupBox GBStats;
    private System.Windows.Forms.Timer TmrUpdater;
    private System.Windows.Forms.GroupBox GBMissions;
    private System.Windows.Forms.Label LblBonusMission;
    private System.Windows.Forms.Label LblMissions;
    private System.Windows.Forms.Label LblStreetRaces;
    private System.Windows.Forms.GroupBox GBCards;
    private System.Windows.Forms.CheckedListBox CLBCards;
    private System.Windows.Forms.GroupBox GBRewards;
    private System.Windows.Forms.CheckedListBox CLBRewards;
    private System.Windows.Forms.GroupBox GBWasps;
    private System.Windows.Forms.CheckedListBox CLBWasps;
    private System.Windows.Forms.GroupBox GBGags;
    private System.Windows.Forms.CheckedListBox CLBGags;
    private System.Windows.Forms.SplitContainer SCGagsWasps;
    private CheckBox CBDarkMode;
    private Button BtnLevel1;
    private Button BtnLevel2;
    private Button BtnLevel3;
    private Button BtnLevel7;
    private Button BtnLevel6;
    private Button BtnLevel5;
    private Button BtnLevel4;
    private CheckBox CBLargeFont;
    private ContextMenuStrip CMSCards;
    private ToolStripMenuItem TSMIShowCardLocationSpoilers;
}