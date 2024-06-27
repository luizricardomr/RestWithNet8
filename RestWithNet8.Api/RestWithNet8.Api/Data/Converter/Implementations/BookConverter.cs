using RestWithNet8.Api.Data.Converter.Contract;
using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Model;

namespace RestWithNet8.Api.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null) return null;

            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                LauchDate = origin.LauchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }        

        public BookVO Parse(Book origin)
        {
            if (origin == null) return null;

            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                LauchDate = origin.LauchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }
        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(x => Parse(x)).ToList();
        }

        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(x => Parse(x)).ToList();
        }
    }
}
