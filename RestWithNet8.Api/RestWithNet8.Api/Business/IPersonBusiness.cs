using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Hipermedia.Utils;

namespace RestWithNet8.Api.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindByName(string firstName, string lastName);
        PersonVO Update(PersonVO person);
        void Delete(long id);
        List<PersonVO> FindAll();

        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int currentPage);

        PersonVO Disable(long id);
    }
}
