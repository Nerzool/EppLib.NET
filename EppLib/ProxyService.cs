using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Xml;
using EppLib.Entities;

namespace EppLib
{
    public class ProxyService : IService
    {
        private readonly ITransport transport;

        public ProxyService(ITransport transport)
        {
            this.transport = transport;
        }

        public void Connect(SslProtocols sslProtocols = SslProtocols.None)
        {
            transport.Connect(sslProtocols);
        }

        public void Disconnect()
        {
            transport.Disconnect();
        }

        public T Execute<T>(EppBase<T> command) where T : EppResponse
        {
            byte[] bytes = SendAndReceive(command.ToXml());

            return command.FromBytes(bytes);
        }

        internal byte[] SendAndReceive(XmlDocument xmlDocument)
        {
            transport.Write(xmlDocument);

            return transport.Read();
        }
    }
}
