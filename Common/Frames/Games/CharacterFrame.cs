using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Oficios;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
    class CharacterFrame : Frame
    {
        [PackageAttribut("As")]
        public void get_Stats_Updated(TcpClient cliente, string paquete) => cliente.Account.Game.Character.actualizar_Caracteristicas(paquete);

        [PackageAttribut("PIK")]
        public void get_Request_Group(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_information("Group", $"New Character group invitation: {paquete.Substring(3).Split('|')[0]}");
            cliente.Send("PR");
            cliente.Account.Logger.log_information("Group", "Invitation Rejected");
        }

        [PackageAttribut("SL")]
        public void get_List_Spells(TcpClient cliente, string paquete)
        {
            if (!paquete[2].Equals('o'))
                cliente.Account.Game.Character.actualizar_Hechizos(paquete.Substring(2));
        }

        [PackageAttribut("Ow")]
        public void get_Update_Pods(TcpClient cliente, string paquete)
        {
            string[] pods = paquete.Substring(2).Split('|');
            short pods_actuales = short.Parse(pods[0]);
            short pods_maximos = short.Parse(pods[1]);
            GameCharacter personaje = cliente.Account.Game.Character;

            personaje.inventario.pods_actuales = pods_actuales;
            personaje.inventario.pods_maximos = pods_maximos;
            cliente.Account.Game.Character.evento_Pods_Actualizados();
        }

        [PackageAttribut("DV")]
        public void get_Close_Dialogue(TcpClient cliente, string paquete)
        {
            Account cuenta = cliente.Account;

            switch (cuenta.AccountStatus)
            {
                case AccountStatus.Storing:
                    cuenta.Game.Character.inventario.evento_Almacenamiento_Abierto();
                    break;

                case AccountStatus.Dialoguing:
                    IEnumerable<Npcs> npcs = cuenta.Game.Map.lista_npcs();
                    Npcs npc = npcs.ElementAt((cuenta.Game.Character.hablando_npc_id * -1) - 1);
                    npc.respuestas.Clear();
                    npc.respuestas = null;

                    cuenta.AccountStatus = AccountStatus.ConnectedInactive;
                    cuenta.Game.Character.evento_Dialogo_Acabado();
                break;
            }
        }

        [PackageAttribut("EV")]
        public void get_Window_Closed(TcpClient cliente, string paquete)
        {
            Account cuenta = cliente.Account;

            if (cuenta.AccountStatus == AccountStatus.Storing)
            {
                cuenta.AccountStatus = AccountStatus.ConnectedInactive;
                cuenta.Game.Character.inventario.evento_Almacenamiento_Cerrado();
            }
        }

        [PackageAttribut("JS")]
        public void get_Skills_Office(TcpClient cliente, string paquete)
        {
            string[] separador_skill;
            GameCharacter personaje = cliente.Account.Game.Character;
            Oficio oficio;
            SkillsOficio skill = null;
            short id_oficio, id_skill;
            byte cantidad_minima, cantidad_maxima;
            float tiempo;

            foreach (string datos_oficio in paquete.Substring(3).Split('|'))
            {
                id_oficio = short.Parse(datos_oficio.Split(';')[0]);
                oficio = personaje.oficios.Find(x => x.id == id_oficio);

                if (oficio == null)
                {
                    oficio = new Oficio(id_oficio);
                    personaje.oficios.Add(oficio);
                }

                foreach (string datos_skill in datos_oficio.Split(';')[1].Split(','))
                {
                    separador_skill = datos_skill.Split('~');
                    id_skill = short.Parse(separador_skill[0]);
                    cantidad_minima = byte.Parse(separador_skill[1]);
                    cantidad_maxima = byte.Parse(separador_skill[2]);
                    tiempo = float.Parse(separador_skill[4]);
                    skill = oficio.skills.Find(actividad => actividad.id == id_skill);

                    if (skill != null)
                        skill.set_Actualizar(id_skill, cantidad_minima, cantidad_maxima, tiempo);
                    else
                        oficio.skills.Add(new SkillsOficio(id_skill, cantidad_minima, cantidad_maxima, tiempo));
                }
            }

            personaje.evento_Oficios_Actualizados();
        }

        [PackageAttribut("JX")]
        public void get_Experience_Office(TcpClient cliente, string paquete)
        {
            string[] separador_oficio_experiencia = paquete.Substring(3).Split('|');
            GameCharacter personaje = cliente.Account.Game.Character;
            uint experiencia_actual, experiencia_base, experiencia_siguiente_nivel;
            short id;
            byte nivel;

            foreach (string oficio in separador_oficio_experiencia)
            {
                id = short.Parse(oficio.Split(';')[0]);
                nivel = byte.Parse(oficio.Split(';')[1]);
                experiencia_base = uint.Parse(oficio.Split(';')[2]);
                experiencia_actual = uint.Parse(oficio.Split(';')[3]);

                if (nivel < 100)
                    experiencia_siguiente_nivel = uint.Parse(oficio.Split(';')[4]);
                else
                    experiencia_siguiente_nivel = 0;

                personaje.oficios.Find(x => x.id == id).set_Actualizar_Oficio(nivel, experiencia_base, experiencia_actual, experiencia_siguiente_nivel);
            }
            personaje.evento_Oficios_Actualizados();
        }

        [PackageAttribut("Re")]
        public void get_Data_Mount(TcpClient cliente, string paquete) => cliente.Account.CanUseDrago = true;

        [PackageAttribut("OAKO")]
        public void get_appear_Object(TcpClient cliente, string paquete) => cliente.Account.Game.Character.inventario.agregar_Objetos(paquete.Substring(4));

        [PackageAttribut("OR")]
        public void get_Delete_Object(TcpClient cliente, string paquete) => cliente.Account.Game.Character.inventario.eliminar_Objeto(uint.Parse(paquete.Substring(2)), 1, false);

        [PackageAttribut("OQ")]
        public void get_Change_Object_Quantity(TcpClient cliente, string paquete) => cliente.Account.Game.Character.inventario.modificar_Objetos(paquete.Substring(2));

        [PackageAttribut("ECK")]
        public void get_Exchange_Window_Open(TcpClient cliente, string paquete) => cliente.Account.AccountStatus = AccountStatus.Storing;

        [PackageAttribut("PCK")]
        public void get_Group_Accepted(TcpClient cliente, string paquete) => cliente.Account.Game.Character.en_grupo = true;

        [PackageAttribut("PV")]
        public void get_Abandoned_Group(TcpClient cliente, string paquete) => cliente.Account.Game.Character.en_grupo = true;

        [PackageAttribut("ERK")]
        public void get_Request_Exchange(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_information("INFORMATION", "Exchange invitation received, rejecting");
            cliente.Send("EV", true);
        }

        [PackageAttribut("ILS")]
        public void get_Regenerated_Time(TcpClient cliente, string paquete)
        {
            paquete = paquete.Substring(3);
            int tiempo = int.Parse(paquete);
            Account cuenta = cliente.Account;
            GameCharacter personaje = cuenta.Game.Character;

            personaje.timer_regeneracion.Change(Timeout.Infinite, Timeout.Infinite);
            personaje.timer_regeneracion.Change(tiempo, tiempo);

            cuenta.Logger.log_information("DOFUS", $"Your Character recovers 1 pdv each {tiempo / 1000} seconds");
        }

        [PackageAttribut("ILF")]
        public void get_Quantity_Life_Regenerated(TcpClient cliente, string paquete)
        {
            paquete = paquete.Substring(3);
            int vida = int.Parse(paquete);
            Account cuenta = cliente.Account;
            GameCharacter personaje = cuenta.Game.Character;

            personaje.caracteristicas.vitalidad_actual += vida;
            cuenta.Logger.log_information("DOFUS", $"You've recovered {vida} life points");
        }

        [PackageAttribut("eUK")]
        public void get_Emote_Received(TcpClient cliente, string paquete)
        {
            string[] separador = paquete.Substring(3).Split('|');
            int id = int.Parse(separador[0]), emote_id = int.Parse(separador[1]);
            Account cuenta = cliente.Account;

            if (cuenta.Game.Character.id != id)
                return;

            if (emote_id == 1 && cuenta.AccountStatus != AccountStatus.Regenerating)
                cuenta.AccountStatus = AccountStatus.Regenerating;
            else if (emote_id == 0 && cuenta.AccountStatus == AccountStatus.Regenerating)
                cuenta.AccountStatus = AccountStatus.ConnectedInactive;
        }

        [PackageAttribut("Bp")]
        public void get_Ping_Average(TcpClient cliente, string paquete) => cliente.Send($"Bp{cliente.get_Average_Pings()}|{cliente.get_Total_Pings()}|50");

        [PackageAttribut("pong")]
        public void get_Ping_Pong(TcpClient cliente, string paquete) => cliente.Account.Logger.log_information("DOFUS", $"Ping: {cliente.get_Actual_Ping()} ms");
    }
}
