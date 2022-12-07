namespace Api.Services;

public interface IUserService
{
    string GetUsername();
    Guid GetUserId();
}