﻿using Application.Core.Entities;
using Application.Core.Interfaces.Specifications;
using System;
using System.Linq;

namespace Application.Core.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
        Task<IEnumerable<TEntity>> GetAllNoTrackingAsync(ISpecifications<TEntity, TKey> specifications);
        Task<TEntity> GetByIdNoTrackingAsync(ISpecifications<TEntity, TKey> specifications);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entity);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entity);
        Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
    }
}