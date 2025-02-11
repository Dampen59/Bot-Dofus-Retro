﻿using Bot_Dofus_1._29._1.Otros.Enums;
using System;

namespace Bot_Dofus_1._29._1.Otros.Game.Servidor
{
    public class GameServer : IEliminable, IDisposable
    {
        public int id;
        public string nombre;
        public EstadosServidor estado;
        private bool disposed = false;

        public GameServer() => actualizar_Datos(0, "UNDEFINED", EstadosServidor.OFFLINE);

        public void actualizar_Datos(int _id, string _nombre, EstadosServidor _estado)
        {
            id = _id;
            nombre = _nombre;
            estado = _estado;
        }

        #region Zona Dispose
        public void Dispose() => Dispose(true);
        ~GameServer() => Dispose(false);

        public void Clean()
        {
            id = 0;
            nombre = null;
            estado = EstadosServidor.OFFLINE;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            id = 0;
            nombre = null;
            estado = EstadosServidor.OFFLINE;
            disposed = true;
        }
        #endregion
    }
}
