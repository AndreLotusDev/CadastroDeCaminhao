using CadastroDeCaminhao.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Data
{
    public class AtualInformacaoUsuario : IAtualInformacaoUsuario
    {
        public AtualInformacaoUsuario(IHttpContextAccessor httpContextAccessor)
        {
            UsuarioId = httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;
            UsuarioNome = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UsuarioEmail = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        }

        public string UsuarioId { get; }

        public string UsuarioNome { get; }
        public string UsuarioEmail { get; }
    }

}
