using Nancy;
using Nancy.Extensions;
using System;

namespace ConsoleApp_TesteNancy
{
    public class SimpleNancyModule : NancyModule
    {
        private IMyService _srv;

        private static Response RequiresAuthentication(NancyContext context)
        {
            Response response = null;
            if ((context.CurrentUser == null) ||
                String.IsNullOrWhiteSpace(context.CurrentUser.UserName))
            {
                response = new Response { StatusCode = HttpStatusCode.Unauthorized };
            }

            return response;
        }

        public SimpleNancyModule(IMyService srv): base("/hbsis")
        {
            _srv = srv;

            Before.AddItemToStartOfPipeline(RequiresAuthentication);

            Get["/test/{param}"] = paramns =>
            {
                var feeds = new string[] { "foo", "bar", paramns.param };
                return Response.AsJson(feeds);
            };

            Get["/test2"] = paramns =>
            {
                var model = new ModeloTeste
                {
                    Nome = "Nome Teste",
                    Data = DateTime.Now
                };

                var r = Response.AsJson(model);

                return r;
            };

            Get["/test3"] = _ =>
            {
                var fromSrv = srv.MyMethod();
                var r = Response.AsJson(fromSrv);

                return r;
            };

            Post["/post-test"] = _ =>
            {
                Console.WriteLine("***Post called***");
                var s = Response.AsJson(Request.Body.AsString(), HttpStatusCode.OK);
                return s;
            };
        }
    }
}
