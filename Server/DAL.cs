using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Interface;

namespace Server.Core
{
    internal class DAL
    {

        // Получить данные о файловой системе из файла.
        public string GetFoldersFromFile()
        {
            string readFilesystemTxt;

            try
            {
                // Чтение файла.
                StreamReader fl = File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filesystem.txt"));
                readFilesystemTxt = fl.ReadToEnd();
                fl.Close();
            }
            catch
            {
                return null; // Ошибка получения FS.
            }

            return readFilesystemTxt;
        }

        // Записать данные в файл.
        public bool PutFoldersIntoFile(List<Node> nodeList)
        {
            try
            {
                FileInfo f3 = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filesystem.txt"));
                StreamWriter w1 = f3.CreateText();

                DiveInFolderToSaveInFile(nodeList, "", w1);

                w1.Close();
            }
            catch
            {
                return false; // Ошибка записи в файл.
            }

            return true;
        }

        private void DiveInFolderToSaveInFile(List<Node> nodeList, string path, StreamWriter w1)
        {
            foreach (var node in nodeList)
            {
                w1.Write(path + "\\" + node.name + ";");
                
                if (node is Folder && (node as Folder).nodeList.Count != 0)
                {
                    DiveInFolderToSaveInFile((node as Folder).nodeList, path + "\\" + node.name, w1);
                }
            }
        }


    }
}
