using Bot_Dofus_1._29._1.Otros.Enums;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Utilidades.Extensiones
{
    public static class Extensiones
    {
        public static string cadena_Amigable(this StateAccount estado)
        {
            switch (estado)
            {
                case StateAccount.CONNECTED:
                    return "Connected";
                case StateAccount.DISCONNECTED:
                    return "Disconnected";
                case StateAccount.EXCHANGE:
                    return "Exchange";
                case StateAccount.FIGHTING:
                    return "Combate";
                case StateAccount.COLLECTING:
                    return "Collecting";
                case StateAccount.MOVING:
                    return "Moving";
                case StateAccount.AWAY:
                    return "Away";
                case StateAccount.BANKING:
                    return "Banking";
                case StateAccount.DIALOG:
                    return "Dialog";
                case StateAccount.BUYING:
                    return "Buying";
                case StateAccount.SELLING:
                    return "Selling";
                case StateAccount.REGENERATING:
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
