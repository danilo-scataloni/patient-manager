using AutoMapper;
using pacient_manager.DTOs;
using pacient_manager.Models;

namespace pacient_manager.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Pacient and PacientDTO
        CreateMap<Pacient, PacientDto>();
        CreateMap<PacientDto, Pacient>().ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}