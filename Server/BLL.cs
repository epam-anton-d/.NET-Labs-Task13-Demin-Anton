using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Server.Core
{
    internal class BLL
    {
        DAL dal;

        public BLL()
        {
            dal = new DAL();
        }

        internal string[] GetFilesystemFromString()
        {
            string txt = dal.GetFoldersFromFile();

            string[] read = txt.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            return read;
        }

        internal void SendFilesystemToDal(List<Node> nodeList)
        {
            dal.PutFoldersIntoFile(nodeList);
        }
    }
}
