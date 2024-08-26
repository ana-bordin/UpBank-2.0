using AutoMapper;
using UPBank.Employee.Domain.Commands.CreateEmployee;
using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Employee.Domain.Profiles
{
    public class CreateEmployeeResponseCommandProfile : Profile
    {
        public CreateEmployeeResponseCommandProfile()
        {
            CreateMap<CreatePersonCommandResponse, CreateEmployeeCommandResponse>()
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<Entities.Employee, CreateEmployeeCommandResponse>()
                .ForMember(dest => dest.Manager, opt => opt.MapFrom(src => src.Manager))
                .ForMember(dest => dest.RecordNumber, opt => opt.MapFrom(src => src.RecordNumber))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));
        }
    }
}