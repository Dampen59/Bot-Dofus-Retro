using Bot_Dofus_1._29._1.Otros.Enums;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Utilidades.Extensiones
{
    public static class Extensiones
    {
        public static string cadena_Amigable(this EstadoAccount estado)
        {
            switch (estado)
            {
                case EstadoAccount.CONECTANDO:
                    return "Conectando";
                case EstadoAccount.DESCONECTADO:
                    return "Desconectado";
                case EstadoAccount.INTERCAMBIO:
                    return "Intercambiando";
                case EstadoAccount.LUCHANDO:
                    return "Combate";
                case EstadoAccount.RECOLECTANDO:
                    return "Recolectando";
                case EstadoAccount.MOVIMIENTO:
                    return "Desplazando";
                case EstadoAccount.CONECTADO_INACTIVO:
                    return "Inactivo";
                case EstadoAccount.ALMACENAMIENTO:
                    return "Almacenamiento";
                case EstadoAccount.DIALOGANDO:
                    return "Dialogando";
                case EstadoAccount.COMPRANDO:
                    return "Comprando";
                case EstadoAccount.VENDIENDO:
                    return "Vendiendo";
                case EstadoAccount.REGENERANDO:
                    return "Regenerando Vida";
                default:
                    return "-";
            }
        }

        public static T get_Or<T>(this Table table, string key, DataType type, T orValue)
        {
            DynValue bandera = table.Get(key);

            if (bandera.IsNil() || bandera.Type != type)
                return orValue;

            try
            {
                return (T)bandera.ToObject(typeof(T));
            }
            catch
            {
                return orValue;
            }
        }
    }
}
