using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game;
using Bot_Dofus_1._29._1.Otros.Grupos;
using Bot_Dofus_1._29._1.Otros.Peleas;
using Bot_Dofus_1._29._1.Otros.Scripts;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;
using Bot_Dofus_1._29._1.Utilidades.Logs;
using System;
using System.Net;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
	Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Otros
{
    public class Account : IDisposable
    {
        public string Nickname { get; set; } = string.Empty;
        public string WelcomeKey { get; set; } = string.Empty;
        public string GameTicket { get; set; } = string.Empty;
        public Logger Logger { get; private set; }
        public TcpClient Connection { get; set; }
        public Game.Game Game { get; private set; }
        public ScriptHandler ScriptHandler { get; set; }
        public CombatExtensions CombatExtensions { get; set; }
        public AccountConfiguration AccountConfiguration { get; private set; }
        private AccountStatus _accountStatus = AccountStatus.Disconnected;
        public bool CanUseDrago = false;

        public Grupo Group { get; set; }
        public bool HasGroup => Group != null;
        public bool IsGroupLeader => !HasGroup || Group.Leader == this;

        private bool _disposed;
        public event Action AccountStatusEvent;
        public event Action AccountDisconnectedEvent;

        public Account(AccountConfiguration accountConfiguration)
        {
            AccountConfiguration = accountConfiguration;
            Logger = new Logger();
            Game = new Game.Game(this);
            CombatExtensions = new CombatExtensions(this);
            ScriptHandler = new ScriptHandler(this);
        }

        public void Connect()
        {
            Connection = new TcpClient(this);
            Connection.ConnectToServer(IPAddress.Parse(GlobalConf.Ip), GlobalConf.Port);
        }

        public void Disconnect()
        {
            Connection?.Dispose();
            Connection = null;

            ScriptHandler.StopScript();
            Game.Clean();
            AccountStatus = AccountStatus.Disconnected;
            AccountDisconnectedEvent?.Invoke();
        }

        public void ChangeGameServer(string ip, int port)
        {
            Connection.DisconnectSocket();
            Connection.ConnectToServer(IPAddress.Parse(ip), port);
        }

        public AccountStatus AccountStatus
        {
            get => _accountStatus;
            set
            {
                _accountStatus = value;
                AccountStatusEvent?.Invoke();
            }
        }

        public bool IsBusy() => AccountStatus != AccountStatus.ConnectedInactive && AccountStatus != AccountStatus.Regenerating;
        public bool IsDialoguing() => AccountStatus == AccountStatus.Storing || AccountStatus == AccountStatus.Dialoguing || AccountStatus == AccountStatus.Exchanging || AccountStatus == AccountStatus.Buying || AccountStatus == AccountStatus.Selling;
        public bool IsFighting() => AccountStatus == AccountStatus.Fighting;
        public bool IsCollecting() => AccountStatus == AccountStatus.Collecting;
        public bool IsMoving() => AccountStatus == AccountStatus.Moving;

        #region Zona Dispose
        public void Dispose() => Dispose(true);
        ~Account() => Dispose(false);

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ScriptHandler.Dispose();
                    Connection?.Dispose();
                    Game.Dispose();
                }
                AccountStatus = AccountStatus.Disconnected;
                ScriptHandler = null;
                WelcomeKey = null;
                Connection = null;
                Logger = null;
                Game = null;
                Nickname = null;
                AccountConfiguration = null;
                _disposed = true;
            }
        }
        #endregion
    }
}
