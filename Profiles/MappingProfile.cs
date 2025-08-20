using AutoMapper;
using patient_manager.Models;

namespace patient_manager.Profiles;

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