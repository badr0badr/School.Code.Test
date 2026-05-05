﻿using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Application.Core.Interfaces.Specifications;
using Application.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Application.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AppDbContext context;

        public GenericRepository(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<TEntity> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecifications(specifications).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecifications(specifications).ToListAsync();
        }
        public async Task<TEntity> GetByIdNoTrackingAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecifications(specifications).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllNoTrackingAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await ApplySpecifications(specifications).AsNoTracking().ToListAsync();
        }
        public async Task AddAsync(TEntity entity)
        {
            await context.AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await context.AddRangeAsync(entity);
        }
        public void Update(TEntity entity)
        {
            context.Update(entity);
        }
        public void UpdateRange(IEnumerable<TEntity> entity)
        {
            context.UpdateRange(entity);
        }
        public void Delete(TEntity entity)
        {
            context.Remove(entity);
        }
        public void DeleteRange(IEnumerable<TEntity> entity)
        {
            context.RemoveRange(entity);
        }
        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> specifications)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQuery(context.Set<TEntity>(), specifications);
        }
        public Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQueryCount(context.Set<TEntity>(), specifications);
        }
    }
}