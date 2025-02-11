﻿using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Servidor;
using Bot_Dofus_1._29._1.Utilidades.Criptografia;
using System.Threading.Tasks;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Comun.Frames.LoginAccount
{
    public class LoginAccount : Frame
    {
        [PackageAttribut("HC")]
        public void get_Key_BienvenidaAsync(TcpClient cliente, string paquete)
        {
            Account cuenta = cliente.Account;

            cuenta.AccountStatus = AccountStatus.CONECTANDO;
            cuenta.WelcomeKey = paquete.Substring(2);

            cliente.Send("1.30");
            cliente.Send(cliente.Account.AccountConfiguration.nombre_cuenta + "\n" + Hash.encriptar_Password(cliente.Account.AccountConfiguration.password, cliente.Account.WelcomeKey));
            cliente.Send("Af");
        }

        [PackageAttribut("Ad")]
        public void get_Apodo(TcpClient cliente, string paquete) => cliente.Account.Nickname = paquete.Substring(2);

        [PackageAttribut("Af")]
        public void get_Fila_Espera_Login(TcpClient cliente, string paquete) => cliente.Account.Logger.log_information("WAITING ROW", "Position " + paquete[2] + "/" + paquete[4]);

        [PackageAttribut("AH")]
        public void get_Servidor_Estado(TcpClient cliente, string paquete)
        {
            Account cuenta = cliente.Account;
            string[] separado_servidores = paquete.Substring(2).Split('|');
            GameServer servidor = cuenta.Game.Server;
            bool primera_vez = true;

            foreach(string sv in separado_servidores)
            {
                string[] separador = sv.Split(';');

                int id = int.Parse(separador[0]);
                EstadosServidor estado = (EstadosServidor)byte.Parse(separador[1]);
                string nombre = id == 601 ? "Eratz" : "Henual";

                if (id == cuenta.AccountConfiguration.get_Servidor_Id())
                {
                    servidor.actualizar_Datos(id, nombre, estado);
                    cuenta.Logger.log_information("LOGIN", $"The server {cuenta.AccountConfiguration.get_Servidor_Id()} is {estado}");

                    if (estado != EstadosServidor.ONLINE)
                        primera_vez = false;
                }
            }

            if(!primera_vez  && servidor.estado == EstadosServidor.ONLINE)
                cliente.Send("Ax");
        }

        [PackageAttribut("AQ")]
        public void get_Pregunta_Secreta(TcpClient cliente, string paquete)
        {
            if (cliente.Account.Game.Server.estado == EstadosServidor.ONLINE)
                cliente.Send("Ax", true);
        }

        [PackageAttribut("AxK")]
        public void get_Servidores_Lista(TcpClient cliente, string paquete)
        {
            Account cuenta = cliente.Account;
            string[] loc5 = paquete.Substring(3).Split('|');
            int contador = 1;
            bool seleccionado = false;

            while (contador < loc5.Length && !seleccionado)
            {
                string[] _loc10_ = loc5[contador].Split(',');
                int servidor_id = int.Parse(_loc10_[0]);

                if (servidor_id == cuenta.Game.Server.id)
                {
                    if(cuenta.Game.Server.estado == EstadosServidor.ONLINE)
                    {
                        seleccionado = true;
                        cuenta.Game.Character.evento_Servidor_Seleccionado();
                    }
                    else
                        cuenta.Logger.log_Error("LOGIN", "Server not accessible when this accessible will re-connect");
                }
                contador++;
            }

            if(seleccionado)
                cliente.Send($"AX{cuenta.Game.Server.id}", true);
        }

        [PackageAttribut("AXK")]
        public void get_Seleccion_Servidor(TcpClient cliente, string paquete)
        {
            cliente.Account.GameTicket = paquete.Substring(14);
            cliente.Account.ChangeGameServer(Hash.desencriptar_Ip(paquete.Substring(3, 8)), Hash.desencriptar_Puerto(paquete.Substring(11, 3).ToCharArray()));
        }
    }
}
