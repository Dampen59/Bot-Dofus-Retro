﻿namespace Bot_Dofus_1._29._1.Forms
{
    partial class GestionAccounts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionAccounts));
            this.tabControlPrincipalAccounts = new System.Windows.Forms.TabControl();
            this.ListaAccounts = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox_informacion = new System.Windows.Forms.PictureBox();
            this.label_informacionClickAccounts = new System.Windows.Forms.Label();
            this.listViewAccounts = new System.Windows.Forms.ListView();
            this.ColumnaNombreAccount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnaNombreServidor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnaNombrePersonaje = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripFormAccounts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.conectarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contraseñaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nombreDelPersonajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AgregarAccount = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox_informacion_agregar_cuenta = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_Agregar_Retroceder = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label_Nombre_Account = new System.Windows.Forms.Label();
            this.label_Password = new System.Windows.Forms.Label();
            this.label_Eleccion_Servidor = new System.Windows.Forms.Label();
            this.label_Nombre_Personaje = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_nombre_personaje = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox_Servidor = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Nombre_Account = new System.Windows.Forms.TextBox();
            this.boton_Agregar_Account = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.imagenesFormAccounts = new System.Windows.Forms.ImageList(this.components);
            this.tabControlPrincipalAccounts.SuspendLayout();
            this.ListaAccounts.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_informacion)).BeginInit();
            this.contextMenuStripFormAccounts.SuspendLayout();
            this.AgregarAccount.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_informacion_agregar_cuenta)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlPrincipalAccounts
            // 
            this.tabControlPrincipalAccounts.Controls.Add(this.ListaAccounts);
            this.tabControlPrincipalAccounts.Controls.Add(this.AgregarAccount);
            this.tabControlPrincipalAccounts.Controls.Add(this.tabPage1);
            this.tabControlPrincipalAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPrincipalAccounts.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tabControlPrincipalAccounts.ImageList = this.imagenesFormAccounts;
            this.tabControlPrincipalAccounts.ItemSize = new System.Drawing.Size(137, 28);
            this.tabControlPrincipalAccounts.Location = new System.Drawing.Point(0, 0);
            this.tabControlPrincipalAccounts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControlPrincipalAccounts.Name = "tabControlPrincipalAccounts";
            this.tabControlPrincipalAccounts.SelectedIndex = 0;
            this.tabControlPrincipalAccounts.Size = new System.Drawing.Size(463, 398);
            this.tabControlPrincipalAccounts.TabIndex = 0;
            // 
            // ListaAccounts
            // 
            this.ListaAccounts.Controls.Add(this.tableLayoutPanel1);
            this.ListaAccounts.ImageKey = "lista_cuentas.png";
            this.ListaAccounts.Location = new System.Drawing.Point(4, 32);
            this.ListaAccounts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ListaAccounts.Name = "ListaAccounts";
            this.ListaAccounts.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ListaAccounts.Size = new System.Drawing.Size(455, 362);
            this.ListaAccounts.TabIndex = 0;
            this.ListaAccounts.Text = "List of accounts";
            this.ListaAccounts.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewAccounts, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.00565F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.99435F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(449, 354);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.15801F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.84199F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox_informacion, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_informacionClickAccounts, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 311);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(443, 40);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // pictureBox_informacion
            // 
            this.pictureBox_informacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_informacion.Image = global::Bot_Dofus_1._29._1.Properties.Resources.informacion;
            this.pictureBox_informacion.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_informacion.Name = "pictureBox_informacion";
            this.pictureBox_informacion.Size = new System.Drawing.Size(38, 34);
            this.pictureBox_informacion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_informacion.TabIndex = 0;
            this.pictureBox_informacion.TabStop = false;
            // 
            // label_informacionClickAccounts
            // 
            this.label_informacionClickAccounts.AutoSize = true;
            this.label_informacionClickAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_informacionClickAccounts.Location = new System.Drawing.Point(47, 0);
            this.label_informacionClickAccounts.Name = "label_informacionClickAccounts";
            this.label_informacionClickAccounts.Size = new System.Drawing.Size(393, 40);
            this.label_informacionClickAccounts.TabIndex = 1;
            this.label_informacionClickAccounts.Text = "Right click to connect / modify / delete an account\rdouble click on d" +
    "e an account to connect it";
            this.label_informacionClickAccounts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listViewAccounts
            // 
            this.listViewAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnaNombreAccount,
            this.ColumnaNombreServidor,
            this.ColumnaNombrePersonaje});
            this.listViewAccounts.ContextMenuStrip = this.contextMenuStripFormAccounts;
            this.listViewAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAccounts.FullRowSelect = true;
            this.listViewAccounts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewAccounts.HideSelection = false;
            this.listViewAccounts.Location = new System.Drawing.Point(3, 4);
            this.listViewAccounts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listViewAccounts.Name = "listViewAccounts";
            this.listViewAccounts.Size = new System.Drawing.Size(443, 300);
            this.listViewAccounts.TabIndex = 1;
            this.listViewAccounts.UseCompatibleStateImageBehavior = false;
            this.listViewAccounts.View = System.Windows.Forms.View.Details;
            this.listViewAccounts.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listViewAccounts_ColumnWidthChanging);
            this.listViewAccounts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAccounts_MouseDoubleClick);
            // 
            // ColumnaNombreAccount
            // 
            this.ColumnaNombreAccount.Text = "Account Name";
            this.ColumnaNombreAccount.Width = 148;
            // 
            // ColumnaNombreServidor
            // 
            this.ColumnaNombreServidor.Text = "Server";
            this.ColumnaNombreServidor.Width = 107;
            // 
            // ColumnaNombrePersonaje
            // 
            this.ColumnaNombrePersonaje.Text = "Character Name";
            this.ColumnaNombrePersonaje.Width = 184;
            // 
            // contextMenuStripFormAccounts
            // 
            this.contextMenuStripFormAccounts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conectarToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStripFormAccounts.Name = "contextMenuStripFormAccounts";
            this.contextMenuStripFormAccounts.Size = new System.Drawing.Size(126, 70);
            // 
            // conectarToolStripMenuItem
            // 
            this.conectarToolStripMenuItem.Image = global::Bot_Dofus_1._29._1.Properties.Resources.flecha_direccion_izquierda;
            this.conectarToolStripMenuItem.Name = "conectarToolStripMenuItem";
            this.conectarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.conectarToolStripMenuItem.Text = "Connect";
            this.conectarToolStripMenuItem.Click += new System.EventHandler(this.conectarToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cuentaToolStripMenuItem,
            this.contraseñaToolStripMenuItem,
            this.nombreDelPersonajeToolStripMenuItem});
            this.modificarToolStripMenuItem.Image = global::Bot_Dofus_1._29._1.Properties.Resources.boton_ajustes;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.modificarToolStripMenuItem.Text = "Modify";
            // 
            // cuentaToolStripMenuItem
            // 
            this.cuentaToolStripMenuItem.Name = "cuentaToolStripMenuItem";
            this.cuentaToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.cuentaToolStripMenuItem.Text = "Account";
            this.cuentaToolStripMenuItem.Click += new System.EventHandler(this.modificar_Account);
            // 
            // contraseñaToolStripMenuItem
            // 
            this.contraseñaToolStripMenuItem.Name = "contraseñaToolStripMenuItem";
            this.contraseñaToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.contraseñaToolStripMenuItem.Text = "Password";
            this.contraseñaToolStripMenuItem.Click += new System.EventHandler(this.modificar_Account);
            // 
            // nombreDelPersonajeToolStripMenuItem
            // 
            this.nombreDelPersonajeToolStripMenuItem.Name = "nombreDelPersonajeToolStripMenuItem";
            this.nombreDelPersonajeToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.nombreDelPersonajeToolStripMenuItem.Text = "Character Name";
            this.nombreDelPersonajeToolStripMenuItem.Click += new System.EventHandler(this.modificar_Account);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::Bot_Dofus_1._29._1.Properties.Resources.cruz_roja;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.eliminarToolStripMenuItem.Text = "Delete";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // AgregarAccount
            // 
            this.AgregarAccount.Controls.Add(this.tableLayoutPanel3);
            this.AgregarAccount.ImageKey = "agregar_cuenta.png";
            this.AgregarAccount.Location = new System.Drawing.Point(4, 32);
            this.AgregarAccount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AgregarAccount.Name = "Add Account";
            this.AgregarAccount.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AgregarAccount.Size = new System.Drawing.Size(455, 362);
            this.AgregarAccount.TabIndex = 1;
            this.AgregarAccount.Text = "Add an account";
            this.AgregarAccount.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBox_Agregar_Retroceder, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.71429F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.28571F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.714286F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(449, 354);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.374384F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.62562F));
            this.tableLayoutPanel4.Controls.Add(this.pictureBox_informacion_agregar_cuenta, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(443, 35);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // pictureBox_informacion_agregar_cuenta
            // 
            this.pictureBox_informacion_agregar_cuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_informacion_agregar_cuenta.Image = global::Bot_Dofus_1._29._1.Properties.Resources.informacion;
            this.pictureBox_informacion_agregar_cuenta.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_informacion_agregar_cuenta.Name = "pictureBox_informacion_agregar_cuenta";
            this.pictureBox_informacion_agregar_cuenta.Size = new System.Drawing.Size(31, 29);
            this.pictureBox_informacion_agregar_cuenta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_informacion_agregar_cuenta.TabIndex = 1;
            this.pictureBox_informacion_agregar_cuenta.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(40, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(400, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Leave the \"Character\" field blank if you want the bot to connect the first character " +
    "account";
            // 
            // checkBox_Agregar_Retroceder
            // 
            this.checkBox_Agregar_Retroceder.AutoSize = true;
            this.checkBox_Agregar_Retroceder.Checked = true;
            this.checkBox_Agregar_Retroceder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Agregar_Retroceder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox_Agregar_Retroceder.Location = new System.Drawing.Point(3, 329);
            this.checkBox_Agregar_Retroceder.Name = "checkBox_Agregar_Retroceder";
            this.checkBox_Agregar_Retroceder.Size = new System.Drawing.Size(443, 22);
            this.checkBox_Agregar_Retroceder.TabIndex = 51;
            this.checkBox_Agregar_Retroceder.Text = "Return to the \"List of Accounts\" tab after adding the account.";
            this.checkBox_Agregar_Retroceder.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.boton_Agregar_Account, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 44);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.63636F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.36364F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(443, 279);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.96552F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.03448F));
            this.tableLayoutPanel6.Controls.Add(this.label_Nombre_Account, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label_Password, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label_Eleccion_Servidor, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.label_Nombre_Personaje, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel9, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel10, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(437, 238);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // label_Nombre_Account
            // 
            this.label_Nombre_Account.AutoSize = true;
            this.label_Nombre_Account.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Nombre_Account.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Nombre_Account.Location = new System.Drawing.Point(3, 0);
            this.label_Nombre_Account.Name = "label_Nombre_Account";
            this.label_Nombre_Account.Size = new System.Drawing.Size(120, 59);
            this.label_Nombre_Account.TabIndex = 1;
            this.label_Nombre_Account.Text = "Account:";
            this.label_Nombre_Account.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Password.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label_Password.Location = new System.Drawing.Point(3, 59);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(120, 59);
            this.label_Password.TabIndex = 3;
            this.label_Password.Text = "Password:";
            this.label_Password.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Eleccion_Servidor
            // 
            this.label_Eleccion_Servidor.AutoSize = true;
            this.label_Eleccion_Servidor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Eleccion_Servidor.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label_Eleccion_Servidor.Location = new System.Drawing.Point(3, 118);
            this.label_Eleccion_Servidor.Name = "label_Eleccion_Servidor";
            this.label_Eleccion_Servidor.Size = new System.Drawing.Size(120, 59);
            this.label_Eleccion_Servidor.TabIndex = 5;
            this.label_Eleccion_Servidor.Text = "Server:";
            this.label_Eleccion_Servidor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Nombre_Personaje
            // 
            this.label_Nombre_Personaje.AutoSize = true;
            this.label_Nombre_Personaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Nombre_Personaje.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label_Nombre_Personaje.Location = new System.Drawing.Point(3, 177);
            this.label_Nombre_Personaje.Name = "label_Nombre_Personaje";
            this.label_Nombre_Personaje.Size = new System.Drawing.Size(120, 61);
            this.label_Nombre_Personaje.TabIndex = 7;
            this.label_Nombre_Personaje.Text = "Character Name:";
            this.label_Nombre_Personaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.textBox_nombre_personaje, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(129, 180);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(305, 55);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // textBox_nombre_personaje
            // 
            this.textBox_nombre_personaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_nombre_personaje.Location = new System.Drawing.Point(3, 21);
            this.textBox_nombre_personaje.MaxLength = 25;
            this.textBox_nombre_personaje.Name = "textBox_nombre_personaje";
            this.textBox_nombre_personaje.Size = new System.Drawing.Size(299, 25);
            this.textBox_nombre_personaje.TabIndex = 5;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.comboBox_Servidor, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(129, 121);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.39F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.61F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(305, 53);
            this.tableLayoutPanel8.TabIndex = 3;
            // 
            // comboBox_Servidor
            // 
            this.comboBox_Servidor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_Servidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Servidor.FormattingEnabled = true;
            this.comboBox_Servidor.Items.AddRange(new object[] {
            "Eratz",
            "Henual",
            "Clustus",
            "Nabur",
            "Arty",
            "Agathe",
            "Hogmeiser",
            "Droupik",
            "Bilby",
            "Ayuto"});
            this.comboBox_Servidor.Location = new System.Drawing.Point(3, 16);
            this.comboBox_Servidor.Name = "comboBox_Servidor";
            this.comboBox_Servidor.Size = new System.Drawing.Size(299, 25);
            this.comboBox_Servidor.TabIndex = 6;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.textBox_Password, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(129, 62);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 3;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(305, 53);
            this.tableLayoutPanel9.TabIndex = 2;
            // 
            // textBox_Password
            // 
            this.textBox_Password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Password.Location = new System.Drawing.Point(3, 20);
            this.textBox_Password.MaxLength = 25;
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(299, 25);
            this.textBox_Password.TabIndex = 4;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.textBox_Nombre_Account, 0, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(129, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(305, 53);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // textBox_Nombre_Account
            // 
            this.textBox_Nombre_Account.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Nombre_Account.Location = new System.Drawing.Point(3, 20);
            this.textBox_Nombre_Account.MaxLength = 25;
            this.textBox_Nombre_Account.Name = "textBox_Nombre_Account";
            this.textBox_Nombre_Account.Size = new System.Drawing.Size(299, 25);
            this.textBox_Nombre_Account.TabIndex = 2;
            // 
            // boton_Agregar_Account
            // 
            this.boton_Agregar_Account.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boton_Agregar_Account.Location = new System.Drawing.Point(3, 247);
            this.boton_Agregar_Account.Name = "boton_Agregar_Account";
            this.boton_Agregar_Account.Size = new System.Drawing.Size(437, 29);
            this.boton_Agregar_Account.TabIndex = 9;
            this.boton_Agregar_Account.Text = "Add account";
            this.boton_Agregar_Account.UseVisualStyleBackColor = true;
            this.boton_Agregar_Account.Click += new System.EventHandler(this.boton_Agregar_Account_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 32);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(455, 362);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Add multiple accounts";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // imagenesFormAccounts
            // 
            this.imagenesFormAccounts.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagenesFormAccounts.ImageStream")));
            this.imagenesFormAccounts.TransparentColor = System.Drawing.Color.Transparent;
            this.imagenesFormAccounts.Images.SetKeyName(0, "agregar_cuenta.png");
            this.imagenesFormAccounts.Images.SetKeyName(1, "lista_cuentas.png");
            // 
            // GestionAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 398);
            this.Controls.Add(this.tabControlPrincipalAccounts);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(479, 437);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(479, 437);
            this.Name = "GestionAccounts";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Manager";
            this.tabControlPrincipalAccounts.ResumeLayout(false);
            this.ListaAccounts.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_informacion)).EndInit();
            this.contextMenuStripFormAccounts.ResumeLayout(false);
            this.AgregarAccount.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_informacion_agregar_cuenta)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPrincipalAccounts;
        private System.Windows.Forms.TabPage ListaAccounts;
        private System.Windows.Forms.TabPage AgregarAccount;
        private System.Windows.Forms.ImageList imagenesFormAccounts;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFormAccounts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox_informacion;
        private System.Windows.Forms.Label label_informacionClickAccounts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.PictureBox pictureBox_informacion_agregar_cuenta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_Agregar_Retroceder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label_Nombre_Account;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.Label label_Eleccion_Servidor;
        private System.Windows.Forms.Button boton_Agregar_Account;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label_Nombre_Personaje;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.ComboBox comboBox_Servidor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TextBox textBox_Nombre_Account;
        private System.Windows.Forms.ListView listViewAccounts;
        private System.Windows.Forms.ColumnHeader ColumnaNombreAccount;
        private System.Windows.Forms.ColumnHeader ColumnaNombreServidor;
        private System.Windows.Forms.ColumnHeader ColumnaNombrePersonaje;
        private System.Windows.Forms.ToolStripMenuItem conectarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox_nombre_personaje;
        private System.Windows.Forms.ToolStripMenuItem cuentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contraseñaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nombreDelPersonajeToolStripMenuItem;
    }
}