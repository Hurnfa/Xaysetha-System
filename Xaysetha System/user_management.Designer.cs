namespace Xaysetha_System
{
    partial class user_management
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnAddUser = new Guna.UI2.WinForms.Guna2Button();
            this.labelTotalUser = new System.Windows.Forms.Label();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.data = new Guna.UI2.WinForms.Guna2DataGridView();
            this.userID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editButton = new System.Windows.Forms.DataGridViewImageColumn();
            this.delButton = new System.Windows.Forms.DataGridViewImageColumn();
            this.guna2Panel1.SuspendLayout();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1046, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 766);
            this.panel2.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 766);
            this.panel1.TabIndex = 8;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.Controls.Add(this.label4);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(20, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1026, 44);
            this.guna2Panel1.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(71)))), ((int)(((byte)(61)))));
            this.label4.Location = new System.Drawing.Point(18, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "ຜູ້ໃຊ້ງານລະບົບ";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel2.Location = new System.Drawing.Point(20, 720);
            this.guna2Panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1026, 46);
            this.guna2Panel2.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(71)))), ((int)(((byte)(61)))));
            this.label2.Location = new System.Drawing.Point(18, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "ຄົ້ນຫາ";
            // 
            // txtUserSearch
            // 
            this.txtUserSearch.BorderRadius = 4;
            this.txtUserSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserSearch.DefaultText = "";
            this.txtUserSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtUserSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtUserSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUserSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUserSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUserSearch.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUserSearch.IconLeft = global::Xaysetha_System.Properties.Resources.Search;
            this.txtUserSearch.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtUserSearch.Location = new System.Drawing.Point(22, 49);
            this.txtUserSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUserSearch.Name = "txtUserSearch";
            this.txtUserSearch.PasswordChar = '\0';
            this.txtUserSearch.PlaceholderText = "ປ້ອນລະຫັດ ຫຼື ຊື່ຜູ້ໃຊ້ງານ...";
            this.txtUserSearch.SelectedText = "";
            this.txtUserSearch.Size = new System.Drawing.Size(238, 39);
            this.txtUserSearch.TabIndex = 5;
            this.txtUserSearch.TextChanged += new System.EventHandler(this.txtUserSearch_TextChanged);
            // 
            // btnAddUser
            // 
            this.btnAddUser.BorderRadius = 4;
            this.btnAddUser.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddUser.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddUser.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddUser.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddUser.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(125)))), ((int)(((byte)(201)))));
            this.btnAddUser.Font = new System.Drawing.Font("Noto Sans Lao", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUser.ForeColor = System.Drawing.Color.White;
            this.btnAddUser.Image = global::Xaysetha_System.Properties.Resources.fi_plus;
            this.btnAddUser.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAddUser.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnAddUser.Location = new System.Drawing.Point(833, 49);
            this.btnAddUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(158, 39);
            this.btnAddUser.TabIndex = 18;
            this.btnAddUser.Text = "ເພີ່ມບັນຊີ";
            this.btnAddUser.TextOffset = new System.Drawing.Point(10, 0);
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // labelTotalUser
            // 
            this.labelTotalUser.AutoSize = true;
            this.labelTotalUser.BackColor = System.Drawing.Color.Transparent;
            this.labelTotalUser.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(71)))), ((int)(((byte)(61)))));
            this.labelTotalUser.Location = new System.Drawing.Point(18, 106);
            this.labelTotalUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTotalUser.Name = "labelTotalUser";
            this.labelTotalUser.Size = new System.Drawing.Size(110, 23);
            this.labelTotalUser.TabIndex = 19;
            this.labelTotalUser.Text = "ທັງໝົດ 3 ລາຍການ";
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.BorderRadius = 20;
            this.guna2CustomGradientPanel1.BorderThickness = 1;
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2Panel3);
            this.guna2CustomGradientPanel1.Controls.Add(this.txtUserSearch);
            this.guna2CustomGradientPanel1.Controls.Add(this.label2);
            this.guna2CustomGradientPanel1.Controls.Add(this.btnAddUser);
            this.guna2CustomGradientPanel1.Controls.Add(this.labelTotalUser);
            this.guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(20, 44);
            this.guna2CustomGradientPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(1026, 676);
            this.guna2CustomGradientPanel1.TabIndex = 13;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Controls.Add(this.data);
            this.guna2Panel3.Location = new System.Drawing.Point(22, 132);
            this.guna2Panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(976, 483);
            this.guna2Panel3.TabIndex = 21;
            // 
            // data
            // 
            this.data.AllowUserToResizeColumns = false;
            this.data.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.data.ColumnHeadersHeight = 18;
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userID,
            this.userName,
            this.Surname,
            this.Gender,
            this.role,
            this.Tel,
            this.password,
            this.editButton,
            this.delButton});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Noto Sans Lao", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(125)))), ((int)(((byte)(201)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.data.DefaultCellStyle = dataGridViewCellStyle3;
            this.data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.data.Location = new System.Drawing.Point(0, 0);
            this.data.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.data.Name = "data";
            this.data.ReadOnly = true;
            this.data.RowHeadersVisible = false;
            this.data.RowHeadersWidth = 51;
            this.data.RowTemplate.Height = 24;
            this.data.Size = new System.Drawing.Size(976, 483);
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
            this.data.ThemeStyle.ReadOnly = true;
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
            // userID
            // 
            this.userID.HeaderText = "ລະຫັດຜູ້ໃຊ້";
            this.userID.MinimumWidth = 6;
            this.userID.Name = "userID";
            this.userID.ReadOnly = true;
            // 
            // userName
            // 
            this.userName.HeaderText = "ຊື່";
            this.userName.MinimumWidth = 6;
            this.userName.Name = "userName";
            this.userName.ReadOnly = true;
            // 
            // Surname
            // 
            this.Surname.HeaderText = "ນາມສະກຸນ";
            this.Surname.MinimumWidth = 6;
            this.Surname.Name = "Surname";
            this.Surname.ReadOnly = true;
            // 
            // Gender
            // 
            this.Gender.HeaderText = "ເພດ";
            this.Gender.MinimumWidth = 6;
            this.Gender.Name = "Gender";
            this.Gender.ReadOnly = true;
            // 
            // role
            // 
            this.role.HeaderText = "ຕຳແໜ່ງ";
            this.role.MinimumWidth = 6;
            this.role.Name = "role";
            this.role.ReadOnly = true;
            // 
            // Tel
            // 
            this.Tel.HeaderText = "ເບີໂທ";
            this.Tel.MinimumWidth = 6;
            this.Tel.Name = "Tel";
            this.Tel.ReadOnly = true;
            // 
            // password
            // 
            this.password.HeaderText = "ລະຫັດ";
            this.password.MinimumWidth = 6;
            this.password.Name = "password";
            this.password.ReadOnly = true;
            // 
            // editButton
            // 
            this.editButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.editButton.HeaderText = "";
            this.editButton.Image = global::Xaysetha_System.Properties.Resources.border_color2;
            this.editButton.MinimumWidth = 6;
            this.editButton.Name = "editButton";
            this.editButton.ReadOnly = true;
            this.editButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.editButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.editButton.Width = 20;
            // 
            // delButton
            // 
            this.delButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.delButton.HeaderText = "";
            this.delButton.Image = global::Xaysetha_System.Properties.Resources.Group;
            this.delButton.MinimumWidth = 6;
            this.delButton.Name = "delButton";
            this.delButton.ReadOnly = true;
            this.delButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.delButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.delButton.Width = 20;
            // 
            // user_management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 766);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(1066, 766);
            this.Name = "user_management";
            this.Text = "user_management";
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            this.guna2Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtUserSearch;
        private Guna.UI2.WinForms.Guna2Button btnAddUser;
        private System.Windows.Forms.Label labelTotalUser;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2DataGridView data;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn userID;
        private System.Windows.Forms.DataGridViewTextBoxColumn userName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn role;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewImageColumn editButton;
        private System.Windows.Forms.DataGridViewImageColumn delButton;
    }
}