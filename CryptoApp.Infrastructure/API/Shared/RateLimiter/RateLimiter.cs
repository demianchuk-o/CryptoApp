namespace CryptoApp.Infrastructure.API.Shared.RateLimiter;

public class RateLimiter
{
    private readonly TimeSpan _cooldownPeriod;
    private DateTime _lastRequestTime = DateTime.MinValue;
    private readonly object _lock = new();

    public RateLimiter(TimeSpan cooldownPeriod)
    {
        _cooldownPeriod = cooldownPeriod;
    }

    public bool CanProcess()
    {
        lock (_lock)
        {
            var now = DateTime.UtcNow;
            if (now - _lastRequestTime < _cooldownPeriod)
            {
                return false;
            }
                
            _lastRequestTime = now;
            return true;
        }
    }
}