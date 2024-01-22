﻿using System.Linq.Expressions;
using Core.Persistence.EntityBaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.Repositories;

public class EFRepositoryBase<TContext, TEntity, TId> : IEntityRepository<TEntity, TId>, IQuery<TEntity>
    where TEntity : Entity<TId>
    where TContext : DbContext
{
    protected TContext Context { get; set; }
    public EFRepositoryBase(TContext context)
    {
        Context = context;
    }
    public void Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
        Context.SaveChanges();
    }
    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        Context.SaveChanges();
    }
    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> queryable = Query();
        if (predicate is not null)
            queryable = queryable.Where(predicate);
        if (include is not null)
            queryable = include(queryable);

        return queryable.ToList();
    }
    public TEntity? GetByFilter(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> queryable = Query();
        queryable = queryable.Where(predicate);

        if (include is not null)
            queryable = include(queryable);

        return queryable.FirstOrDefault();
    }
    public TEntity? GetById(TId id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> queryable = Query();
        if (include is not null)
            queryable = include(queryable);

        return queryable.SingleOrDefault(x => x.Id.Equals(id));
    }
    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        Context.SaveChanges();
    }
    public IQueryable<TEntity> Query()
    {
        return Context.Set<TEntity>();
    }
}
