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
            this.DeleteAmiibo = new System.Windows.Forms.DataGridViewImageColumn();
            this.amiiboPanel = new System.Windows.Forms.Panel();
            this.Divider1 = new System.Windows.Forms.Panel();
            this.gridActionsPanel = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.amiiboImage = new System.Windows.Forms.PictureBox();
            this.Divider2 = new System.Windows.Forms.Panel();
            this.managerPanel = new System.Windows.Forms.Panel();
            this.Divider3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.amiiboPanel.SuspendLayout();
            this.gridActionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amiiboImage)).BeginInit();
            this.managerPanel.SuspendLayout();
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
            this.dataGrid.Size = new System.Drawing.Size(591, 460);
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
            this.amiiboPanel.Size = new System.Drawing.Size(590, 505);
            this.amiiboPanel.TabIndex = 1;
            // 
            // Divider1
            // 
            this.Divider1.BackColor = System.Drawing.Color.Silver;
            this.Divider1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Divider1.Location = new System.Drawing.Point(0, 45);
            this.Divider1.Name = "Divider1";
            this.Divider1.Size = new System.Drawing.Size(590, 1);
            this.Divider1.TabIndex = 2;
            // 
            // gridActionsPanel
            // 
            this.gridActionsPanel.Controls.Add(this.btnAdd);
            this.gridActionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridActionsPanel.Location = new System.Drawing.Point(0, 0);
            this.gridActionsPanel.Name = "gridActionsPanel";
            this.gridActionsPanel.Size = new System.Drawing.Size(590, 45);
            this.gridActionsPanel.TabIndex = 2;
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
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(545, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAdd.Size = new System.Drawing.Size(45, 45);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // amiiboImage
            // 
            this.amiiboImage.InitialImage = global::ASNC_GUI.Properties.Resources.Empty;
            this.amiiboImage.Location = new System.Drawing.Point(17, 12);
            this.amiiboImage.Name = "amiiboImage";
            this.amiiboImage.Size = new System.Drawing.Size(200, 200);
            this.amiiboImage.TabIndex = 2;
            this.amiiboImage.TabStop = false;
            // 
            // Divider2
            // 
            this.Divider2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Divider2.BackColor = System.Drawing.Color.Silver;
            this.Divider2.Location = new System.Drawing.Point(597, 12);
            this.Divider2.Name = "Divider2";
            this.Divider2.Size = new System.Drawing.Size(1, 481);
            this.Divider2.TabIndex = 3;
            // 
            // managerPanel
            // 
            this.managerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.managerPanel.Controls.Add(this.Divider3);
            this.managerPanel.Controls.Add(this.amiiboImage);
            this.managerPanel.Location = new System.Drawing.Point(604, 0);
            this.managerPanel.Name = "managerPanel";
            this.managerPanel.Size = new System.Drawing.Size(450, 505);
            this.managerPanel.TabIndex = 4;
            // 
            // Divider3
            // 
            this.Divider3.BackColor = System.Drawing.Color.Silver;
            this.Divider3.Location = new System.Drawing.Point(13, 443);
            this.Divider3.Name = "Divider3";
            this.Divider3.Size = new System.Drawing.Size(422, 1);
            this.Divider3.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1054, 505);
            this.Controls.Add(this.managerPanel);
            this.Controls.Add(this.Divider2);
            this.Controls.Add(this.amiiboPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(620, 460);
            this.Name = "MainForm";
            this.Text = "Amiibo SN Changer - Manager";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.amiiboPanel.ResumeLayout(false);
            this.gridActionsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.amiiboImage)).EndInit();
            this.managerPanel.ResumeLayout(false);
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
    }
}

