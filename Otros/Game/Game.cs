using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Game.Servidor;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Peleas;
using System;

namespace Bot_Dofus_1._29._1.Otros.Game
{
    public class Game : IEliminable, IDisposable
    {
        public ServidorJuego servidor { get; private set; }
        public Mapa mapa { get; private set; }
        public PersonajeJuego personaje { get; private set; }
        public Manejador manejador { get; private set; }
        public Pelea pelea{ get; private set; }
        private bool disposed = false;

        internal Game(Account account)
        {
            servidor = new ServidorJuego();
            mapa = new Mapa();
            personaje = new PersonajeJuego(account);
            manejador = new Manejador(account, mapa, personaje);
            pelea = new Pelea(account);
        }

        #region Zona Dispose
        ~Game() => Dispose(false);
        public void Dispose() => Dispose(true);

        public void Clean()
        {
            mapa.Clean();
            manejador.Clean();
            pelea.Clean();
            personaje.Clean();
            servidor.Clean();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    mapa.Dispose();
                    personaje.Dispose();
                    manejador.Dispose();
                    pelea.Dispose();
                    servidor.Dispose();
                }

                servidor = null;
                mapa = null;
                personaje = null;
                manejador = null;
                pelea = null;
                disposed = true;
            }
        }
        #endregion
    }
}
