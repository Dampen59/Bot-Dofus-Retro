using System;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;

namespace Bot_Dofus_1._29._1.Utilidades.Logs
{
    public class Logger
    {
        public event Action<LogMessage, string> log_event;

        private void log_Final(string _reference, string _message, string color, Exception ex = null)
        {
            try
            {
                LogMessage log_Message = new LogMessage(_reference, _message, ex);
                log_event?.Invoke(log_Message, color);
            }
            catch (Exception e)
            {
                log_Final("LOGGER", "An exception occurred when you activated the logged event.", LogTypes.ERROR, e);
            }
        }

        private void log_Final(string _reference, string _message, LogTypes color, Exception ex = null)
        {
            if (color == LogTypes.DEBUG && !GlobalConf.show_debug_messages)
                return;
            log_Final(_reference, _message, ((int)color).ToString("X"), ex);
        }

        public void log_Error(string _reference, string _message) => log_Final(_reference, _message, LogTypes.ERROR);
        public void log_Danger(string _reference, string _message) => log_Final(_reference, _message, LogTypes.WARNING);
        public void log_information(string _reference, string _message) => log_Final(_reference, _message, LogTypes.INFORMATION);
        public void log_normal(string _reference, string _message) => log_Final(_reference, _message, LogTypes.NORMAL);
        public void private_log(string _reference, string _message) => log_Final(_reference, _message, LogTypes.PRIVATE);
    }
}
