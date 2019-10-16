using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;
using System.Threading.Tasks;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
    internal class ServerSelectionFrame : Frame
    {
        [PackageAttribut("HG")]
        public void bienvenida_Juego(TcpClient cliente, string paquete) => cliente.Send("AT" + cliente.Account.GameTicket);

        [PackageAttribut("ATK0")]
        public void resultado_Servidor_Seleccion(TcpClient cliente, string paquete)
        {
            cliente.Send("Ak0");
            cliente.Send("AV");
        }

        [PackageAttribut("AV0")]
        public void lista_Personajes(TcpClient cliente, string paquete)
        {
            cliente.Send("Ages");
            cliente.Send("AL");
            cliente.Send("Af");
        }

        [PackageAttribut("ALK")]
        public void seleccionar_Personaje(TcpClient cliente, string paquete)
        {
            Account cuenta = cliente.Account;
            string[] _loc6_ = paquete.Substring(3).Split('|');
            int contador = 2;
            bool encontrado = false;

            while (contador < _loc6_.Length && !encontrado)
            {
                string[] _loc11_ = _loc6_[contador].Split(';');
                int id = int.Parse(_loc11_[0]);
                string nombre = _loc11_[1];

                if (nombre.ToLower().Equals(cuenta.AccountConfiguration.characterNumber.ToLower()) || string.IsNullOrEmpty(cuenta.AccountConfiguration.characterNumber))
                {
                    cliente.Send("AS" + id, true);
                    encontrado = true;
                }

                contador++;
            }
        }

        [PackageAttribut("BT")]
        public void get_Tiempo_Servidor(TcpClient cliente, string paquete) => cliente.Send("GI");

        [PackageAttribut("ASK")]
        public void personaje_Seleccionado(TcpClient cliente, string paquete)
        {
            Account cuenta = cliente.Account;
            string[] _loc4 = paquete.Substring(4).Split('|');

            int id = int.Parse(_loc4[0]);
            string nombre = _loc4[1];
            byte nivel = byte.Parse(_loc4[2]);
            byte raza_id = byte.Parse(_loc4[3]);
            byte sexo = byte.Parse(_loc4[4]);

            cuenta.Game.Character.set_Datos_Personaje(id, nombre, nivel, sexo, raza_id);
            cuenta.Game.Character.inventario.agregar_Objetos(_loc4[9]);

            cliente.Send("GC1");
            cliente.Send("BYA");

            cuenta.Game.Character.evento_Personaje_Seleccionado();
            cuenta.Game.Character.timer_afk.Change(1200000, 1200000);
            cliente.Account.AccountStatus = AccountStatus.ConnectedInactive;
        }
    }
}
