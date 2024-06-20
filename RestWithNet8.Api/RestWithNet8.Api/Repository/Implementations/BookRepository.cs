﻿using RestWithNet8.Api.Model.Context;
using RestWithNet8.Api.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace RestWithNet8.Api.Repository.Implementations
{
    public class BookRepository: IBookRepository
    {
        private MySQLContext _context;

        public BookRepository(MySQLContext context)
        {
            _context = context;
        }

        public Book Create(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return book;
        }

        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<Book> FinAll()
        {
            return _context.Books.ToList();
        }



        public Book FindById(long id)
        {
            return _context.Books.SingleOrDefault(p => p.Id == id);
        }

        public Book Update(Book book)
        {
            if (!Exists(book.Id)) return null;

            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(book.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return book;
        }

        public bool Exists(long id)
        {
            return _context.Books.Any(p => p.Id.Equals(id));
        }

    }
}