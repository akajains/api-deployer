using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataHub.Data
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;

            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> All()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public IEnumerable<TEntity> AllInclude
        (params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public IEnumerable<TEntity> FindByInclude
          (Expression<Func<TEntity, bool>> predicate,
          params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<TEntity> results = query.Where(predicate).ToList();
            return results;
        }

        private IQueryable<TEntity> GetAllIncluding
        (params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _dbSet.AsNoTracking();

            return includeProperties.Aggregate
              (queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {

            IEnumerable<TEntity> results = _dbSet.AsNoTracking()
              .Where(predicate).ToList();
            return results;
        }

        public TEntity FindByKey(int id)
        {
            Expression<Func<TEntity, bool>> lambda = Utilities.BuildLambdaForFindByKey<TEntity>(id);
            return _dbSet.AsNoTracking().SingleOrDefault(lambda);
        }

        public TEntity FindByKeyInclude(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            Expression<Func<TEntity, bool>> lambda = Utilities.BuildLambdaForFindByKey<TEntity>(id);
            return query.SingleOrDefault(lambda);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = FindByKey(id);
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}