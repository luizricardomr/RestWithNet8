using RestWithNet8.Api.Model;

namespace RestWithNet8.Api.Repository
{
    public interface IBookRepository
    {
        Book Create(Book person);
        Book FindById(long id);
        Book Update(Book person);
        void Delete(long id);
        List<Book> FinAll();
        bool Exists(long id);
    }
}
