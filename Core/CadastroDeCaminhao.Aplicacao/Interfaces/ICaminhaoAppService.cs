using CadastroDeCaminhao.Aplicacao.DTO;
using CadastroDeCaminhao.Dominio.Embrulhadores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Aplicacao.Interfaces
{
    public interface ICaminhaoAppService
    {
        Task<(CaminhaoDTO caminhaoDTOCadastro, bool criadoComSucesso, List<string> mensagens)> CriaCaminhaoAsync(CaminhaoCriacaoDTO caminhaoParaCriar);
        Task<(CaminhaoDTO caminhaoDTOAtualizado, bool atualizadoComSucesso, List<string> mensagens)> AtualizaCaminhaoAsync(CaminhaoAtualizacaoDTO caminhaoParaAtualizar);

        Task<(bool deletadoComSucesso, List<string> mensagens)> DeletaCaminhaoPorIdAsync(Guid caminhaoIdParaDeletar);
        Task<(CaminhaoAtualizacaoDTO caminhaoDTOParaRetornar, bool encontrado, List<string> mensagens)> PegaCaminhaoPorIdAsync(Guid caminhaoIdParaPesquisar);
        Task<(Paginacao<CaminhaoDTO> paginacaoDeCaminhaoDTO, bool encontrado, List<string> mensagens)> PegaCaminhaoPorPaginacaoAsync(int paginaAtual, int quantidadeAPegar);
    }
}
