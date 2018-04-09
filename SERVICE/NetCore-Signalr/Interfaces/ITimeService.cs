using System;

namespace SignalrCore.Interfaces
{
    public interface ITimeService
    {
        double DateTimeUtcToUnix(DateTime dateTime);

        DateTime UnixToDateTimeUtc(double unixTime);
    }
}