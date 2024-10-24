using AutoMapper;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer;
using UPBank.Customer.Domain.Entities;

namespace UPBank.Customer.Domain.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap <CustomerRequest, Person> ()
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => CustomerResponse.CpfAddMask(src.CPF)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
        }
    }
}