namespace Xaysetha_System
{
    partial class resident_management
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnResident = new Guna.UI2.WinForms.Guna2Button();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.cbGender = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cbVillage = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.data = new Guna.UI2.WinForms.Guna2DataGridView();
            this.citizenID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.citizenName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VillageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Religious = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Job = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editButton = new System.Windows.Forms.DataGridViewImageColumn();
            this.delButton = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtNameSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnResident
            // 
            this.btnResident.BorderRadius = 4;
            this.btnResident.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnResident.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnResident.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnResident.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnResident.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(125)))), ((int)(((byte)(201)))));
            this.btnResident.Font = new System.Drawing.Font("Noto Sans Lao", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResident.ForeColor = System.Drawing.Color.White;
            this.btnResident.Image = global::Xaysetha_System.Properties.Resources.fi_plus;
            this.btnResident.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnResident.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnResident.Location = new System.Drawing.Point(1250, 75);
            this.btnResident.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnResident.Name = "btnResident";
            this.btnResident.Size = new System.Drawing.Size(237, 60);
            this.btnResident.TabIndex = 18;
            this.btnResident.Text = "ເພີ່ມຂໍ້ມູນເຈົ້າຂອງບ້ານ";
            this.btnResident.TextOffset = new System.Drawing.Point(10, 0);
            this.btnResident.Click += new System.EventHandler(this.btnResident_Click);
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.BorderRadius = 20;
            this.guna2CustomGradientPanel1.BorderThickness = 1;
            this.guna2CustomGradientPanel1.Controls.Add(this.cbGender);
            this.guna2CustomGradientPanel1.Controls.Add(this.label16);
            this.guna2CustomGradientPanel1.Controls.Add(this.cbVillage);
            this.guna2CustomGradientPanel1.Controls.Add(this.label15);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2Panel3);
            this.guna2CustomGradientPanel1.Controls.Add(this.txtNameSearch);
            this.guna2CustomGradientPanel1.Controls.Add(this.label2);
            this.guna2CustomGradientPanel1.Controls.Add(this.btnResident);
            this.guna2CustomGradientPanel1.Controls.Add(this.labelTotal);
            this.guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(30, 68);
            this.guna2CustomGradientPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(1539, 1040);
            this.guna2CustomGradientPanel1.TabIndex = 23;
            // 
            // cbGender
            // 
            this.cbGender.BackColor = System.Drawing.Color.Transparent;
            this.cbGender.BorderRadius = 4;
            this.cbGender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGender.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbGender.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbGender.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F);
            this.cbGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(71)))), ((int)(((byte)(61)))));
            this.cbGender.ItemHeight = 30;
            this.cbGender.Items.AddRange(new object[] {
            " ",
            "ຊາຍ",
            "ຍິງ",
            "ບໍ່ລະບຸ"});
            this.cbGender.Location = new System.Drawing.Point(708, 74);
            this.cbGender.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(220, 36);
            this.cbGender.TabIndex = 25;
            this.cbGender.SelectedIndexChanged += new System.EventHandler(this.cbGender_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(71)))), ((int)(((byte)(61)))));
            this.label16.Location = new System.Drawing.Point(702, 35);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 35);
            this.label16.TabIndex = 24;
            this.label16.Text = "ເພດ";
            // 
            // cbVillage
            // 
            this.cbVillage.BackColor = System.Drawing.Color.Transparent;
            this.cbVillage.BorderRadius = 4;
            this.cbVillage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbVillage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVillage.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbVillage.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbVillage.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVillage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(71)))), ((int)(((byte)(61)))));
            this.cbVillage.ItemHeight = 30;
            this.cbVillage.Items.AddRange(new object[] {
            "ບ້ານທັງໝົດ"});
            this.cbVillage.Location = new System.Drawing.Point(451, 74);
            this.cbVillage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbVillage.Name = "cbVillage";
            this.cbVillage.Size = new System.Drawing.Size(220, 36);
            this.cbVillage.TabIndex = 23;
            this.cbVillage.SelectedIndexChanged += new System.EventHandler(this.cbVillage_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(71)))), ((int)(((byte)(61)))));
            this.label15.Location = new System.Drawing.Point(446, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 35);
            this.label15.TabIndex = 22;
            this.label15.Text = "ບ້ານ";
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Controls.Add(this.data);
            this.guna2Panel3.Location = new System.Drawing.Point(33, 318);
            this.guna2Panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(1464, 615);
            this.guna2Panel3.TabIndex = 21;
            // 
            // data
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.data.ColumnHeadersHeight = 18;
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.citizenID,
            this.citizenName,
            this.Surname,
            this.VillageName,
            this.Tel,
            this.Gender,
            this.Religious,
            this.Job,
            this.editButton,
            this.delButton});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(125)))), ((int)(((byte)(201)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.data.DefaultCellStyle = dataGridViewCellStyle9;
            this.data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.data.Location = new System.Drawing.Point(0, 0);
            this.data.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.data.Name = "data";
            this.data.RowHeadersVisible = false;
            this.data.RowHeadersWidth = 51;
            this.data.RowTemplate.Height = 24;
            this.data.Size = new System.Drawing.Size(1464, 615);
            this.data.TabIndex = 20;
            this.data.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.data.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.data.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.data.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.data.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.data.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.data.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.data.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.data.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.data.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.data.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.data.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.data.ThemeStyle.HeaderStyle.Height = 18;
            this.data.ThemeStyle.ReadOnly = false;
            this.data.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.data.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.data.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.data.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.data.ThemeStyle.RowsStyle.Height = 24;
            this.data.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.data.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.data.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_CellContentClick);
            this.data.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.data_CellPainting);
            // 
            // citizenID
            // 
            this.citizenID.HeaderText = "ລະຫັດບຸກຄົນ";
            this.citizenID.MinimumWidth = 6;
            this.citizenID.Name = "citizenID";
            // 
            // citizenName
            // 
            this.citizenName.HeaderText = "ຊື່";
            this.citizenName.MinimumWidth = 6;
            this.citizenName.Name = "citizenName";
            // 
            // Surname
            // 
            this.Surname.HeaderText = "ນາມສະກຸນ";
            this.Surname.MinimumWidth = 6;
            this.Surname.Name = "Surname";
            // 
            // VillageName
            // 
            this.VillageName.HeaderText = "ບ້ານ";
            this.VillageName.MinimumWidth = 6;
            this.VillageName.Name = "VillageName";
            // 
            // Tel
            // 
            this.Tel.HeaderText = "ເບີໂທ";
            this.Tel.MinimumWidth = 6;
            this.Tel.Name = "Tel";
            // 
            // Gender
            // 
            this.Gender.HeaderText = "ເພດ";
            this.Gender.MinimumWidth = 6;
            this.Gender.Name = "Gender";
            // 
            // Religious
            // 
            this.Religious.HeaderText = "ສາສະໜາ";
            this.Religious.MinimumWidth = 6;
            this.Religious.Name = "Religious";
            // 
            // Job
            // 
            this.Job.HeaderText = "ອາຊີບ";
            this.Job.MinimumWidth = 6;
            this.Job.Name = "Job";
            // 
            // editButton
            // 
            this.editButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.editButton.HeaderText = "ຈັດການ";
            this.editButton.Image = global::Xaysetha_System.Properties.Resources.border_color2;
            this.editButton.MinimumWidth = 6;
            this.editButton.Name = "editButton";
            this.editButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.editButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.editButton.Width = 79;
            // 
            // delButton
            // 
            this.delButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.delButton.HeaderText = "ລົບ";
            this.delButton.Image = global::Xaysetha_System.Properties.Resources.Group;
            this.delButton.MinimumWidth = 6;
            this.delButton.Name = "delButton";
            this.delButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.delButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.delButton.Width = 58;
            // 
            // txtNameSearch
            // 
            this.txtNameSearch.BorderRadius = 4;
            this.txtNameSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNameSearch.DefaultText = "";
            this.txtNameSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNameSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNameSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNameSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNameSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNameSearch.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNameSearch.IconLeft = global::Xaysetha_System.Properties.Resources.Search;
            this.txtNameSearch.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtNameSearch.Location = new System.Drawing.Point(33, 75);
            this.txtNameSearch.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtNameSearch.Name = "txtNameSearch";
            this.txtNameSearch.PasswordChar = '\0';
            this.txtNameSearch.PlaceholderText = "ຊື່ເຈົ້າຂອງບ້ານ";
            this.txtNameSearch.SelectedText = "";
            this.txtNameSearch.Size = new System.Drawing.Size(378, 60);
            this.txtNameSearch.TabIndex = 5;
            this.txtNameSearch.TextChanged += new System.EventHandler(this.txtNameSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(71)))), ((int)(((byte)(61)))));
            this.label2.Location = new System.Drawing.Point(27, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 35);
            this.label2.TabIndex = 4;
            this.label2.Text = "ຄົ້ນຫາ";
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.BackColor = System.Drawing.Color.Transparent;
            this.labelTotal.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(71)))), ((int)(((byte)(61)))));
            this.labelTotal.Location = new System.Drawing.Point(27, 278);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(160, 35);
            this.labelTotal.TabIndex = 19;
            this.labelTotal.Text = "ທັງໝົດ 0 ລາຍການ";
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::Xaysetha_System.Properties.Resources.delete;
            this.dataGridViewImageColumn2.MinimumWidth = 6;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn2.Width = 150;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel2.Location = new System.Drawing.Point(30, 1108);
            this.guna2Panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1539, 71);
            this.guna2Panel2.TabIndex = 22;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::Xaysetha_System.Properties.Resources.edit;
            this.dataGridViewImageColumn1.MinimumWidth = 6;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 150;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.Controls.Add(this.label4);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(30, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1539, 68);
            this.guna2Panel1.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(71)))), ((int)(((byte)(61)))));
            this.label4.Location = new System.Drawing.Point(27, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 35);
            this.label4.TabIndex = 6;
            this.label4.Text = "ຂໍ້ມູນເຈົ້າຂອງບ້ານ";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(30, 1179);
            this.panel1.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1569, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 1179);
            this.panel2.TabIndex = 20;
            // 
            // resident_management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1599, 1179);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1599, 1179);
            this.MinimumSize = new System.Drawing.Size(1599, 1179);
            this.Name = "resident_management";
            this.Text = "resident_management";
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            this.guna2Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnResident;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2DataGridView data;
        private Guna.UI2.WinForms.Guna2TextBox txtNameSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2ComboBox cbGender;
        private System.Windows.Forms.Label label16;
        private Guna.UI2.WinForms.Guna2ComboBox cbVillage;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn citizenID;
        private System.Windows.Forms.DataGridViewTextBoxColumn citizenName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn VillageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn Religious;
        private System.Windows.Forms.DataGridViewTextBoxColumn Job;
        private System.Windows.Forms.DataGridViewImageColumn editButton;
        private System.Windows.Forms.DataGridViewImageColumn delButton;
    }
}