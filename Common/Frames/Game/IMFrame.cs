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
    class IMFrame : Frame
    {
        [PackageAttribut("Im189")]
        public void get_Welcome_Message_Dofus(TcpClient cliente, string paquete) => cliente.Account.Logger.log_Error("DOFUS", "Welcome to DOFUS, the World of the Twelve! Attention It is forbidden to communicate your account username and password.");

        [PackageAttribut("Im039")]
        public void get_Fight_Viewer_Off(TcpClient cliente, string paquete) => cliente.Account.Logger.log_informacion("COMBATE", "Spectator mode is disabled.");

        [PackageAttribut("Im040")]
        public void get_Fight_Spectator_Activated(TcpClient cliente, string paquete) => cliente.Account.Logger.log_informacion("COMBATE", "Spectator mode is activated.");

        [PackageAttribut("Im0152")]
        public void get_Last_Message_IP_Connection(TcpClient cliente, string paquete)
        {
            string mensaje = paquete.Substring(3).Split(';')[1];
            cliente.Account.Logger.log_informacion("DOFUS", "Last connection to your account made on " + mensaje.Split('~')[0] + "/" + mensaje.Split('~')[1] + "/" + mensaje.Split('~')[2] + " to  " + mensaje.Split('~')[3] + ":" + mensaje.Split('~')[4] + " by IP address " + mensaje.Split('~')[5]);
        }

        [PackageAttribut("Im0153")]
        public void get_Message_New_ConnectionIP(TcpClient cliente, string paquete) => cliente.Account.Logger.log_informacion("DOFUS", "Your current IP address is " + paquete.Substring(3).Split(';')[1]);

        [PackageAttribut("Im020")]
        public void getMessageOpenBank(TcpClient cliente, string paquete) => cliente.Account.Logger.log_informacion("DOFUS", "You've had to give " + paquete.Split(';')[1] + " kamas to access to your bank");

        [PackageAttribut("Im025")]
        public void get_Message_Pet_Happy(TcpClient cliente, string paquete) => cliente.Account.Logger.log_informacion("DOFUS", "Your pet is so happy to see you again!");

        [PackageAttribut("Im0157")]
        public void get_Message_Error_Chat_Diffusion(TcpClient cliente, string paquete) => cliente.Account.Logger.log_informacion("DOFUS", "This channel does not have the accessible diffusion only to the subscribers of level " + paquete.Split(';')[1]);

        [PackageAttribut("Im037")]
        public void get_Message_Away_Mode_Dofus(TcpClient cliente, string paquete) => cliente.Account.Logger.log_informacion("DOFUS", "From now on you will be considered away.");

        [PackageAttribut("Im112")]
        public void get_Message_Pods_Full(TcpClient cliente, string paquete) => cliente.Account.Logger.log_Error("DOFUS", "You're too loaded. Throw away some objects to move around");
    }
}
