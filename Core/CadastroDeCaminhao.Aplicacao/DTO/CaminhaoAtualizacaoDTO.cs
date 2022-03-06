using CadastroDeCaminhao.Aplicacao.Auxiliares;
using CadastroDeCaminhao.Dominio.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeCaminhao.Aplicacao.DTO
{
    public class CaminhaoAtualizacaoDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do caminhão é obrigatório")]
        [Display(Name = "Nome do caminhão")]
        public string NomeDoCaminhao { get; set; }

        [Display(Name = "Descrição sobre o caminhão")]
        public string? DescricaoSobreOCaminhao { get; set; }

        [Required(ErrorMessage = "O preço do caminhão é obrigatório")]
        [Display(Name = "Preço do caminhão")]
        public decimal? PrecoDoCaminhao { get; set; }

        [Display(Name = "Modelo do caminhão")]
        public EModelo Modelo { get; set; }

        [Required(ErrorMessage = "O ano de fabricação é obrigatório")]
        [Display(Name = "Ano de fabricação")]
        [DataType(DataType.Date)]
        [ValidadorDeAnoAtual(ErrorMessage = "O ano deve ser atual")]
        public DateTime AnoDeFabricacao { get; set; }

        [Required(ErrorMessage = "O ano do modelo é obrigatório")]
        [Display(Name = "Ano do modelo")]
        [DataType(DataType.Date)]
        [ValidadorDeAnoAtualESubsequente(ErrorMessage = "O ano deve ser atual ou subsequente")]
        public DateTime AnoModelo { get; set; }

        [Display(Name = "Imagem")]
        public string ImageBase64 { get; set; }

    }
}
