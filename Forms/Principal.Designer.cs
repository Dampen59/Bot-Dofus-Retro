namespace Bot_Dofus_1._29._1.Forms
{
    partial class Principal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuSuperiorPrincipal = new System.Windows.Forms.MenuStrip();
            this.gestionDeAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripInferiorPrincipal = new System.Windows.Forms.StatusStrip();
            this.tabControlAccounts = new Bot_Dofus_1._29._1.Controles.TabControl.TabControl();
            this.menuSuperiorPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuSuperiorPrincipal
            // 
            this.menuSuperiorPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionDeAccountsToolStripMenuItem,
            this.opcionesToolStripMenuItem});
            this.menuSuperiorPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuSuperiorPrincipal.Name = "menuSuperiorPrincipal";
            this.menuSuperiorPrincipal.Size = new System.Drawing.Size(980, 24);
            this.menuSuperiorPrincipal.TabIndex = 0;
            this.menuSuperiorPrincipal.Text = "menuSuperiorPrincipal";
            // 
            // gestionDeAccountsToolStripMenuItem
            // 
            this.gestionDeAccountsToolStripMenuItem.Image = global::Bot_Dofus_1._29._1.Properties.Resources.gestion_cuentas;
            this.gestionDeAccountsToolStripMenuItem.Name = "gestionDeAccountsToolStripMenuItem";
            this.gestionDeAccountsToolStripMenuItem.Size = new System.Drawing.Size(135, 20);
            this.gestionDeAccountsToolStripMenuItem.Text = "Account Manager";
            this.gestionDeAccountsToolStripMenuItem.Click += new System.EventHandler(this.gestionDeAccountsToolStripMenuItem_Click);
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.Image = global::Bot_Dofus_1._29._1.Properties.Resources.boton_ajustes;
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.opcionesToolStripMenuItem.Text = "Settings";
            this.opcionesToolStripMenuItem.Click += new System.EventHandler(this.opcionesToolStripMenuItem_Click);
            // 
            // statusStripInferiorPrincipal
            // 
            this.statusStripInferiorPrincipal.Location = new System.Drawing.Point(0, 639);
            this.statusStripInferiorPrincipal.Name = "statusStripInferiorPrincipal";
            this.statusStripInferiorPrincipal.Size = new System.Drawing.Size(980, 22);
            this.statusStripInferiorPrincipal.TabIndex = 1;
            this.statusStripInferiorPrincipal.Text = "statusStripInferiorPrincipal";
            // 
            // tabControlAccounts
            // 
            this.tabControlAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAccounts.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tabControlAccounts.Location = new System.Drawing.Point(0, 24);
            this.tabControlAccounts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControlAccounts.Name = "tabControlAccounts";
            this.tabControlAccounts.Size = new System.Drawing.Size(980, 615);
            this.tabControlAccounts.TabIndex = 2;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 661);
            this.Controls.Add(this.tabControlAccounts);
            this.Controls.Add(this.statusStripInferiorPrincipal);
            this.Controls.Add(this.menuSuperiorPrincipal);
            this.MainMenuStrip = this.menuSuperiorPrincipal;
            this.MinimumSize = new System.Drawing.Size(996, 700);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bot Retro - 1.30";
            this.menuSuperiorPrincipal.ResumeLayout(false);
            this.menuSuperiorPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuSuperiorPrincipal;
        private System.Windows.Forms.StatusStrip statusStripInferiorPrincipal;
        private Controles.TabControl.TabControl tabControlAccounts;
        private System.Windows.Forms.ToolStripMenuItem gestionDeAccountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
    }
}

