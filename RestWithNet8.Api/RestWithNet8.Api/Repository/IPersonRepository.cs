using RestWithNet8.Api.Model;

namespace RestWithNet8.Api.Repository
{
    public interface IPersonRepository
    {
        Person Create(Person person);
        Person FindById(long id);
        Person Update(Person person);
        void Delete(long id);
        List<Person> FinAll();
        bool Exists(long id);
    }
}
