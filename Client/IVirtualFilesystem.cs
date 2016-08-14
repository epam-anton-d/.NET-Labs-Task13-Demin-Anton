using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;

namespace Client.Core
{
    /// <summary>
    /// Интерфейс "общения" клиента с сервером.
    /// </summary>
    [ServiceContract]
    public interface IVirtualFilesystem
    {
        [OperationContract]
        int CreateFile(string name);
        [OperationContract]
        int CreateFolder(string name);
        [OperationContract]
        List<string> ShowTree(string dir);
        [OperationContract]
        int Copy(string sourceDir, string destinationDir);
        [OperationContract]
        int Delete(string dir);
        [OperationContract]
        int Move(string sourceDir, string destinationDir);
    }
}
