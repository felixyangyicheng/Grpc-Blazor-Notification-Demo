using AutoMapper;
using RealTime_D3.Dtos;
using RealTime_D3.Models;

namespace RealTime_D3.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {

            CreateMap<TbllogCreateDto, Tbllog>().ReverseMap();
            CreateMap<TbllogDto, Tbllog>().ReverseMap();
        }
    }
}
