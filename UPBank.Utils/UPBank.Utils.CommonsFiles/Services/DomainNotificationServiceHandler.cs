using System.Collections.Immutable;
using UPBank.Utils.CommonsFiles.Contracts.Services;

namespace UPBank.Utils.CommonsFiles.Services
{
    public class DomainNotificationServiceHandler : IDomainNotificationService
    {
        private readonly List<string> _notifications = new List<string>();
        public bool HasNotification => _notifications.Count > 0;

        public bool _disposed;

        public void Add(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException(message);

            _notifications.Add(message);
        }

        public void AddRange(IEnumerable<string> messages)
        {
            _notifications.AddRange(messages);
        }

        public void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
                _notifications.Clear();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IReadOnlyList<string> Get()
        {
            return _notifications.ToImmutableList();
        }
    }
}