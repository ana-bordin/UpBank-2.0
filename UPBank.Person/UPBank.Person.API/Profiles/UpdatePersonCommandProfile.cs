using AutoMapper;
using UPBank.Person.API.Models;
using UPBank.Person.Domain.Commands.UpdatePerson;

namespace UPBank.Person.API.Profiles
{
    public class UpdatePersonCommandProfile : Profile
    {
        public UpdatePersonCommandProfile()
        {
            CreateMap<InputPatchPersonModel, UpdatePersonCommand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<string, UpdatePersonCommand>()
                .ForPath(dest => dest.CPF, opt => opt.MapFrom(src => src));
        }
    }
}