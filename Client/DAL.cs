using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;
using Interface;

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
            //connectionFactory.channel.Create("c:\\folder1");
            //connectionFactory.channel.Create("c:\\folder2");
            //connectionFactory.channel.Create("c:\\folder3");
            //
            //connectionFactory.channel.Create("c:\\folder1\\folder4");
            //connectionFactory.channel.Create("c:\\folder1\\folder5");
            //connectionFactory.channel.Create("c:\\folder1\\folder6");
            //                                                      
            //connectionFactory.channel.Create("c:\\folder2\\folder7");
            //connectionFactory.channel.Create("c:\\folder2\\folder8");
            //connectionFactory.channel.Create("c:\\folder2\\folder9");
            //
            //connectionFactory.channel.Create("c:\\folder3\\folder10");
            //connectionFactory.channel.Create("c:\\folder3\\folder11");
            //connectionFactory.channel.Create("c:\\folder3\\folder12");
            //
            //connectionFactory.channel.Create("c:\\folder1\\folder4\\folder13");
            //connectionFactory.channel.Create("c:\\folder1\\folder4\\folder14");
            //connectionFactory.channel.Create("c:\\folder1\\folder4\\folder15");
            //                                                                 
            //connectionFactory.channel.Create("c:\\folder1\\folder5\\folder16");
            //connectionFactory.channel.Create("c:\\folder1\\folder5\\folder17");
            //connectionFactory.channel.Create("c:\\folder1\\folder5\\folder18");
            //                                                                 
            //connectionFactory.channel.Create("c:\\folder1\\folder6\\folder19");
            //connectionFactory.channel.Create("c:\\folder1\\folder6\\folder20");
            //connectionFactory.channel.Create("c:\\folder1\\folder6\\folder21");
            //                                                                 
            //connectionFactory.channel.Create("c:\\folder2\\folder7\\folder22");
            //connectionFactory.channel.Create("c:\\folder2\\folder7\\folder23");
            //connectionFactory.channel.Create("c:\\folder2\\folder7\\folder24");
            //                                                                 
            //connectionFactory.channel.Create("c:\\folder2\\folder8\\folder25");
            //connectionFactory.channel.Create("c:\\folder2\\folder8\\folder26");
            //connectionFactory.channel.Create("c:\\folder2\\folder8\\folder27");
            //                                                                 
            //connectionFactory.channel.Create("c:\\folder2\\folder9\\folder28");
            //connectionFactory.channel.Create("c:\\folder2\\folder9\\folder29");
            //connectionFactory.channel.Create("c:\\folder2\\folder9\\folder30");
            //
            //connectionFactory.channel.Create("c:\\folder2\\folder8\\file1.txt");
            //connectionFactory.channel.Create("c:\\folder2\\file1.txt");
            //connectionFactory.channel.Create("c:\\folder2\\folder8\\folder26\\file1.txt");
            //connectionFactory.channel.Create("c:\\file.txt");
        }

        /// <summary>
        /// Локальные методы, принимающие результаты от RPC-методов.
        /// </summary>
        /// <param name="dir"></param>
        internal void Create(string dir)
        {
            if (connectionFactory.channel.Create(dir))
            {
                Console.WriteLine("Файл/папка успешно создан(а)!!!");
            }
            else
            {
                Console.WriteLine("Ошибка!!!");
            }
        }

        internal void Copy(string sourceDir, string destinationDir)
        {
            if (connectionFactory.channel.Copy(sourceDir, destinationDir))
            {
                Console.WriteLine("Файл/папка успешно скопирован(а)!!!");
            }
            else
            {
                Console.WriteLine("Ошибка!!!");
            }
        }
        
        internal void Delete(string dir)
        {
            if (connectionFactory.channel.Delete(dir))
            {
                Console.WriteLine("Файл/папка успешно удален(а)!!!");
            }
            else
            {
                Console.WriteLine("Ошибка!!!");
            }
        }
        
        internal void Move(string sourceDir, string destinationDir)
        {
            if (connectionFactory.channel.Move(sourceDir, destinationDir))
            {
                Console.WriteLine("Файл/папка успешно перемещен(а)!!!");
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
