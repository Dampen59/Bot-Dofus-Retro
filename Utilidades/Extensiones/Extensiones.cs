using Bot_Dofus_1._29._1.Otros.Enums;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Utilidades.Extensiones
{
    public static class Extensiones
    {
        public static string cadena_Amigable(this AccountStatus estado)
        {
            switch (estado)
            {
                case AccountStatus.CONECTANDO:
                    return "Conectando";
                case AccountStatus.Disconnected:
                    return "Desconectado";
                case AccountStatus.Exchanging:
                    return "Intercambiando";
                case AccountStatus.Fighting:
                    return "Combate";
                case AccountStatus.Collecting:
                    return "Recolectando";
                case AccountStatus.Moving:
                    return "Desplazando";
                case AccountStatus.ConnectedInactive:
                    return "Inactivo";
                case AccountStatus.Storing:
                    return "Almacenamiento";
                case AccountStatus.Dialoguing:
                    return "Dialogando";
                case AccountStatus.Buying:
                    return "Comprando";
                case AccountStatus.Selling:
                    return "Vendiendo";
                case AccountStatus.Regenerating:
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
