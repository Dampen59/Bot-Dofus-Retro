using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Recolecciones;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using Bot_Dofus_1._29._1.Otros.Peleas;
using Bot_Dofus_1._29._1.Otros.Peleas.Enums;
using Bot_Dofus_1._29._1.Otros.Peleas.Peleadores;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;
using Bot_Dofus_1._29._1.Utilidades.Criptografia;
using System.Threading.Tasks;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
    internal class MapaFrame : Frame
    {
        [PaqueteAtributo("GM")]
        public async Task get_Movimientos_Personajes(TcpClient cliente, string paquete)
        {
            Account cuenta = cliente.Account;
            string[] separador_jugadores = paquete.Substring(3).Split('|'), informaciones;
            string _loc6, nombre_template, tipo;

            for (int i = 0; i < separador_jugadores.Length; ++i)
            {
                _loc6 = separador_jugadores[i];
                if (_loc6.Length != 0)
                {
                    informaciones = _loc6.Substring(1).Split(';');
                    if (_loc6[0].Equals('+'))
                    {
                        Celda celda = cuenta.Game.Map.get_Celda_Id(short.Parse(informaciones[0]));
                        Fight fight = cuenta.Game.Fight;
                        int id = int.Parse(informaciones[3]);
                        nombre_template = informaciones[4];
                        tipo = informaciones[5];
                        if (tipo.Contains(","))
                            tipo = tipo.Split(',')[0];

                        switch (int.Parse(tipo))
                        {
                            case -1:
                            case -2:
                                if (cuenta.AccountStatus == AccountStatus.Fighting)
                                {
                                    int vida = int.Parse(informaciones[12]);
                                    byte pa = byte.Parse(informaciones[13]);
                                    byte pm = byte.Parse(informaciones[14]);
                                    byte equipo = byte.Parse(informaciones[15]);

                                    fight.get_Agregar_Luchador(new Luchadores(id, true, vida, pa, pm, celda, vida, equipo));
                                }
                            break;

                            case -3://monstruos
                                string[] templates = nombre_template.Split(',');
                                string[] niveles = informaciones[7].Split(',');
                                
                                Monstruos monstruo = new Monstruos(id, int.Parse(templates[0]), celda, int.Parse(niveles[0]));
                                monstruo.lider_grupo = monstruo;

                                for (int m = 1; m < templates.Length; ++m)
                                    monstruo.moobs_dentro_grupo.Add(new Monstruos(id, int.Parse(templates[m]), celda, int.Parse(niveles[m])));

                                cuenta.Game.Map.entidades.TryAdd(id, monstruo);
                            break;

                            case -4://NPC
                                cuenta.Game.Map.entidades.TryAdd(id, new Npcs(id, int.Parse(nombre_template), celda));
                            break;

                            case -5:
                            case -6:
                            case -7:
                            case -8:
                            case -9:
                            case -10:
                            break;

                            default:// jugador
                                if (cuenta.AccountStatus != AccountStatus.Fighting)
                                {
                                    if (cuenta.Game.Character.id != id)
                                        cuenta.Game.Map.entidades.TryAdd(id, new Personajes(id, nombre_template, byte.Parse(informaciones[7].ToString()), celda));
                                    else
                                        cuenta.Game.Character.celda = celda;
                                }
                                else
                                {
                                    int vida = int.Parse(informaciones[14]);
                                    byte pa = byte.Parse(informaciones[15]);
                                    byte pm = byte.Parse(informaciones[16]);
                                    byte equipo = byte.Parse(informaciones[24]);

                                    fight.get_Agregar_Luchador(new Luchadores(id, true, vida, pa, pm, celda, vida, equipo));

                                    if (cuenta.Game.Character.id == id && cuenta.CombatExtensions.configuracion.posicionamiento != PosicionamientoInicioPelea.INMOVIL)
                                    {
                                        await Task.Delay(300);

                                        /** la posicion es aleatoria pero el paquete GP siempre aparecera primero el team donde esta el pj **/
                                        short celda_posicion = fight.get_Celda_Mas_Cercana_O_Lejana(cuenta.CombatExtensions.configuracion.posicionamiento == PosicionamientoInicioPelea.CERCA_DE_ENEMIGOS, fight.celdas_preparacion);

                                        if (celda_posicion != celda.id)
                                            cuenta.Connection.enviar_Paquete("Gp" + celda_posicion, true);
                                        else
                                            cuenta.Connection.enviar_Paquete("GR1");
                                    }
                                    else if (cuenta.Game.Character.id == id)
                                    {
                                        await Task.Delay(300);
                                        cuenta.Connection.enviar_Paquete("GR1");//boton listo
                                    }
                                }
                             break;
                        }
                    }
                    else if (_loc6[0].Equals('-'))
                    {
                        if (cuenta.AccountStatus != AccountStatus.Fighting)
                        {
                            int id = int.Parse(_loc6.Substring(1));
                            cuenta.Game.Map.entidades.TryRemove(id, out Entidad entidad);
                        }
                    }
                }
            }
        }

        [PaqueteAtributo("GAF")]
        public void get_Finalizar_Accion(TcpClient cliente, string paquete)
        {
            string[] id_fin_accion = paquete.Substring(3).Split('|');

            cliente.Account.Connection.enviar_Paquete("GKK" + id_fin_accion[0]);
        }

        [PaqueteAtributo("GAS")]
        public async Task get_Inicio_Accion(TcpClient cliente, string paquete) => await Task.Delay(200);

        [PaqueteAtributo("GA")]
        public async Task get_Iniciar_Accion(TcpClient cliente, string paquete)
        {
            string[] separador = paquete.Substring(2).Split(';');
            int id_accion = int.Parse(separador[1]);
            Account cuenta = cliente.Account;
            GameCharacter personaje = cuenta.Game.Character;

            if (id_accion > 0)
            {
                int id_entidad = int.Parse(separador[2]);
                byte tipo_gkk_movimiento;
                Celda celda;
                Luchadores luchador;
                Map map = cuenta.Game.Map;
                Fight fight = cuenta.Game.Fight;

                switch (id_accion)
                {
                    case 1:
                        celda = map.get_Celda_Id(Hash.get_Celda_Id_Desde_hash(separador[3].Substring(separador[3].Length - 2)));

                        if (!cuenta.IsFighting())
                        {
                            if (id_entidad == personaje.id && celda.id > 0 && personaje.celda.id != celda.id)
                            {
                                tipo_gkk_movimiento = byte.Parse(separador[0]);

                                await cuenta.Game.Handler.movimientos.evento_Movimiento_Finalizado(celda, tipo_gkk_movimiento, true);
                            }
                            else if (map.entidades.TryGetValue(id_entidad, out Entidad entidad))
                            {
                                entidad.celda = celda;

                                if (GlobalConf.mostrar_mensajes_debug)
                                    cuenta.Logger.log_informacion("DEBUG", "Detectado movimiento de una entidad a la casilla: " + celda.id);
                            }
                            map.evento_Entidad_Actualizada();
                        }
                        else
                        {
                            luchador = fight.get_Luchador_Por_Id(id_entidad);
                            if (luchador != null)
                            {
                                luchador.celda = celda;

                                if (luchador.id == personaje.id)
                                {
                                    tipo_gkk_movimiento = byte.Parse(separador[0]);

                                    await Task.Delay(400 + (100 * personaje.celda.get_Distancia_Entre_Dos_Casillas(celda)));
                                    cuenta.Connection.enviar_Paquete("GKK" + tipo_gkk_movimiento);
                                }
                            }
                        }
                    break;

                    case 4:
                        separador = separador[3].Split(',');
                        celda = map.get_Celda_Id(short.Parse(separador[1]));

                        if (!cuenta.IsFighting() && id_entidad == personaje.id && celda.id > 0 && personaje.celda.id != celda.id)
                        {
                            personaje.celda = celda;
                            await Task.Delay(150);
                            cuenta.Connection.enviar_Paquete("GKK1");
                            map.evento_Entidad_Actualizada();
                            cuenta.Game.Handler.movimientos.movimiento_Actualizado(true);
                        }
                   break;

                    case 5:
                        if (cuenta.IsFighting())
                        {
                            separador = separador[3].Split(',');
                            luchador = fight.get_Luchador_Por_Id(int.Parse(separador[0]));

                            if (luchador != null)
                                luchador.celda = map.get_Celda_Id(short.Parse(separador[1]));
                        }
                        break;

                    case 102:
                        if (cuenta.IsFighting())
                        {
                            luchador = fight.get_Luchador_Por_Id(id_entidad);
                            byte pa_utilizados = byte.Parse(separador[3].Split(',')[1].Substring(1));

                            if (luchador != null)
                                luchador.pa -= pa_utilizados;
                        }
                        break;

                    case 103:
                        if (cuenta.IsFighting())
                        {
                            int id_muerto = int.Parse(separador[3]);

                            luchador = fight.get_Luchador_Por_Id(id_muerto);
                            if (luchador != null)
                                luchador.esta_vivo = false;
                        }
                        break;

                    case 129: //movimiento en pelea con exito
                        if (cuenta.IsFighting())
                        {
                            luchador = fight.get_Luchador_Por_Id(id_entidad);
                            byte pm_utilizados = byte.Parse(separador[3].Split(',')[1].Substring(1));

                            if (luchador != null)
                                luchador.pm -= pm_utilizados;

                            if (luchador.id == personaje.id)
                                fight.get_Movimiento_Exito(true);
                        }
                        break;

                    case 151://obstaculos invisibles
                        if (cuenta.IsFighting())
                        {
                            luchador = fight.get_Luchador_Por_Id(id_entidad);

                            if (luchador != null && luchador.id == personaje.id)
                            {
                                cuenta.Logger.log_Error("INFORMACIÓN", "No es posible realizar esta acción por culpa de un obstáculo invisible.");
                                fight.get_Hechizo_Lanzado(short.Parse(separador[3]), false);
                            }
                        }
                    break;

                    case 181: //efecto de invocacion (pelea)
                        celda = map.get_Celda_Id(short.Parse(separador[3].Substring(1)));
                        short id_luchador = short.Parse(separador[6]);
                        short vida = short.Parse(separador[15]);
                        byte pa = byte.Parse(separador[16]);
                        byte pm = byte.Parse(separador[17]);
                        byte equipo = byte.Parse(separador[25]);

                        fight.get_Agregar_Luchador(new Luchadores(id_luchador, true, vida, pa, pm, celda, vida, equipo, id_entidad));
                    break;

                    case 302://fallo critico
                        if (cuenta.IsFighting() && id_entidad == cuenta.Game.Character.id)
                            fight.get_Hechizo_Lanzado(0, false);
                   break;

                    case 300: //hechizo lanzado con exito
                        if (cuenta.IsFighting() && id_entidad == cuenta.Game.Character.id)
                        {
                            short celda_id_lanzado = short.Parse(separador[3].Split(',')[1]);
                            fight.get_Hechizo_Lanzado(celda_id_lanzado, true);
                        }
                    break;

                    case 501:
                        int tiempo_recoleccion = int.Parse(separador[3].Split(',')[1]);
                        celda = map.get_Celda_Id(short.Parse(separador[3].Split(',')[0]));
                        byte tipo_gkk_recoleccion = byte.Parse(separador[0]);

                        await cuenta.Game.Handler.recoleccion.evento_Recoleccion_Iniciada(id_entidad, tiempo_recoleccion, celda.id, tipo_gkk_recoleccion);
                    break;

                    case 900:
                        cuenta.Connection.enviar_Paquete("GA902" + id_entidad, true);
                        cuenta.Logger.log_informacion("INFORMACIÓN", "Desafio del Character id: " + id_entidad + " cancelado");
                    break;
                }
            }
        }

        [PaqueteAtributo("GDF")]
        public void get_Estado_Interactivo(TcpClient cliente, string paquete)
        {
            foreach (string interactivo in paquete.Substring(4).Split('|'))
            {
                string[] separador = interactivo.Split(';');
                Account cuenta = cliente.Account;
                short celda_id = short.Parse(separador[0]);
                byte estado = byte.Parse(separador[1]);

                switch (estado)
                {
                    case 2:
                        cuenta.Game.Map.interactivos[celda_id].es_utilizable = false;
                    break;

                    case 3:
                        cuenta.Game.Map.interactivos[celda_id].es_utilizable = false;

                        if(cuenta.IsCollecting())
                            cuenta.Game.Handler.recoleccion.evento_Recoleccion_Acabada(RecoleccionResultado.RECOLECTADO, celda_id);
                        else
                            cuenta.Game.Handler.recoleccion.evento_Recoleccion_Acabada(RecoleccionResultado.ROBADO, celda_id);
                    break;

                    case 4:// reaparece asi se fuerza el cambio de Map 
                        cuenta.Game.Map.interactivos[celda_id].es_utilizable = false;
                    break;
                }
            }
        }

        [PaqueteAtributo("GDM")]
        public void get_Nuevo_Mapa(TcpClient cliente, string paquete) => cliente.Account.Game.Map.get_Actualizar_Mapa(paquete.Substring(4));

        [PaqueteAtributo("GDK")]
        public void get_Mapa_Cambiado(TcpClient cliente, string paquete) => cliente.Account.Game.Map.get_Evento_Mapa_Cambiado();

        [PaqueteAtributo("GV")]
        public void get_Reiniciar_Pantalla(TcpClient cliente, string paquete) => cliente.Account.Connection.enviar_Paquete("GC1");
    }
}
