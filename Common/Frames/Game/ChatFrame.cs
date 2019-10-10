using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
    internal class ChatFrame : Frame
    {
        [PackageAttribut("cC+")]
        public void get_Agregar_Canal(ClienteTcp cliente, string package) => cliente.Account.juego.personaje.agregar_Canal_Personaje(package.Substring(3));

        [PackageAttribut("cC-")]
        public void get_Eliminar_Canal(ClienteTcp cliente, string package) => cliente.Account.juego.personaje.eliminar_Canal_Personaje(package.Substring(3));

        [PackageAttribut("cMK")]
        public void get_Mensajes_Chat(ClienteTcp cliente, string package)
        {
            string[] separador = package.Substring(3).Split('|');
            string canal = string.Empty;

            switch (separador[0])
            {
                case "?":
                    canal = "RECLUTAMIENTO";
                break;

                case ":":
                    canal = "COMERCIO";
                break;

                case "^":
                    canal = "INCARNAM";
                break;

                case "i":
                    canal = "INFORMACIÓN";
                break;

                case "#":
                    canal = "EQUIPO";
                break;

                case "$":
                    canal = "GRUPO";
                break;

                case "%":
                    canal = "GREMIO";
                break;

                case "F":
                    cliente.Account.logger.log_privado("RECIBIDO-PRIVADO", separador[2] + ": " + separador[3]);
                break;

                case "T":
                    cliente.Account.logger.log_privado("ENVIADO-PRIVADO", separador[2] + ": " + separador[3]);
                break;

                default:
                    canal = "GENERAL";
                break;
            }

            if (!canal.Equals(string.Empty))
                cliente.Account.logger.log_normal(canal, separador[2] + ": " + separador[3]);
        }
    }
}
