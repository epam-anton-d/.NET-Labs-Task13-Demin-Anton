using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core
{
    internal class Folder : FileOrFolder
    {
        // name - наследованное поле.
        public List<Folder> folderList = new List<Folder>();
        public List<Files> fileList = new List<Files>();
    }
}
