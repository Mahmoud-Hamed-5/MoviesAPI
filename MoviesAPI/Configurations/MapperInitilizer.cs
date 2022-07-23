using AutoMapper;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Configurations
{
    public class MapperInitilizer : Profile
    {

        public MapperInitilizer()
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Genre, GenreCreateDTO>().ReverseMap();
            CreateMap<Genre, GenreUpdateDTO>().ReverseMap();
        }
    }
}
