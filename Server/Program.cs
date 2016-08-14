using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Server.Core;
using System.IO;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SERVER";

            MessageDispatcher messageDispatcher = new MessageDispatcher();
            
            Console.WriteLine("Приложение готово к приему сообщений.");
            Console.ReadKey();

        }
    }
}
