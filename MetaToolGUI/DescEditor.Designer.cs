namespace AX9.MetaToolGUI
{
    partial class DescEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DENUD_MemoryRegion = new System.Windows.Forms.NumericUpDown();
            this.DETB_ProgramIdMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DETB_ProgramIdMax = new System.Windows.Forms.TextBox();
            this.DECL_FsAccess = new System.Windows.Forms.CheckedListBox();
            this.DETB_Acid = new System.Windows.Forms.TextBox();
            this.DECB_AcidType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DEDGV_SrvAccess = new System.Windows.Forms.DataGridView();
            this.DEDGV_SrvAccess_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEDGV_SrvAccess_IsServer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DEDGV_SrvAccess_Remove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.DECB_Presets = new System.Windows.Forms.ComboBox();
            this.DEB_AddPreset = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.DECLB_SystemCalls = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DETB_MainThreadStackSize = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.DECB_EnableDebug = new System.Windows.Forms.CheckBox();
            this.DECB_ForceDebug = new System.Windows.Forms.CheckBox();
            this.DENUD_MainThreadCoreNumber = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.DECB_ProcessAddressSpace = new System.Windows.Forms.ComboBox();
            this.DENUD_HandleTableSize = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.DENUD_MinorVersion = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.DENUD_MajorVersion = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.DECB_ProgramType = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.DENUD_MinCoreNumber = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.DENUD_MaxCoreNumber = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.DENUD_LowestPriority = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.DENUD_HighestPriority = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.DENUD_MainThreadPriority = new System.Windows.Forms.NumericUpDown();
            this.DEB_Save = new System.Windows.Forms.Button();
            this.DEB_SaveAs = new System.Windows.Forms.Button();
            this.DEB_UpdateAcid = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MemoryRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DEDGV_SrvAccess)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MainThreadCoreNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_HandleTableSize)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MinorVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MajorVersion)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MinCoreNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MaxCoreNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_LowestPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_HighestPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MainThreadPriority)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "MemoryRegion:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "ProgramIdMin:";
            // 
            // DENUD_MemoryRegion
            // 
            this.DENUD_MemoryRegion.Location = new System.Drawing.Point(115, 12);
            this.DENUD_MemoryRegion.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.DENUD_MemoryRegion.Name = "DENUD_MemoryRegion";
            this.DENUD_MemoryRegion.Size = new System.Drawing.Size(200, 20);
            this.DENUD_MemoryRegion.TabIndex = 3;
            // 
            // DETB_ProgramIdMin
            // 
            this.DETB_ProgramIdMin.Location = new System.Drawing.Point(115, 38);
            this.DETB_ProgramIdMin.MaxLength = 18;
            this.DETB_ProgramIdMin.Name = "DETB_ProgramIdMin";
            this.DETB_ProgramIdMin.Size = new System.Drawing.Size(200, 20);
            this.DETB_ProgramIdMin.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "ProgramIdMax:";
            // 
            // DETB_ProgramIdMax
            // 
            this.DETB_ProgramIdMax.Location = new System.Drawing.Point(115, 64);
            this.DETB_ProgramIdMax.MaxLength = 18;
            this.DETB_ProgramIdMax.Name = "DETB_ProgramIdMax";
            this.DETB_ProgramIdMax.Size = new System.Drawing.Size(200, 20);
            this.DETB_ProgramIdMax.TabIndex = 6;
            // 
            // DECL_FsAccess
            // 
            this.DECL_FsAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DECL_FsAccess.FormattingEnabled = true;
            this.DECL_FsAccess.Location = new System.Drawing.Point(5, 15);
            this.DECL_FsAccess.Name = "DECL_FsAccess";
            this.DECL_FsAccess.Size = new System.Drawing.Size(302, 124);
            this.DECL_FsAccess.TabIndex = 8;
            // 
            // DETB_Acid
            // 
            this.DETB_Acid.Location = new System.Drawing.Point(5, 436);
            this.DETB_Acid.Multiline = true;
            this.DETB_Acid.Name = "DETB_Acid";
            this.DETB_Acid.Size = new System.Drawing.Size(310, 83);
            this.DETB_Acid.TabIndex = 10;
            // 
            // DECB_AcidType
            // 
            this.DECB_AcidType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DECB_AcidType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DECB_AcidType.FormattingEnabled = true;
            this.DECB_AcidType.Location = new System.Drawing.Point(113, 409);
            this.DECB_AcidType.Name = "DECB_AcidType";
            this.DECB_AcidType.Size = new System.Drawing.Size(200, 21);
            this.DECB_AcidType.TabIndex = 11;
            this.DECB_AcidType.SelectedIndexChanged += new System.EventHandler(this.DECB_AcidType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.Location = new System.Drawing.Point(5, 412);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Acid:";
            // 
            // DEDGV_SrvAccess
            // 
            this.DEDGV_SrvAccess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DEDGV_SrvAccess.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DEDGV_SrvAccess.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DEDGV_SrvAccess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DEDGV_SrvAccess.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DEDGV_SrvAccess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DEDGV_SrvAccess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DEDGV_SrvAccess_Name,
            this.DEDGV_SrvAccess_IsServer,
            this.DEDGV_SrvAccess_Remove});
            this.DEDGV_SrvAccess.Location = new System.Drawing.Point(99, 15);
            this.DEDGV_SrvAccess.MultiSelect = false;
            this.DEDGV_SrvAccess.Name = "DEDGV_SrvAccess";
            this.DEDGV_SrvAccess.RowHeadersVisible = false;
            this.DEDGV_SrvAccess.Size = new System.Drawing.Size(208, 143);
            this.DEDGV_SrvAccess.TabIndex = 15;
            this.DEDGV_SrvAccess.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DEDGV_SrvAccess_CellContentClick);
            // 
            // DEDGV_SrvAccess_Name
            // 
            this.DEDGV_SrvAccess_Name.FillWeight = 180.9508F;
            this.DEDGV_SrvAccess_Name.HeaderText = "Name";
            this.DEDGV_SrvAccess_Name.Name = "DEDGV_SrvAccess_Name";
            // 
            // DEDGV_SrvAccess_IsServer
            // 
            this.DEDGV_SrvAccess_IsServer.FillWeight = 110.0281F;
            this.DEDGV_SrvAccess_IsServer.HeaderText = "IsServer";
            this.DEDGV_SrvAccess_IsServer.Name = "DEDGV_SrvAccess_IsServer";
            // 
            // DEDGV_SrvAccess_Remove
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "X";
            this.DEDGV_SrvAccess_Remove.DefaultCellStyle = dataGridViewCellStyle1;
            this.DEDGV_SrvAccess_Remove.FillWeight = 32.87897F;
            this.DEDGV_SrvAccess_Remove.HeaderText = "X";
            this.DEDGV_SrvAccess_Remove.Name = "DEDGV_SrvAccess_Remove";
            this.DEDGV_SrvAccess_Remove.ReadOnly = true;
            this.DEDGV_SrvAccess_Remove.Text = "X";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.Location = new System.Drawing.Point(10, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Presets:";
            // 
            // DECB_Presets
            // 
            this.DECB_Presets.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DECB_Presets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DECB_Presets.FormattingEnabled = true;
            this.DECB_Presets.Items.AddRange(new object[] {
            "acc:su",
            "acc:u0",
            "aoc:u",
            "apm",
            "apm:sys",
            "appletOE",
            "audctl",
            "audin:u",
            "audout:u",
            "audren:u",
            "banana",
            "bcat:m",
            "bcat:u",
            "bsd:u",
            "bsdcfg",
            "btm:sys",
            "caps:su",
            "csrng",
            "erpt:r",
            "es",
            "fgm",
            "friend:u",
            "fsp-srv",
            "gpio",
            "hid",
            "hid:dbg",
            "hid:sys",
            "htc",
            "htc:tenv",
            "htcs",
            "hwopus",
            "i2c",
            "idle:sys",
            "irs",
            "lbl",
            "ldn:u",
            "ldr:ro",
            "lm",
            "mii:u",
            "mm:u",
            "ncm",
            "nfc:mf:u",
            "nfc:user",
            "nfp:user",
            "nifm:u",
            "nim:shp",
            "npns:s",
            "ns:am2",
            "ns:dev",
            "ns:su",
            "nsd:a",
            "nsd:u",
            "ntc",
            "nvdrv",
            "pcie",
            "pcm",
            "pctl",
            "pdm:qry",
            "pl:u",
            "pm:dmnt",
            "pm:shell",
            "prepo:a",
            "prepo:u",
            "psm",
            "pwm",
            "set",
            "set:fd",
            "set:sys",
            "sfdnsres",
            "ssl",
            "tcap",
            "time:a",
            "time:s",
            "time:u",
            "tspm",
            "vi:m",
            "vi:s",
            "vi:u"});
            this.DECB_Presets.Location = new System.Drawing.Point(5, 80);
            this.DECB_Presets.Name = "DECB_Presets";
            this.DECB_Presets.Size = new System.Drawing.Size(91, 21);
            this.DECB_Presets.TabIndex = 17;
            // 
            // DEB_AddPreset
            // 
            this.DEB_AddPreset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DEB_AddPreset.Location = new System.Drawing.Point(45, 105);
            this.DEB_AddPreset.Name = "DEB_AddPreset";
            this.DEB_AddPreset.Size = new System.Drawing.Size(50, 23);
            this.DEB_AddPreset.TabIndex = 18;
            this.DEB_AddPreset.Text = "Add ->";
            this.DEB_AddPreset.UseVisualStyleBackColor = true;
            this.DEB_AddPreset.Click += new System.EventHandler(this.DEB_AddPreset_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(5, 280);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "SystemCalls:";
            // 
            // DECLB_SystemCalls
            // 
            this.DECLB_SystemCalls.FormattingEnabled = true;
            this.DECLB_SystemCalls.Location = new System.Drawing.Point(105, 280);
            this.DECLB_SystemCalls.Name = "DECLB_SystemCalls";
            this.DECLB_SystemCalls.Size = new System.Drawing.Size(200, 94);
            this.DECLB_SystemCalls.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DECL_FsAccess);
            this.groupBox1.Location = new System.Drawing.Point(8, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 147);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fs Access";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DEDGV_SrvAccess);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.DECB_Presets);
            this.groupBox2.Controls.Add(this.DEB_AddPreset);
            this.groupBox2.Location = new System.Drawing.Point(8, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 160);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Srv Access";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DETB_MainThreadStackSize);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.DECB_EnableDebug);
            this.groupBox3.Controls.Add(this.DECB_ForceDebug);
            this.groupBox3.Controls.Add(this.DENUD_MainThreadCoreNumber);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.DECLB_SystemCalls);
            this.groupBox3.Controls.Add(this.DECB_ProcessAddressSpace);
            this.groupBox3.Controls.Add(this.DENUD_HandleTableSize);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.DENUD_MainThreadPriority);
            this.groupBox3.Location = new System.Drawing.Point(319, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox3.Size = new System.Drawing.Size(310, 505);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Kernel";
            // 
            // DETB_MainThreadStackSize
            // 
            this.DETB_MainThreadStackSize.Location = new System.Drawing.Point(125, 459);
            this.DETB_MainThreadStackSize.MaxLength = 10;
            this.DETB_MainThreadStackSize.Name = "DETB_MainThreadStackSize";
            this.DETB_MainThreadStackSize.Size = new System.Drawing.Size(180, 20);
            this.DETB_MainThreadStackSize.TabIndex = 24;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(5, 459);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(120, 20);
            this.label18.TabIndex = 23;
            this.label18.Text = "MainThreadStackSize:";
            // 
            // DECB_EnableDebug
            // 
            this.DECB_EnableDebug.Location = new System.Drawing.Point(45, 483);
            this.DECB_EnableDebug.Name = "DECB_EnableDebug";
            this.DECB_EnableDebug.Size = new System.Drawing.Size(100, 20);
            this.DECB_EnableDebug.TabIndex = 32;
            this.DECB_EnableDebug.Text = "EnableDebug";
            this.DECB_EnableDebug.UseVisualStyleBackColor = true;
            // 
            // DECB_ForceDebug
            // 
            this.DECB_ForceDebug.Location = new System.Drawing.Point(151, 483);
            this.DECB_ForceDebug.Name = "DECB_ForceDebug";
            this.DECB_ForceDebug.Size = new System.Drawing.Size(100, 20);
            this.DECB_ForceDebug.TabIndex = 33;
            this.DECB_ForceDebug.Text = "ForceDebug";
            this.DECB_ForceDebug.UseVisualStyleBackColor = true;
            // 
            // DENUD_MainThreadCoreNumber
            // 
            this.DENUD_MainThreadCoreNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DENUD_MainThreadCoreNumber.Location = new System.Drawing.Point(125, 433);
            this.DENUD_MainThreadCoreNumber.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.DENUD_MainThreadCoreNumber.Name = "DENUD_MainThreadCoreNumber";
            this.DENUD_MainThreadCoreNumber.Size = new System.Drawing.Size(180, 20);
            this.DENUD_MainThreadCoreNumber.TabIndex = 35;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(5, 433);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 20);
            this.label17.TabIndex = 34;
            this.label17.Text = "MainThreadCoreNumber:";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(5, 381);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 20);
            this.label12.TabIndex = 24;
            this.label12.Text = "ProcessAddressSpace:";
            // 
            // DECB_ProcessAddressSpace
            // 
            this.DECB_ProcessAddressSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DECB_ProcessAddressSpace.FormattingEnabled = true;
            this.DECB_ProcessAddressSpace.Location = new System.Drawing.Point(125, 380);
            this.DECB_ProcessAddressSpace.Name = "DECB_ProcessAddressSpace";
            this.DECB_ProcessAddressSpace.Size = new System.Drawing.Size(180, 21);
            this.DECB_ProcessAddressSpace.TabIndex = 25;
            // 
            // DENUD_HandleTableSize
            // 
            this.DENUD_HandleTableSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DENUD_HandleTableSize.Location = new System.Drawing.Point(125, 255);
            this.DENUD_HandleTableSize.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.DENUD_HandleTableSize.Name = "DENUD_HandleTableSize";
            this.DENUD_HandleTableSize.Size = new System.Drawing.Size(180, 20);
            this.DENUD_HandleTableSize.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(5, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 20);
            this.label11.TabIndex = 27;
            this.label11.Text = "HandleTableSize:";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.DENUD_MinorVersion);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.DENUD_MajorVersion);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Location = new System.Drawing.Point(5, 182);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(300, 65);
            this.groupBox6.TabIndex = 31;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "KernelVersion";
            // 
            // DENUD_MinorVersion
            // 
            this.DENUD_MinorVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DENUD_MinorVersion.Location = new System.Drawing.Point(120, 41);
            this.DENUD_MinorVersion.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.DENUD_MinorVersion.Name = "DENUD_MinorVersion";
            this.DENUD_MinorVersion.Size = new System.Drawing.Size(180, 20);
            this.DENUD_MinorVersion.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(10, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 20);
            this.label13.TabIndex = 25;
            this.label13.Text = "MinorVersion:";
            // 
            // DENUD_MajorVersion
            // 
            this.DENUD_MajorVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DENUD_MajorVersion.Location = new System.Drawing.Point(120, 15);
            this.DENUD_MajorVersion.Maximum = new decimal(new int[] {
            8191,
            0,
            0,
            0});
            this.DENUD_MajorVersion.Name = "DENUD_MajorVersion";
            this.DENUD_MajorVersion.Size = new System.Drawing.Size(180, 20);
            this.DENUD_MajorVersion.TabIndex = 24;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(10, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(93, 20);
            this.label15.TabIndex = 23;
            this.label15.Text = "MajorVersion:";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.DECB_ProgramType);
            this.groupBox5.Location = new System.Drawing.Point(5, 136);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(300, 40);
            this.groupBox5.TabIndex = 31;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Misc Params";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(10, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 20);
            this.label14.TabIndex = 23;
            this.label14.Text = "ProgramType:";
            // 
            // DECB_ProgramType
            // 
            this.DECB_ProgramType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DECB_ProgramType.FormattingEnabled = true;
            this.DECB_ProgramType.Location = new System.Drawing.Point(120, 15);
            this.DECB_ProgramType.Name = "DECB_ProgramType";
            this.DECB_ProgramType.Size = new System.Drawing.Size(180, 21);
            this.DECB_ProgramType.TabIndex = 23;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.DENUD_MinCoreNumber);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.DENUD_MaxCoreNumber);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.DENUD_LowestPriority);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.DENUD_HighestPriority);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(5, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(300, 115);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Thread Info";
            // 
            // DENUD_MinCoreNumber
            // 
            this.DENUD_MinCoreNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DENUD_MinCoreNumber.Location = new System.Drawing.Point(120, 93);
            this.DENUD_MinCoreNumber.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.DENUD_MinCoreNumber.Name = "DENUD_MinCoreNumber";
            this.DENUD_MinCoreNumber.Size = new System.Drawing.Size(180, 20);
            this.DENUD_MinCoreNumber.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(10, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 20);
            this.label10.TabIndex = 29;
            this.label10.Text = "MinCoreNumber:";
            // 
            // DENUD_MaxCoreNumber
            // 
            this.DENUD_MaxCoreNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DENUD_MaxCoreNumber.Location = new System.Drawing.Point(120, 67);
            this.DENUD_MaxCoreNumber.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.DENUD_MaxCoreNumber.Name = "DENUD_MaxCoreNumber";
            this.DENUD_MaxCoreNumber.Size = new System.Drawing.Size(180, 20);
            this.DENUD_MaxCoreNumber.TabIndex = 28;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(10, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 20);
            this.label9.TabIndex = 27;
            this.label9.Text = "MaxCoreNumber:";
            // 
            // DENUD_LowestPriority
            // 
            this.DENUD_LowestPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DENUD_LowestPriority.Location = new System.Drawing.Point(120, 41);
            this.DENUD_LowestPriority.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.DENUD_LowestPriority.Name = "DENUD_LowestPriority";
            this.DENUD_LowestPriority.Size = new System.Drawing.Size(180, 20);
            this.DENUD_LowestPriority.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "LowestPriority:";
            // 
            // DENUD_HighestPriority
            // 
            this.DENUD_HighestPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DENUD_HighestPriority.Location = new System.Drawing.Point(120, 15);
            this.DENUD_HighestPriority.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.DENUD_HighestPriority.Name = "DENUD_HighestPriority";
            this.DENUD_HighestPriority.Size = new System.Drawing.Size(180, 20);
            this.DENUD_HighestPriority.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "HighestPriority:";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(5, 407);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 20);
            this.label16.TabIndex = 32;
            this.label16.Text = "MainThreadPriority:";
            // 
            // DENUD_MainThreadPriority
            // 
            this.DENUD_MainThreadPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DENUD_MainThreadPriority.Location = new System.Drawing.Point(125, 407);
            this.DENUD_MainThreadPriority.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.DENUD_MainThreadPriority.Name = "DENUD_MainThreadPriority";
            this.DENUD_MainThreadPriority.Size = new System.Drawing.Size(180, 20);
            this.DENUD_MainThreadPriority.TabIndex = 33;
            // 
            // DEB_Save
            // 
            this.DEB_Save.Location = new System.Drawing.Point(242, 522);
            this.DEB_Save.Name = "DEB_Save";
            this.DEB_Save.Size = new System.Drawing.Size(75, 23);
            this.DEB_Save.TabIndex = 23;
            this.DEB_Save.Text = "Save";
            this.DEB_Save.UseVisualStyleBackColor = true;
            this.DEB_Save.Click += new System.EventHandler(this.DEB_Save_Click);
            // 
            // DEB_SaveAs
            // 
            this.DEB_SaveAs.Location = new System.Drawing.Point(317, 522);
            this.DEB_SaveAs.Name = "DEB_SaveAs";
            this.DEB_SaveAs.Size = new System.Drawing.Size(75, 23);
            this.DEB_SaveAs.TabIndex = 24;
            this.DEB_SaveAs.Text = "Save As";
            this.DEB_SaveAs.UseVisualStyleBackColor = true;
            this.DEB_SaveAs.Click += new System.EventHandler(this.DEB_SaveAs_Click);
            // 
            // DEB_UpdateAcid
            // 
            this.DEB_UpdateAcid.Location = new System.Drawing.Point(86, 409);
            this.DEB_UpdateAcid.Name = "DEB_UpdateAcid";
            this.DEB_UpdateAcid.Size = new System.Drawing.Size(21, 21);
            this.DEB_UpdateAcid.TabIndex = 36;
            this.DEB_UpdateAcid.Text = "⟳";
            this.DEB_UpdateAcid.UseVisualStyleBackColor = true;
            this.DEB_UpdateAcid.Click += new System.EventHandler(this.DEB_UpdateAcid_Click);
            // 
            // DescEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 546);
            this.Controls.Add(this.DEB_UpdateAcid);
            this.Controls.Add(this.DEB_SaveAs);
            this.Controls.Add(this.DEB_Save);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DECB_AcidType);
            this.Controls.Add(this.DETB_Acid);
            this.Controls.Add(this.DETB_ProgramIdMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DETB_ProgramIdMin);
            this.Controls.Add(this.DENUD_MemoryRegion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DescEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Desc Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DescEditor_FormClosing);
            this.Load += new System.EventHandler(this.DescEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MemoryRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DEDGV_SrvAccess)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MainThreadCoreNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_HandleTableSize)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MinorVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MajorVersion)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MinCoreNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MaxCoreNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_LowestPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_HighestPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DENUD_MainThreadPriority)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown DENUD_MemoryRegion;
        private System.Windows.Forms.TextBox DETB_ProgramIdMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DETB_ProgramIdMax;
        private System.Windows.Forms.CheckedListBox DECL_FsAccess;
        private System.Windows.Forms.TextBox DETB_Acid;
        private System.Windows.Forms.ComboBox DECB_AcidType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView DEDGV_SrvAccess;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEDGV_SrvAccess_Name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DEDGV_SrvAccess_IsServer;
        private System.Windows.Forms.DataGridViewButtonColumn DEDGV_SrvAccess_Remove;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox DECB_Presets;
        private System.Windows.Forms.Button DEB_AddPreset;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox DECLB_SystemCalls;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown DENUD_MinCoreNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown DENUD_MaxCoreNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown DENUD_LowestPriority;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown DENUD_HighestPriority;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox DECB_ProgramType;
        private System.Windows.Forms.NumericUpDown DENUD_HandleTableSize;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown DENUD_MinorVersion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown DENUD_MajorVersion;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox DECB_EnableDebug;
        private System.Windows.Forms.CheckBox DECB_ForceDebug;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox DECB_ProcessAddressSpace;
        private System.Windows.Forms.NumericUpDown DENUD_MainThreadPriority;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown DENUD_MainThreadCoreNumber;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox DETB_MainThreadStackSize;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button DEB_Save;
        private System.Windows.Forms.Button DEB_SaveAs;
        private System.Windows.Forms.Button DEB_UpdateAcid;
    }
}