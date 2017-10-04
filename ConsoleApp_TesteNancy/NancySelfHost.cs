using Nancy.Hosting.Self;
using System;

namespace ConsoleApp_TesteNancy
{
    public class NancySelfHost
    {
        private NancyHost _nancyHost;

        public void Start()
        {
            _nancyHost = new NancyHost(new Uri("http://localhost:5000"));
            _nancyHost.Start();
        } 

        public void Stop()
        {
            _nancyHost?.Stop();
        }
    }
}
