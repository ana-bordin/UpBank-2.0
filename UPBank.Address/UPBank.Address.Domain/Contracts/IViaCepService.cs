namespace UPBank.Address.Domain.Contracts
{
    public interface IViaCepService
    {
        Task<Entities.Address> GetAddressInAPI(string zipCode);
    }
}
