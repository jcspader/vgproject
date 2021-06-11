using AutoMapper;
using VG.Infra.Data.Entities;

namespace VG.Domain.Dto
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ModelDto, ModelEntity>().ReverseMap();
            CreateMap<TruckDto, TruckEntity>()
                .ReverseMap()
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.Name));
        }
    }
}
