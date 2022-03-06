using CadastroDeCaminhao.Dominio.Enums;
using System;
using System.Collections.Generic;

namespace CadastroDeCaminhao.Dominio.Entidades
{
    public class Caminhao : EntidadeBase
    {
        public string NomeDoCaminhao { get; set; }
        public string? DescricaoSobreOCaminhao { get; set; }
        public decimal? PrecoDoCaminhao { get; set; }
        public EModelo Modelo { get; set; }
        public int AnoDeFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public string ImageBase64 { get; set; }

        public override (bool ehValido, List<string> mensagensDeValidao) Validar()
        {
            List<string> mensagensDeValidacao = new();
            const bool VALIDO = true;
            const bool INVALIDO = false;
            const int PROXIMO_ANO = 1;

            var anoAtual = DateTime.Now.Year;

            var anoEhInferiorAoAtual = AnoDeFabricacao < anoAtual;
            if (anoEhInferiorAoAtual)
            {
                mensagensDeValidacao.Add("O Ano de fabricação não pode ser inferior ao atual");
                return (INVALIDO, mensagensDeValidacao);
            }

            var anoDoModeloEhInferiorAoAtual = AnoModelo < anoAtual;
            if (anoDoModeloEhInferiorAoAtual)
            {
                mensagensDeValidacao.Add("O Ano do modelo não pode ser inferior ao atual");
                return (INVALIDO, mensagensDeValidacao);
            }

            var anoDoModeloEhSuperiorAoAtualESubsequente = AnoModelo > anoAtual && AnoModelo > anoAtual + PROXIMO_ANO;
            if(anoDoModeloEhSuperiorAoAtualESubsequente)
            {
                mensagensDeValidacao.Add("O Ano do modelo não pode ser superior a 1 ano (subsequente)");
                return (INVALIDO, mensagensDeValidacao);
            }

            var nomeDoCaminhaoEhVazio = NomeDoCaminhao.Length is 0;
            if(nomeDoCaminhaoEhVazio)
            {
                mensagensDeValidacao.Add("O nome do caminhão deve ser preenchida");
                return (INVALIDO, mensagensDeValidacao);
            }

            return (VALIDO, mensagensDeValidacao);
        }
    }
}
