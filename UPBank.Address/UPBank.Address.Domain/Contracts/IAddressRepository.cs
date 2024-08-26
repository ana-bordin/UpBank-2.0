namespace UPBank.Address.Domain.Contracts
{
    public interface IAddressRepository
    {
        public Task<Entities.Address?> GetOneAsync(string zipCode);
        public Task<Entities.Address?> AddAsync(Entities.Address address);
    }
}