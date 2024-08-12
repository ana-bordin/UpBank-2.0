using AutoMapper;
using UPBank.Address.Domain.Commands.CreateAddress;

namespace UPBank.Person.Domain.Commands.CreatePerson
{
    public class CreatePersonCommandProfile : Profile
    {
        public CreatePersonCommandProfile()
        {
            CreateMap<Entities.Person, CreatePersonCommandResponse>()
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));

            //CreateMap<CreateAddressCommandResponse, CreatePersonCommandResponse>()
            //    .ForMember(dest => dest.Address.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
            //    .ForMember(dest => dest.Address.City, opt => opt.MapFrom(src => src.City))
            //    .ForMember(dest => dest.Address.State, opt => opt.MapFrom(src => src.State))
            //    .ForMember(dest => dest.Address.Neighborhood, opt => opt.MapFrom(src => src.Neighborhood))
            //    .ForMember(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street));
        }

    }

    public class CreatePerson : Profile
    {
        public CreatePerson()
        {
            CreateMap<CreatePersonCommand, Entities.Person>()
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
        }
    }
}
