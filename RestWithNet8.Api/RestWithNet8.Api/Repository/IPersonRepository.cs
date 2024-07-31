using RestWithNet8.Api.Model;
using RestWithNet8.Api.Repository.Generic;
using System.Reflection.Metadata;

namespace RestWithNet8.Api.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
        List<Person> FindByName(string firstName, string secondName);
    }
}
