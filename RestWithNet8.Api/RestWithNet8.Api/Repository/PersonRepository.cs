using RestWithNet8.Api.Model;
using RestWithNet8.Api.Model.Context;
using RestWithNet8.Api.Repository.Implementations;

namespace RestWithNet8.Api.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context): base(context)
        {
            
        }

        public Person Disable(long id)
        {
            if (!_context.Persons.Any(x=> x.Id == id)) 
                return null;

            var user = _context.Persons.SingleOrDefault(x => x.Id == id);
            if (user != null)
                user.Enabled = false;

            try
            {
                _context.Entry(user).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return user;
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(x =>
                                                          x.FirstName.Contains(firstName) &&
                                                          x.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstName))
            {
                return _context.Persons.Where(x => x.FirstName.Contains(firstName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(x => x.LastName.Contains(lastName)).ToList();
            }
            return null;
        }
    }
}
