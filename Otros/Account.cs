﻿using Bot_Dofus_1._29._1.Comun.Network;
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
        public string apodo { get; set; } = string.Empty;
        public string key_bienvenida { get; set; } = string.Empty;
        public string tiquet_game { get; set; } = string.Empty;
        public Logger logger { get; private set; }
        public ClienteTcp conexion { get; set; }
        public Juego juego { get; private set; }
        public ManejadorScript script { get; set; }
        public PeleaExtensiones pelea_extension { get; set; }
        public AccountConf configuracion { get; private set; }
        private StateAccount estado_cuenta = StateAccount.DISCONNECTED;
        public bool puede_utilizar_dragopavo = false;

        public Grupo grupo { get; set; }
        public bool tiene_grupo => grupo != null;
        public bool es_lider_grupo => !tiene_grupo || grupo.lider == this;

        private bool disposed;
        public event Action evento_estado_cuenta;
        public event Action cuenta_desconectada;

        public Account(AccountConf _configuracion)
        {
            configuracion = _configuracion;
            logger = new Logger();
            juego = new Juego(this);
            pelea_extension = new PeleaExtensiones(this);
            script = new ManejadorScript(this);
        }

        public void Connected()
        {
            conexion = new ClienteTcp(this);
            conexion.ConnexionServer(IPAddress.Parse(GlobalConf.ip_conexion), GlobalConf.puerto_conexion);
        }

        public void disconnect()
        {
            conexion?.Dispose();
            conexion = null;

            script.detener_Script();
            juego.limpiar();
            Estado_Account = StateAccount.DISCONNECTED;
            cuenta_desconectada?.Invoke();
        }

        public void ChangingToGameServer(string ip, int puerto)
        {
            conexion.DisconnectSocket();
            conexion.ConnexionServer(IPAddress.Parse(ip), puerto);
        }

        public StateAccount Estado_Account
        {
            get => estado_cuenta;
            set
            {
                estado_cuenta = value;
                evento_estado_cuenta?.Invoke();
            }
        }

        public bool isOccupied() => Estado_Account != StateAccount.AWAY && Estado_Account != StateAccount.REGENERATING;
        public bool isTalking() => Estado_Account == StateAccount.BANKING || Estado_Account == StateAccount.DIALOG || Estado_Account == StateAccount.EXCHANGE || Estado_Account == StateAccount.BUYING || Estado_Account == StateAccount.SELLING;
        public bool isFighting() => Estado_Account == StateAccount.FIGHTING;
        public bool isCollecting() => Estado_Account == StateAccount.COLLECTING;
        public bool isMoving() => Estado_Account == StateAccount.MOVING;

        #region Zona Dispose
        public void Dispose() => Dispose(true);
        ~Account() => Dispose(false);

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    script.Dispose();
                    conexion?.Dispose();
                    juego.Dispose();
                }
                Estado_Account = StateAccount.DISCONNECTED;
                script = null;
                key_bienvenida = null;
                conexion = null;
                logger = null;
                juego = null;
                apodo = null;
                configuracion = null;
                disposed = true;
            }
        }
        #endregion
    }
}
