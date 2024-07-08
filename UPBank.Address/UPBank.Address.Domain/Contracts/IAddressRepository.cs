namespace UPBank.Address.Infra.Repositories
{
    public interface IAddressRepository
    {
        Task<bool> CreateAddress(Domain.Entities.Address address);
        Task<Guid> CreateCompleteAddress(Domain.Entities.CompleteAddress addressInputModel);
        Task<Domain.Entities.CompleteAddress> GetCompleteAddressById(Guid id);
        Task<Domain.Entities.Address> GetAddressByZipCode(string zipCode);
        Task<Domain.Entities.CompleteAddress> UpdateAddress(Guid id, Domain.Entities.CompleteAddress addressDTO);
        Task<bool> DeleteAddressById(Guid id);
    }
}
