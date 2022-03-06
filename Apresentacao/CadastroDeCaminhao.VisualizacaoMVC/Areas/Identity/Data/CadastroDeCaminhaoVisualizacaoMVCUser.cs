using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CadastroDeCaminhao.VisualizacaoMVC.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CadastroDeCaminhaoVisualizacaoMVCUser class
    public class CadastroDeCaminhaoVisualizacaoMVCUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "varchar(200)")]
        public string FirstName { get; set; }
        [PersonalData]
        [Column(TypeName = "varchar(200)")]
        public string LastName { get; set; }
    }
}
