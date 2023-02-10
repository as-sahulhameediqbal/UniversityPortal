using UniversityPortal.Interfaces.Common;

namespace UniversityPortal.Common
{
    public class DateTimeProvider: IDateTimeProvider
    {
        public DateTimeOffset DateTimeOffsetNow => DateTimeOffset.Now;
    }
}
