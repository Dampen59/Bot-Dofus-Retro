using Bot_Dofus_1._29._1.Controles.ControlMapa;
using Bot_Dofus_1._29._1.Controles.ControlMapa.Animaciones;
using Bot_Dofus_1._29._1.Controles.ControlMapa.Celdas;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Movimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Interfaces
{
    public partial class UI_Mapa : UserControl
    {
        private Account cuenta = null;

        public UI_Mapa(Account _cuenta)
        {
            InitializeComponent();

            cuenta = _cuenta;
            control_mapa.set_Account(cuenta);
        }

        private void UI_Mapa_Load(object sender, EventArgs e)
        {
            comboBox_calidad_minimapa.SelectedIndex = (byte)control_mapa.TipoCalidad;

            control_mapa.clic_celda += mapa_Control_Celda_Clic;
            cuenta.Game.Map.mapa_actualizado += get_Eventos_Mapa_Cambiado;
            cuenta.Game.Map.entidades_actualizadas += () => control_mapa.Invalidate();
            cuenta.Game.Character.movimiento_pathfinding_minimapa += get_Dibujar_Pathfinding;
        }

        private void get_Eventos_Mapa_Cambiado()
        {
            Map map = cuenta.Game.Map;

            byte anchura_actual = control_mapa.mapa_anchura, altura_actual = control_mapa.mapa_altura;
            byte anchura_nueva = map.anchura, altura_nueva = map.altura;

            if (anchura_actual != anchura_nueva || altura_actual != altura_nueva)
            {
                control_mapa.mapa_anchura = anchura_nueva;
                control_mapa.mapa_altura = altura_nueva;

                control_mapa.set_Celda_Numero();
                control_mapa.dibujar_Cuadricula();
            }

            BeginInvoke((Action)(() =>
            {
                label_mapa_id.Text = "MAPA ID: " + map.id;
            }));

            control_mapa.refrescar_Mapa();
        }

        private void mapa_Control_Celda_Clic(CeldaMapa celda, MouseButtons botones, bool abajo)
        {
            Map map = cuenta.Game.Map;
            Celda celda_actual = cuenta.Game.Character.celda, celda_destino = map.get_Celda_Id(celda.id);

            if (botones == MouseButtons.Left && celda_actual.id != 0 && celda_destino.id != 0 && !abajo)
            {
                switch (cuenta.Game.Handler.movimientos.get_Mover_A_Celda(celda_destino, map.celdas_ocupadas()))
                {
                    case ResultadoMovimientos.EXITO:
                        cuenta.Logger.log_information("UI_MAPA", $"Personaje desplazado a la casilla: {celda_destino.id}");
                    break;

                    case ResultadoMovimientos.MISMA_CELDA:
                        cuenta.Logger.log_Error("UI_MAPA", "El jugador está en la misma a la seleccionada");
                    break;

                    default:
                        cuenta.Logger.log_Error("UI_MAPA", $"Error desplazando el Character a la casilla: {celda_destino.id}");
                    break;
                }
            }
        }

        private void get_Dibujar_Pathfinding(List<Celda> lista_celdas) => Task.Run(() => control_mapa.agregar_Animacion(cuenta.Game.Character.id, lista_celdas, PathFinderUtil.get_Tiempo_Desplazamiento_Mapa(lista_celdas.First(), lista_celdas), TipoAnimaciones.PERSONAJE));
        private void comboBox_calidad_minimapa_SelectedIndexChanged(object sender, EventArgs e) => control_mapa.TipoCalidad = (CalidadMapa)comboBox_calidad_minimapa.SelectedIndex;
        private void checkBox_animaciones_CheckedChanged(object sender, EventArgs e) => control_mapa.Mostrar_Animaciones = checkBox_animaciones.Checked;
        private void checkBox_mostrar_celdas_CheckedChanged(object sender, EventArgs e) => control_mapa.Mostrar_Celdas_Id = checkBox_mostrar_celdas.Checked;
    }
}
