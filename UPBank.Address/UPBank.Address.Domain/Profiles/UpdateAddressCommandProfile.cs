using AutoMapper;
using UPBank.Address.Domain.Commands.UpdateAddress;
using UPBank.Address.Domain.Entities;

namespace UPBank.Address.Domain.Profiles
{
    public class UpdateAddressCommandProfile : Profile
    {
        public UpdateAddressCommandProfile()
        {
            CreateMap<UpdateAddressCommand, CompleteAddress>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complement));
        }
    }
}