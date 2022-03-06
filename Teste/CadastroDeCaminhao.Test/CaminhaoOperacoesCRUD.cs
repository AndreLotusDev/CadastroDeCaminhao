using CadastroDeCaminhao.Data.Contexto;
using CadastroDeCaminhao.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using CadastroDeCaminhao.Data.Repositorio;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CadastroDeCaminhao.Dominio.Entidades;
using CadastroDeCaminhao.Dominio.Enums;

namespace CadastroDeCaminhao.Test
{
    public class Tests
    {
        private ICaminhaoRepository _caminhaoRepository;
        private Mock<ILogger<CaminhaoRepository>> _logger;

        [SetUp]
        public void Setup()
        {
            var servicos = new ServiceCollection();

            var nomeDoBancoDeDados = "meu_banco_" + DateTime.Now.ToFileTimeUtc();

            var opcoes = new DbContextOptionsBuilder<Banco>()
                            .UseInMemoryDatabase(databaseName: nomeDoBancoDeDados)
                            .Options;

            servicos.AddDbContext<Banco>(options =>
            {
                options.UseInMemoryDatabase(databaseName: nomeDoBancoDeDados);
            });

            var construido = servicos.BuildServiceProvider();

            _logger = new Mock<ILogger<CaminhaoRepository>>();
            _caminhaoRepository = new CaminhaoRepository(construido.GetService<Banco>() , _logger.Object);
        }

        [Test]
        public async Task Verifica_Se_Integracao_E_Feita()
        {
            //Act
            var noComeco = await _caminhaoRepository.PegaCaminhaoPorPaginacaoAsync(1, 1);

            Caminhao caminhao = new();
            caminhao.AnoDeFabricacao = DateTime.Now.Year;
            caminhao.AnoModelo = DateTime.Now.Year;
            caminhao.Modelo = EModelo.FM;
            caminhao.NomeDoCaminhao = "Caminhao teste";
            caminhao.PrecoDoCaminhao = 100.90m;

            await _caminhaoRepository.CriaCaminhaoAsync(caminhao);
            var noFinal = await _caminhaoRepository.PegaCaminhaoPorPaginacaoAsync(1, 1);

            //Assert
            Assert.AreEqual(0, noComeco.caminhoesEncontrados.Count);
            Assert.AreEqual(1, noFinal.caminhoesEncontrados.Count);
        }

        [Test]
        public async Task Verifica_Se_Delecao_Acha_Id()
        {
            const bool FOI_DELETADO_COM_SUCESSO = true;
            const int NAO_HA_MAIS_NADA_NO_BANCO = 0;

            //Arrange
            Caminhao caminhao = new();
            caminhao.AnoDeFabricacao = DateTime.Now.Year;
            caminhao.AnoModelo = DateTime.Now.Year;
            caminhao.Modelo = EModelo.FM;
            caminhao.NomeDoCaminhao = "Caminhao teste";
            caminhao.PrecoDoCaminhao = 100.90m;

            //Act
            var caminhaoCriado = await _caminhaoRepository.CriaCaminhaoAsync(caminhao);
            var delecaoOperacao = await _caminhaoRepository.DeletaCaminhaoPorIdAsync(caminhaoCriado.caminhaoCriado.Id);

            var noFinal = await _caminhaoRepository.PegaCaminhaoPorPaginacaoAsync(1, 1);

            //Assert
            Assert.AreEqual(delecaoOperacao.deletadoComSucesso, FOI_DELETADO_COM_SUCESSO);
            Assert.AreEqual(noFinal.caminhoesEncontrados.Count , NAO_HA_MAIS_NADA_NO_BANCO);
        }

        [Test]
        [TestCase("NOVO NOME")]
        [TestCase("CAMINHAO TESTE")]
        [TestCase("CAMINHAO NOVINHO")]
        public async Task VerificaSeCaminhaoInseridoPodeSerAtualizado(string caminhaoNome)
        {
            const bool FOI_ATUALIZADO = true;

            //Arrange
            Caminhao caminhao = new();
            caminhao.AnoDeFabricacao = DateTime.Now.Year;
            caminhao.AnoModelo = DateTime.Now.Year;
            caminhao.Modelo = EModelo.FM;
            caminhao.NomeDoCaminhao = "Caminhao teste";
            caminhao.PrecoDoCaminhao = 100.90m;

            //Act
            var caminhaoCriado = await _caminhaoRepository.CriaCaminhaoAsync(caminhao);

            caminhaoCriado.caminhaoCriado.NomeDoCaminhao = caminhaoNome;
            var atualizaOperacao = await _caminhaoRepository.AtualizaCaminhaoAsync(caminhaoCriado.caminhaoCriado);

            //Assert
            Assert.AreEqual(FOI_ATUALIZADO, atualizaOperacao.atualizadoComSucesso);
            Assert.AreEqual(caminhaoNome, atualizaOperacao.caminhaoAtualizado.NomeDoCaminhao);
        }

    }
}