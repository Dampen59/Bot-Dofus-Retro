﻿using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using Bot_Dofus_1._29._1.Otros.Peleas.Configuracion;
using Bot_Dofus_1._29._1.Otros.Peleas.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bot_Dofus_1._29._1.Interfaces
{
    public partial class UI_Pelea : UserControl
    {
        private Account cuenta;

        public UI_Pelea(Account _cuenta)
        {
            InitializeComponent();
            cuenta = _cuenta;

            refrescar_Lista_Hechizos();
            cuenta.Game.Character.hechizos_actualizados += actualizar_Agregar_Lista_Hechizos;
        }

        private void UI_Pelea_Load(object sender, EventArgs e)
        {
            comboBox_focus_hechizo.SelectedIndex = 0;
            checkbox_espectadores.Checked = cuenta.CombatExtensions.configuracion.desactivar_espectador;

            if (cuenta.CanUseDrago)
                checkBox_utilizar_dragopavo.Checked = cuenta.CombatExtensions.configuracion.utilizar_dragopavo;
            else
                checkBox_utilizar_dragopavo.Enabled = false;

            comboBox_lista_tactica.SelectedIndex = (byte)cuenta.CombatExtensions.configuracion.tactica;
            comboBox_lista_posicionamiento.SelectedIndex = (byte)cuenta.CombatExtensions.configuracion.posicionamiento;
            comboBox_modo_lanzamiento.SelectedIndex = 0;
            numericUp_regeneracion1.Value = cuenta.CombatExtensions.configuracion.iniciar_regeneracion;
            numericUp_regeneracion2.Value = cuenta.CombatExtensions.configuracion.detener_regeneracion;
        }

        private void actualizar_Agregar_Lista_Hechizos()
        {
            comboBox_lista_hechizos.DisplayMember = "nombre";
            comboBox_lista_hechizos.ValueMember = "id";
            comboBox_lista_hechizos.DataSource = cuenta.Game.Character.hechizos.Values.ToList();

            comboBox_lista_hechizos.SelectedIndex = 0;
        }

        private void button_agregar_hechizo_Click(object sender, EventArgs e)
        {
            Hechizo hechizo = comboBox_lista_hechizos.SelectedItem as Hechizo;
            cuenta.CombatExtensions.configuracion.hechizos.Add(new HechizoPelea(hechizo.id, hechizo.nombre, (HechizoFocus)comboBox_focus_hechizo.SelectedIndex, (MetodoLanzamiento)comboBox_modo_lanzamiento.SelectedIndex, Convert.ToByte(numeric_lanzamientos_turno.Value)));
            cuenta.CombatExtensions.configuracion.guardar();
            refrescar_Lista_Hechizos();
        }

        private void refrescar_Lista_Hechizos()
        {
            listView_hechizos_pelea.Items.Clear();

            foreach(HechizoPelea hechizo in cuenta.CombatExtensions.configuracion.hechizos)
            {
                listView_hechizos_pelea.Items.Add(hechizo.id.ToString()).SubItems.AddRange(new string[4]
                {
                    hechizo.nombre, hechizo.focus.ToString(), hechizo.lanzamientos_x_turno.ToString(), hechizo.metodo_lanzamiento.ToString()
                });
            }
        }

        private void button_subir_hechizo_Click(object sender, EventArgs e)
        {
            if (listView_hechizos_pelea.FocusedItem == null || listView_hechizos_pelea.FocusedItem.Index == 0)
                return;

            List<HechizoPelea> hechizo = cuenta.CombatExtensions.configuracion.hechizos;
            HechizoPelea temporal = hechizo[listView_hechizos_pelea.FocusedItem.Index - 1];

            hechizo[listView_hechizos_pelea.FocusedItem.Index - 1] = hechizo[listView_hechizos_pelea.FocusedItem.Index];
            hechizo[listView_hechizos_pelea.FocusedItem.Index] = temporal;
            cuenta.CombatExtensions.configuracion.guardar();
            refrescar_Lista_Hechizos();
        }

        private void button_bajar_hechizo_Click(object sender, EventArgs e)
        {
            if (listView_hechizos_pelea.FocusedItem == null || listView_hechizos_pelea.FocusedItem.Index == 0)
                return;

            List<HechizoPelea> hechizo = cuenta.CombatExtensions.configuracion.hechizos;
            HechizoPelea temporal = hechizo[listView_hechizos_pelea.FocusedItem.Index + 1];

            hechizo[listView_hechizos_pelea.FocusedItem.Index + 1] = hechizo[listView_hechizos_pelea.FocusedItem.Index];
            hechizo[listView_hechizos_pelea.FocusedItem.Index] = temporal;
            cuenta.CombatExtensions.configuracion.guardar();
            refrescar_Lista_Hechizos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Map map = cuenta.Game.Map;

            List<Monstruos> monstruos = cuenta.Game.Map.lista_monstruos();

            if (monstruos.Count > 0)
            {
                Celda celda_actual = cuenta.Game.Character.celda, celda_monstruo_destino = monstruos[0].celda;

                if (celda_actual.id != celda_monstruo_destino.id & celda_monstruo_destino.id > 0)
                {
                    cuenta.Logger.log_information("UI_PELEAS", "Monstruo encontrado en la casilla " + celda_monstruo_destino.id);

                    switch (cuenta.Game.Handler.movimientos.get_Mover_A_Celda(celda_monstruo_destino, new List<Celda>()))
                    {
                        case ResultadoMovimientos.EXITO:
                            cuenta.Logger.log_information("UI_PELEAS", "Desplazando para comenzar el combate");
                        break;

                        case ResultadoMovimientos.MISMA_CELDA:
                        case ResultadoMovimientos.FALLO:
                        case ResultadoMovimientos.PATHFINDING_ERROR:
                            cuenta.Logger.log_Error("UI_PELEAS", "El monstruo no esta en la casilla selecciona");
                        break;
                    }
                }
            }
            else
                cuenta.Logger.log_Error("PELEAS", "No hay monstruos disponibles en el Map");
        }

        private void checkbox_espectadores_CheckedChanged(object sender, EventArgs e)
        {
            cuenta.CombatExtensions.configuracion.desactivar_espectador = checkbox_espectadores.Checked;
            cuenta.CombatExtensions.configuracion.guardar();
        }

        private void checkBox_utilizar_dragopavo_CheckedChanged(object sender, EventArgs e)
        {
            cuenta.CombatExtensions.configuracion.utilizar_dragopavo = checkBox_utilizar_dragopavo.Checked;
            cuenta.CombatExtensions.configuracion.guardar();
        }

        private void comboBox_lista_tactica_SelectedIndexChanged(object sender, EventArgs e)
        {
            cuenta.CombatExtensions.configuracion.tactica = (Tactica)comboBox_lista_tactica.SelectedIndex;
            cuenta.CombatExtensions.configuracion.guardar();
        }

        private void button_eliminar_hechizo_Click(object sender, EventArgs e)
        {
            if (listView_hechizos_pelea.FocusedItem == null)
                return;

            cuenta.CombatExtensions.configuracion.hechizos.RemoveAt(listView_hechizos_pelea.FocusedItem.Index);
            cuenta.CombatExtensions.configuracion.guardar();
            refrescar_Lista_Hechizos();
        }

        private void comboBox_lista_posicionamiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cuenta.CombatExtensions.configuracion.posicionamiento = (PosicionamientoInicioPelea)comboBox_lista_posicionamiento.SelectedIndex;
            cuenta.CombatExtensions.configuracion.guardar();
        }

        private void NumericUp_regeneracion1_ValueChanged(object sender, EventArgs e)
        {
            cuenta.CombatExtensions.configuracion.iniciar_regeneracion = (byte)numericUp_regeneracion1.Value;
            cuenta.CombatExtensions.configuracion.guardar();
        }

        private void NumericUp_regeneracion2_ValueChanged(object sender, EventArgs e)
        {
            cuenta.CombatExtensions.configuracion.detener_regeneracion = (byte)numericUp_regeneracion2.Value;
            cuenta.CombatExtensions.configuracion.guardar();
        }
    }
}
