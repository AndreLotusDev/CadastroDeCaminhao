using CadastroDeCaminhao.VisualizacaoMVC.Areas.Identity.Data;
using CadastroDeCaminhao.VisualizacaoMVC.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CadastroDeCaminhao.VisualizacaoMVC.Areas.Identity.IdentityHostingStartup))]
namespace CadastroDeCaminhao.VisualizacaoMVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CadastroDeCaminhaoVisualizacaoMVCContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("CadastroDeCaminhaoVisualizacaoMVCContextConnection")));

                services.AddDefaultIdentity<CadastroDeCaminhaoVisualizacaoMVCUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<CadastroDeCaminhaoVisualizacaoMVCContext>();

                var servicoConstruido = services.BuildServiceProvider();

                servicoConstruido.GetService<CadastroDeCaminhaoVisualizacaoMVCContext>().Database.Migrate();
            });
        }
    }
}