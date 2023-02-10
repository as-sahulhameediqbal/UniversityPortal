namespace UniversityPortal.Common
{
    public static class AppResult
    {
        public static AppResponse msg(bool success, string message)
        {
            return new AppResponse(success, message);
        }
    }
}
