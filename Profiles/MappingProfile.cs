using AutoMapper;
using pacient_manager.DTOs;
using patient_manager.Models;

namespace pacient_manager.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Patient and PacientDTO
        CreateMap<Patient, PatientDto>();
        CreateMap<PatientDto, Patient>().
            ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}