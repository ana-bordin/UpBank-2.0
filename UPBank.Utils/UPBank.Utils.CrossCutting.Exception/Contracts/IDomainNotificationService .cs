namespace UPBank.Utils.CrossCutting.Exception.Contracts
{
    public interface IDomainNotificationService : IDisposable
    {
        bool HasNotification { get; }
        void Add(string message);
        void AddRange(IEnumerable<string> messages);
        IReadOnlyList<string> Get();
    }
}