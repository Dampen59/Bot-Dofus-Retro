﻿using Bot_Dofus_1._29._1.Otros.Scripts.Acciones;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace Bot_Dofus_1._29._1.Otros.Grupos
{
    public class Grupo : IDisposable
    {
        private Agrupamiento agrupamiento;
        private Dictionary<Account, ManualResetEvent> cuentas_acabadas;

        public Account Leader { get; private set; }
        public ObservableCollection<Account> miembros { get; private set; }
        private bool disposed;

        public Grupo(Account leader)
        {
            agrupamiento = new Agrupamiento(this);
            cuentas_acabadas = new Dictionary<Account, ManualResetEvent>();
            Leader = leader;
            miembros = new ObservableCollection<Account>();

            Leader.Group = this;
        }

        public void agregar_Miembro(Account miembro)
        {
            if (miembros.Count >= 7)//dofus solo se pueden 8 Character en un grupo
                return;

            miembro.Group = this;
            miembros.Add(miembro);
            cuentas_acabadas.Add(miembro, new ManualResetEvent(false));
        }

        public void eliminar_Miembro(Account miembro) => miembros.Remove(miembro);

        public void conectar_Accounts()
        {
            Leader.Connect();

            foreach (Account miembro in miembros)
                miembro.Connect();
        }

        public void desconectar_Accounts()
        {
            foreach (Account miembro in miembros)
                miembro.Disconnect();
        }

        #region Acciones
        public void enqueue_Acciones_Miembros(AccionesScript accion, bool iniciar_dequeue = false)
        {
            if (accion is PeleasAccion)
            {
                foreach (Account miembro in miembros)
                    cuentas_acabadas[miembro].Set();
                return;
            }

            foreach (Account miembro in miembros)
                miembro.ScriptHandler.manejar_acciones.enqueue_Accion(accion, iniciar_dequeue);

            if (iniciar_dequeue)
            {
                for (int i = 0; i < miembros.Count; i++)
                    cuentas_acabadas[miembros[i]].Reset();
            }
        }

        public void esperar_Acciones_Terminadas() => WaitHandle.WaitAll(cuentas_acabadas.Values.ToArray());

        private void miembro_Acciones_Acabadas(Account cuenta)
        {
            cuenta.Logger.log_information("GRUPO", "Acciones acabadas");
            cuentas_acabadas[cuenta].Set();
        }
        #endregion

        #region Zona Dispose
        public void Dispose() => Dispose(true);
        ~Grupo() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    agrupamiento.Dispose();
                    Leader.Dispose();

                    for (int i = 0; i < miembros.Count; i++)
                    {
                        miembros[i].Dispose();
                    }
                }

                agrupamiento = null;
                cuentas_acabadas.Clear();
                cuentas_acabadas = null;
                miembros.Clear();
                miembros = null;
                Leader = null;

                disposed = true;
            }
        }
        #endregion
    }
}