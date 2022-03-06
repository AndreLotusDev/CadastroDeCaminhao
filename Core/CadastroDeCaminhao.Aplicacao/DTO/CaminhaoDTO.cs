using CadastroDeCaminhao.Dominio.Enums;
using System;

namespace CadastroDeCaminhao.Aplicacao.DTO
{
    public class CaminhaoDTO
    {
        public Guid Id { get; set; }
        public string NomeDoCaminhao { get; set; }
        public string? DescricaoSobreOCaminhao { get; set; }
        public decimal? PrecoDoCaminhao { get; set; }
        public EModelo Modelo { get; set; }
        public int AnoDeFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public string ImageBase64 { get; set; }

    }
}
