using Bot_Dofus_1._29._1.Otros.Scripts.Acciones;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Mapas;
using Bot_Dofus_1._29._1.Otros.Scripts.Manejadores;
using MoonSharp.Interpreter;
using System;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Api
{
    [MoonSharpUserData]
    public class MapaApi : IDisposable
    {
        private Account cuenta;
        private ManejadorAcciones manejador_acciones;
        private bool disposed = false;

        public MapaApi(Account _cuenta, ManejadorAcciones _manejador_acciones)
        {
            cuenta = _cuenta;
            manejador_acciones = _manejador_acciones;
        }

        public bool cambiarMapa(string posicion)
        {
            if (cuenta.IsBusy())
                return false;

            if (!CambiarMapaAccion.TryParse(posicion, out CambiarMapaAccion accion))
            {
                cuenta.Logger.log_Error("MapaApi", $"Verifica el cambio de Map {posicion}");
                return false;
            }

            manejador_acciones.enqueue_Accion(accion, true);
            return true;
        }

        public bool moverCelda(short celda_id)
        {
            if (celda_id < 0 || celda_id > cuenta.Game.Map.celdas.Length)
                return false;

            if (!cuenta.Game.Map.get_Celda_Id(celda_id).es_Caminable() || cuenta.Game.Map.get_Celda_Id(celda_id).es_linea_vision)
                return false;

            manejador_acciones.enqueue_Accion(new MoverCeldaAccion(celda_id), true);
            return true;
        }

        public bool enCelda(short celda_id) => cuenta.Game.Character.celda.id == celda_id;
        public bool enMapa(string coordenadas) => cuenta.Game.Map.esta_En_Mapa(coordenadas);
        public string actualMapa() => cuenta.Game.Map.id.ToString();
        public string actualPosicion() => cuenta.Game.Map.coordenadas;

        #region Zona Dispose
        ~MapaApi() => Dispose(false);
        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                cuenta = null;
                manejador_acciones = null;
                disposed = true;
            }
        }
        #endregion
    }
}
