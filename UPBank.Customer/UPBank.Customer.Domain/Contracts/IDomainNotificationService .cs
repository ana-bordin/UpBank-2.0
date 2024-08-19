namespace UPBank.Utils.CommonsFiles.Contracts.Services
{
    public interface IDomainNotificationService : IDisposable
    {
        bool HasNotification { get; }
        void Add(string message);
        void AddRange(IEnumerable<string> messages);
        IReadOnlyList<string> Get();
    }
}
