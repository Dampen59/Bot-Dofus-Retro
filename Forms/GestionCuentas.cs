using Bot_Dofus_1._29._1.Utilidades.Configuracion;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
	Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Forms
{
    public partial class GestionAccounts : Form
    {
        private List<AccountConfiguration> cuentas_cargadas;

        public GestionAccounts()
        {
            InitializeComponent();
            cuentas_cargadas = new List<AccountConfiguration>();

            comboBox_Servidor.SelectedIndex = 0;
            cargar_Accounts_Lista();
        }

        private void cargar_Accounts_Lista()
        {
            listViewAccounts.Items.Clear();

            GlobalConf.GetAccountList().ForEach(x =>
            {
                if (!Principal.cuentas_cargadas.ContainsKey(x.accountNumber))
                    listViewAccounts.Items.Add(x.accountNumber).SubItems.AddRange(new string[2] { x.server, x.characterNumber });
            });
        }

        private void boton_Agregar_Account_Click(object sender, EventArgs e)
        {
            if (GlobalConf.get_Account(textBox_Nombre_Account.Text) != null && GlobalConf.show_debug_messages)
            {
                MessageBox.Show("An account with the same nick already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool tiene_errores = false;
            tableLayoutPanel6.Controls.OfType<TableLayoutPanel>().ToList().ForEach(panel =>
            {
                panel.Controls.OfType<TextBox>().ToList().ForEach(textbox =>
                {
                    if (string.IsNullOrEmpty(textbox.Text) || textbox.Text.Split(new char[0]).Length > 1)
                    {
                        textbox.BackColor = Color.Red;
                        tiene_errores = true;
                    }
                    else
                        textbox.BackColor = Color.White;
                });
            });

            if (!tiene_errores)
            {
                GlobalConf.AddAccount(textBox_Nombre_Account.Text, textBox_Password.Text, comboBox_Servidor.SelectedItem.ToString(), textBox_nombre_personaje.Text);
                cargar_Accounts_Lista();

                textBox_Nombre_Account.Clear();
                textBox_Password.Clear();
                textBox_nombre_personaje.Clear();

                if (checkBox_Agregar_Retroceder.Checked)
                    tabControlPrincipalAccounts.SelectedIndex = 0;

                GlobalConf.SaveConfig();
            }
        }

        private void listViewAccounts_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listViewAccounts.Columns[e.ColumnIndex].Width;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedItems.Count > 0 && listViewAccounts.FocusedItem != null)
            {
                foreach (ListViewItem cuenta in listViewAccounts.SelectedItems)
                {
                    GlobalConf.DeleteAccount(cuenta.Index);
                    cuenta.Remove();
                }
                GlobalConf.SaveConfig();
                cargar_Accounts_Lista();
            }
        }

        private void conectarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedItems.Count > 0 && listViewAccounts.FocusedItem != null)
            {
                foreach (ListViewItem cuenta in listViewAccounts.SelectedItems)
                    cuentas_cargadas.Add(GlobalConf.GetAccountList().FirstOrDefault(f => f.accountNumber == cuenta.Text));

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        public List<AccountConfiguration> get_Accounts_Cargadas() => cuentas_cargadas;
        private void listViewAccounts_MouseDoubleClick(object sender, MouseEventArgs e) => conectarToolStripMenuItem.PerformClick();

        private void modificar_Account(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedItems.Count == 1 && listViewAccounts.FocusedItem != null)
            {
                AccountConfiguration cuenta = GlobalConf.get_Account(listViewAccounts.SelectedItems[0].Index);

                switch (sender.ToString())
                {
                    case "Account":
                        string nueva_cuenta = Interaction.InputBox($"Ingresa la nueva cuenta", "Modificar cuenta", cuenta.accountNumber);

                        if (!string.IsNullOrEmpty(nueva_cuenta) || nueva_cuenta.Split(new char[0]).Length == 0)
                            cuenta.accountNumber = nueva_cuenta;
                    break;

                    case "Contraseña":
                        string nueva_password = Interaction.InputBox($"Ingresa la nueva contraseña", "Modificar contraseña", cuenta.password);

                        if (!string.IsNullOrEmpty(nueva_password) || nueva_password.Split(new char[0]).Length == 0)
                            cuenta.password = nueva_password;
                    break;

                    default:
                        string nuevo_personaje = Interaction.InputBox($"Ingresa el nombre del nuevo Character", "Modificar nombre de Character", cuenta.characterNumber);
                        cuenta.characterNumber = nuevo_personaje;
                    break;
                }

                GlobalConf.SaveConfig();
                cargar_Accounts_Lista();
            }
        }
    }
}
