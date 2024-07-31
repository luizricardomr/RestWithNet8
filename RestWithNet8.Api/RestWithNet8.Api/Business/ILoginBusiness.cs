using RestWithNet8.Api.Data.VO;

namespace RestWithNet8.Api.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO token);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
    }
}
