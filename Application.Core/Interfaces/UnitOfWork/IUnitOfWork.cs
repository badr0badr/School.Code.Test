﻿using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using System;
using System.Linq;

namespace Application.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}