﻿using System;
using Bot_Dofus_1._29._1.Otros.Entidades.Personajes.Stats;

namespace Bot_Dofus_1._29._1.Otros.Entidades
{
    public interface Entidad : IDisposable
    {
        int id { get; set; }
        CaracteristicasInformacion caracteristicas { get; set; }
    }
}