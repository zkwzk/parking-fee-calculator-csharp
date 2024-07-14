public class CalculateDaysResult
{
    public TimeOnly DayStartTime { get; private set; }
    public TimeOnly DayEndTime { get; private set; }
    public bool IsWeekend { get; private set; }

    public CalculateDaysResult(TimeOnly dayStartTime, TimeOnly dayEndTime, bool isWeekend)
    {
        this.DayStartTime = dayStartTime;
        this.DayEndTime = dayEndTime;
        this.IsWeekend = isWeekend;
    }
}