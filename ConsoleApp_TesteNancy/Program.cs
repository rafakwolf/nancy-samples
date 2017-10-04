﻿using Topshelf;

namespace ConsoleApp_TesteNancy
{
    class Program
    {
        static void Main(string[] args) =>
            HostFactory.Run(x =>
                {
                    x.Service<NancySelfHost>(s =>
                    {
                        s.ConstructUsing(name => new NancySelfHost());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });

                    x.RunAsLocalSystem();
                    x.SetDescription("Nancy-SelfHost example");
                    x.SetDisplayName("Nancy-SelfHost Service");
                    x.SetServiceName("Nancy-SelfHost");
                });
    }
}
