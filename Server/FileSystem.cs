using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using Interface;

namespace Server.Core
{
    internal class Filesystem : IVirtualFilesystem
    {
        char[] slash = new char[] { '\\' };
        List<Node> root;
        string filePattern = @"[a-z0-9]+\.[a-z0-9]{1,3}";
        BLL bll;

        public Filesystem()
        {
            bll = new BLL();
            root = new List<Node>();
            root.Add(new Folder() { name = "c:" });
            string[] read = bll.GetFilesystemFromString();
            
            if (read.Length != 0)
            {
                foreach (var item in read)
                {
                    Create(item);
                }
            }
            
        }

        public bool Create(string dir)
        {
            string[] path = dir.Split(slash);

            try
            {
                CreateRecursion((root.Find(x => x.name == "c:") as Folder).nodeList, 0, path);

                bll.SendFilesystemToDal(root);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private List<Node> CreateRecursion(List<Node> nodeList, int i, string[] path)
        {
            i++;
            if(i < path.Length - 1)
            {
                return CreateRecursion(((nodeList.Find(x => x.name == path[i])) as Folder).nodeList, i, path);
            }
            else
            {
                if(Regex.IsMatch(path[path.Length - 1], filePattern))
                {
                    nodeList.Add(new Files() { name = path[path.Length - 1] });
                    return nodeList;
                }
                else
                {
                    nodeList.Add(new Folder() { name = path[path.Length - 1] });
                    return nodeList;
                }
            }
        }

        /// <summary>
        /// Показать дерево каталогов.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public List<string> ShowTree(string dir)
        {
            string[] path = dir.Split(slash);

            List<string> treeList;

            try
            {
                Node rootFolder = DiveInFolderToShowTree(0, path, root.Find(x => x.name == "c:"));

                string branch = rootFolder.name + '\\';

                treeList = TreeRecursion(branch, rootFolder, null);
            }
            catch
            {
                return null;
            }

            return treeList;
        }

        private Node DiveInFolderToShowTree(int i, string[] path, Node node)
        {
            i++;
            if (i < path.Length)
            {
                return DiveInFolderToShowTree(i, path, (node as Folder).nodeList.Find(x => x.name == path[i]));
            }
            else
            {
                return node;
            }
        
        }

        private List<string> TreeRecursion(string branch, Node treeRoot, List<string> treeList)
        {
            if (treeList == null)
            {
                treeList = new List<string>();
            }
        
            if ((treeRoot as Folder).nodeList.Count != 0)
            {
                foreach (var node in (treeRoot as Folder).nodeList)
                {
                    if (node is Files)
                    {
                        treeList.Add(branch + node.name);
                    }
                    
                    if (node is Folder)
                    {
                        treeList.Add(branch + node.name + '\\');

                        TreeRecursion(branch + node.name + '\\', node, treeList);
                    }
                }
            }
            return treeList;
        }

        /// <summary>
        /// Копируем файл или папку.
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destinationDir"></param>
        /// <returns></returns>
        public bool Copy(string sourceDir, string destinationDir)
        {
            string[] sourcePath = sourceDir.Split(slash);
            string[] destinationPath = destinationDir.Split(slash);
        
            if (Regex.IsMatch(sourcePath[sourcePath.Length - 1], filePattern))
            {
                try
                {
                    (DiveIntoTheFolder(-1, destinationPath, root).Find(x => x.name == destinationPath[destinationPath.Length - 1]) as Folder).nodeList.Add((DiveIntoTheFolderToCopyFile(-1, sourcePath, root).Find(x => x.name == sourcePath[sourcePath.Length - 2]) as Folder).nodeList.Find(x => x.name == sourcePath[sourcePath.Length - 1]));

                    bll.SendFilesystemToDal(root);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    (DiveIntoTheFolder(-1, destinationPath, root).Find(x => x.name == destinationPath[destinationPath.Length - 1]) as Folder).nodeList.Add(DiveIntoTheFolder(-1, sourcePath, root).Find(x => x.name == sourcePath[sourcePath.Length - 1]));

                    bll.SendFilesystemToDal(root);
                }
                catch
                {
                    return false;
                }
            }
        
            return true;
        }
        
        private List<Node> DiveIntoTheFolder(int i, string[] path, List<Node> nodeList)
        {
            i++;
            if (i < path.Length - 1)
            {
                return DiveIntoTheFolder(i, path, (nodeList.Find(x => x.name == path[i]) as Folder).nodeList);
            }
            else
            {
                return nodeList;
            }
        
        }

        private List<Node> DiveIntoTheFolderToCopyFile(int i, string[] path, List<Node> nodeList)
        {
            i++;
            if (i < path.Length - 2)
            {
                return DiveIntoTheFolderToCopyFile(i, path, (nodeList.Find(x => x.name == path[i]) as Folder).nodeList);
            }
            else
            {
                return nodeList;
            }
        
        }

        /// <summary>
        /// Удаляем файл или папку.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public bool Delete(string dir)
        {
            string[] path = dir.Split(slash);
        
            // Проверяем - удаляем файл или папку.
            if (Regex.IsMatch(path[path.Length - 1], filePattern))
            {
                try
                {
                    (DiveIntoTheFolderToCopyFile(-1, path, root).Find(x => x.name == path[path.Length - 2]) as Folder).nodeList.RemoveAll(x => x.name == path[path.Length - 1]);

                    bll.SendFilesystemToDal(root);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    DiveIntoTheFolder(-1, path, root).RemoveAll(x => x.name == path[path.Length - 1]);

                    bll.SendFilesystemToDal(root);
                }
                catch
                {
                    return false;
                }
            }
        
            return true;
        }

        /// <summary>
        /// Перемещение файла или папки.
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destinationDir"></param>
        /// <returns></returns>
        public bool Move(string sourceDir, string destinationDir)
        {
            try
            {
                Copy(sourceDir, destinationDir);
        
                Delete(sourceDir);
            }
            catch
            {
                return false;
            }
        
            return true;
        }
    }
}
