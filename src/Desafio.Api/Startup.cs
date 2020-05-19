using Desafio.Core.Contratos;
using Desafio.Core.Settings;
using Desafio.Infra.Extensions;
using Desafio.Infra.Servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Desafio.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            services.AddClassesMatchingInterfaces(typeof(IConsultaFilmeServico).Assembly);
            services.AddClassesMatchingInterfaces(typeof(Core.Contratos.IMovieApi).Assembly);
            services.AddClassesMatchingInterfaces(typeof(Infra.Servicos.MovieApi).Assembly);

            services.Configure<EndPoints>(Configuration.GetSection("Endpoints"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
