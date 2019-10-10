using Bot_Dofus_1._29._1.Controles.LayoutPanel;
using System.Windows.Forms;

namespace Bot_Dofus_1._29._1.Controles.TabControl
{
    partial class TabControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelCabezeraAccounts = new FlowLayoutPanelBuffered();
            this.panelContenidoAccount = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelCabezeraAccounts
            // 
            this.panelCabezeraAccounts.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCabezeraAccounts.Location = new System.Drawing.Point(0, 0);
            this.panelCabezeraAccounts.Name = "panelCabezeraAccounts";
            this.panelCabezeraAccounts.Size = new System.Drawing.Size(174, 540);
            this.panelCabezeraAccounts.TabIndex = 0;
            // 
            // panelContenidoAccount
            // 
            this.panelContenidoAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenidoAccount.Location = new System.Drawing.Point(174, 0);
            this.panelContenidoAccount.Name = "panelContenidoAccount";
            this.panelContenidoAccount.Size = new System.Drawing.Size(734, 540);
            this.panelContenidoAccount.TabIndex = 0;
            // 
            // TabControl_Horizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContenidoAccount);
            this.Controls.Add(this.panelCabezeraAccounts);
            this.Name = "TabControl_Horizontal";
            this.Size = new System.Drawing.Size(908, 540);
            this.ResumeLayout(false);

        }

        private FlowLayoutPanelBuffered panelCabezeraAccounts;
        private Panel panelContenidoAccount;
    }
}
