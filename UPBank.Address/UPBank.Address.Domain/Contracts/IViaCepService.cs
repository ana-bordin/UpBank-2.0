namespace UPBank.Address.Domain.Contracts
{
    public interface IViaCepService
    {
        Task<Entities.Address> GetAddressByZipCode(string zipCode);
    }
}