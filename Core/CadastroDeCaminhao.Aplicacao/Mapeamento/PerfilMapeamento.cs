using AutoMapper;
using AutoMapper.Features;
using CadastroDeCaminhao.Aplicacao.DTO;
using CadastroDeCaminhao.Dominio.Entidades;
using System;
using System.Globalization;

namespace CadastroDeCaminhao.Aplicacao.Mapeamento
{
    public class PerfilMapeamento : Profile
    {
        public PerfilMapeamento()
        {
            CreateMap<int, DateTime>().ConvertUsing<TicksToDateTimeConverter>();

            CreateMap<Caminhao, CaminhaoDTO>().ReverseMap();
            CreateMap<Caminhao, CaminhaoCriacaoDTO>();

            CreateMap<CaminhaoCriacaoDTO, Caminhao>()
                .ForMember(x => x.AnoDeFabricacao, y => y.MapFrom(z => z.AnoDeFabricacao.Year))
                .ForMember(x => x.AnoModelo, y => y.MapFrom(z => z.AnoModelo.Year))
                .ForMember(x => x.PrecoDoCaminhao, y => y.MapFrom(z => Convert.ToDecimal(z.PrecoDoCaminhao, CultureInfo.InvariantCulture)));

            CreateMap<CaminhaoAtualizacaoDTO, Caminhao>()
                .ForMember(x => x.AnoDeFabricacao, y => y.MapFrom(z => z.AnoDeFabricacao.Year))
                .ForMember(x => x.AnoModelo, y => y.MapFrom(z => z.AnoModelo.Year))
                .ForMember(x => x.PrecoDoCaminhao, y => y.MapFrom(z => Convert.ToDecimal(z.PrecoDoCaminhao, CultureInfo.InvariantCulture)));

            CreateMap<Caminhao ,CaminhaoAtualizacaoDTO>()
               .ForMember(x => x.AnoDeFabricacao, y => y.MapFrom(z =>z.AnoDeFabricacao))
               .ForMember(x => x.AnoModelo, y => y.MapFrom(z => z.AnoModelo));
        }

        public class TicksToDateTimeConverter : ITypeConverter<int, DateTime>
        {
            public DateTime Convert(int source, DateTime destination, ResolutionContext context)
            {
                destination = new DateTime(source, 1, 1);
                return destination;
            }
        }

        
    }
}
