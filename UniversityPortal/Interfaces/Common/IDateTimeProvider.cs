namespace UniversityPortal.Interfaces.Common
{
    public interface IDateTimeProvider
    {
        DateTimeOffset DateTimeOffsetNow { get; }
    }
}
