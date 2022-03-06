using CadastroDeCaminhao.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Dominio.Interfaces
{
    public interface ICaminhaoRepository
    {
        Task<(Caminhao caminhaoCriado, bool criadoComSucesso, List<string> mensagens)> CriaCaminhaoAsync(Caminhao caminhaoMapeadoParaCriar);
        Task<(Caminhao caminhaoAtualizado, bool atualizadoComSucesso, List<string> mensages)> AtualizaCaminhaoAsync(Caminhao caminhaoMapeadoParaCriar);
        Task<(bool deletadoComSucesso, List<string> mensagens)> DeletaCaminhaoPorIdAsync(Guid caminhaoIdParaDeletar);
        Task<(Caminhao caminhaoEncontrado, bool encontrado, List<string> mensagens)> PegaCaminhaoPorIdAsync(Guid caminhaoIdParaPesquisar);
        Task<(List<Caminhao> caminhoesEncontrados, bool encontrado, List<string> mensagens, int totalPaginas)> PegaCaminhaoPorPaginacaoAsync(int paginaAtual, int quantidadeAPegar);
    }
}
