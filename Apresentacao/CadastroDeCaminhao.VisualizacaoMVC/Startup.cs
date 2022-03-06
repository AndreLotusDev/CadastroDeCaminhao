using CadastroDeCaminhao.Aplicacao.Interfaces;
using CadastroDeCaminhao.Aplicacao.Mapeamento;
using CadastroDeCaminhao.Aplicacao.Servicos;
using CadastroDeCaminhao.Data;
using CadastroDeCaminhao.Data.Contexto;
using CadastroDeCaminhao.Data.Repositorio;
using CadastroDeCaminhao.Dominio.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace CadastroDeCaminhao.VisualizacaoMVC
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
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(PerfilMapeamento));

            services.AddDbContext<Banco>(options => options.UseNpgsql(Configuration.GetConnectionString("CadastroDeCaminhaoVisualizacaoMVCContextConnection")));
            services.AddScoped<ICaminhaoAppService, CaminhaoAppService>();
            services.AddScoped<ICaminhaoRepository, CaminhaoRepository>();
            services.AddTransient<IAtualInformacaoUsuario, AtualInformacaoUsuario>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Banco contexto)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            contexto.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
