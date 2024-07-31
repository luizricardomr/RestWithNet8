using Microsoft.EntityFrameworkCore;
using RestWithNet8.Api.Model.Base;
using RestWithNet8.Api.Model.Context;
using RestWithNet8.Api.Repository.Generic;

namespace RestWithNet8.Api.Repository.Implementations
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MySQLContext _context;
        private DbSet<T> dataset;
        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return item;
        }

        public void Delete(long id)
        {
            try
            {
                var result = dataset.SingleOrDefault(p => p.Id.Equals(id));

                if (result != null)
                {

                    dataset.Remove(result);
                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }



        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id == id);
        }

        public T Update(T item)
        {
            if (!Exists(item.Id)) return null;
            try
            {
                var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));
                if (result != null)
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return item;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool Exists(long id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return dataset.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = "";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
                return int.Parse(result);
            }
        }
    }
}
