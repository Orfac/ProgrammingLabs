﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
