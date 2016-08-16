using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Server.Core
{
    class MessageDispatcher
    {
        Uri address;
        NetTcpBinding binding;
        Type contract;
        ServiceHost host;
        
        /// <summary>
        /// Создает хост и ожидает пользовательские сообщения.
        /// </summary>
        public MessageDispatcher()
        {
            // Указание адреса, где ожидать входящие сообщения.
            address = new Uri("net.tcp://127.0.0.1:4000/IVirtualFilesystem"); // ADDRESS.   (A)

            // Указание привязки, как обмениваться сообщениями.
            binding = new NetTcpBinding();                                   // BINDING.   (B)

            // Указание контракта.
            contract = typeof(IVirtualFilesystem);                        // CONTRACT.  (C) 

            // Создание провайдера Хостинга с указанием Сервиса.
            host = new ServiceHost(typeof(Filesystem));

            // Добавление "Конечной Точки".
            host.AddServiceEndpoint(contract, binding, address);

            // Начало ожидания прихода сообщений.
            host.Open();
        }

        ~MessageDispatcher()
        {
            // Завершение ожидания прихода сообщений.
            host.Close();
        }
    }
}
