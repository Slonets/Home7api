using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappers
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CreateUnitDto, Unit>();                 

            CreateMap<Unit, CreateUnitDto>()
                 .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Unit, UnitDto>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<UnitDto, Unit>();              

            CreateMap<ImagesUnitDto, ImagesUnit>().ReverseMap();
        }
    }
}
