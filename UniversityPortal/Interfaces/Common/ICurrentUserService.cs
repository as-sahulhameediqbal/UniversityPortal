namespace UniversityPortal.Interfaces.Common
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Name { get; }
        string Email { get; }
        string Role { get; }
    }
}
