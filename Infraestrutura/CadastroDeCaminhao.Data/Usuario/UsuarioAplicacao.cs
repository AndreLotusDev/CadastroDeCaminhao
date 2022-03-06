using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Data.Usuario
{
    public class UsuarioAplicacao : IdentityUser
    {
        public UsuarioAplicacao()
        {

        }

        public string UltimoNome { get; set; }
        public string PrimeiroNome { get; set; }
        public List<RefreshToken> TokensDeRefresh { get; set; }
        public bool OwnsToken(string token)
        {
            return this.TokensDeRefresh?.Find(x => x.AccessToken.ToString() == token) != null;
        }
    }

}
