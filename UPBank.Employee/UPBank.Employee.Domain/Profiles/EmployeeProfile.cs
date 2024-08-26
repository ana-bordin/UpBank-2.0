using AutoMapper;
using UPBank.Employee.Domain.Commands.CreateEmployee;

namespace UPBank.Employee.Domain.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeCommand, Entities.Employee>()
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