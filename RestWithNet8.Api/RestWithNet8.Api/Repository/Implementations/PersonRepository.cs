using RestWithNet8.Api.Model;
using RestWithNet8.Api.Model.Context;
using System;

namespace RestWithNet8.Api.Repository.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        private MySQLContext _context;

        public PersonRepository(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<Person> FinAll()
        {
            return _context.Persons.ToList();
        }

        

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id == id);
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if(result != null) 
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return person;
        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }

    }
}
