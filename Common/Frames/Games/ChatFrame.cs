using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
    internal class ChatFrame : Frame
    {
        [PackageAttribut("cC+")]
        public void get_Agregar_Canal(TcpClient cliente, string paquete) => cliente.Account.Game.Character.agregar_Canal_Personaje(paquete.Substring(3));

        [PackageAttribut("cC-")]
        public void get_Eliminar_Canal(TcpClient cliente, string paquete) => cliente.Account.Game.Character.eliminar_Canal_Personaje(paquete.Substring(3));

        [PackageAttribut("cMK")]
        public void get_Mensajes_Chat(TcpClient cliente, string paquete)
        {
            string[] separador = paquete.Substring(3).Split('|');
            string canal = string.Empty;

            switch (separador[0])
            {
                case "?":
                    canal = "RECRUITMENT";
                break;

                case ":":
                    canal = "TRADE";
                break;

                case "^":
                    canal = "INCARNAM";
                break;

                case "i":
                    canal = "INFORMATION";
                break;

                case "#":
                    canal = "TEAM";
                break;

                case "$":
                    canal = "GROUP";
                break;

                case "%":
                    canal = "GUILDS";
                break;

                case "F":
                    cliente.Account.Logger.private_log("Receveid Private", separador[2] + ": " + separador[3]);
                break;

                case "T":
                    cliente.Account.Logger.private_log("Send Private", separador[2] + ": " + separador[3]);
                break;

                default:
                    canal = "GENERAL";
                break;
            }

            if (!canal.Equals(string.Empty))
                cliente.Account.Logger.log_normal(canal, separador[2] + ": " + separador[3]);
        }
    }
}
