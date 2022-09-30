namespace Airbnb.Application.Exceptions.AuthenticationExceptions.TokenExceptions
{
    public interface IAuthTokenException:IServiceException
    {
        public string DetailErrorMessage { get; }
    }
}
