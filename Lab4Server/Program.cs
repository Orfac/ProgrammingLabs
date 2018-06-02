using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(new DictionaryService());
            host.Open();
            Console.WriteLine("Приложение готово");
            Console.ReadKey();
            host.Close();
        }
    }
}
