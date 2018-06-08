using DictionaryLib.Services;
using System.ServiceModel;

namespace Lab4Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IDictionaryContract> factory;
            factory = new ChannelFactory<IDictionaryContract>
                (new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/IDictionaryContract")
                );

            IDictionaryContract channel = factory.CreateChannel();
            var consoleClient = new ConsoleClient(channel);
            consoleClient.Run();
        }
    }
}
