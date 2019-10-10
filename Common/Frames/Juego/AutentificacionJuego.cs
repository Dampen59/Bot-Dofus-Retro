﻿using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
    class AutentificacionJuego : Frame
    {
        [PaqueteAtributo("M030")]
        public void get_Error_Streaming(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("Login", "Conexión rechazada. No se te ha podido autentificar para este Server porque tu conexión ha caducado. Asegúrate de cortar las descargas, así como la música o los vídeos en difusión continua (streaming), para mejorar la calidad y la velocidad de tu conexión.");
            cliente.Account.Disconnect();
        }

        [PaqueteAtributo("M031")]
        public void get_Error_Red(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("Login", "Conexión rechazada. El Server del Game no ha recibido las informaciones de autentificación necesarias tras tu identificación. Por favor, vuelve a intentarlo otra vez y, si el problema persiste, contacta con tu administrador de redes o con tu Server de acceso a Internet. Se trata de un problema de re-dirección debido a una mala configuración DNS.");
            cliente.Account.Disconnect();
        }

        [PaqueteAtributo("M032")]
        public void get_Error_Flood_Conexion(TcpClient cliente, string paquete)
        {
            cliente.Account.Logger.log_Error("Login", "Para no ocasionar molestias al resto de jugadores, espera %1 segundos antes de volver a conectarte.");
            cliente.Account.Disconnect();
        }
    }
}
