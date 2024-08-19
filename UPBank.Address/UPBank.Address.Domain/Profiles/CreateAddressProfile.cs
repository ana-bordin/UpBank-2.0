using AutoMapper;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Entities;

namespace UPBank.Address.Domain.Profiles
{
    public class CreateAddressProfile : Profile
    {
        public CreateAddressProfile()
        {
            CreateMap<CreateAddressCommand, CompleteAddress>()
                      .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                      .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                      .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complement))
                      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}