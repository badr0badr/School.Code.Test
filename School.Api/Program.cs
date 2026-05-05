using School.Api.Helper;
using System.Threading.Tasks;

namespace School.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependency(builder.Configuration);

            var app = builder.Build();

            app.UseCustomMiddlewareAsync();

            app.Run();
        }
    }
}