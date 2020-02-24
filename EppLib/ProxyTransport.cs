using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Xml;

namespace EppLib
{
    public class ProxyTransport : ITransport
    {
        private NetworkStream stream;

        private readonly string EPP_REGISTRY_COM;
        private readonly int PORT;

        private readonly int READ_TIMEOUT;
        private readonly int WRITE_TIMEOUT;

        private readonly bool loggingEnabled;

        public ProxyTransport(string host, int port, bool loggingEnabled = false, int readTimeout = Timeout.Infinite, int writeTimeout = Timeout.Infinite)
        {
            EPP_REGISTRY_COM = host;
            PORT = port;
            READ_TIMEOUT = readTimeout;
            WRITE_TIMEOUT = writeTimeout;
            this.loggingEnabled = loggingEnabled;
        }

        /// <summary>
        /// Connect to the proxy end point
        /// </summary>
        public void Connect(SslProtocols sslProtocols = SslProtocols.None)
        {
            var client = new TcpClient(EPP_REGISTRY_COM, PORT);
            stream = client.GetStream();
        }

        /// <summary>
        /// Disconnect from the proxy end point
        /// </summary>
        public void Disconnect()
        {
            stream.Close();
        }

        /// <summary>
        /// Read the command response
        /// </summary>
        /// <returns></returns>
        public byte[] Read()
        {
            byte[] buffer = new byte[1024];
            StringBuilder msg = new StringBuilder();
            int bytesRead = 0;

            // Incoming message may be larger than the buffer size.
            do
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);

                msg.AppendFormat("{0}", Encoding.UTF8.GetString(buffer, 0, bytesRead));

            }
            while (stream.DataAvailable);

            byte[] bytes = Encoding.UTF8.GetBytes(msg.ToString());

            if (loggingEnabled)
            {
                Debug.Setup(new ConsoleLogger());
                Debug.Log("****************** Received ******************");
                Debug.Log(bytes);
            }

            return bytes;
        }

        /// <summary>
        /// Writes an XmlDocument to the transport stream
        /// </summary>
        /// <param name="s"></param>
        public void Write(XmlDocument s)
        {
            byte[] buffer = GetBytes(s);
            stream.Write(buffer, 0, buffer.Length);

            if (loggingEnabled)
            {
                Debug.Setup(new ConsoleLogger());
                Debug.Log("****************** Sending ******************");
                Debug.Log(buffer);
            }
        }

        private static byte[] GetBytes(XmlDocument s)
        {
            return Encoding.UTF8.GetBytes(s.OuterXml);
        }

        public void Dispose()
        {
            if (stream != null)
            {
                stream.Dispose();
            }
        }
    }
}
