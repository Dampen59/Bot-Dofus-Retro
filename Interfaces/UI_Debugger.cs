using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Interfaces
{
    public partial class UI_Debugger : UserControl
    {
        private List<string> lista_packages;

        public UI_Debugger()
        {
            InitializeComponent();
            lista_packages = new List<string>();
        }

        public void package_Recibido(string package)
        {
            agregar_Nuevo_Paquete(package, false);
        }

        public void package_Enviado(string package)
        {
            agregar_Nuevo_Paquete(package, true);
        }

        private void agregar_Nuevo_Paquete(string package, bool enviado)
        {
            if (!checkbox_debugger.Checked)
                return;

            try
            {
                BeginInvoke((Action)(() =>
                {
                    if (lista_packages.Count == 200)
                    {
                        lista_packages.RemoveAt(0);
                        listView.Items.RemoveAt(0);
                    }

                    lista_packages.Add(package);

                    ListViewItem objeto_lista = listView.Items.Add(DateTime.Now.ToString("HH:mm:ss"));
                    objeto_lista.BackColor = enviado ? Color.FromArgb(242, 174, 138) : Color.FromArgb(170, 196, 237);
                    objeto_lista.SubItems.Add(package);
                }));
            }
            catch { }

        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.FocusedItem?.Index == -1 || listView.SelectedItems.Count == 0)
                return;

            string package = lista_packages[listView.FocusedItem.Index];
            treeView.Nodes.Clear();

            if (PackageReceive.metodos.Count == 0)
                return;

            foreach (PackageData metodo in PackageReceive.metodos)
            {
                if (package.StartsWith(metodo.nombre_package))
                {
                    treeView.Nodes.Add(metodo.nombre_package);
                    treeView.Nodes[0].Nodes.Add(package.Remove(0, metodo.nombre_package.Length));
                    treeView.Nodes[0].Expand();
                    break;
                }
            }
        }

        private void listView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView.Columns[e.ColumnIndex].Width;
        }

        private void button_limpiar_logs_debugger_Click(object sender, EventArgs e)
        {
            lista_packages.Clear();
            listView.Items.Clear();
        }
    }
}
