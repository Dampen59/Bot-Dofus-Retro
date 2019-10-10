using System.Reflection;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1
    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Comun.Frames.Transporte
{
    public class PackageData
    {
        public object instancia { get; set; }
        public string nombre_package { get; set; }
        public MethodInfo informacion { get; set; }

        public PackageData(object _instancia, string _nombre_package, MethodInfo _informacion)
        {
            instancia = _instancia;
            nombre_package = _nombre_package;
            informacion = _informacion;
        }
    }
}