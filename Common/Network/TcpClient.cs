using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Otros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
    Este archivo es parte del proyecto BotDofus_1.29.1

    BotDofus_1.29.1 Copyright (C) 2019 Alvaro Prendes — Todos los derechos reservados.
    Creado por Alvaro Prendes
    web: http://www.salesprendes.com
*/

namespace Bot_Dofus_1._29._1.Comun.Network
{
    public class TcpClient : IDisposable
    {
        private Socket socket { get; set; }
        private byte[] buffer { get; set; }
        public Account Account;
        private SemaphoreSlim semaforo;
        private bool disposed;

        public event Action<string> package_received;
        public event Action<string> package_sent;
        public event Action<string> socket_information;

        /** ping **/
        private bool esta_esperando_paquete = false;
        private int ticks;
        private List<int> pings;

        public TcpClient(Account _account) => Account = _account;

        public void ConnectToServer(IPAddress ip, int puerto)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                buffer = new byte[socket.ReceiveBufferSize];
                semaforo = new SemaphoreSlim(1);
                pings = new List<int>(50);
                socket.BeginConnect(ip, puerto, new AsyncCallback(conectar_CallBack), socket);
            }
            catch (Exception ex)
            {
                socket_information?.Invoke(ex.ToString());
                DisconnectSocket();
            }
        }

        private void conectar_CallBack(IAsyncResult ar)
        {
            try
            {
                if (isConnected())
                {
                    socket = ar.AsyncState as Socket;
                    socket.EndConnect(ar);

                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(recibir_CallBack), socket);
                    socket_information?.Invoke("Socket connected correctly");
                }
                else
                {
                    DisconnectSocket();
                    socket_information?.Invoke("Impossible to send socket with host");
                }
            }
            catch (Exception ex)
            {
                socket_information?.Invoke(ex.ToString());
                DisconnectSocket();
            }
        }

        public void recibir_CallBack(IAsyncResult ar)
        {
            if (!isConnected() || disposed)
            {
                DisconnectSocket();
                return;
            }

            int bytes_leidos = socket.EndReceive(ar, out SocketError respuesta);

            if (bytes_leidos > 0 && respuesta == SocketError.Success)
            {
                string datos = Encoding.UTF8.GetString(buffer, 0, bytes_leidos);

                foreach (string paquete in datos.Replace("\x0a", string.Empty).Split('\0').Where(x => x != string.Empty))
                {
                    package_received?.Invoke(paquete);

                    if (esta_esperando_paquete)
                    {
                        pings.Add(Environment.TickCount - ticks);

                        if (pings.Count > 48)
                            pings.RemoveAt(1);

                        esta_esperando_paquete = false;
                    }

                    PackageReceived.Recibir(this, paquete);
                }

                if (isConnected())
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(recibir_CallBack), socket);
            }
            else
                Account.Disconnect();
        }

        public async Task Send_Async(string paquete, bool necesita_respuesta)
        {
            try
            {
                if (!isConnected())
                    return;

                paquete += "\n\x00";
                byte[] byte_paquete = Encoding.UTF8.GetBytes(paquete);

                await semaforo.WaitAsync().ConfigureAwait(false);

                if (necesita_respuesta)
                    esta_esperando_paquete = true;

                socket.Send(byte_paquete);

                if (necesita_respuesta)
                    ticks = Environment.TickCount;

                package_sent?.Invoke(paquete);
                semaforo.Release();
            }
            catch (Exception ex)
            {
                socket_information?.Invoke(ex.ToString());
                DisconnectSocket();
            };
        }

        public void Send(string paquete, bool necesita_respuesta = false) => Send_Async(paquete, necesita_respuesta).Wait();

        public void DisconnectSocket()
        {
            if (isConnected())
            {
                if (socket != null && socket.Connected)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(false);
                    socket.Close();
                }

                socket_information?.Invoke("Socket disconnected from host");
            }
        }

        public bool isConnected()
        {
            try
            {
                return !(disposed || socket == null || !socket.Connected && socket.Available == 0);
            }
            catch (SocketException)
            {
                return false;
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
        }

        public int get_Total_Pings() => pings.Count();
        public int get_Promedio_Pings() => (int)pings.Average();
        public int get_Actual_Ping() => Environment.TickCount - ticks;

        #region Zona Dispose
        ~TcpClient() => Dispose(false);
        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (socket != null && socket.Connected)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(false);
                    socket.Close();
                }

                if (disposing)
                {
                    socket.Dispose();
                    semaforo.Dispose();
                }

                semaforo = null;
                Account = null;
                socket = null;
                buffer = null;
                package_received = null;
                package_sent = null;
                disposed = true;
            }
        }
        #endregion
    }
}
