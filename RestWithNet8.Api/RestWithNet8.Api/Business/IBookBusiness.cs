using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Hipermedia.Utils;

namespace RestWithNet8.Api.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO person);
        BookVO FindById(long id);
        BookVO Update(BookVO person);
        void Delete(long id);
        List<BookVO> FindAll();
        PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int currentPage);
    }
}
