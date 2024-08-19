using AutoMapper;
using UPBank.Address.API.Models;
using UPBank.Address.Domain.Commands.CreateAddress;

namespace UPBank.Address.API.Profiles
{
    public class OutputAddressModelProfile : Profile
    {
        public OutputAddressModelProfile()
        {
            CreateMap<CreateAddressCommandResponse, OutputAddressModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complement))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Neighborhood));
        }
    }
}