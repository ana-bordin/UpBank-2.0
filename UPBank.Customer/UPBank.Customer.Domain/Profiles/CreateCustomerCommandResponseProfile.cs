using AutoMapper;
using UPBank.Customer.Domain.Commands.CreateCustomer;
using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Customer.Domain.Profiles
{
    public class CreateCustomerCommandResponseProfile : Profile
    {
        public CreateCustomerCommandResponseProfile()
        {
            CreateMap<CreatePersonCommandResponse, CreateCustomerCommandResponse>()
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));

            CreateMap<Entities.Customer, CreateCustomerCommandResponse>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.Restriction, opt => opt.MapFrom(src => src.Restriction));   
        }       
    }
}