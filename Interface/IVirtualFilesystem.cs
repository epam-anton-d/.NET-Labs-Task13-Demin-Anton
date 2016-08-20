using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;

namespace Interface
{
    /// <summary>
    /// Интерфейс "общения" клиента с сервером.
    /// </summary>
    [ServiceContract]
    public interface IVirtualFilesystem
    {
        [OperationContract]
        bool Create(string dir);
        [OperationContract]
        bool Copy(string sourceDir, string destinationDir);
        [OperationContract]
        bool Delete(string dir);
        [OperationContract]
        bool Move(string sourceDir, string destinationDir);
        [OperationContract]
        List<string> ShowTree(string dir);
    }
}
