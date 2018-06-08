using System;
using System.ServiceModel;

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
