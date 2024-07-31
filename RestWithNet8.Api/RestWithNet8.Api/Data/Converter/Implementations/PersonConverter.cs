using RestWithNet8.Api.Data.Converter.Contract;
using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Model;

namespace RestWithNet8.Api.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;

            return new Person
            {
                Id = origin.Id,
                Address = origin.Address,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender  ,
                Enabled = origin.Enabled 
            };
        }       

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;

            return new PersonVO
            {
                Id = origin.Id,
                Address = origin.Address,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender,
                Enabled = origin.Enabled
            };
        }
        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;

            return origin.Select(x=> Parse(x)).ToList();
        }
        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;

            return origin.Select(x => Parse(x)).ToList();
        }
    }
}
