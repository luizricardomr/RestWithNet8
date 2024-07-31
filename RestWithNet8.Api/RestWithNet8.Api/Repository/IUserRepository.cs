using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Model;

namespace RestWithNet8.Api.Repository
{
    public interface IUserRepository
    {
        User? ValidateCredentials(UserVO user);
        User? ValidateCredentials(string userName);
        User RefreshUserInfo(User user);
        bool RevokeToken(string userName);
    }
}
