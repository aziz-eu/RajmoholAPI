using AutoMapper;
using Rajmohol.Models;
using Rajmohol.Models.DTOs.Villa;

namespace Rajmohol
{
    public class MapingConfig : Profile
    {
        public MapingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        }
    }
}
