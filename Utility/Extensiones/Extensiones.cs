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
                    return "Connected";
                case AccountStatus.Disconnected:
                    return "Disconnected";
                case AccountStatus.Exchanging:
                    return "Exchange";
                case AccountStatus.Fighting:
                    return "Fight";
                case AccountStatus.Collecting:
                    return "Collecting";
                case AccountStatus.Moving:
                    return "Move";
                case AccountStatus.ConnectedInactive:
                    return "Inactive";
                case AccountStatus.Storing:
                    return "Storing";
                case AccountStatus.Dialoguing:
                    return "Dialog";
                case AccountStatus.Buying:
                    return "Buying";
                case AccountStatus.Selling:
                    return "Selling";
                case AccountStatus.Regenerating:
                    return "Regenerating";
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
