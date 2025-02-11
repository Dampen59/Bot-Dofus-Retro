﻿using MoonSharp.Interpreter;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Otros.Scripts.Api
{
    [MoonSharpUserData]
    public class PersonajeApi
    {
        private Account cuenta;
        private bool disposed = false;

        public PersonajeApi(Account _cuenta) => cuenta = _cuenta;

        public string nombre() => cuenta.Game.Character.nombre;
        public byte nivel() => cuenta.Game.Character.nivel;
        public int experiencia() => cuenta.Game.Character.porcentaje_experiencia;
        public int kamas() => cuenta.Game.Character.kamas;

        #region Zona Dispose
        ~PersonajeApi() => Dispose(false);
        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                cuenta = null;
                disposed = true;
            }
        }
        #endregion
    }
}
