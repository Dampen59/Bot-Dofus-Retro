﻿using Bot_Dofus_1._29._1.Controles.ControlMapa;
using System.IO;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
	Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Utilidades.Configuracion
{
    public class AccountConf
    {
        public string nombre_cuenta { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string servidor { get; set; } = string.Empty;
        public string nombre_personaje { get; set; } = string.Empty;

        public AccountConf(string _nombre_cuenta, string _password, string _servidor, string _nombre_personaje)
        {
            nombre_cuenta = _nombre_cuenta;
            password = _password;
            servidor = _servidor;
            nombre_personaje = _nombre_personaje;
        }

        public void guardar_Account(BinaryWriter bw)
        {
            bw.Write(nombre_cuenta);
            bw.Write(password);
            bw.Write(servidor);
            bw.Write(nombre_personaje);
        }

        public static AccountConf cargar_Una_Account(BinaryReader br)
        {
            try
            {
                return new AccountConf(br.ReadString(), br.ReadString(), br.ReadString(), br.ReadString());
            }
            catch
            {
                return null;
            }
        }

        public int get_Servidor_Id() => servidor.Equals("Eratz") ? 601 : 602;
    }
}
