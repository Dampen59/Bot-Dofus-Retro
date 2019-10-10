using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
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
        public void get_Key_BienvenidaAsync(ClienteTcp cliente, string package)
        {
            Account cuenta = cliente.Account;

            cuenta.Estado_Account = StateAccount.CONNECTED;
            cuenta.key_bienvenida = package.Substring(2);

            cliente.SendPackage("1.30");
            cliente.SendPackage(cliente.Account.configuracion.nombre_cuenta + "\n" + Hash.encriptar_Password(cliente.Account.configuracion.password, cliente.Account.key_bienvenida));
            cliente.SendPackage("Af");
        }

        [PackageAttribut("Ad")]
        public void get_Apodo(ClienteTcp cliente, string package) => cliente.Account.apodo = package.Substring(2);

        [PackageAttribut("Af")]
        public void get_Fila_Espera_Login(ClienteTcp cliente, string package) => cliente.Account.logger.log_informacion("FILA DE ESPERA", "Posición " + package[2] + "/" + package[4]);

        [PackageAttribut("AH")]
        public void get_Servidor_Estado(ClienteTcp cliente, string package)
        {
            Account cuenta = cliente.Account;
            string[] separado_servidores = package.Substring(2).Split('|');
            ServidorJuego servidor = cuenta.juego.servidor;
            bool primera_vez = true;

            foreach(string sv in separado_servidores)
            {
                string[] separador = sv.Split(';');

                int id = int.Parse(separador[0]);
                EstadosServidor estado = (EstadosServidor)byte.Parse(separador[1]);
                string nombre = id == 601 ? "Eratz" : "Henual";

                if (id == cuenta.configuracion.get_Servidor_Id())
                {
                    servidor.actualizar_Datos(id, nombre, estado);
                    cuenta.logger.log_informacion("LOGIN", $"El servidor {nombre} esta {estado}");

                    if (estado != EstadosServidor.CONECTADO)
                        primera_vez = false;
                }
            }

            if(!primera_vez  && servidor.estado == EstadosServidor.CONECTADO)
                cliente.SendPackage("Ax");
        }

        [PackageAttribut("AQ")]
        public void get_Pregunta_Secreta(ClienteTcp cliente, string package)
        {
            if (cliente.Account.juego.servidor.estado == EstadosServidor.CONECTADO)
                cliente.SendPackage("Ax", true);
        }

        [PackageAttribut("AxK")]
        public void get_Servidores_Lista(ClienteTcp cliente, string package)
        {
            Account cuenta = cliente.Account;
            string[] loc5 = package.Substring(3).Split('|');
            int contador = 1;
            bool seleccionado = false;

            while (contador < loc5.Length && !seleccionado)
            {
                string[] _loc10_ = loc5[contador].Split(',');
                int servidor_id = int.Parse(_loc10_[0]);

                if (servidor_id == cuenta.juego.servidor.id)
                {
                    if(cuenta.juego.servidor.estado == EstadosServidor.CONECTADO)
                    {
                        seleccionado = true;
                        cuenta.juego.personaje.evento_Servidor_Seleccionado();
                    }
                    else
                        cuenta.logger.log_Error("LOGIN", "Servidor no accesible cuando este accesible se re-Connecteda");
                }
                contador++;
            }

            if(seleccionado)
                cliente.SendPackage($"AX{cuenta.juego.servidor.id}", true);
        }

        [PackageAttribut("AXK")]
        public void get_Seleccion_Servidor(ClienteTcp cliente, string package)
        {
            cliente.Account.tiquet_game = package.Substring(14);
            cliente.Account.ChangingToGameServer(Hash.desencriptar_Ip(package.Substring(3, 8)), Hash.desencriptar_Puerto(package.Substring(11, 3).ToCharArray()));
        }
    }
}
