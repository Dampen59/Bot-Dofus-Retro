﻿using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Recolecciones;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Mapas;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Npcs;
using Bot_Dofus_1._29._1.Utilidades;
using MoonSharp.Interpreter;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Manejadores
{
    public class ManejadorAcciones : IDisposable
    {
        private Account cuenta;
        public LuaManejadorScript manejador_script;
        private ConcurrentQueue<AccionesScript> fila_acciones;
        private AccionesScript accion_actual;
        private DynValue coroutine_actual;
        private TimerWrapper timer_out;
        public int contador_pelea, contador_recoleccion, contador_peleas_mapa;
        private bool mapa_cambiado;
        private bool disposed;

        public event Action<bool> evento_accion_normal;
        public event Action<bool> evento_accion_personalizada;

        public ManejadorAcciones(Account _cuenta, LuaManejadorScript _manejador_script)
        {
            cuenta = _cuenta;
            manejador_script = _manejador_script;
            fila_acciones = new ConcurrentQueue<AccionesScript>();
            timer_out = new TimerWrapper(60000, time_Out_Callback);
            GameCharacter personaje = cuenta.Game.Character;
            
            cuenta.Game.Map.mapa_actualizado += evento_Mapa_Cambiado;
            cuenta.Game.Fight.pelea_creada += get_Pelea_Creada;
            cuenta.Game.Handler.movimientos.movimiento_finalizado += evento_Movimiento_Celda;
            personaje.dialogo_npc_recibido += npcs_Dialogo_Recibido;
            personaje.dialogo_npc_acabado += npcs_Dialogo_Acabado;
            personaje.inventario.almacenamiento_abierto += iniciar_Almacenamiento;
            personaje.inventario.almacenamiento_cerrado += cerrar_Almacenamiento;
            cuenta.Game.Handler.recoleccion.recoleccion_iniciada += get_Recoleccion_Iniciada;
            cuenta.Game.Handler.recoleccion.recoleccion_acabada += get_Recoleccion_Acabada;
        }

        private void evento_Mapa_Cambiado()
        {
            if (!cuenta.ScriptHandler.corriendo || accion_actual == null)
                return;

            mapa_cambiado = true;

            // cuando inicia una pelea "resetea el Map"
            if (!(accion_actual is PeleasAccion))
                contador_peleas_mapa = 0;

            if (accion_actual is CambiarMapaAccion || accion_actual is PeleasAccion || accion_actual is RecoleccionAccion || coroutine_actual != null)
            {
                limpiar_Acciones();
                acciones_Salida(1500);
            }
        }

        private async void evento_Movimiento_Celda(bool es_correcto)
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            if (accion_actual is PeleasAccion)
            {
                if (es_correcto)
                {
                    for (int delay = 0; delay < 10000 && cuenta.AccountStatus != AccountStatus.Fighting; delay += 500)
                        await Task.Delay(500);

                    if (cuenta.AccountStatus != AccountStatus.Fighting)
                    {
                        cuenta.Logger.log_Danger("SCRIPT", "Error when throwing the fight, the monsters could have moved or been stolen!");
                        acciones_Salida(0);
                    }
                }
            }
            else if (accion_actual is MoverCeldaAccion celda)
            {
                if (es_correcto)
                    acciones_Salida(0);
                else
                    cuenta.ScriptHandler.StopScript("error when moving to cell" + celda.celda_id);
            }
            else if (accion_actual is CambiarMapaAccion && !es_correcto)
                cuenta.ScriptHandler.StopScript("error when changing Map");
        }

        private void get_Recoleccion_Iniciada()
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            if (accion_actual is RecoleccionAccion)
            {
                contador_recoleccion++;

                if (manejador_script.get_Global_Or("HARVEST_COUNT", DataType.Boolean, false))
                    cuenta.Logger.log_information("SCRIPT", $"Collecting #{contador_recoleccion}");
            }
        }

        private void get_Recoleccion_Acabada(RecoleccionResultado resultado)
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            if (accion_actual is RecoleccionAccion)
            {
                switch (resultado)
                {
                    case RecoleccionResultado.FALLO:
                        cuenta.ScriptHandler.StopScript("Collecting error");
                    break;

                    default:
                        acciones_Salida(800);
                    break;
                }
            }
        }

        private void get_Pelea_Creada()
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            if (accion_actual is PeleasAccion)
            {
                timer_out.Stop();
                contador_peleas_mapa++;
                contador_pelea++;

                if (manejador_script.get_Global_Or("MOSTRAR_CONTADOR_PELEAS", DataType.Boolean, false))
                    cuenta.Logger.log_information("SCRIPT", $"Combate #{contador_pelea}");
            }
        }

        private void npcs_Dialogo_Recibido()
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            if (accion_actual is NpcBancoAccion nba)
            {
                if (cuenta.AccountStatus != AccountStatus.Dialoguing)
                    return;

                IEnumerable<Npcs> npcs = cuenta.Game.Map.lista_npcs();
                Npcs npc = npcs.ElementAt((cuenta.Game.Character.hablando_npc_id * -1) - 1);

                cuenta.Connection.Send("DR" + npc.pregunta + "|" + npc.respuestas[0], true);
            }
            else if (accion_actual is NpcAccion || accion_actual is RespuestaAccion)
                acciones_Salida(400);
        }

        private void npcs_Dialogo_Acabado()
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            if (accion_actual is RespuestaAccion || accion_actual is CerrarVentanaAccion)
                acciones_Salida(200); 
        }

        public void enqueue_Accion(AccionesScript accion, bool iniciar_dequeue_acciones = false)
        {
            fila_acciones.Enqueue(accion);

            if (iniciar_dequeue_acciones)
                acciones_Salida(0);
        }

        public void get_Funcion_Personalizada(DynValue coroutine)
        {
            if (!cuenta.ScriptHandler.corriendo || coroutine_actual != null)
                return;

            coroutine_actual = manejador_script.script.CreateCoroutine(coroutine);
            procesar_Coroutine();
        }

        private void limpiar_Acciones()
        {
            while (fila_acciones.TryDequeue(out AccionesScript temporal)) { };
            accion_actual = null;
        }

        private void iniciar_Almacenamiento()
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            if (accion_actual is NpcBancoAccion)
                acciones_Salida(400);
        }

        private void cerrar_Almacenamiento()
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            if (accion_actual is CerrarVentanaAccion)
                acciones_Salida(400);
        }

        private void procesar_Coroutine()
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            try
            {
                DynValue result = coroutine_actual.Coroutine.Resume();

                if (result.Type == DataType.Void)
                    acciones_Funciones_Finalizadas();
            }
            catch (Exception ex)
            {
                cuenta.ScriptHandler.StopScript(ex.ToString());
            }
        }

        private async Task procesar_Accion_Actual()
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            string tipo = accion_actual.GetType().Name;

            switch (await accion_actual.proceso(cuenta))
            {
                case ResultadosAcciones.HECHO:
                    acciones_Salida(100);
                break;

                case ResultadosAcciones.FALLO:
                    cuenta.Logger.log_Danger("SCRIPT", $"{tipo} fallo al procesar.");
                break;

                case ResultadosAcciones.PROCESANDO:
                    timer_out.Start();
                break;
            }
        }

        private void time_Out_Callback(object state)
        {
            if (!cuenta.ScriptHandler.corriendo)
                return;

            cuenta.Logger.log_Danger("SCRIPT", "Tiempo acabado");
            cuenta.ScriptHandler.StopScript();
            cuenta.ScriptHandler.activar_Script();
        }

        private void acciones_Finalizadas()
        {
            if (mapa_cambiado)
            {
                mapa_cambiado = false;
                evento_accion_normal?.Invoke(true);
            }
            else
                evento_accion_normal?.Invoke(false);
        }

        private void acciones_Funciones_Finalizadas()
        {
            coroutine_actual = null;

            if (mapa_cambiado)
            {
                mapa_cambiado = false;
                evento_accion_personalizada?.Invoke(true);
            }
            else
                evento_accion_personalizada?.Invoke(false);
        }

        private void acciones_Salida(int delay) => Task.Factory.StartNew(async () =>
        {
            if (cuenta?.ScriptHandler.corriendo == false)
                return;

            if (timer_out.habilitado)
                timer_out.Stop();

            if (delay > 0)
                await Task.Delay(delay);

            if (fila_acciones.Count > 0)
            {
                if (fila_acciones.TryDequeue(out AccionesScript accion))
                {
                    accion_actual = accion;
                    await procesar_Accion_Actual();
                }
            }
            else
            {
                if (coroutine_actual != null)
                    procesar_Coroutine();
                else
                    acciones_Finalizadas();
            }

        }, TaskCreationOptions.LongRunning);

        public void get_Borrar_Todo()
        {
            limpiar_Acciones();
            accion_actual = null;
            coroutine_actual = null;
            timer_out.Stop();

            contador_pelea = 0;
            contador_peleas_mapa = 0;
            contador_recoleccion = 0;
        }

        #region Zona Dispose
        public void Dispose() => Dispose(true);
        ~ManejadorAcciones() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    timer_out.Dispose();
                }
                accion_actual = null;
                fila_acciones = null;
                cuenta = null;
                manejador_script = null;
                timer_out = null;
                disposed = true;
            }
        }
        #endregion
    }
}
