using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Aplicacao.Interfaces
{
    public interface IAtualInformacaoUsuario
    {
        string UsuarioId { get; }
        string UsuarioNome { get; }
    }

}
