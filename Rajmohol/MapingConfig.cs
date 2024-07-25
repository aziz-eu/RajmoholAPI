using AutoMapper;
using Rajmohol.Models;
using Rajmohol.Models.DTOs.Villa;
using Rajmohol.Models.DTOs.VillaNumber;

namespace Rajmohol
{
    public class MapingConfig : Profile
    {
        public MapingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}
