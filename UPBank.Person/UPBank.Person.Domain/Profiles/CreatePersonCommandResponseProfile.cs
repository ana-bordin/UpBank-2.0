using AutoMapper;
using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Person.Domain.Profiles
{
    public class CreatePersonCommandResponseProfile : Profile
    {
        public CreatePersonCommandResponseProfile()
        {
            CreateMap<Entities.Person, CreatePersonCommandResponse>()
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => CreatePersonCommandResponse.CpfAddMask(src.CPF)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
        }
    }
}