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
        private static List<AccountConfiguration> lista_cuentas;
        private static readonly string ruta_archivo_cuentas = Path.Combine(Directory.GetCurrentDirectory(), "cuentas.bot");
        public static bool mostrar_mensajes_debug { get; set; }
        public static string Ip = "34.251.172.139";
        public static short Port = 443;

        static GlobalConf()
        {
            lista_cuentas = new List<AccountConfiguration>();
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
                        lista_cuentas.Add(AccountConfiguration.cargar_Una_Account(br));
                    }
                    mostrar_mensajes_debug = br.ReadBoolean();
                    Ip = br.ReadString();
                    Port = br.ReadInt16();
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
                bw.Write(Ip);
                bw.Write(Port);
            }
        }

        public static void agregar_Account(string nombre_cuenta, string password, string servidor, string nombre_personaje) => lista_cuentas.Add(new AccountConfiguration(nombre_cuenta, password, servidor, nombre_personaje));
        public static void eliminar_Account(int cuenta_index) => lista_cuentas.RemoveAt(cuenta_index);
        public static AccountConfiguration get_Account(string nombre_cuenta) => lista_cuentas.FirstOrDefault(cuenta => cuenta.nombre_cuenta == nombre_cuenta);
        public static AccountConfiguration get_Account(int cuenta_index) => lista_cuentas.ElementAt(cuenta_index);
        public static List<AccountConfiguration> get_Lista_Accounts() => lista_cuentas;
    }
}
