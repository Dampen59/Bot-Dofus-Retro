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
        public GameServer Server { get; private set; }
        public Map Map { get; private set; }
        public GameCharacter Character { get; private set; }
        public Handler Handler { get; private set; }
        public Fight Fight{ get; private set; }
        private bool disposed = false;

        internal Game(Account account)
        {
            Server = new GameServer();
            Map = new Map();
            Character = new GameCharacter(account);
            Handler = new Handler(account, Map, Character);
            Fight = new Fight(account);
        }

        #region Zona Dispose
        ~Game() => Dispose(false);
        public void Dispose() => Dispose(true);

        public void Clean()
        {
            Map.Clean();
            Handler.Clean();
            Fight.Clean();
            Character.Clean();
            Server.Clean();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Map.Dispose();
                    Character.Dispose();
                    Handler.Dispose();
                    Fight.Dispose();
                    Server.Dispose();
                }

                Server = null;
                Map = null;
                Character = null;
                Handler = null;
                Fight = null;
                disposed = true;
            }
        }
        #endregion
    }
}
