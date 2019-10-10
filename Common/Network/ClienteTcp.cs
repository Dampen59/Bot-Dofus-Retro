﻿using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
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
    public class ClienteTcp : IDisposable
    {
        private Socket socket { get; set; }
        private byte[] buffer { get; set; }
        public Account Account;
        private SemaphoreSlim semaforo;
        private bool disposed;

        public event Action<string> package_recibido;
        public event Action<string> package_enviado;
        public event Action<string> socket_informacion;

        /** ping **/
        private bool esta_esperando_package = false;
        private int ticks;
        private List<int> pings;

        public ClienteTcp(Account _account) => Account = _account;

        public void ConnexionServer(IPAddress ip, int puerto)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                buffer = new byte[socket.ReceiveBufferSize];
                semaforo = new SemaphoreSlim(1);
                pings = new List<int>(50);
                socket.BeginConnect(ip, puerto, new AsyncCallback(Connected_CallBack), socket);
            }
            catch (Exception ex)
            {
                socket_informacion?.Invoke(ex.ToString());
                DisconnectSocket();
            }
        }

        private void Connected_CallBack(IAsyncResult ar)
        {
            try
            {
                if (isConnected())
                {
                    socket = ar.AsyncState as Socket;
                    socket.EndConnect(ar);

                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(Receive_CallBack), socket);
                    socket_informacion?.Invoke("Socket conectado correctamente");
                }
                else
                {
                    DisconnectSocket();
                    socket_informacion?.Invoke("Impossible enviar el socket con el host");
                }
            }
            catch (Exception ex)
            {
                socket_informacion?.Invoke(ex.ToString());
                DisconnectSocket();
            }
        }

        public void Receive_CallBack(IAsyncResult ar)
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

                foreach (string package in datos.Replace("\x0a", string.Empty).Split('\0').Where(x => x != string.Empty))
                {
                    package_recibido?.Invoke(package);

                    if (esta_esperando_package)
                    {
                        pings.Add(Environment.TickCount - ticks);

                        if (pings.Count > 48)
                            pings.RemoveAt(1);

                        esta_esperando_package = false;
                    }

                    PaqueteRecibido.Recibir(this, package);
                }

                if (isConnected())
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(Receive_CallBack), socket);
            }
            else
                Account.disconnect();
        }

        public async Task SendPackage_Async(string package, bool need_response)
        {
            try
            {
                if (!isConnected())
                    return;

                package += "\n\x00";
                byte[] byte_package = Encoding.UTF8.GetBytes(package);

                await semaforo.WaitAsync().ConfigureAwait(false);

                if (need_response)
                    esta_esperando_package = true;

                socket.Send(byte_package);

                if (need_response)
                    ticks = Environment.TickCount;

                package_enviado?.Invoke(package);
                semaforo.Release();
            }
            catch (Exception ex)
            {
                socket_informacion?.Invoke(ex.ToString());
                DisconnectSocket();
            };
        }

        public void SendPackage(string package, bool need_response = false) => SendPackage_Async(package, need_response).Wait();

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

                socket_informacion?.Invoke("Socket desconectado del host");
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
        public int GetAveragePings() => (int)pings.Average();
        public int get_Actual_Ping() => Environment.TickCount - ticks;

        #region Zona Dispose
        ~ClienteTcp() => Dispose(false);
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
                package_recibido = null;
                package_enviado = null;
                disposed = true;
            }
        }
        #endregion
    }
}
