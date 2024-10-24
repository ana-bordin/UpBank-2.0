using AutoMapper;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Address;
using UPBank.Customer.Domain.Services;

namespace UPBank.Customer.Domain.Profiles
{
    public class AddressResponseProfile : Profile
    {
        public AddressResponseProfile()
        {
            CreateMap<AddressResponseData, AddressResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complement))
                .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Neighborhood))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))  
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode));
        }
    }
}