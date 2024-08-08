using AutoMapper;
using UPBank.Address.Domain.Entities;

namespace UPBank.Address.Domain.Commands.CreateAddress
{
    public class CreateAddressCommandProfile : Profile
    {
        public static string GetOnlyNumbers(string zipcode)
        {
            var stringToBeConverted = zipcode.Replace("-", "").Replace(".", "").Replace(" ", "");
            return stringToBeConverted;
        }
    }

    public class CreateCompleteAddressResponseCommandProfile : Profile
    {
        public CreateCompleteAddressResponseCommandProfile()
        {
            CreateMap<Entities.Address, CreateAddressCommandResponse>()
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Neighborhood))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street));

            CreateMap<CompleteAddress, CreateAddressCommandResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complement));
        }
    }

    public class CreateCompleteAddressCommandProfile : Profile
    {
        public CreateCompleteAddressCommandProfile()
        {
            CreateMap<CreateAddressCommand, CompleteAddress>()
                      .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                      .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                      .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complement))
                      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}