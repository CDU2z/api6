using apiAuthores.DTOs;
using apiAuthores.Entidades;
using AutoMapper;

namespace apiAuthores.Utilidades
{
    public class AutoMapperProfiles  : Profile
    {

        public AutoMapperProfiles() {
            CreateMap<AutoresDTO, Autor>();
        
        
        } 
    }
}
