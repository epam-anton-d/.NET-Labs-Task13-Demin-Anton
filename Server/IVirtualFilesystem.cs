using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;

namespace Server.Core
{
    [ServiceContract]
    interface IVirtualFilesystem
    {
        [OperationContract]
        FileStream CreateFile(string name);
        [OperationContract]
        FileStream CreateFolder(string name);
        [OperationContract]
        FileStream ShowTree(string dir);
        [OperationContract]
        FileStream Copy(string sourceDir, string destinationDir);
        [OperationContract]
        FileStream Delete(string dir);
        [OperationContract]
        FileStream Move(string sourceDir, string destinationDir);
    }
}
