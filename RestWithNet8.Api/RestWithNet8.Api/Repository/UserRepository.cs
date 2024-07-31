using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Model;
using RestWithNet8.Api.Model.Context;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace RestWithNet8.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User? ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, SHA256.Create());
            return _context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == pass);
        }

        public User? ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
                return false;

            user.RefreshToken = null;
            _context.SaveChanges();

            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(x => x.Id.Equals(user.Id)))
                return null;
            else
            {

                var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
                if (result != null)
                {
                    try
                    {
                        _context.Entry(result).CurrentValues.SetValues(user);
                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return result;
            }

        }

        private string ComputeHash(string input, HashAlgorithm hashAlgorithm)
        {
            byte[] hashedBytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var builder = new StringBuilder();

            foreach (var item in hashedBytes)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }


    }
}
