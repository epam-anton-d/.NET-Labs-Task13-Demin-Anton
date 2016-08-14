using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Core;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CLIENT";

            BLL bll = new BLL();
            
            string read;

            // Мини help.
            Console.WriteLine(@"Help:");
            Console.WriteLine(@"create <путь к файлу> - создать файл");
            Console.WriteLine(@"Пример: create c:\file123.cs");
            Console.WriteLine(@"mkdir <путь к папке> - создать папку");
            Console.WriteLine(@"Пример: mkdir c:\folder123");
            Console.WriteLine(@"copy <путь откуда> <путь куда> - копирование");
            Console.WriteLine(@"Пример: copy c:\folder1\file1.txt c:\folder2");
            Console.WriteLine(@"move <путь откуда> <путь куда> - перемещение");
            Console.WriteLine(@"аналгично копированию");
            Console.WriteLine(@"delete <путь> - удаление");
            Console.WriteLine(@"Пример: delete c:\folder1");
            Console.WriteLine(@"tree <путь>");
            Console.WriteLine(@"Пример: tree c: - покажет содержимое диска c:");

            // Ожидание пользовательского ввода.
            do
            {               
                Console.Write("c:\\>");
                read = Console.ReadLine();
                read = read.Trim();

            } while (bll.ConsoleDialog(read));
        }
    }
}
