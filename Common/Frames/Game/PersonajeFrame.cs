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
    class PersonajeFrame : Frame
    {
        [PackageAttribut("As")]
        public void get_Stats_Actualizados(ClienteTcp cliente, string package) => cliente.Account.juego.personaje.actualizar_Caracteristicas(package);

        [PackageAttribut("PIK")]
        public void get_Peticion_Grupo(ClienteTcp cliente, string package)
        {
            cliente.Account.logger.log_informacion("Grupo", $"Nueva invitación de grupo del personaje: {package.Substring(3).Split('|')[0]}");
            cliente.SendPackage("PR");
            cliente.Account.logger.log_informacion("Grupo", "Petición rechazada");
        }

        [PackageAttribut("SL")]
        public void get_Lista_Hechizos(ClienteTcp cliente, string package)
        {
            if (!package[2].Equals('o'))
                cliente.Account.juego.personaje.actualizar_Hechizos(package.Substring(2));
        }

        [PackageAttribut("Ow")]
        public void get_Actualizacion_Pods(ClienteTcp cliente, string package)
        {
            string[] pods = package.Substring(2).Split('|');
            short pods_actuales = short.Parse(pods[0]);
            short pods_maximos = short.Parse(pods[1]);
            PersonajeJuego personaje = cliente.Account.juego.personaje;

            personaje.inventario.pods_actuales = pods_actuales;
            personaje.inventario.pods_maximos = pods_maximos;
            cliente.Account.juego.personaje.evento_Pods_Actualizados();
        }

        [PackageAttribut("DV")]
        public void get_Cerrar_Dialogo(ClienteTcp cliente, string package)
        {
            Account cuenta = cliente.Account;

            switch (cuenta.Estado_Account)
            {
                case StateAccount.BANKING:
                    cuenta.juego.personaje.inventario.evento_Almacenamiento_Abierto();
                    break;

                case StateAccount.DIALOG:
                    IEnumerable<Npcs> npcs = cuenta.juego.mapa.lista_npcs();
                    Npcs npc = npcs.ElementAt((cuenta.juego.personaje.hablando_npc_id * -1) - 1);
                    npc.respuestas.Clear();
                    npc.respuestas = null;

                    cuenta.Estado_Account = StateAccount.AWAY;
                    cuenta.juego.personaje.evento_Dialogo_Acabado();
                break;
            }
        }

        [PackageAttribut("EV")]
        public void get_Ventana_Cerrada(ClienteTcp cliente, string package)
        {
            Account cuenta = cliente.Account;

            if (cuenta.Estado_Account == StateAccount.BANKING)
            {
                cuenta.Estado_Account = StateAccount.AWAY;
                cuenta.juego.personaje.inventario.evento_Almacenamiento_Cerrado();
            }
        }

        [PackageAttribut("JS")]
        public void get_Skills_Oficio(ClienteTcp cliente, string package)
        {
            string[] separador_skill;
            PersonajeJuego personaje = cliente.Account.juego.personaje;
            Oficio oficio;
            SkillsOficio skill = null;
            short id_oficio, id_skill;
            byte cantidad_minima, cantidad_maxima;
            float tiempo;

            foreach (string datos_oficio in package.Substring(3).Split('|'))
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
        public void get_Experiencia_Oficio(ClienteTcp cliente, string package)
        {
            string[] separador_oficio_experiencia = package.Substring(3).Split('|');
            PersonajeJuego personaje = cliente.Account.juego.personaje;
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
        public void get_Datos_Montura(ClienteTcp cliente, string package) => cliente.Account.puede_utilizar_dragopavo = true;

        [PackageAttribut("OAKO")]
        public void get_Aparecer_Objeto(ClienteTcp cliente, string package) => cliente.Account.juego.personaje.inventario.agregar_Objetos(package.Substring(4));

        [PackageAttribut("OR")]
        public void get_Eliminar_Objeto(ClienteTcp cliente, string package) => cliente.Account.juego.personaje.inventario.eliminar_Objeto(uint.Parse(package.Substring(2)), 1, false);

        [PackageAttribut("OQ")]
        public void get_Modificar_Cantidad_Objeto(ClienteTcp cliente, string package) => cliente.Account.juego.personaje.inventario.modificar_Objetos(package.Substring(2));

        [PackageAttribut("ECK")]
        public void get_Intercambio_Ventana_Abierta(ClienteTcp cliente, string package) => cliente.Account.Estado_Account = StateAccount.BANKING;

        [PackageAttribut("PCK")]
        public void get_Grupo_Aceptado(ClienteTcp cliente, string package) => cliente.Account.juego.personaje.en_grupo = true;

        [PackageAttribut("PV")]
        public void get_Grupo_Abandonado(ClienteTcp cliente, string package) => cliente.Account.juego.personaje.en_grupo = true;

        [PackageAttribut("ERK")]
        public void get_Peticion_Intercambio(ClienteTcp cliente, string package)
        {
            cliente.Account.logger.log_informacion("INFORMACIÓN", "Invitación de intercambio recibida, rechazando");
            cliente.SendPackage("EV", true);
        }

        [PackageAttribut("ILS")]
        public void get_Tiempo_Regenerado(ClienteTcp cliente, string package)
        {
            package = package.Substring(3);
            int tiempo = int.Parse(package);
            Account cuenta = cliente.Account;
            PersonajeJuego personaje = cuenta.juego.personaje;

            personaje.timer_regeneracion.Change(Timeout.Infinite, Timeout.Infinite);
            personaje.timer_regeneracion.Change(tiempo, tiempo);

            cuenta.logger.log_informacion("DOFUS", $"Tú personaje recupera 1 pdv cada {tiempo / 1000} segundos");
        }

        [PackageAttribut("ILF")]
        public void get_Cantidad_Vida_Regenerada(ClienteTcp cliente, string package)
        {
            package = package.Substring(3);
            int vida = int.Parse(package);
            Account cuenta = cliente.Account;
            PersonajeJuego personaje = cuenta.juego.personaje;

            personaje.caracteristicas.vitalidad_actual += vida;
            cuenta.logger.log_informacion("DOFUS", $"Has recuperado {vida} puntos de vida");
        }

        [PackageAttribut("eUK")]
        public void get_Emote_Recibido(ClienteTcp cliente, string package)
        {
            string[] separador = package.Substring(3).Split('|');
            int id = int.Parse(separador[0]), emote_id = int.Parse(separador[1]);
            Account cuenta = cliente.Account;

            if (cuenta.juego.personaje.id != id)
                return;

            if (emote_id == 1 && cuenta.Estado_Account != StateAccount.REGENERATING)
                cuenta.Estado_Account = StateAccount.REGENERATING;
            else if (emote_id == 0 && cuenta.Estado_Account == StateAccount.REGENERATING)
                cuenta.Estado_Account = StateAccount.AWAY;
        }

        [PackageAttribut("Bp")]
        public void get_Ping_Promedio(ClienteTcp cliente, string package) => cliente.SendPackage($"Bp{cliente.GetAveragePings()}|{cliente.get_Total_Pings()}|50");

        [PackageAttribut("pong")]
        public void get_Ping_Pong(ClienteTcp cliente, string package) => cliente.Account.logger.log_informacion("DOFUS", $"Ping: {cliente.get_Actual_Ping()} ms");
    }
}
