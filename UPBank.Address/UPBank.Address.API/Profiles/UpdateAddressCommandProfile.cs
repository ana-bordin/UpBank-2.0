using AutoMapper;
using UPBank.Address.API.Models;
using UPBank.Address.Domain.Commands.UpdateAddress;

namespace UPBank.Address.API.Profiles
{
    public class UpdateAddressCommandProfile : Profile
    {
        public UpdateAddressCommandProfile()
        {
            CreateMap<InputAddressModel, UpdateAddressCommand>()
                      .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                      .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                      .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complement));
        }
    }
}