
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Interface;

namespace Client.Core
{
    /// <summary>
    /// Класс, соединяющий клиент с сервером через TCP-протокол.
    /// </summary>
    public class ConnectionFactory
    {
        // Указание, где ожидать входящие сообщения.
        Uri address;

        // Указание, как обмениваться сообщениями.
        //BasicHttpBinding binding;
        NetTcpBinding binding;

        // Создание Конечной Точки.
        EndpointAddress endpoint;

        // Создание фабрики каналов.
        ChannelFactory<IVirtualFilesystem> factory;

        // Использование factory для создания канала (прокси).
        public IVirtualFilesystem channel;


        public ConnectionFactory()
        {
            // Указание, где ожидать входящие сообщения.
            address = new Uri("net.tcp://127.0.0.1:4000/IVirtualFilesystem");

            // Указание, как обмениваться сообщениями.
            binding = new NetTcpBinding();

            // Создание Конечной Точки.
            endpoint = new EndpointAddress(address);

            // Создание фабрики каналов.
            factory = new ChannelFactory<IVirtualFilesystem>(binding, endpoint);

            // Использование factory для создания канала (прокси).
            channel = factory.CreateChannel();
        }
    }
}
