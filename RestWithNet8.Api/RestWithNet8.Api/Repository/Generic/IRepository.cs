﻿using RestWithNet8.Api.Model;
using RestWithNet8.Api.Model.Base;

namespace RestWithNet8.Api.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        T Update(T item);
        void Delete(long id);
        List<T> FinAll();
        bool Exists(long id);
    }
}
