namespace UPBank.Address.Application.Contracts
{
    public interface IViaCepService
    {
        Task<Domain.Entities.Address> GetAddressInAPI(string zipCode);
    }
}
