using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroDeCaminhao.VisualizacaoMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeCaminhao.VisualizacaoMVC.Data
{
    public class CadastroDeCaminhaoVisualizacaoMVCContext : IdentityDbContext<CadastroDeCaminhaoVisualizacaoMVCUser>
    {
        public CadastroDeCaminhaoVisualizacaoMVCContext(DbContextOptions<CadastroDeCaminhaoVisualizacaoMVCContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
