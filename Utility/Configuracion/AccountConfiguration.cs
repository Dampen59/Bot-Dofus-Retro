using Bot_Dofus_1._29._1.Controles.ControlMapa;
using System.IO;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
	Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Utilidades.Configuracion
{
    public class AccountConfiguration
    {
        public string accountNumber { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string server { get; set; } = string.Empty;
        public string characterNumber { get; set; } = string.Empty;

        public AccountConfiguration(string _accountNumber, string _password, string _server, string _characterNumber)
        {
            accountNumber = _accountNumber;
            password = _password;
            server = _server;
            characterNumber = _characterNumber;
        }

        public void SaveAccount(BinaryWriter bw)
        {
            bw.Write(accountNumber);
            bw.Write(password);
            bw.Write(server);
            bw.Write(characterNumber);
        }

        public static AccountConfiguration Load_Account(BinaryReader br)
        {
            try
            {
                return new AccountConfiguration(br.ReadString(), br.ReadString(), br.ReadString(), br.ReadString());
            }
            catch
            {
                return null;
            }
        }

        public int Get_ServerID()
        {
            switch (server)
            {
                case "Eratz":
                    return 601;
                case "Henual":
                    return 602;
                case "Nabur":
                    return 603;
                case "Arty":
                    return 604;
                case "Algathe":
                    return 605;
                case "Hogmeiser":
                    return 606;
                case "Droupik":
                    return 607;
                case "Ayuto":
                    return 608;
                case "Bilby":
                    return 609;
                case "Clustus":
                    return 610;
                default:
                    return 601;
                    break;
            }
        }
        //603: 'Nabur', 604: 'Arty', 605: 'Algathe', 606: 'Hogmeiser', 607: 'Droupik', 608: 'Ayuto', 609: 'Bilby', 610: 'Clustus'
    }
}
