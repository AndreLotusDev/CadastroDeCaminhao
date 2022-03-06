using AutoMapper;
using CadastroDeCaminhao.Aplicacao.DTO;
using CadastroDeCaminhao.Aplicacao.Interfaces;
using CadastroDeCaminhao.Dominio.Embrulhadores;
using CadastroDeCaminhao.Dominio.Entidades;
using CadastroDeCaminhao.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Aplicacao.Servicos
{
    public class CaminhaoAppService : ICaminhaoAppService
    {
        public ICaminhaoRepository _caminhaoRepository { get; }
        public IMapper _mapper { get; }

        public CaminhaoAppService(ICaminhaoRepository caminhaoRepository, IMapper mapper)
        {
            _caminhaoRepository = caminhaoRepository;
            _mapper = mapper;
        }

        public async Task<(CaminhaoDTO caminhaoDTOCadastro, bool criadoComSucesso, List<string> mensagens)> 
            CriaCaminhaoAsync(CaminhaoCriacaoDTO caminhaoParaCriar)
        {
            var caminhaoMapeadoParaCriar = _mapper.Map<Caminhao>(caminhaoParaCriar);

            var criacaoOperacao = await _caminhaoRepository.CriaCaminhaoAsync(caminhaoMapeadoParaCriar);

            var caminhaoCriadoEMapeado = _mapper.Map<CaminhaoDTO>(criacaoOperacao.caminhaoCriado);
            return (caminhaoCriadoEMapeado, criacaoOperacao.criadoComSucesso, criacaoOperacao.mensagens);
        }

        public async Task<(CaminhaoDTO caminhaoDTOAtualizado, bool atualizadoComSucesso, List<string> mensagens)> 
            AtualizaCaminhaoAsync(CaminhaoAtualizacaoDTO caminhaoParaAtualizar)
        {
            var caminhaoMapeadoParaCriar = _mapper.Map<Caminhao>(caminhaoParaAtualizar);

            var criacaoOperacao = await _caminhaoRepository.AtualizaCaminhaoAsync(caminhaoMapeadoParaCriar);

            var caminhaoCriadoEMapeado = _mapper.Map<CaminhaoDTO>(criacaoOperacao.caminhaoAtualizado);
            return (caminhaoCriadoEMapeado, criacaoOperacao.atualizadoComSucesso, criacaoOperacao.mensages);
        }

        public async Task<(bool deletadoComSucesso, List<string> mensagens)> 
            DeletaCaminhaoPorIdAsync(Guid caminhaoIdParaDeletar)
        {
            var delecaoOperacao = await _caminhaoRepository.DeletaCaminhaoPorIdAsync(caminhaoIdParaDeletar);

            return (delecaoOperacao.deletadoComSucesso, delecaoOperacao.mensagens);
        }

        public async Task<(CaminhaoAtualizacaoDTO caminhaoDTOParaRetornar, bool encontrado, List<string> mensagens)> 
            PegaCaminhaoPorIdAsync(Guid caminhaoIdParaPesquisar)
        {
            var procuraOperacao = await _caminhaoRepository.PegaCaminhaoPorIdAsync(caminhaoIdParaPesquisar);

            var caminhaoDTOMapeado = _mapper.Map<CaminhaoAtualizacaoDTO>(procuraOperacao.caminhaoEncontrado);
            return (caminhaoDTOMapeado, procuraOperacao.encontrado, procuraOperacao.mensagens);
        }

        public async Task<(Paginacao<CaminhaoDTO> paginacaoDeCaminhaoDTO, bool encontrado, List<string> mensagens)> 
            PegaCaminhaoPorPaginacaoAsync(int paginaAtual, int quantidadeAPegar)
        {
            List<CaminhaoDTO> caminhosDTOSParaPaginar = new();
            var caminhoesOperacao = await _caminhaoRepository.PegaCaminhaoPorPaginacaoAsync(paginaAtual, quantidadeAPegar);

            caminhoesOperacao.caminhoesEncontrados.ForEach(c => 
                caminhosDTOSParaPaginar.Add(_mapper.Map<CaminhaoDTO>(c)));

            var caminhoesDTOPaginacao = new Paginacao<CaminhaoDTO>()
            { 
                Itens = caminhosDTOSParaPaginar,
                PaginaAtual = paginaAtual,
                TamanhoDaPagina = quantidadeAPegar,
                TotalDeElementos = caminhosDTOSParaPaginar.Count,
                TotalDePagina = caminhoesOperacao.totalPaginas
            };

            return (caminhoesDTOPaginacao, caminhoesOperacao.encontrado, caminhoesOperacao.mensagens);
        }
    }
}
