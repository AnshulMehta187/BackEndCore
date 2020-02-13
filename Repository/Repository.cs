using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected TestContext testContext = null;

        public Repository(TestContext TestContext)
        {
            testContext = TestContext;
        }
        public void Add(TEntity entity)
        {
             testContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            testContext.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return testContext.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetByCondition(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return testContext.Set<TEntity>().Where(expression).ToList();
        }

        public TEntity GetById(int id)
        {
            return testContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Query()
        {
            IQueryable<TEntity> query = testContext.Set<TEntity>();
            return query.ToList();
        }

        public void Remove(int id)
        {
            var entity = testContext.Set<TEntity>().Find(id);
            testContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            testContext.Set<TEntity>().RemoveRange(entities);
        }

        public int Save()
        {
            return testContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            testContext.Set<TEntity>().Update(entity);
        }
    }
}
