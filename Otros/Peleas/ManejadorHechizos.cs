using Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Peleas;
using Bot_Dofus_1._29._1.Otros.Peleas.Configuracion;
using Bot_Dofus_1._29._1.Otros.Peleas.Enums;
using Bot_Dofus_1._29._1.Otros.Peleas.Peleadores;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Otros.Peleas
{
    public class ManejadorHechizos : IDisposable
    {
        private Account cuenta;
        private Map _map;
        private Fight _fight;
        private bool disposed;

        public ManejadorHechizos(Account _cuenta)
        {
            cuenta = _cuenta;
            _map = cuenta.Game.Map;
            _fight = cuenta.Game.Fight;
        }

        public async Task<ResultadoLanzandoHechizo> manejador_Hechizos(HechizoPelea hechizo)
        {
            if (hechizo.focus == HechizoFocus.CELDA_VACIA)
                return await lanzar_Hechizo_Celda_Vacia(hechizo);

            if(hechizo.metodo_lanzamiento == MetodoLanzamiento.AMBOS)
                return await get_Lanzar_Hechizo_Simple(hechizo);

            if (hechizo.metodo_lanzamiento == MetodoLanzamiento.ALEJADO && !cuenta.Game.Fight.esta_Cuerpo_A_Cuerpo_Con_Enemigo())
                return await get_Lanzar_Hechizo_Simple(hechizo);

            if (hechizo.metodo_lanzamiento == MetodoLanzamiento.CAC && cuenta.Game.Fight.esta_Cuerpo_A_Cuerpo_Con_Enemigo())
                return await get_Lanzar_Hechizo_Simple(hechizo);

            if (hechizo.metodo_lanzamiento == MetodoLanzamiento.CAC && !cuenta.Game.Fight.esta_Cuerpo_A_Cuerpo_Con_Enemigo())
                return await get_Mover_Lanzar_hechizo_Simple(hechizo, get_Objetivo_Mas_Cercano(hechizo));

            return ResultadoLanzandoHechizo.NO_LANZADO;
        }

        private async Task<ResultadoLanzandoHechizo> get_Lanzar_Hechizo_Simple(HechizoPelea hechizo)
        {
            if (_fight.get_Puede_Lanzar_hechizo(hechizo.id) != FallosLanzandoHechizo.NINGUNO)
                return ResultadoLanzandoHechizo.NO_LANZADO;

            Luchadores enemigo = get_Objetivo_Mas_Cercano(hechizo);

            if (enemigo != null)
            {
                FallosLanzandoHechizo resultado = _fight.get_Puede_Lanzar_hechizo(hechizo.id, _fight.jugador_luchador.celda, enemigo.celda, _map);
                
                if (resultado == FallosLanzandoHechizo.NINGUNO)
                {
                    await _fight.get_Lanzar_Hechizo(hechizo.id, enemigo.celda.id);
                    return ResultadoLanzandoHechizo.LANZADO;
                }

                if (resultado == FallosLanzandoHechizo.NO_ESTA_EN_RANGO)
                    return await get_Mover_Lanzar_hechizo_Simple(hechizo, enemigo);
            }
            else if (hechizo.focus == HechizoFocus.CELDA_VACIA)
                return await lanzar_Hechizo_Celda_Vacia(hechizo);

            return ResultadoLanzandoHechizo.NO_LANZADO;
        }

        private async Task<ResultadoLanzandoHechizo> get_Mover_Lanzar_hechizo_Simple(HechizoPelea hechizo_pelea, Luchadores enemigo)
        {
            KeyValuePair<short, MovimientoNodo>? nodo = null;
            int pm_utilizados = 99;

            foreach (KeyValuePair<short, MovimientoNodo> movimiento in PeleasPathfinder.get_Celdas_Accesibles(_fight, _map, _fight.jugador_luchador.celda))
            {
                if (!movimiento.Value.alcanzable)
                    continue;

                if (hechizo_pelea.metodo_lanzamiento == MetodoLanzamiento.CAC && !_fight.esta_Cuerpo_A_Cuerpo_Con_Aliado(_map.get_Celda_Id(movimiento.Key)))
                    continue;

                if (_fight.get_Puede_Lanzar_hechizo(hechizo_pelea.id, _map.get_Celda_Id(movimiento.Key), enemigo.celda, _map) != FallosLanzandoHechizo.NINGUNO)
                    continue;

                if (movimiento.Value.camino.celdas_accesibles.Count <= pm_utilizados)
                {
                    nodo = movimiento;
                    pm_utilizados = movimiento.Value.camino.celdas_accesibles.Count;
                }
            }

            if (nodo != null)
            {
                await cuenta.Game.Handler.movimientos.get_Mover_Celda_Pelea(nodo);
                return ResultadoLanzandoHechizo.MOVIDO;
            }

            return ResultadoLanzandoHechizo.NO_LANZADO;
        }

        private async Task<ResultadoLanzandoHechizo> lanzar_Hechizo_Celda_Vacia(HechizoPelea hechizo_pelea)
        {
            if (_fight.get_Puede_Lanzar_hechizo(hechizo_pelea.id) != FallosLanzandoHechizo.NINGUNO)
                return ResultadoLanzandoHechizo.NO_LANZADO;

            if (hechizo_pelea.focus == HechizoFocus.CELDA_VACIA && _fight.get_Cuerpo_A_Cuerpo_Enemigo().Count() == 4)
                return ResultadoLanzandoHechizo.NO_LANZADO;

            Hechizo hechizo = cuenta.Game.Character.get_Hechizo(hechizo_pelea.id);
            HechizoStats datos_hechizo = hechizo.get_Stats();

            List<short> rangos_disponibles = _fight.get_Rango_hechizo(_fight.jugador_luchador.celda, datos_hechizo, _map);
            foreach (short rango in rangos_disponibles)
            {
                if (_fight.get_Puede_Lanzar_hechizo(hechizo_pelea.id, _fight.jugador_luchador.celda, _map.get_Celda_Id(rango), _map) == FallosLanzandoHechizo.NINGUNO)
                {
                    if (hechizo_pelea.metodo_lanzamiento == MetodoLanzamiento.CAC || hechizo_pelea.metodo_lanzamiento == MetodoLanzamiento.AMBOS && _map.get_Celda_Id(rango).get_Distancia_Entre_Dos_Casillas(_fight.jugador_luchador.celda) != 1)
                        continue;

                    await _fight.get_Lanzar_Hechizo(hechizo_pelea.id, rango);
                    return ResultadoLanzandoHechizo.LANZADO;
                }
            }

            return ResultadoLanzandoHechizo.NO_LANZADO;
        }

        private Luchadores get_Objetivo_Mas_Cercano(HechizoPelea hechizo)
        {
            if (hechizo.focus == HechizoFocus.ENCIMA)
                return _fight.jugador_luchador;

            if (hechizo.focus == HechizoFocus.CELDA_VACIA)
                return null;

            return hechizo.focus == HechizoFocus.ENEMIGO ? _fight.get_Obtener_Enemigo_Mas_Cercano() : _fight.get_Obtener_Aliado_Mas_Cercano();
        }


        #region Zona Dispose
        public void Dispose() => Dispose(true);
        ~ManejadorHechizos() => Dispose(false);
        
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                cuenta = null;
                _map = null;
                _fight = null;
                disposed = true;
            }
        }
        #endregion
    }
}
