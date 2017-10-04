using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace ConsoleApp_TesteNancy
{
    public class AppBootstrapper: DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<IMyService, MyService>().AsSingleton();            

            base.ApplicationStartup(container, pipelines);
        }
    }
}
