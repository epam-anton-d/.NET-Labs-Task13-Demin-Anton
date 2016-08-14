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
    /// <summary>
    /// Data Access Layer.
    /// </summary>
    class DAL
    {
        ConnectionFactory connectionFactory;

        public DAL()
        {
            connectionFactory = new ConnectionFactory();

            // Создание примера файловой системы.
            connectionFactory.channel.CreateFolder("c:\\folder1");
            connectionFactory.channel.CreateFolder("c:\\folder2");
            connectionFactory.channel.CreateFolder("c:\\folder3");
            
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder4");
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder5");
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder6");
            
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder7");
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder8");
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder9");
            
            connectionFactory.channel.CreateFolder("c:\\folder3\\folder10");
            connectionFactory.channel.CreateFolder("c:\\folder3\\folder11");
            connectionFactory.channel.CreateFolder("c:\\folder3\\folder12");
            
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder4\\folder13");
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder4\\folder14");
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder4\\folder15");
            
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder5\\folder16");
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder5\\folder17");
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder5\\folder18");
            
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder6\\folder19");
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder6\\folder20");
            connectionFactory.channel.CreateFolder("c:\\folder1\\folder6\\folder21");
            
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder7\\folder22");
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder7\\folder23");
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder7\\folder24");
            
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder8\\folder25");
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder8\\folder26");
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder8\\folder27");
            
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder9\\folder28");
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder9\\folder29");
            connectionFactory.channel.CreateFolder("c:\\folder2\\folder9\\folder30");
            
            connectionFactory.channel.CreateFile("c:\\folder2\\folder8\\file1.txt");
            connectionFactory.channel.CreateFile("c:\\folder2\\file1.txt");
            connectionFactory.channel.CreateFile("c:\\folder2\\folder8\\folder26\\file1.txt");
            connectionFactory.channel.CreateFile("c:\\file.txt");
        }

        /// <summary>
        /// Локальные методы, принимающие результаты от RPC-методов.
        /// </summary>
        /// <param name="dir"></param>
        internal void CreateFile(string dir)
        {
            if (connectionFactory.channel.CreateFile(dir) == 0)
            {
                Console.WriteLine("Файл успешно создан!!!");
            }
            else
            {
                Console.WriteLine("Ошибка!!!");
            }
        }

        internal void CreateFolder(string dir)
        {
            if (connectionFactory.channel.CreateFolder(dir) == 0)
            {
                Console.WriteLine("Папка успешно создана!!!");
            }
            else
            {
                Console.WriteLine("Ошибка!!!");
            }
        }

        internal void Copy(string sourceDir, string destinationDir)
        {
            if (connectionFactory.channel.Copy(sourceDir, destinationDir) == 0)
            {
                Console.WriteLine("Файл/папка успешно скопирована!!!");
            }
            else
            {
                Console.WriteLine("Ошибка!!!");
            }
        }

        internal void Delete(string dir)
        {
            if (connectionFactory.channel.Delete(dir) == 0)
            {
                Console.WriteLine("Файл/папка успешно удалена!!!");
            }
            else
            {
                Console.WriteLine("Ошибка!!!");
            }
        }

        internal void Move(string sourceDir, string destinationDir)
        {
            if (connectionFactory.channel.Move(sourceDir, destinationDir) == 0)
            {
                Console.WriteLine("Файл/папка успешно перемещена!!!");
            }
            else
            {
                Console.WriteLine("Ошибка!!!");
            }
        }

        internal void ShowTree(string dir)
        {
            List<string> treeList = connectionFactory.channel.ShowTree(dir);

            if (treeList.Count != 0)
            {
                foreach (var item in treeList)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
