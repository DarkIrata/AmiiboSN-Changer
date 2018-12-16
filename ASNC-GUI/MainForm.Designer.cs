namespace ASNC_GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.Image = new System.Windows.Forms.DataGridViewImageColumn();
            this.AmiiboName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HasUserData = new System.Windows.Forms.DataGridViewImageColumn();
            this.HasAppData = new System.Windows.Forms.DataGridViewImageColumn();
            this.IsDecrypted = new System.Windows.Forms.DataGridViewImageColumn();
            this.amiiboPanel = new System.Windows.Forms.Panel();
            this.Divider1 = new System.Windows.Forms.Panel();
            this.gridActionsPanel = new System.Windows.Forms.Panel();
            this.Divider2 = new System.Windows.Forms.Panel();
            this.managerPanel = new System.Windows.Forms.Panel();
            this.tbUID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbSerial = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOutputSelect = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPlatform = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDataSource = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRegisterDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStatueId = new System.Windows.Forms.TextBox();
            this.lblAmiiboNr = new System.Windows.Forms.Label();
            this.tbOwner = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblCounter = new System.Windows.Forms.Label();
            this.tbNickname = new System.Windows.Forms.TextBox();
            this.lblWrites = new System.Windows.Forms.Label();
            this.lblNickname = new System.Windows.Forms.Label();
            this.lblAmiiboName = new System.Windows.Forms.Label();
            this.Divider3 = new System.Windows.Forms.Panel();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnInfo = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.amiiboImage = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.DeleteAmiibo = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.amiiboPanel.SuspendLayout();
            this.gridActionsPanel.SuspendLayout();
            this.managerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amiiboImage)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.AllowDrop = true;
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid.ColumnHeadersHeight = 30;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Image,
            this.AmiiboName,
            this.HasUserData,
            this.HasAppData,
            this.IsDecrypted,
            this.DeleteAmiibo});
            this.dataGrid.Location = new System.Drawing.Point(0, 45);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.dataGrid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGrid.RowTemplate.Height = 42;
            this.dataGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.ShowEditingIcon = false;
            this.dataGrid.Size = new System.Drawing.Size(596, 460);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellContentClick);
            this.dataGrid.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellMouseEnter);
            this.dataGrid.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellMouseLeave);
            this.dataGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGrid_RowsAdded);
            this.dataGrid.SelectionChanged += new System.EventHandler(this.dataGrid_SelectionChanged);
            this.dataGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGrid_DragDrop);
            this.dataGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGrid_DragEnter);
            // 
            // Image
            // 
            this.Image.DataPropertyName = "AmiiboImage";
            this.Image.HeaderText = "";
            this.Image.Name = "Image";
            this.Image.ReadOnly = true;
            this.Image.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Image.Width = 50;
            // 
            // AmiiboName
            // 
            this.AmiiboName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AmiiboName.DataPropertyName = "Name";
            this.AmiiboName.FillWeight = 200F;
            this.AmiiboName.HeaderText = "Amiibo Name";
            this.AmiiboName.Name = "AmiiboName";
            this.AmiiboName.ReadOnly = true;
            this.AmiiboName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // HasUserData
            // 
            this.HasUserData.DataPropertyName = "HasUserData";
            this.HasUserData.HeaderText = "";
            this.HasUserData.Name = "HasUserData";
            this.HasUserData.ReadOnly = true;
            this.HasUserData.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HasUserData.ToolTipText = "Amiibo has a Mii registered";
            this.HasUserData.Width = 32;
            // 
            // HasAppData
            // 
            this.HasAppData.DataPropertyName = "HasAppData";
            this.HasAppData.HeaderText = "";
            this.HasAppData.Name = "HasAppData";
            this.HasAppData.ReadOnly = true;
            this.HasAppData.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HasAppData.ToolTipText = "Amiibo has Gamedata";
            this.HasAppData.Width = 32;
            // 
            // IsDecrypted
            // 
            this.IsDecrypted.DataPropertyName = "IsDecrypted";
            this.IsDecrypted.HeaderText = "";
            this.IsDecrypted.Name = "IsDecrypted";
            this.IsDecrypted.ReadOnly = true;
            this.IsDecrypted.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsDecrypted.ToolTipText = "Status indicator about decryption while loading";
            this.IsDecrypted.Width = 32;
            // 
            // amiiboPanel
            // 
            this.amiiboPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.amiiboPanel.Controls.Add(this.Divider1);
            this.amiiboPanel.Controls.Add(this.gridActionsPanel);
            this.amiiboPanel.Controls.Add(this.dataGrid);
            this.amiiboPanel.Location = new System.Drawing.Point(0, 0);
            this.amiiboPanel.Name = "amiiboPanel";
            this.amiiboPanel.Size = new System.Drawing.Size(595, 505);
            this.amiiboPanel.TabIndex = 1;
            // 
            // Divider1
            // 
            this.Divider1.BackColor = System.Drawing.Color.Silver;
            this.Divider1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Divider1.Location = new System.Drawing.Point(0, 45);
            this.Divider1.Name = "Divider1";
            this.Divider1.Size = new System.Drawing.Size(595, 1);
            this.Divider1.TabIndex = 2;
            // 
            // gridActionsPanel
            // 
            this.gridActionsPanel.Controls.Add(this.btnAdd);
            this.gridActionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridActionsPanel.Location = new System.Drawing.Point(0, 0);
            this.gridActionsPanel.Name = "gridActionsPanel";
            this.gridActionsPanel.Size = new System.Drawing.Size(595, 45);
            this.gridActionsPanel.TabIndex = 1;
            // 
            // Divider2
            // 
            this.Divider2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Divider2.BackColor = System.Drawing.Color.Silver;
            this.Divider2.Location = new System.Drawing.Point(602, 12);
            this.Divider2.Name = "Divider2";
            this.Divider2.Size = new System.Drawing.Size(1, 481);
            this.Divider2.TabIndex = 3;
            // 
            // managerPanel
            // 
            this.managerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.managerPanel.Controls.Add(this.btnInfo);
            this.managerPanel.Controls.Add(this.tbUID);
            this.managerPanel.Controls.Add(this.label7);
            this.managerPanel.Controls.Add(this.tbSerial);
            this.managerPanel.Controls.Add(this.label6);
            this.managerPanel.Controls.Add(this.btnOutputSelect);
            this.managerPanel.Controls.Add(this.tbOutput);
            this.managerPanel.Controls.Add(this.label5);
            this.managerPanel.Controls.Add(this.tbPlatform);
            this.managerPanel.Controls.Add(this.label4);
            this.managerPanel.Controls.Add(this.tbDataSource);
            this.managerPanel.Controls.Add(this.label3);
            this.managerPanel.Controls.Add(this.pictureBox2);
            this.managerPanel.Controls.Add(this.tbRegisterDate);
            this.managerPanel.Controls.Add(this.label2);
            this.managerPanel.Controls.Add(this.tbStatueId);
            this.managerPanel.Controls.Add(this.lblAmiiboNr);
            this.managerPanel.Controls.Add(this.tbOwner);
            this.managerPanel.Controls.Add(this.label1);
            this.managerPanel.Controls.Add(this.pictureBox1);
            this.managerPanel.Controls.Add(this.numUpDown);
            this.managerPanel.Controls.Add(this.lblCounter);
            this.managerPanel.Controls.Add(this.btnExecute);
            this.managerPanel.Controls.Add(this.tbNickname);
            this.managerPanel.Controls.Add(this.lblWrites);
            this.managerPanel.Controls.Add(this.lblNickname);
            this.managerPanel.Controls.Add(this.lblAmiiboName);
            this.managerPanel.Controls.Add(this.Divider3);
            this.managerPanel.Controls.Add(this.amiiboImage);
            this.managerPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.managerPanel.Location = new System.Drawing.Point(609, 0);
            this.managerPanel.Name = "managerPanel";
            this.managerPanel.Size = new System.Drawing.Size(497, 505);
            this.managerPanel.TabIndex = 4;
            // 
            // tbUID
            // 
            this.tbUID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbUID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbUID.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUID.Location = new System.Drawing.Point(257, 90);
            this.tbUID.Name = "tbUID";
            this.tbUID.ReadOnly = true;
            this.tbUID.Size = new System.Drawing.Size(228, 20);
            this.tbUID.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(222, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 15);
            this.label7.TabIndex = 27;
            this.label7.Text = "UID:";
            // 
            // tbSerial
            // 
            this.tbSerial.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbSerial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSerial.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSerial.Location = new System.Drawing.Point(257, 64);
            this.tbSerial.Name = "tbSerial";
            this.tbSerial.ReadOnly = true;
            this.tbSerial.Size = new System.Drawing.Size(228, 20);
            this.tbSerial.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 15);
            this.label6.TabIndex = 25;
            this.label6.Text = "Serial:";
            // 
            // btnOutputSelect
            // 
            this.btnOutputSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputSelect.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOutputSelect.FlatAppearance.BorderSize = 0;
            this.btnOutputSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnOutputSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOutputSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOutputSelect.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutputSelect.Location = new System.Drawing.Point(335, 458);
            this.btnOutputSelect.Name = "btnOutputSelect";
            this.btnOutputSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOutputSelect.Size = new System.Drawing.Size(47, 25);
            this.btnOutputSelect.TabIndex = 11;
            this.btnOutputSelect.Text = "...";
            this.btnOutputSelect.UseVisualStyleBackColor = true;
            this.btnOutputSelect.Click += new System.EventHandler(this.btnOutputSelect_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.BackColor = System.Drawing.Color.White;
            this.tbOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbOutput.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tbOutput.Location = new System.Drawing.Point(156, 458);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(177, 25);
            this.tbOutput.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(13, 462);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 19);
            this.label5.TabIndex = 22;
            this.label5.Text = "Output directory:";
            // 
            // tbPlatform
            // 
            this.tbPlatform.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbPlatform.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPlatform.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPlatform.Location = new System.Drawing.Point(243, 268);
            this.tbPlatform.Name = "tbPlatform";
            this.tbPlatform.ReadOnly = true;
            this.tbPlatform.Size = new System.Drawing.Size(217, 20);
            this.tbPlatform.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Platform:";
            // 
            // tbDataSource
            // 
            this.tbDataSource.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbDataSource.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDataSource.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDataSource.Location = new System.Drawing.Point(243, 227);
            this.tbDataSource.Name = "tbDataSource";
            this.tbDataSource.ReadOnly = true;
            this.tbDataSource.Size = new System.Drawing.Size(217, 20);
            this.tbDataSource.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Data from Game (TitleID):";
            // 
            // tbRegisterDate
            // 
            this.tbRegisterDate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbRegisterDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRegisterDate.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRegisterDate.Location = new System.Drawing.Point(17, 309);
            this.tbRegisterDate.Name = "tbRegisterDate";
            this.tbRegisterDate.ReadOnly = true;
            this.tbRegisterDate.Size = new System.Drawing.Size(217, 20);
            this.tbRegisterDate.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Register date:";
            // 
            // tbStatueId
            // 
            this.tbStatueId.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbStatueId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbStatueId.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbStatueId.Location = new System.Drawing.Point(257, 38);
            this.tbStatueId.Name = "tbStatueId";
            this.tbStatueId.ReadOnly = true;
            this.tbStatueId.Size = new System.Drawing.Size(228, 20);
            this.tbStatueId.TabIndex = 0;
            // 
            // lblAmiiboNr
            // 
            this.lblAmiiboNr.AutoEllipsis = true;
            this.lblAmiiboNr.AutoSize = true;
            this.lblAmiiboNr.Location = new System.Drawing.Point(194, 42);
            this.lblAmiiboNr.Name = "lblAmiiboNr";
            this.lblAmiiboNr.Size = new System.Drawing.Size(57, 15);
            this.lblAmiiboNr.TabIndex = 13;
            this.lblAmiiboNr.Text = "Statue ID:";
            // 
            // tbOwner
            // 
            this.tbOwner.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbOwner.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbOwner.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOwner.Location = new System.Drawing.Point(17, 268);
            this.tbOwner.Name = "tbOwner";
            this.tbOwner.ReadOnly = true;
            this.tbOwner.Size = new System.Drawing.Size(217, 20);
            this.tbOwner.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Mii-Owner:";
            // 
            // numUpDown
            // 
            this.numUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numUpDown.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numUpDown.Location = new System.Drawing.Point(156, 425);
            this.numUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDown.Name = "numUpDown";
            this.numUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numUpDown.Size = new System.Drawing.Size(80, 25);
            this.numUpDown.TabIndex = 9;
            this.numUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCounter
            // 
            this.lblCounter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCounter.AutoSize = true;
            this.lblCounter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCounter.Location = new System.Drawing.Point(13, 427);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(137, 19);
            this.lblCounter.TabIndex = 8;
            this.lblCounter.Text = "Amount to generate:";
            // 
            // tbNickname
            // 
            this.tbNickname.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbNickname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbNickname.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNickname.Location = new System.Drawing.Point(17, 227);
            this.tbNickname.Name = "tbNickname";
            this.tbNickname.ReadOnly = true;
            this.tbNickname.Size = new System.Drawing.Size(217, 20);
            this.tbNickname.TabIndex = 3;
            // 
            // lblWrites
            // 
            this.lblWrites.AutoEllipsis = true;
            this.lblWrites.AutoSize = true;
            this.lblWrites.Location = new System.Drawing.Point(194, 118);
            this.lblWrites.Name = "lblWrites";
            this.lblWrites.Size = new System.Drawing.Size(84, 15);
            this.lblWrites.TabIndex = 6;
            this.lblWrites.Text = "Write Counter:";
            // 
            // lblNickname
            // 
            this.lblNickname.AutoEllipsis = true;
            this.lblNickname.AutoSize = true;
            this.lblNickname.Location = new System.Drawing.Point(14, 209);
            this.lblNickname.Name = "lblNickname";
            this.lblNickname.Size = new System.Drawing.Size(64, 15);
            this.lblNickname.TabIndex = 5;
            this.lblNickname.Text = "Nickname:";
            // 
            // lblAmiiboName
            // 
            this.lblAmiiboName.AutoEllipsis = true;
            this.lblAmiiboName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmiiboName.Location = new System.Drawing.Point(193, 13);
            this.lblAmiiboName.Name = "lblAmiiboName";
            this.lblAmiiboName.Size = new System.Drawing.Size(292, 25);
            this.lblAmiiboName.TabIndex = 4;
            this.lblAmiiboName.Text = "Amiibo Name";
            // 
            // Divider3
            // 
            this.Divider3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Divider3.BackColor = System.Drawing.Color.Silver;
            this.Divider3.Location = new System.Drawing.Point(13, 412);
            this.Divider3.Name = "Divider3";
            this.Divider3.Size = new System.Drawing.Size(472, 1);
            this.Divider3.TabIndex = 3;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::ASNC_GUI.Properties.Resources.garbage;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.ToolTipText = "Remove Amiibo from list";
            this.dataGridViewImageColumn1.Width = 32;
            // 
            // btnInfo
            // 
            this.btnInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnInfo.FlatAppearance.BorderSize = 0;
            this.btnInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInfo.Image = global::ASNC_GUI.Properties.Resources.info;
            this.btnInfo.Location = new System.Drawing.Point(453, 374);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnInfo.Size = new System.Drawing.Size(32, 32);
            this.btnInfo.TabIndex = 8;
            this.btnInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ASNC_GUI.Properties.Resources.database;
            this.pictureBox2.InitialImage = global::ASNC_GUI.Properties.Resources.Empty;
            this.pictureBox2.Location = new System.Drawing.Point(243, 188);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ASNC_GUI.Properties.Resources.id_card;
            this.pictureBox1.InitialImage = global::ASNC_GUI.Properties.Resources.Empty;
            this.pictureBox1.Location = new System.Drawing.Point(17, 188);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Enabled = false;
            this.btnExecute.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExecute.FlatAppearance.BorderSize = 0;
            this.btnExecute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnExecute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.Image = global::ASNC_GUI.Properties.Resources.play;
            this.btnExecute.Location = new System.Drawing.Point(413, 415);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnExecute.Size = new System.Drawing.Size(72, 87);
            this.btnExecute.TabIndex = 12;
            this.btnExecute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // amiiboImage
            // 
            this.amiiboImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.amiiboImage.InitialImage = global::ASNC_GUI.Properties.Resources.Empty;
            this.amiiboImage.Location = new System.Drawing.Point(17, 12);
            this.amiiboImage.Name = "amiiboImage";
            this.amiiboImage.Size = new System.Drawing.Size(170, 170);
            this.amiiboImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.amiiboImage.TabIndex = 2;
            this.amiiboImage.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::ASNC_GUI.Properties.Resources.plus;
            this.btnAdd.Location = new System.Drawing.Point(550, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAdd.Size = new System.Drawing.Size(45, 45);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // DeleteAmiibo
            // 
            this.DeleteAmiibo.HeaderText = "";
            this.DeleteAmiibo.Image = global::ASNC_GUI.Properties.Resources.garbage;
            this.DeleteAmiibo.Name = "DeleteAmiibo";
            this.DeleteAmiibo.ReadOnly = true;
            this.DeleteAmiibo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DeleteAmiibo.ToolTipText = "Remove Amiibo from list";
            this.DeleteAmiibo.Width = 32;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1106, 505);
            this.Controls.Add(this.managerPanel);
            this.Controls.Add(this.Divider2);
            this.Controls.Add(this.amiiboPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(976, 502);
            this.Name = "MainForm";
            this.Text = "Amiibo SN Changer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.amiiboPanel.ResumeLayout(false);
            this.gridActionsPanel.ResumeLayout(false);
            this.managerPanel.ResumeLayout(false);
            this.managerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amiiboImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Panel amiiboPanel;
        private System.Windows.Forms.Panel gridActionsPanel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel Divider1;
        private System.Windows.Forms.DataGridViewImageColumn Image;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmiiboName;
        private System.Windows.Forms.DataGridViewImageColumn HasUserData;
        private System.Windows.Forms.DataGridViewImageColumn HasAppData;
        private System.Windows.Forms.DataGridViewImageColumn IsDecrypted;
        private System.Windows.Forms.DataGridViewImageColumn DeleteAmiibo;
        private System.Windows.Forms.PictureBox amiiboImage;
        private System.Windows.Forms.Panel Divider2;
        private System.Windows.Forms.Panel managerPanel;
        private System.Windows.Forms.Panel Divider3;
        private System.Windows.Forms.Label lblAmiiboName;
        private System.Windows.Forms.Label lblWrites;
        private System.Windows.Forms.Label lblNickname;
        private System.Windows.Forms.TextBox tbStatueId;
        private System.Windows.Forms.Label lblAmiiboNr;
        private System.Windows.Forms.TextBox tbOwner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numUpDown;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox tbNickname;
        private System.Windows.Forms.TextBox tbRegisterDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDataSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox tbPlatform;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOutputSelect;
        private System.Windows.Forms.TextBox tbUID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbSerial;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
    }
}

