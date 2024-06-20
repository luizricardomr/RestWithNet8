using RestWithNet8.Api.Model;
using RestWithNet8.Api.Model.Context;
using RestWithNet8.Api.Repository;
using System;

namespace RestWithNet8.Api.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
        }

        public List<Person> FinAll()
        {
            return _repository.FinAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
            
        }   

    }
}
