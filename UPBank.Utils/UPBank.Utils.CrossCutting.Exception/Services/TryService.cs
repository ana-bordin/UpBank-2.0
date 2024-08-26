using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Utils.CrossCutting.Exception.Services
{
    public class TryService
    {
        private readonly IDomainNotificationService _domainNotificationService;
        public TryService(IDomainNotificationService domainNotificationService)
        {
            _domainNotificationService = domainNotificationService;
        }

        public async Task<T?> ExecuteTryCatchAsync<T>(Func<Task<T>> func, string type)
        {
            try
            {
                return await func();
            }
            catch (System.Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao conectar com a API de" + type + ": " + e);

                return default;
            }
        }

    }
}