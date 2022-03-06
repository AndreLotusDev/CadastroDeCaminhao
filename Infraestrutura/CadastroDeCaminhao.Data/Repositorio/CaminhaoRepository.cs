using CadastroDeCaminhao.Dominio.Entidades;
using CadastroDeCaminhao.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Data.Repositorio
{
    public class CaminhaoRepository : GenericoAsyncRepositorio<Caminhao>, ICaminhaoRepository
    {
        public ILogger<CaminhaoRepository> _logger { get; }
        List<string> mensagensParaRetornar = new();
        const bool ENCONTRADO = true;
        const bool SALVO = true;
        const bool NAO_ENCONTRADO = false;
        const bool ERRO_NEGOCIO = false;
        const bool ERRO_INTERNO = false;


        public CaminhaoRepository(Contexto.Banco contexto, ILogger<CaminhaoRepository> logger) : base(contexto)
        {
            _logger = logger;
        }

        public async Task<(Caminhao caminhaoAtualizado, bool atualizadoComSucesso, List<string> mensages)> 
            AtualizaCaminhaoAsync(Caminhao caminhaoMapeadoParaCriar)
        {
            try
            {
                await AtualizaAsync(caminhaoMapeadoParaCriar);

                await _contexto.SalvaAlteracoesAsync();

                mensagensParaRetornar.Add("Adicionado com sucesso");
                return (caminhaoMapeadoParaCriar, SALVO, mensagensParaRetornar);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                mensagensParaRetornar.Add("Houve um erro interno durante o salvamento do caminhao");
                return (caminhaoMapeadoParaCriar, ERRO_INTERNO, mensagensParaRetornar);
            }
        }

        public async Task<(Caminhao caminhaoCriado, bool criadoComSucesso, List<string> mensagens)> 
            CriaCaminhaoAsync(Caminhao caminhaoMapeadoParaCriar)
        {
            await AdicionaAsync(caminhaoMapeadoParaCriar);
            await _contexto.SalvaAlteracoesAsync();

            mensagensParaRetornar.Add("Encontrado com sucesso!");
            return (caminhaoMapeadoParaCriar, ENCONTRADO, mensagensParaRetornar);
        }

        public async Task<(bool deletadoComSucesso, List<string> mensagens)> 
            DeletaCaminhaoPorIdAsync(Guid caminhaoIdParaDeletar)
        {
            var caminhaoEncontrado = await PegaPorIdAsync(caminhaoIdParaDeletar);

            if (caminhaoEncontrado == null)
            {
                mensagensParaRetornar.Add("Caminhão não encontrado pelo ID");
                return (NAO_ENCONTRADO, mensagensParaRetornar);
            }

            try
            {
                mensagensParaRetornar.Add("Encontrado e deletado com sucesso!");

                await DeletaAsync(caminhaoEncontrado);
                await _contexto.SalvaAlteracoesAsync();

                return (ENCONTRADO, mensagensParaRetornar);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                mensagensParaRetornar.Add("Houve um erro interno durante o delete do caminhao");
                return (ERRO_INTERNO, mensagensParaRetornar);
            }

            
        }

        public async Task<(Caminhao caminhaoEncontrado, bool encontrado, List<string> mensagens)> 
            PegaCaminhaoPorIdAsync(Guid caminhaoIdParaPesquisar)
        {
            var caminhaoEncontrado = await PegaPorIdAsync(caminhaoIdParaPesquisar);

            if(caminhaoEncontrado == null)
            {
                mensagensParaRetornar.Add("Nao encontrado com esse ID");
                return (caminhaoEncontrado ,NAO_ENCONTRADO, mensagensParaRetornar);
            }

            mensagensParaRetornar.Add("Encontrado com sucesso");
            return (caminhaoEncontrado, ENCONTRADO, mensagensParaRetornar);
        }

        public async Task<(List<Caminhao> caminhoesEncontrados, bool encontrado, List<string> mensagens, int totalPaginas)> 
            PegaCaminhaoPorPaginacaoAsync(int paginaAtual, int quantidadeAPegar)
        {
            const int NENHUMA_PAGINA = 0;

            var entidadesARetornar = await Entidade.Skip((paginaAtual - 1) * quantidadeAPegar).Take(quantidadeAPegar).ToListAsync();

            if(entidadesARetornar?.Count == 0)
            {
                mensagensParaRetornar.Add("Não encontrado nenhum caminhão");
                return (entidadesARetornar, NAO_ENCONTRADO, mensagensParaRetornar, NENHUMA_PAGINA);
            }

            var quantidadePaginas = Math.Ceiling((decimal)Entidade.Count() / (decimal)quantidadeAPegar);

            mensagensParaRetornar.Add($"Encontrado {entidadesARetornar.Count} caminhoões");
            return (entidadesARetornar, ENCONTRADO, mensagensParaRetornar, (int)quantidadePaginas);
        }
    }
}
