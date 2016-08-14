using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace Server.Core
{
    internal class Filesystem : IVirtualFilesystem
    {
        char[] slash = new char[] { '\\' };
        List<Folder> root;
        List<Files> files;
        string filePattern = @"[a-z0-9]+\.[a-z0-9]{1,3}";

        public Filesystem()
        {
            root = new List<Folder>();
            root.Add(new Folder() { name = "c:" });
        }

        private List<Folder> DiveIntoTheFolder(int i, string[] path, List<Folder> folderList)
        {
            i++;
            if (i < path.Length - 1)
            {
                return DiveIntoTheFolder(i, path, folderList.Find(x => x.name == path[i]).folderList);
            }
            else
            {
                return folderList;
            }

        }

        private List<Folder> DiveIntoTheFolderToCopyFile(int i, string[] path, List<Folder> folderList)
        {
            i++;
            if (i < path.Length - 2)
            {
                return DiveIntoTheFolderToCopyFile(i, path, folderList.Find(x => x.name == path[i]).folderList);
            }
            else
            {
                return folderList;
            }

        }

        private void DiveInFolderToCreateFile(int i, string[] path, Folder folder, List<Files> fileList)
        {
            i++;
            if (i < path.Length - 1)
            {
                DiveInFolderToCreateFile(i, path, folder.folderList.Find(x => x.name == path[i]), fileList);
            }
            else
            {
                folder.fileList.Add(new Files() { name = path[i] });
            }

        }

        private Folder DiveInFolderToShowTree(int i, string[] path, Folder folderList)
        {
            i++;
            if (i < path.Length)
            {
                return DiveInFolderToShowTree(i, path, folderList.folderList.Find(x => x.name == path[i]));
            }
            else
            {
                return folderList;
            }

        }

        /// <summary>
        /// Создание папки. Работает.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        //public int CreateFolder(string dir)
        //{
        //    string[] path = dir.Split(slash);
        //
        //    try
        //    {
        //        DiveIntoTheFolder(-1, path, root).Add(new Folder() { name = path[path.Length - 1] });
        //    }
        //    catch
        //    {
        //        return 1;
        //    }
        //
        //    return 0;
        //}

        public FileStream CreateFolder(string dir)
        {
            string[] path = dir.Split(slash);

            try
            {
                DiveIntoTheFolder(-1, path, root).Add(new Folder() { name = path[path.Length - 1] });

                FileStream stream = File.Create("Data.xml");

                // Помещаем объектный граф (для базовых типов) в поток в двоичном формате.
                SoapFormatter formatter = new SoapFormatter();

                // Cериализация.
                formatter.Serialize(stream, "Папка успешно создана");

                return stream;
            }
            catch
            {
                return null;
            }
        }

        private List<string> TreeRecursion(string branch, Folder treeRoot, List<string> treeList)
        {
            if (treeList == null)
            {
                treeList = new List<string>();
            }

            if (treeRoot.folderList.Count != 0)
            {
                foreach (var folder in treeRoot.folderList)
                {
                    //Console.WriteLine(branch + folder.name + '\\');
                    treeList.Add(branch + folder.name + '\\');
                    
                    TreeRecursion(branch + folder.name + '\\', folder, treeList);
                }
            }

            if (treeRoot.fileList.Count != 0)
            {
                foreach (var file in treeRoot.fileList)
                {
                    //Console.WriteLine(branch + file.name);

                    treeList.Add(branch + file.name);
                }
            }

            return treeList;
        }

        /// <summary>
        /// Создание файла.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public FileStream CreateFile(string dir)
        {
            string[] path = dir.Split(slash);

            try
            {
                DiveInFolderToCreateFile(0, path, root.Find(x => x.name == path[0]), files);

                FileStream stream = File.Create("Data.xml");

                // Помещаем объектный граф (для базовых типов) в поток в двоичном формате.
                SoapFormatter formatter = new SoapFormatter();

                // Cериализация.
                formatter.Serialize(stream, "Файл успешно создан");

                stream.Close();

                stream = File.OpenRead("Data.xml");

                return stream;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Показать дерево каталогов.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public FileStream ShowTree(string dir)
        {
            string[] path = dir.Split(slash);

            try
            {
                Folder rootFolder = DiveInFolderToShowTree(0, path, root.Find(x => x.name == path[0]));

                string branch = rootFolder.name + '\\';

                List<string> treeList = TreeRecursion(branch, rootFolder, null);

                FileStream stream = File.Create("Data.xml");

                // Помещаем объектный граф (для базовых типов) в поток в двоичном формате.
                SoapFormatter formatter = new SoapFormatter();

                // Cериализация.
                formatter.Serialize(stream, treeList);

                return stream;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Копируем файл или папку.
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destinationDir"></param>
        /// <returns></returns>
        public FileStream Copy(string sourceDir, string destinationDir)
        {
            string[] sourcePath = sourceDir.Split(slash);
            string[] destinationPath = destinationDir.Split(slash);

            if (Regex.IsMatch(sourcePath[sourcePath.Length - 1], filePattern))
            {
                try
                {
                    DiveIntoTheFolder(-1, destinationPath, root).Find(x => x.name == destinationPath[destinationPath.Length - 1]).fileList.Add(DiveIntoTheFolderToCopyFile(-1, sourcePath, root).Find(x => x.name == sourcePath[sourcePath.Length - 2]).fileList.Find(x => x.name == sourcePath[sourcePath.Length - 1]));

                    FileStream stream = File.Create("Data.xml");

                    // Помещаем объектный граф (для базовых типов) в поток в двоичном формате.
                    SoapFormatter formatter = new SoapFormatter();

                    // Cериализация.
                    formatter.Serialize(stream, "Файл успешно скопирован");

                    return stream;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                try
                {
                    DiveIntoTheFolder(-1, destinationPath, root).Find(x => x.name == destinationPath[destinationPath.Length - 1]).folderList.Add(DiveIntoTheFolder(-1, sourcePath, root).Find(x => x.name == sourcePath[sourcePath.Length - 1]));

                    FileStream stream = File.Create("Data.xml");

                    // Помещаем объектный граф (для базовых типов) в поток в двоичном формате.
                    SoapFormatter formatter = new SoapFormatter();

                    // Cериализация.
                    formatter.Serialize(stream, "Папка успешно скопирована");

                    return stream;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Удаляем файл или папку.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public FileStream Delete(string dir)
        {
            string[] path = dir.Split(slash);

            // Проверяем - удаляем файл или папку.
            if (Regex.IsMatch(path[path.Length - 1], filePattern))
            {
                try
                {
                    DiveIntoTheFolderToCopyFile(-1, path, root).Find(x => x.name == path[path.Length - 2]).fileList.RemoveAll(x => x.name == path[path.Length - 1]);

                    FileStream stream = File.Create("Data.xml");

                    // Помещаем объектный граф (для базовых типов) в поток в двоичном формате.
                    SoapFormatter formatter = new SoapFormatter();

                    // Cериализация.
                    formatter.Serialize(stream, "Файл успешно удален");

                    return stream;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                try
                {
                    DiveIntoTheFolder(-1, path, root).RemoveAll(x => x.name == path[path.Length - 1]);

                    FileStream stream = File.Create("Data.xml");

                    // Помещаем объектный граф (для базовых типов) в поток в двоичном формате.
                    SoapFormatter formatter = new SoapFormatter();

                    // Cериализация.
                    formatter.Serialize(stream, "Папка успешно удалена");

                    return stream;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Перемещение файла или папки.
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destinationDir"></param>
        /// <returns></returns>
        public FileStream Move(string sourceDir, string destinationDir)
        {
            try
            {
                Copy(sourceDir, destinationDir);

                Delete(sourceDir);

                FileStream stream = File.Create("Data.xml");

                // Помещаем объектный граф (для базовых типов) в поток в двоичном формате.
                SoapFormatter formatter = new SoapFormatter();

                // Cериализация.
                formatter.Serialize(stream, "Папка/файл успешно перемещена");

                return stream;
            }
            catch
            {
                return null;
            }
        }

    }
}
