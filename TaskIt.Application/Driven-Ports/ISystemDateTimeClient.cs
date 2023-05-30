namespace TaskIt.Application.Driven_Ports
{
    public interface ISystemDateTimeClient
    {
        public DateTime GetCurrentDateTimeUTC();
    }
}
