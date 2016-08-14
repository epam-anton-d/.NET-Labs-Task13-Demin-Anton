using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Core;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CLIENT";

            BLL bll = new BLL();
            
            string read;

            do
            {
                Console.Write("c:\\>");
                read = Console.ReadLine();
                read = read.Trim();

            } while (bll.ConsoleDialog(read));
        }
    }
}
