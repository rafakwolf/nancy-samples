using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using System;

namespace ConsoleApp_TesteNancy
{
    public class SimpleNancyModule : NancyModule
    {
        private static Response RequiresAuthentication(NancyContext context)
        {
            Response response = null;
            if (string.IsNullOrWhiteSpace(context.CurrentUser?.UserName))
            {
                response = new Response { StatusCode = HttpStatusCode.Unauthorized };
            }

            return response;
        }

        public SimpleNancyModule(IMyService srv): base("/hbsis")
        {

            // Before.AddItemToStartOfPipeline(RequiresAuthentication);

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
                var s = Response.AsJson(Request.Body.AsString());
                return s;
            };

            Post["/post-model"] = _ =>
            {
                var model = this.BindAndValidate<ModeloTeste>();
                var valRes = ModelValidationResult;

                return !valRes.IsValid ? Response.AsJson(valRes) : Response.AsJson(model);
            };
        }
    }
}
