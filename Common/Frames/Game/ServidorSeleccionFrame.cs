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
    internal class ServidorSeleccionFrame : Frame
    {
        [PackageAttribut("HG")]
        public void bienvenida_Juego(ClienteTcp cliente, string package) => cliente.SendPackage("AT" + cliente.Account.tiquet_game);

        [PackageAttribut("ATK0")]
        public void resultado_Servidor_Seleccion(ClienteTcp cliente, string package)
        {
            cliente.SendPackage("Ak0");
            cliente.SendPackage("AV");
        }

        [PackageAttribut("AV0")]
        public void lista_Personajes(ClienteTcp cliente, string package)
        {
            cliente.SendPackage("Ages");
            cliente.SendPackage("AL");
            cliente.SendPackage("Af");
        }

        [PackageAttribut("ALK")]
        public void seleccionar_Personaje(ClienteTcp cliente, string package)
        {
            Account cuenta = cliente.Account;
            string[] _loc6_ = package.Substring(3).Split('|');
            int contador = 2;
            bool encontrado = false;

            while (contador < _loc6_.Length && !encontrado)
            {
                string[] _loc11_ = _loc6_[contador].Split(';');
                int id = int.Parse(_loc11_[0]);
                string nombre = _loc11_[1];

                if (nombre.ToLower().Equals(cuenta.configuracion.nombre_personaje.ToLower()) || string.IsNullOrEmpty(cuenta.configuracion.nombre_personaje))
                {
                    cliente.SendPackage("AS" + id, true);
                    encontrado = true;
                }

                contador++;
            }
        }

        [PackageAttribut("BT")]
        public void get_Tiempo_Servidor(ClienteTcp cliente, string package) => cliente.SendPackage("GI");

        [PackageAttribut("ASK")]
        public void personaje_Seleccionado(ClienteTcp cliente, string package)
        {
            Account cuenta = cliente.Account;
            string[] _loc4 = package.Substring(4).Split('|');

            int id = int.Parse(_loc4[0]);
            string nombre = _loc4[1];
            byte nivel = byte.Parse(_loc4[2]);
            byte raza_id = byte.Parse(_loc4[3]);
            byte sexo = byte.Parse(_loc4[4]);

            cuenta.juego.personaje.set_Datos_Personaje(id, nombre, nivel, sexo, raza_id);
            cuenta.juego.personaje.inventario.agregar_Objetos(_loc4[9]);

            cliente.SendPackage("GC1");
            cliente.SendPackage("BYA");

            cuenta.juego.personaje.evento_Personaje_Seleccionado();
            cuenta.juego.personaje.timer_afk.Change(1200000, 1200000);
            cliente.Account.Estado_Account = StateAccount.AWAY;
        }
    }
}
