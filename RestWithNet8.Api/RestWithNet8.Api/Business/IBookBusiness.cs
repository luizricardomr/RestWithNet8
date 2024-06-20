using RestWithNet8.Api.Model;

namespace RestWithNet8.Api.Business
{
    public interface IBookBusiness
    {
        Book Create(Book person);
        Book FindById(long id);
        Book Update(Book person);
        void Delete(long id);
        List<Book> FinAll();
    }
}
