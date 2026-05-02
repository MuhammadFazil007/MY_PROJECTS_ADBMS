namespace ADBMS_Screens_Project
{
    partial class Dashboard
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
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.sidebar_panel = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnGoReports = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnGoOrders = new System.Windows.Forms.Button();
            this.btnGoCustomers = new System.Windows.Forms.Button();
            this.btnGoMenu = new System.Windows.Forms.Button();
            this.Dashboard_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblCafeStatus = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTotalCustomers = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTodaySales = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvRecentOrders = new System.Windows.Forms.DataGridView();
            this.panelContent = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.sidebar_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).BeginInit();
            this.panelContent.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(1, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(818, 43);
            this.panel2.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SandyBrown;
            this.label2.Location = new System.Drawing.Point(243, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(324, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cafe Shop Management System";
            // 
            // sidebar_panel
            // 
            this.sidebar_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sidebar_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sidebar_panel.Controls.Add(this.btnLogout);
            this.sidebar_panel.Controls.Add(this.btnGoReports);
            this.sidebar_panel.Controls.Add(this.button4);
            this.sidebar_panel.Controls.Add(this.btnGoOrders);
            this.sidebar_panel.Controls.Add(this.btnGoCustomers);
            this.sidebar_panel.Controls.Add(this.btnGoMenu);
            this.sidebar_panel.Controls.Add(this.Dashboard_button);
            this.sidebar_panel.Location = new System.Drawing.Point(1, 41);
            this.sidebar_panel.Name = "sidebar_panel";
            this.sidebar_panel.Size = new System.Drawing.Size(118, 490);
            this.sidebar_panel.TabIndex = 10;
            // 
            // btnLogout
            // 
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLogout.Location = new System.Drawing.Point(-3, 172);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(201, 30);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnGoReports
            // 
            this.btnGoReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoReports.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoReports.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGoReports.Location = new System.Drawing.Point(-2, 143);
            this.btnGoReports.Name = "btnGoReports";
            this.btnGoReports.Size = new System.Drawing.Size(118, 30);
            this.btnGoReports.TabIndex = 2;
            this.btnGoReports.Text = "Reports";
            this.btnGoReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoReports.UseVisualStyleBackColor = true;
            this.btnGoReports.Click += new System.EventHandler(this.btnGoReports_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button4.Location = new System.Drawing.Point(-2, 114);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(200, 30);
            this.button4.TabIndex = 2;
            this.button4.Text = "Billing";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // btnGoOrders
            // 
            this.btnGoOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoOrders.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoOrders.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGoOrders.Location = new System.Drawing.Point(-2, 85);
            this.btnGoOrders.Name = "btnGoOrders";
            this.btnGoOrders.Size = new System.Drawing.Size(200, 30);
            this.btnGoOrders.TabIndex = 2;
            this.btnGoOrders.Text = "Orders";
            this.btnGoOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoOrders.UseVisualStyleBackColor = true;
            this.btnGoOrders.Click += new System.EventHandler(this.btnGoOrders_Click);
            // 
            // btnGoCustomers
            // 
            this.btnGoCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGoCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoCustomers.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoCustomers.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGoCustomers.Location = new System.Drawing.Point(-2, 56);
            this.btnGoCustomers.Name = "btnGoCustomers";
            this.btnGoCustomers.Size = new System.Drawing.Size(118, 30);
            this.btnGoCustomers.TabIndex = 2;
            this.btnGoCustomers.Text = "Customers";
            this.btnGoCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoCustomers.UseVisualStyleBackColor = false;
            this.btnGoCustomers.Click += new System.EventHandler(this.btnGoCustomers_Click);
            // 
            // btnGoMenu
            // 
            this.btnGoMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGoMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoMenu.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoMenu.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGoMenu.Location = new System.Drawing.Point(-2, 27);
            this.btnGoMenu.Name = "btnGoMenu";
            this.btnGoMenu.Size = new System.Drawing.Size(118, 30);
            this.btnGoMenu.TabIndex = 2;
            this.btnGoMenu.Text = "Menu";
            this.btnGoMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoMenu.UseVisualStyleBackColor = false;
            this.btnGoMenu.Click += new System.EventHandler(this.btnGoMenu_Click);
            // 
            // Dashboard_button
            // 
            this.Dashboard_button.BackColor = System.Drawing.Color.Chocolate;
            this.Dashboard_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Dashboard_button.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dashboard_button.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Dashboard_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Dashboard_button.ImageKey = "(none)";
            this.Dashboard_button.Location = new System.Drawing.Point(-2, -2);
            this.Dashboard_button.Name = "Dashboard_button";
            this.Dashboard_button.Size = new System.Drawing.Size(118, 30);
            this.Dashboard_button.TabIndex = 2;
            this.Dashboard_button.Text = "Dashboard";
            this.Dashboard_button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Dashboard_button.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.lblCafeStatus);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Location = new System.Drawing.Point(121, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(698, 54);
            this.panel1.TabIndex = 12;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.SandyBrown;
            this.lblWelcome.Location = new System.Drawing.Point(4, 5);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(71, 20);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Tag = "";
            this.lblWelcome.Text = "Welcome";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.SandyBrown;
            this.lblDate.Location = new System.Drawing.Point(540, -1);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 20);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "_____";
            // 
            // lblCafeStatus
            // 
            this.lblCafeStatus.AutoSize = true;
            this.lblCafeStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCafeStatus.ForeColor = System.Drawing.Color.SandyBrown;
            this.lblCafeStatus.Location = new System.Drawing.Point(541, 25);
            this.lblCafeStatus.Name = "lblCafeStatus";
            this.lblCafeStatus.Size = new System.Drawing.Size(48, 17);
            this.lblCafeStatus.TabIndex = 13;
            this.lblCafeStatus.Text = "________";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.lblTotalCustomers);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Location = new System.Drawing.Point(583, 100);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(236, 125);
            this.panel5.TabIndex = 13;
            // 
            // lblTotalCustomers
            // 
            this.lblTotalCustomers.AutoSize = true;
            this.lblTotalCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalCustomers.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCustomers.ForeColor = System.Drawing.Color.SandyBrown;
            this.lblTotalCustomers.Location = new System.Drawing.Point(0, 42);
            this.lblTotalCustomers.Name = "lblTotalCustomers";
            this.lblTotalCustomers.Size = new System.Drawing.Size(51, 40);
            this.lblTotalCustomers.TabIndex = 3;
            this.lblTotalCustomers.Text = "52";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.SandyBrown;
            this.label12.Location = new System.Drawing.Point(3, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 17);
            this.label12.TabIndex = 3;
            this.label12.Text = "Registered";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SandyBrown;
            this.label13.Location = new System.Drawing.Point(3, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 17);
            this.label13.TabIndex = 3;
            this.label13.Text = "Total Customers";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.lblTotalOrders);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Location = new System.Drawing.Point(351, 100);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(217, 125);
            this.panel4.TabIndex = 14;
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalOrders.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrders.ForeColor = System.Drawing.Color.SandyBrown;
            this.lblTotalOrders.Location = new System.Drawing.Point(-2, 42);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(68, 40);
            this.lblTotalOrders.TabIndex = 3;
            this.lblTotalOrders.Text = "105";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.SandyBrown;
            this.label9.Location = new System.Drawing.Point(3, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 17);
            this.label9.TabIndex = 3;
            this.label9.Text = "All Time";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.SandyBrown;
            this.label10.Location = new System.Drawing.Point(3, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 17);
            this.label10.TabIndex = 3;
            this.label10.Text = "Total Orders";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.lblTodaySales);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(121, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(216, 125);
            this.panel3.TabIndex = 15;
            // 
            // lblTodaySales
            // 
            this.lblTodaySales.AutoSize = true;
            this.lblTodaySales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTodaySales.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTodaySales.ForeColor = System.Drawing.Color.SandyBrown;
            this.lblTodaySales.Location = new System.Drawing.Point(-4, 44);
            this.lblTodaySales.Name = "lblTodaySales";
            this.lblTodaySales.Size = new System.Drawing.Size(136, 40);
            this.lblTodaySales.TabIndex = 3;
            this.lblTodaySales.Text = "RS. 2500";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.SandyBrown;
            this.label7.Location = new System.Drawing.Point(3, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "Today\'s Sales";
            // 
            // dgvRecentOrders
            // 
            this.dgvRecentOrders.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentOrders.Location = new System.Drawing.Point(121, 273);
            this.dgvRecentOrders.Name = "dgvRecentOrders";
            this.dgvRecentOrders.Size = new System.Drawing.Size(447, 255);
            this.dgvRecentOrders.TabIndex = 3;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.panel7);
            this.panelContent.Controls.Add(this.panel6);
            this.panelContent.Location = new System.Drawing.Point(1, 3);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(831, 528);
            this.panelContent.TabIndex = 16;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel6.Controls.Add(this.label1);
            this.panel6.Location = new System.Drawing.Point(120, 224);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(216, 42);
            this.panel6.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SandyBrown;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Recent Orders";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel7.Controls.Add(this.label3);
            this.panel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel7.Location = new System.Drawing.Point(582, 270);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(236, 255);
            this.panel7.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 80.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SandyBrown;
            this.label3.Location = new System.Drawing.Point(28, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 142);
            this.label3.TabIndex = 0;
            this.label3.Text = "☕";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 532);
            this.Controls.Add(this.dgvRecentOrders);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.sidebar_panel);
            this.Controls.Add(this.panelContent);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.sidebar_panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel sidebar_panel;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnGoReports;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnGoOrders;
        private System.Windows.Forms.Button btnGoCustomers;
        private System.Windows.Forms.Button btnGoMenu;
        private System.Windows.Forms.Button Dashboard_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblCafeStatus;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTodaySales;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvRecentOrders;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}