using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Client.Core
{
    class BLL
    {
        string patternMkdir = "mkdir";
        string patternPathDisk = @"\sc\:(\\[A-Za-z\-_0-9\.]*)*";
        string patternPathFolder = @"\sc\:(\\[A-Za-z\-_0-9\.]+)+";
        string patternPathFile = @"\sc\:(\\[A-Za-z\-_0-9]+)+\.[a-z0-9]+";
        string patternCreate = "create";
        string patternCopy = "copy";
        string patternDelete = "delete";
        string patternMove = "move";
        string patternTree = "tree";
        string patternExit = "exit";
        char[] space = new char[] { ' ' };
        string[] commands;
        int error;

        private DAL dal;

        public BLL()
        {
            dal = new DAL();
        }

        //internal string CreateFolder(string dir)
        //{
        //    return dal.CreateFolder(dir);
        //}
        //
        //internal string CreateFile(string dir)
        //{
        //    return dal.CreateFile(dir);
        //}
        //
        //internal string Copy(string sourceDir, string destinationDir)
        //{
        //    return dal.Copy(sourceDir, destinationDir);
        //}
        //
        //internal string Delete(string dir)
        //{
        //    return dal.Delete(dir);
        //}
        //
        //internal string Move(string sourceDir, string destinationDir)
        //{
        //    return dal.Move(sourceDir, destinationDir);
        //}
        //
        //internal List<string> ShowTree(string dir)
        //{
        //    return dal.ShowTree(dir);
        //}

        internal bool ConsoleDialog(string read)
        {

            // Проверка ввода пользователя на соответствие шаблонам.
            // Создание директории.
            if (Regex.IsMatch(read, patternMkdir + patternPathFolder))
            {
                commands = read.Split(space);
                
                Console.WriteLine(dal.CreateFolder(commands[1]));
                
                return true;
            }
            // Создание файла.
            else if (Regex.IsMatch(read, patternCreate + patternPathFile))
            {
                commands = read.Split(space);

                Console.WriteLine(dal.CreateFile(commands[1]));
                
                return true;
            }
            // Копирование файла или папки.
            else if (Regex.IsMatch(read, patternCopy + patternPathFile + patternPathDisk) || Regex.IsMatch(read, patternCopy + patternPathFolder + patternPathDisk))
            {
                commands = read.Split(space);

                Console.WriteLine(dal.Copy(commands[1], commands[2]));
                
                return true;
            }
            // Удаление файла или папки.
            else if (Regex.IsMatch(read, patternDelete + patternPathFile) || Regex.IsMatch(read, patternDelete + patternPathFolder))
            {
                commands = read.Split(space);

                Console.WriteLine(dal.Delete(commands[1]));
                
                return true;
            }
            // Перемещение файла или папки.
            else if (Regex.IsMatch(read, patternMove + patternPathFile + patternPathDisk) || Regex.IsMatch(read, patternMove + patternPathFolder + patternPathDisk))
            {
                commands = read.Split(space);

                Console.WriteLine(dal.Move(commands[1], commands[2]));
                
                return true;
            }
            // Получение дерева каталогов.
            else if (Regex.IsMatch(read, patternTree + patternPathDisk))
            {
                commands = read.Split(space);

                foreach (var line in dal.ShowTree(commands[1]))
                {
                    Console.WriteLine(line);
                }
                
                return true;
            }
            // Выход.
            else if (read == patternExit)
            {
                return false;
            }
            // Несуществующая команда.
            else
            {
                Console.WriteLine("\"{0}\" не является внутренней или внешней\nкомандой, исполняемой программой или внешним файлом", read);
                return true;
            }
        }
    }
}
