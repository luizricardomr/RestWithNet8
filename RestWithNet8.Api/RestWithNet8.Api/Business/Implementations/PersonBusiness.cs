using Microsoft.AspNetCore.Mvc.RazorPages;
using RestWithNet8.Api.Data.Converter.Implementations;
using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Hipermedia.Utils;
using RestWithNet8.Api.Model;
using RestWithNet8.Api.Repository;
using RestWithNet8.Api.Repository.Generic;

namespace RestWithNet8.Api.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {            
            var sort = !string.IsNullOrEmpty(sortDirection) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1=1";            

            if (!string.IsNullOrWhiteSpace(name))             
                query +=  $" and p.first_name like '%{name}%'";

            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            var countQuery = @"select count(*) from person p where 1=1";
            if (!string.IsNullOrWhiteSpace(name))
                countQuery += $" and p.first_name like '%{name}%'";

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);
            return new PagedSearchVO<PersonVO> 
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirection = sort,
                TotalResults = totalResults,
            };
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }
        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }


    }
}
