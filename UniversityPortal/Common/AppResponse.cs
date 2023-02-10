namespace UniversityPortal.Common
{
    public class AppResponse
    {
        public AppResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
    }
}
