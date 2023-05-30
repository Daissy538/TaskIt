using TaskIt.Application.Driven_Ports;

namespace TaskIt.Adapter.System
{
    public class SystemDateTimeClient : ISystemDateTimeClient
    {
        public DateTime GetCurrentDateTimeUTC()
        {
            return DateTime.UtcNow;
        }
    }
}
