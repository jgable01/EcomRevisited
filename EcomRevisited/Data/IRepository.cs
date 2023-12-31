﻿using EcomRevisited.Models;
using System.Linq.Expressions;

namespace EcomRevisited.Data
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdWithIncludesAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);



    }
}
