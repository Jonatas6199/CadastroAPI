using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroAPI.Database;
using CadastroAPI.Models;
using CadastroAPI.TestData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CadastroAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            //Adiciona um contexto de banco que será utilizado na API
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("cadastro_database"));

            //Adiciona o CORS habilitando que a API seja utilizada através de um navegador web
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(option => option.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            IServiceScope scope = app.ApplicationServices.CreateScope();

            ApiContext context = scope.ServiceProvider.GetService<ApiContext>();

            DadosTeste.AdicionarDadosTeste(context);
        }

      
    }
}
