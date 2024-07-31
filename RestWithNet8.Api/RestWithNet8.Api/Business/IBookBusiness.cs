using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Model;

namespace RestWithNet8.Api.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO person);
        BookVO FindById(long id);
        BookVO Update(BookVO person);
        void Delete(long id);
        List<BookVO> FindAll();
    }
}
