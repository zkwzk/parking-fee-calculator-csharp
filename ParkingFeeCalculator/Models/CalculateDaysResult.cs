public class CalculateDaysResult
{
    public TimeOnly DayStartTime { get; set; }
    public TimeOnly DayEndTime { get; set; }
    public bool IsWeekend { get; set; }
}