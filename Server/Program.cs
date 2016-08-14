using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Server.Core;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SERVER";

            // Указание адреса, где ожидать входящие сообщения.
            Uri address = new Uri("net.tcp://127.0.0.1:4000/IVirtualFilesystem"); // ADDRESS.   (A)

            // Указание привязки, как обмениваться сообщениями.
            //BasicHttpBinding binding = new BasicHttpBinding();        // BINDING.   (B)
            NetTcpBinding binding = new NetTcpBinding();

            // Указание контракта.
            Type contract = typeof(IVirtualFilesystem);                        // CONTRACT.  (C) 


            // Создание провайдера Хостинга с указанием Сервиса.
            ServiceHost host = new ServiceHost(typeof(Filesystem));

            // Добавление "Конечной Точки".
            host.AddServiceEndpoint(contract, binding, address);

            // Начало ожидания прихода сообщений.
            host.Open();


            Console.WriteLine("Приложение готово к приему сообщений.");
            Console.ReadKey();


            // Завершение ожидания прихода сообщений.
            host.Close();
        }
    }
}
