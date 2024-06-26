﻿using RestWithNet8.Api.Data.Converter.Implementations;
using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Model;
using RestWithNet8.Api.Repository;
using RestWithNet8.Api.Repository.Generic;
using System;

namespace RestWithNet8.Api.Business.Implementations
{
    public class BookBusiness: IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FinAll()
        {
            return _converter.Parse(_repository.FinAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);

        }

    }
}
