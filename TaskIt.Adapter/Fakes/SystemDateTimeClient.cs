using System.Globalization;
using TaskIt.Application.Driven_Ports;

namespace TaskIt.Adapter.Fake.Fakes
{
    public class SystemDateTimeClient : ISystemDateTimeClient
    {
        private readonly DateTime currentDateTime;

        public SystemDateTimeClient(string DateTimeString)
        {
           currentDateTime = DateTime.ParseExact(DateTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        }

        public DateTime GetCurrentDateTimeUTC()
        {
            return currentDateTime.ToUniversalTime();
        }
    }
}
