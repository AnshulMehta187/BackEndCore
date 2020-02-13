using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(int id);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        IEnumerable<TEntity> Query();
        int Save();
    }
}
