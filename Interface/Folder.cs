using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    /// <summary>
    /// Папка.
    /// </summary>
    public class Folder : Node
    {
        // name - наследованное поле.
        public List<Node> nodeList = new List<Node>();
    }
}
