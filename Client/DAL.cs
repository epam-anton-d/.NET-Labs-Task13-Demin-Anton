using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace Client.Core
{
    class DAL
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
        IVirtualFilesystem channel;

        public DAL()
        {
            // Указание, где ожидать входящие сообщения.
            address = new Uri("net.tcp://127.0.0.1:4000/IVirtualFilesystem");

            // Указание, как обмениваться сообщениями.
            //binding = new BasicHttpBinding();
            binding = new NetTcpBinding();

            // Создание Конечной Точки.
            endpoint = new EndpointAddress(address);

            // Создание фабрики каналов.
            factory = new ChannelFactory<IVirtualFilesystem>(binding, endpoint);

            // Использование factory для создания канала (прокси).
            channel = factory.CreateChannel();
        }

        internal string CreateFile(string dir)
        {
            FileStream stream = channel.CreateFile(dir);
            SoapFormatter formatter = new SoapFormatter();
            
            string result = formatter.Deserialize(stream) as string;

            stream.Close();

            return result;
        }

        internal string CreateFolder(string dir)
        {
            FileStream stream = channel.CreateFolder(dir);
            SoapFormatter formatter = new SoapFormatter();

            string result = formatter.Deserialize(stream) as string;

            stream.Close();

            return result;
        }

        internal string Copy(string sourceDir, string destinationDir)
        {
            FileStream stream = channel.Copy(sourceDir, destinationDir);
            SoapFormatter formatter = new SoapFormatter();

            string result = formatter.Deserialize(stream) as string;

            stream.Close();

            return result;
        }

        internal string Delete(string dir)
        {
            FileStream stream = channel.Delete(dir);
            SoapFormatter formatter = new SoapFormatter();

            string result = formatter.Deserialize(stream) as string;

            stream.Close();

            return result;
        }

        internal string Move(string sourceDir, string destinationDir)
        {
            FileStream stream = channel.Move(sourceDir, destinationDir);
            SoapFormatter formatter = new SoapFormatter();

            string result = formatter.Deserialize(stream) as string;

            stream.Close();

            return result;
        }

        internal List<string> ShowTree(string dir)
        {
            channel.ShowTree(dir);

            FileStream stream = channel.ShowTree(dir);
            SoapFormatter formatter = new SoapFormatter();

            List<string> result = formatter.Deserialize(stream) as List<string>;

            stream.Close();

            return result;
        }
    }
}
