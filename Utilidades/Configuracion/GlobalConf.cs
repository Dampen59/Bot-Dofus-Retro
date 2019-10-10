using System.Collections.Generic;
using System.IO;
using System.Linq;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Utilidades.Configuracion
{
    internal class GlobalConf
    {
        private static List<AccountConf> lista_cuentas;
        private static readonly string ruta_archivo_cuentas = Path.Combine(Directory.GetCurrentDirectory(), "cuentas.bot");
        public static bool mostrar_mensajes_debug { get; set; }
        public static string ip_conexion = "34.251.172.139";
        public static short puerto_conexion = 443;

        static GlobalConf()
        {
            lista_cuentas = new List<AccountConf>();
            mostrar_mensajes_debug = false;
        }

        public static void cargar_Todas_Accounts()
        {
            if (File.Exists(ruta_archivo_cuentas))
            {
                lista_cuentas.Clear();
                using (BinaryReader br = new BinaryReader(File.Open(ruta_archivo_cuentas, FileMode.Open)))
                {
                    int registros_totales = br.ReadInt32();
                    for (int i = 0; i < registros_totales; i++)
                    {
                        lista_cuentas.Add(AccountConf.cargar_Una_Account(br));
                    }
                    mostrar_mensajes_debug = br.ReadBoolean();
                    ip_conexion = br.ReadString();
                    puerto_conexion = br.ReadInt16();
                }
            }
            else
            {
                return;
            }
        }

        public static void guardar_Configuracion()
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(ruta_archivo_cuentas, FileMode.Create)))
            {
                bw.Write(lista_cuentas.Count);
                lista_cuentas.ForEach(a => a.guardar_Account(bw));
                bw.Write(mostrar_mensajes_debug);
                bw.Write(ip_conexion);
                bw.Write(puerto_conexion);
            }
        }

        public static void agregar_Account(string nombre_cuenta, string password, string servidor, string nombre_personaje) => lista_cuentas.Add(new AccountConf(nombre_cuenta, password, servidor, nombre_personaje));
        public static void eliminar_Account(int cuenta_index) => lista_cuentas.RemoveAt(cuenta_index);
        public static AccountConf get_Account(string nombre_cuenta) => lista_cuentas.FirstOrDefault(cuenta => cuenta.nombre_cuenta == nombre_cuenta);
        public static AccountConf get_Account(int cuenta_index) => lista_cuentas.ElementAt(cuenta_index);
        public static List<AccountConf> get_Lista_Accounts() => lista_cuentas;
    }
}
