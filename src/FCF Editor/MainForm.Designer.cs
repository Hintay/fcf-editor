using System.Windows.Forms;

namespace FCF_Editor
{
    partial class MainForm
    {

        /// <summary>
        /// <summary>
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
        /// </summary>ComboBox
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sCENEToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sELECTERToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.oUTERLABELToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.SC = new System.Windows.Forms.SplitContainer();
            this.resetButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.outerlabel_browse = new System.Windows.Forms.Button();
            this.alternatives = new System.Windows.Forms.GroupBox();
            this.properties_alternative_tabs = new System.Windows.Forms.TabControl();
            this.addAlt = new System.Windows.Forms.Button();
            this.flagOperations = new System.Windows.Forms.GroupBox();
            this.addOperationTab = new System.Windows.Forms.Button();
            this.properties_operation_tabs = new System.Windows.Forms.TabControl();
            this.checkFlags = new System.Windows.Forms.GroupBox();
            this.properties_check_tabs = new System.Windows.Forms.TabControl();
            this.addCheckTab = new System.Windows.Forms.Button();
            this.properties_jumpTarget = new System.Windows.Forms.ComboBox();
            this.properties_jumpTarget_label = new System.Windows.Forms.Label();
            this.properties_type = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.properties_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.properties_title_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.properties_title = new System.Windows.Forms.TextBox();
            this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.properties_x = new System.Windows.Forms.TextBox();
            this.properties_y = new System.Windows.Forms.TextBox();
            this.properties_file_target = new System.Windows.Forms.ComboBox();
            this.addLabelsInPanel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sCENEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sELECTERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oUTERLABELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayout = new System.Windows.Forms.Panel();
            this.log = new System.Windows.Forms.TextBox();
            this.comment_box = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.outerlabel_open_target = new System.Windows.Forms.OpenFileDialog();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.debug = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SC)).BeginInit();
            this.SC.Panel1.SuspendLayout();
            this.SC.Panel2.SuspendLayout();
            this.SC.SuspendLayout();
            this.alternatives.SuspendLayout();
            this.flagOperations.SuspendLayout();
            this.checkFlags.SuspendLayout();
            this.addLabelsInPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.ToolTipText = "Start over with a new flowchart!";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.ToolTipText = "Open a pre-existing flow chart!";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.ToolTipText = "Save the current Flowchart data!";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(129, 24);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.ToolTipText = "Save with a different file name!";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.ToolTipText = "Exit the program!";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.toolStripSeparator2,
            this.optionsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sCENEToolStripMenuItem1,
            this.sELECTERToolStripMenuItem1,
            this.oUTERLABELToolStripMenuItem1});
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(130, 24);
            this.addToolStripMenuItem1.Text = "&Add";
            this.addToolStripMenuItem1.ToolTipText = "Add a flow element!";
            // 
            // sCENEToolStripMenuItem1
            // 
            this.sCENEToolStripMenuItem1.Name = "sCENEToolStripMenuItem1";
            this.sCENEToolStripMenuItem1.Size = new System.Drawing.Size(165, 24);
            this.sCENEToolStripMenuItem1.Text = "&SCENE";
            this.sCENEToolStripMenuItem1.ToolTipText = "Add a SCENE flow element!";
            this.sCENEToolStripMenuItem1.Click += new System.EventHandler(this.sCENEToolStripMenuItem_Click);
            // 
            // sELECTERToolStripMenuItem1
            // 
            this.sELECTERToolStripMenuItem1.Name = "sELECTERToolStripMenuItem1";
            this.sELECTERToolStripMenuItem1.Size = new System.Drawing.Size(165, 24);
            this.sELECTERToolStripMenuItem1.Text = "S&ELECTER";
            this.sELECTERToolStripMenuItem1.ToolTipText = "Add a SELECTER flow element!";
            this.sELECTERToolStripMenuItem1.Click += new System.EventHandler(this.sELECTERToolStripMenuItem_Click);
            // 
            // oUTERLABELToolStripMenuItem1
            // 
            this.oUTERLABELToolStripMenuItem1.Name = "oUTERLABELToolStripMenuItem1";
            this.oUTERLABELToolStripMenuItem1.Size = new System.Drawing.Size(165, 24);
            this.oUTERLABELToolStripMenuItem1.Text = "O&UTERLABEL";
            this.oUTERLABELToolStripMenuItem1.ToolTipText = "Add a OUTERLABEL flow element!";
            this.oUTERLABELToolStripMenuItem1.Click += new System.EventHandler(this.oUTERLABELToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(127, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.ToolTipText = "Change your preferences!";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.ToolTipText = "Do you need help?";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.ToolTipText = "About what this program is capable of...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openFile
            // 
            this.openFile.DefaultExt = "fcf";
            this.openFile.FileName = "*.fcf";
            this.openFile.Filter = "FCF files|*.fcf";
            this.openFile.Title = "Read the Flowchart data";
            this.openFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFile_FileOk);
            // 
            // SC
            // 
            this.SC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SC.Location = new System.Drawing.Point(0, 24);
            this.SC.Name = "SC";
            // 
            // SC.Panel1
            // 
            this.SC.Panel1.AutoScroll = true;
            this.SC.Panel1.Controls.Add(this.resetButton);
            this.SC.Panel1.Controls.Add(this.removeButton);
            this.SC.Panel1.Controls.Add(this.update);
            this.SC.Panel1.Controls.Add(this.outerlabel_browse);
            this.SC.Panel1.Controls.Add(this.alternatives);
            this.SC.Panel1.Controls.Add(this.flagOperations);
            this.SC.Panel1.Controls.Add(this.checkFlags);
            this.SC.Panel1.Controls.Add(this.properties_jumpTarget);
            this.SC.Panel1.Controls.Add(this.properties_jumpTarget_label);
            this.SC.Panel1.Controls.Add(this.properties_type);
            this.SC.Panel1.Controls.Add(this.label6);
            this.SC.Panel1.Controls.Add(this.properties_id);
            this.SC.Panel1.Controls.Add(this.label3);
            this.SC.Panel1.Controls.Add(this.properties_title_label);
            this.SC.Panel1.Controls.Add(this.label1);
            this.SC.Panel1.Controls.Add(this.label4);
            this.SC.Panel1.Controls.Add(this.label5);
            this.SC.Panel1.Controls.Add(this.properties_title);
            this.SC.Panel1.Controls.Add(this.properties_x);
            this.SC.Panel1.Controls.Add(this.properties_y);
            this.SC.Panel1.Controls.Add(this.properties_file_target);
            this.SC.Panel1MinSize = 300;
            // 
            // SC.Panel2
            // 
            this.SC.Panel2.AllowDrop = true;
            this.SC.Panel2.AutoScroll = true;
            this.SC.Panel2.ContextMenuStrip = this.addLabelsInPanel;
            this.SC.Panel2.Controls.Add(this.splitContainer1);
            this.SC.Panel2MinSize = 300;
            this.SC.Size = new System.Drawing.Size(1008, 680);
            this.SC.SplitterDistance = 321;
            this.SC.TabIndex = 2;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetButton.AutoSize = true;
            this.resetButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resetButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.resetButton.Enabled = false;
            this.resetButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.resetButton.Location = new System.Drawing.Point(7, 601);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(73, 34);
            this.resetButton.TabIndex = 29;
            this.resetButton.Text = "Reset!";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Visible = false;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.AutoSize = true;
            this.removeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.removeButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.removeButton.Enabled = false;
            this.removeButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.removeButton.Location = new System.Drawing.Point(220, 601);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(94, 34);
            this.removeButton.TabIndex = 28;
            this.removeButton.Text = "Remove!";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Visible = false;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // update
            // 
            this.update.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.update.Enabled = false;
            this.update.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.update.Location = new System.Drawing.Point(0, 641);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(317, 35);
            this.update.TabIndex = 27;
            this.update.Text = "Update";
            this.toolTip.SetToolTip(this.update, "Update your Flow Data!");
            this.update.UseVisualStyleBackColor = true;
            this.update.Visible = false;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // outerlabel_browse
            // 
            this.outerlabel_browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.outerlabel_browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.outerlabel_browse.Location = new System.Drawing.Point(256, 119);
            this.outerlabel_browse.Name = "outerlabel_browse";
            this.outerlabel_browse.Size = new System.Drawing.Size(36, 20);
            this.outerlabel_browse.TabIndex = 13;
            this.outerlabel_browse.Text = "...";
            this.outerlabel_browse.UseCompatibleTextRendering = true;
            this.outerlabel_browse.UseVisualStyleBackColor = true;
            this.outerlabel_browse.Visible = false;
            this.outerlabel_browse.Click += new System.EventHandler(this.outerlabel_browse_Click);
            // 
            // alternatives
            // 
            this.alternatives.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alternatives.Controls.Add(this.properties_alternative_tabs);
            this.alternatives.Controls.Add(this.addAlt);
            this.alternatives.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alternatives.Location = new System.Drawing.Point(3, 200);
            this.alternatives.Name = "alternatives";
            this.alternatives.Size = new System.Drawing.Size(307, 149);
            this.alternatives.TabIndex = 24;
            this.alternatives.TabStop = false;
            this.alternatives.Text = "Alternatives";
            this.alternatives.Visible = false;
            // 
            // properties_alternative_tabs
            // 
            this.properties_alternative_tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.properties_alternative_tabs.HotTrack = true;
            this.properties_alternative_tabs.Location = new System.Drawing.Point(7, 16);
            this.properties_alternative_tabs.Name = "properties_alternative_tabs";
            this.properties_alternative_tabs.SelectedIndex = 0;
            this.properties_alternative_tabs.ShowToolTips = true;
            this.properties_alternative_tabs.Size = new System.Drawing.Size(294, 92);
            this.properties_alternative_tabs.TabIndex = 22;
            // 
            // addAlt
            // 
            this.addAlt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addAlt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.addAlt.Location = new System.Drawing.Point(269, 113);
            this.addAlt.Name = "addAlt";
            this.addAlt.Size = new System.Drawing.Size(30, 29);
            this.addAlt.TabIndex = 23;
            this.addAlt.Text = "+";
            this.toolTip.SetToolTip(this.addAlt, "Add Alternative tab!");
            this.addAlt.UseVisualStyleBackColor = true;
            this.addAlt.Click += new System.EventHandler(this.addAlt_Click);
            // 
            // flagOperations
            // 
            this.flagOperations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagOperations.Controls.Add(this.addOperationTab);
            this.flagOperations.Controls.Add(this.properties_operation_tabs);
            this.flagOperations.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagOperations.Location = new System.Drawing.Point(3, 390);
            this.flagOperations.Name = "flagOperations";
            this.flagOperations.Size = new System.Drawing.Size(304, 168);
            this.flagOperations.TabIndex = 23;
            this.flagOperations.TabStop = false;
            this.flagOperations.Text = "Flag Operations";
            this.flagOperations.Visible = false;
            // 
            // addOperationTab
            // 
            this.addOperationTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addOperationTab.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.addOperationTab.Location = new System.Drawing.Point(268, 133);
            this.addOperationTab.Name = "addOperationTab";
            this.addOperationTab.Size = new System.Drawing.Size(30, 29);
            this.addOperationTab.TabIndex = 24;
            this.addOperationTab.Text = "+";
            this.toolTip.SetToolTip(this.addOperationTab, "Add Flag Operation tab!");
            this.addOperationTab.UseVisualStyleBackColor = true;
            this.addOperationTab.Click += new System.EventHandler(this.addOperationTab_Click);
            // 
            // properties_operation_tabs
            // 
            this.properties_operation_tabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.properties_operation_tabs.Location = new System.Drawing.Point(7, 26);
            this.properties_operation_tabs.Name = "properties_operation_tabs";
            this.properties_operation_tabs.SelectedIndex = 0;
            this.properties_operation_tabs.ShowToolTips = true;
            this.properties_operation_tabs.Size = new System.Drawing.Size(291, 103);
            this.properties_operation_tabs.TabIndex = 22;
            // 
            // checkFlags
            // 
            this.checkFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkFlags.Controls.Add(this.properties_check_tabs);
            this.checkFlags.Controls.Add(this.addCheckTab);
            this.checkFlags.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkFlags.Location = new System.Drawing.Point(3, 198);
            this.checkFlags.Name = "checkFlags";
            this.checkFlags.Size = new System.Drawing.Size(307, 186);
            this.checkFlags.TabIndex = 17;
            this.checkFlags.TabStop = false;
            this.checkFlags.Text = "Check Flags";
            this.checkFlags.Visible = false;
            // 
            // properties_check_tabs
            // 
            this.properties_check_tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.properties_check_tabs.HotTrack = true;
            this.properties_check_tabs.Location = new System.Drawing.Point(7, 16);
            this.properties_check_tabs.Name = "properties_check_tabs";
            this.properties_check_tabs.SelectedIndex = 0;
            this.properties_check_tabs.ShowToolTips = true;
            this.properties_check_tabs.Size = new System.Drawing.Size(294, 135);
            this.properties_check_tabs.TabIndex = 22;
            // 
            // addCheckTab
            // 
            this.addCheckTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addCheckTab.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.addCheckTab.Location = new System.Drawing.Point(268, 151);
            this.addCheckTab.Name = "addCheckTab";
            this.addCheckTab.Size = new System.Drawing.Size(30, 29);
            this.addCheckTab.TabIndex = 23;
            this.addCheckTab.Text = "+";
            this.toolTip.SetToolTip(this.addCheckTab, "Add Flag Check tab!");
            this.addCheckTab.UseVisualStyleBackColor = true;
            this.addCheckTab.Click += new System.EventHandler(this.addCheckTab_Click);
            // 
            // properties_jumpTarget
            // 
            this.properties_jumpTarget.Enabled = false;
            this.properties_jumpTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.properties_jumpTarget.FormattingEnabled = true;
            this.properties_jumpTarget.Location = new System.Drawing.Point(142, 173);
            this.properties_jumpTarget.Name = "properties_jumpTarget";
            this.properties_jumpTarget.Size = new System.Drawing.Size(60, 25);
            this.properties_jumpTarget.TabIndex = 1;
            this.properties_jumpTarget.SelectionChangeCommitted += new System.EventHandler(this.properties_jumpTarget_SelectionChangeCommitted);
            // 
            // properties_jumpTarget_label
            // 
            this.properties_jumpTarget_label.AutoSize = true;
            this.properties_jumpTarget_label.Cursor = System.Windows.Forms.Cursors.Default;
            this.properties_jumpTarget_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.properties_jumpTarget_label.Location = new System.Drawing.Point(7, 177);
            this.properties_jumpTarget_label.Name = "properties_jumpTarget_label";
            this.properties_jumpTarget_label.Size = new System.Drawing.Size(161, 17);
            this.properties_jumpTarget_label.TabIndex = 11;
            this.properties_jumpTarget_label.Text = "Default Jump Target:";
            // 
            // properties_type
            // 
            this.properties_type.Enabled = false;
            this.properties_type.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.properties_type.FormattingEnabled = true;
            this.properties_type.Items.AddRange(new object[] {
            "SCENE",
            "SELECTER",
            "OUTERLABEL"});
            this.properties_type.Location = new System.Drawing.Point(62, 92);
            this.properties_type.Name = "properties_type";
            this.properties_type.Size = new System.Drawing.Size(124, 25);
            this.properties_type.TabIndex = 6;
            this.properties_type.SelectionChangeCommitted += new System.EventHandler(this.properties_type_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(7, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Type:";
            // 
            // properties_id
            // 
            this.properties_id.Enabled = false;
            this.properties_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.properties_id.Location = new System.Drawing.Point(62, 63);
            this.properties_id.MaxLength = 3;
            this.properties_id.Name = "properties_id";
            this.properties_id.Size = new System.Drawing.Size(34, 23);
            this.properties_id.TabIndex = 7;
            this.toolTip.SetToolTip(this.properties_id, "ID of the selected flow element");
            this.properties_id.TextChanged += new System.EventHandler(this.properties_id_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "ID: ";
            // 
            // properties_title_label
            // 
            this.properties_title_label.AutoSize = true;
            this.properties_title_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.properties_title_label.Location = new System.Drawing.Point(10, 126);
            this.properties_title_label.Name = "properties_title_label";
            this.properties_title_label.Size = new System.Drawing.Size(45, 17);
            this.properties_title_label.TabIndex = 2;
            this.properties_title_label.Text = "Title:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Cambria", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "PROPERTIES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(21, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "X:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(126, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Y:";
            // 
            // properties_title
            // 
            this.properties_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.properties_title.ContextMenuStrip = this.rightClickMenu;
            this.properties_title.Enabled = false;
            this.properties_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.properties_title.Location = new System.Drawing.Point(62, 119);
            this.properties_title.Name = "properties_title";
            this.properties_title.Size = new System.Drawing.Size(188, 23);
            this.properties_title.TabIndex = 5;
            this.properties_title.TextChanged += new System.EventHandler(this.properties_title_TextChanged);
            // 
            // rightClickMenu
            // 
            this.rightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.rightClickMenu.Name = "rightClickMenu";
            this.rightClickMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // properties_x
            // 
            this.properties_x.Enabled = false;
            this.properties_x.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.properties_x.Location = new System.Drawing.Point(62, 147);
            this.properties_x.MaxLength = 4;
            this.properties_x.Name = "properties_x";
            this.properties_x.Size = new System.Drawing.Size(58, 23);
            this.properties_x.TabIndex = 3;
            this.properties_x.TextChanged += new System.EventHandler(this.properties_x_TextChanged);
            // 
            // properties_y
            // 
            this.properties_y.Enabled = false;
            this.properties_y.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.properties_y.Location = new System.Drawing.Point(167, 147);
            this.properties_y.MaxLength = 4;
            this.properties_y.Name = "properties_y";
            this.properties_y.Size = new System.Drawing.Size(61, 23);
            this.properties_y.TabIndex = 2;
            this.properties_y.TextChanged += new System.EventHandler(this.properties_y_TextChanged);
            // 
            // properties_file_target
            // 
            this.properties_file_target.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.properties_file_target.FormattingEnabled = true;
            this.properties_file_target.Location = new System.Drawing.Point(142, 173);
            this.properties_file_target.Name = "properties_file_target";
            this.properties_file_target.Size = new System.Drawing.Size(60, 25);
            this.properties_file_target.TabIndex = 26;
            this.properties_file_target.Visible = false;
            this.properties_file_target.SelectedIndexChanged += new System.EventHandler(this.properties_file_target_SelectedIndexChanged);
            // 
            // addLabelsInPanel
            // 
            this.addLabelsInPanel.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.addLabelsInPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.addLabelsInPanel.Name = "addLabelsInPanel";
            this.addLabelsInPanel.Size = new System.Drawing.Size(119, 52);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sCENEToolStripMenuItem,
            this.sELECTERToolStripMenuItem,
            this.oUTERLABELToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.ShowShortcutKeys = false;
            this.addToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.addToolStripMenuItem.Text = "&Add";
            // 
            // sCENEToolStripMenuItem
            // 
            this.sCENEToolStripMenuItem.Name = "sCENEToolStripMenuItem";
            this.sCENEToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.sCENEToolStripMenuItem.Text = "&SCENE";
            this.sCENEToolStripMenuItem.Click += new System.EventHandler(this.sCENEToolStripMenuItem_Click);
            // 
            // sELECTERToolStripMenuItem
            // 
            this.sELECTERToolStripMenuItem.Name = "sELECTERToolStripMenuItem";
            this.sELECTERToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.sELECTERToolStripMenuItem.Text = "S&ELECTER";
            this.sELECTERToolStripMenuItem.Click += new System.EventHandler(this.sELECTERToolStripMenuItem_Click);
            // 
            // oUTERLABELToolStripMenuItem
            // 
            this.oUTERLABELToolStripMenuItem.Name = "oUTERLABELToolStripMenuItem";
            this.oUTERLABELToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.oUTERLABELToolStripMenuItem.Text = "O&UTERLABEL";
            this.oUTERLABELToolStripMenuItem.Click += new System.EventHandler(this.oUTERLABELToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShowShortcutKeys = false;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // flowLayout
            // 
            this.flowLayout.AllowDrop = true;
            this.flowLayout.AutoSize = true;
            this.flowLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayout.Location = new System.Drawing.Point(0, 1);
            this.flowLayout.Margin = new System.Windows.Forms.Padding(50);
            this.flowLayout.MinimumSize = new System.Drawing.Size(150, 150);
            this.flowLayout.Name = "flowLayout";
            this.flowLayout.Size = new System.Drawing.Size(150, 150);
            this.flowLayout.TabIndex = 0;
            this.flowLayout.Click += new System.EventHandler(this.flowLayout_Click);
            this.flowLayout.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.flowLayout.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.flowLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayout_Paint);
            // 
            // log
            // 
            this.log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.log.HideSelection = false;
            this.log.Location = new System.Drawing.Point(0, 34);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.log.Size = new System.Drawing.Size(260, 644);
            this.log.TabIndex = 6;
            this.log.WordWrap = false;
            this.log.KeyDown += new System.Windows.Forms.KeyEventHandler(this.log_KeyDown);
            // 
            // comment_box
            // 
            this.comment_box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comment_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.comment_box.Location = new System.Drawing.Point(3, 5);
            this.comment_box.Name = "comment_box";
            this.comment_box.Size = new System.Drawing.Size(252, 23);
            this.comment_box.TabIndex = 7;
            this.toolTip.SetToolTip(this.comment_box, "Comment for this flow chart!");
            // 
            // outerlabel_open_target
            // 
            this.outerlabel_open_target.AddExtension = false;
            this.outerlabel_open_target.DefaultExt = "fcf";
            this.outerlabel_open_target.FileName = "*.fcf";
            this.outerlabel_open_target.Filter = "FCF files|*.fcf";
            this.outerlabel_open_target.Title = "Select the Target file";
            this.outerlabel_open_target.FileOk += new System.ComponentModel.CancelEventHandler(this.outerlabel_open_target_FileOk);
            // 
            // saveFile
            // 
            this.saveFile.DefaultExt = "fcf";
            this.saveFile.FileName = "*.fcf";
            this.saveFile.Filter = "FCF files|*.fcf";
            this.saveFile.Title = "Save the Flowchart data";
            this.saveFile.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFile_FileOk);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debug});
            this.statusStrip1.Location = new System.Drawing.Point(0, 704);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 25);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // debug
            // 
            this.debug.AutoToolTip = true;
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(113, 20);
            this.debug.Text = "                          ";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.flowLayout);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.log);
            this.splitContainer1.Panel2.Controls.Add(this.comment_box);
            this.splitContainer1.Size = new System.Drawing.Size(683, 680);
            this.splitContainer1.SplitterDistance = 417;
            this.splitContainer1.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.SC);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 514);
            this.Name = "MainForm";
            this.Text = "Flowchart Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.SC.Panel1.ResumeLayout(false);
            this.SC.Panel1.PerformLayout();
            this.SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SC)).EndInit();
            this.SC.ResumeLayout(false);
            this.alternatives.ResumeLayout(false);
            this.flagOperations.ResumeLayout(false);
            this.checkFlags.ResumeLayout(false);
            this.addLabelsInPanel.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFile;
        public System.Windows.Forms.SplitContainer SC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label properties_title_label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox properties_title;
        public System.Windows.Forms.TextBox properties_id;
        public System.Windows.Forms.TextBox properties_y;
        public System.Windows.Forms.TextBox properties_x;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox properties_type;
        private System.Windows.Forms.Label properties_jumpTarget_label;
        private System.Windows.Forms.ComboBox properties_jumpTarget;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox checkFlags;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabControl properties_check_tabs;
        private System.Windows.Forms.GroupBox flagOperations;
        private System.Windows.Forms.TabControl properties_operation_tabs;
        private System.Windows.Forms.Button addCheckTab;
        private System.Windows.Forms.Button addOperationTab;
        private System.Windows.Forms.GroupBox alternatives;
        private System.Windows.Forms.TabControl properties_alternative_tabs;
        private System.Windows.Forms.Button addAlt;
        private System.Windows.Forms.Button outerlabel_browse;
        private System.Windows.Forms.OpenFileDialog outerlabel_open_target;
        private System.Windows.Forms.ComboBox properties_file_target;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.ContextMenuStrip addLabelsInPanel;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sCENEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sELECTERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oUTERLABELToolStripMenuItem;
        private System.Windows.Forms.Panel flowLayout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel debug;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;

        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem1;
        private ToolStripMenuItem sCENEToolStripMenuItem1;
        private ToolStripMenuItem sELECTERToolStripMenuItem1;
        private ToolStripMenuItem oUTERLABELToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private Button removeButton;
        private ContextMenuStrip rightClickMenu;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        public TextBox log;
        private TextBox comment_box;
        private SplitContainer splitContainer1;
    }
}