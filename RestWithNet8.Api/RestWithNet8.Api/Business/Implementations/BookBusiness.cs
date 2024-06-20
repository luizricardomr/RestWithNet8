using RestWithNet8.Api.Model;
using RestWithNet8.Api.Repository;

namespace RestWithNet8.Api.Business.Implementations
{
    public class BookBusiness: IBookBusiness
    {
        private readonly IBookRepository _repository;

        public BookBusiness(IBookRepository repository)
        {
            _repository = repository;
        }

        public List<Book> FinAll()
        {
            return _repository.FinAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);

        }

    }
}
