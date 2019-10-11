using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
    class AuthentificationGame : Frame
    {
        [PackageAttribut("M030")]
        public void GetErrorStreaming(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("Login", "Connection rejected. You could not be authenticated for this Server because your connection has expired. Be sure to cut off downloads, music or streaming videos to improve the quality and speed of your connection.");
            cliente.Account.Disconnect();
        }

        [PackageAttribut("M031")]
        public void GetRedError(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("Login", "Connection rejected. The Game Server has not received the necessary authentication information after your identification. Please try again and, if the problem persists, contact your network administrator or your Internet Server. This is a redirection problem due to a bad DNS configuration.");
            cliente.Account.Disconnect();
        }

        [PackageAttribut("M032")]
        public void GetFloodErro(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("Login", "To avoid disturbing other players, wait %1 seconds before reconnecting.");
            cliente.Account.Disconnect();
        }
    }
}
