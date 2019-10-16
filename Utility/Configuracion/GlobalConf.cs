using System.Collections.Generic;
using System.IO;
using System.Linq;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Utilidades.Configuracion
{
    internal class GlobalConf
    {
        private static List<AccountConfiguration> accountsList;
        private static readonly string accountsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "accounts.bot");
        public static bool show_debug_messages { get; set; }
        public static string Ip = "34.251.172.139";
        public static short Port = 443;

        static GlobalConf()
        {
            accountsList = new List<AccountConfiguration>();
            show_debug_messages = false;
        }

        public static void LoadAllAccounts()
        {
            if (File.Exists(accountsFilePath))
            {
                accountsList.Clear();
                using (BinaryReader br = new BinaryReader(File.Open(accountsFilePath, FileMode.Open)))
                {
                    int registros_totales = br.ReadInt32();
                    for (int i = 0; i < registros_totales; i++)
                    {
                        accountsList.Add(AccountConfiguration.Load_Account(br));
                    }
                    show_debug_messages = br.ReadBoolean();
                    Ip = br.ReadString();
                    Port = br.ReadInt16();
                }
            }
            else
            {
                return;
            }
        }

        public static void SaveConfig()
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(accountsFilePath, FileMode.Create)))
            {
                bw.Write(accountsList.Count);
                accountsList.ForEach(a => a.SaveAccount(bw));
                bw.Write(show_debug_messages);
                bw.Write(Ip);
                bw.Write(Port);
            }
        }

        public static void AddAccount(string accountNumber, string password, string server, string characterNumber) => accountsList.Add(new AccountConfiguration(accountNumber, password, server, characterNumber));
        public static void DeleteAccount(int accountIndex) => accountsList.RemoveAt(accountIndex);
        public static AccountConfiguration get_Account(string accountNumber) => accountsList.FirstOrDefault(account => account.accountNumber == accountNumber);
        public static AccountConfiguration get_Account(int accountIndex) => accountsList.ElementAt(accountIndex);
        public static List<AccountConfiguration> GetAccountList() => accountsList;
    }
}
