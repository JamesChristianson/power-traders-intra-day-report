namespace PowerTradersIntraDayReport.Abstractions
{
    public interface ITimePeriodConverter
    {
        string ToTimePeriod(int period);
    }
}