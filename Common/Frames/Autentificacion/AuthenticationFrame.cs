﻿using System.Text;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Comun.Frames.Autentificacion
{
    internal class Authentication : Frame
    {
        [PaqueteAtributo("AlEf")]
        public void get_Error_Datos(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("LOGIN", "Connection rejected. Incorrect account name or password.");
            cliente.Account.Disconnect();
        }

        [PaqueteAtributo("AlEa")]
        public void get_Error_Ya_Conectado(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("LOGIN", "Already connected. Try again.");
            cliente.Account.Disconnect();
        }

        [PaqueteAtributo("AlEv")]
        public void get_Error_Version(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("LOGIN",
                "The %1 version of Dofus that you have installed is not compatible with this server. To play, install version %2. The DOFUS client will be closed.");
            cliente.Account.Disconnect();
        }

        [PaqueteAtributo("AlEb")]
        public void get_Error_Baneado(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("LOGIN", "Connection rejected. Your account has been banned.");
            cliente.Account.Disconnect();
        }

        [PaqueteAtributo("AlEd")]
        public void get_Error_Conectado(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("LOGIN",
                "This account is already connected to a game server. Please try again.");
            cliente.Account.Disconnect();
        }

        [PaqueteAtributo("AlEk")]
        public void get_Error_Baneado_Tiempo(TcpClient cliente, string paquete)
        {
            var informacion_ban = paquete.Substring(3).Split('|');
            int dias = int.Parse(informacion_ban[0].Substring(1)),
                horas = int.Parse(informacion_ban[1]),
                minutos = int.Parse(informacion_ban[2]);
            var mensaje = new StringBuilder().Append("Your account will be invalid during ");

            if (dias > 0)
                mensaje.Append(dias + " days");
            if (horas > 0)
                mensaje.Append(horas + " hours");
            if (minutos > 0)
                mensaje.Append(" and " + minutos + " minutes");

            cliente.Account.Logger.log_Error("LOGIN", mensaje.ToString());
            cliente.Account.Disconnect();
        }
    }
}