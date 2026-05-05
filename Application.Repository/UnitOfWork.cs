﻿using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Application.Core.Interfaces.UnitOfWork;
using Application.Repository.Data.Contexts;
using Application.Repository.Repositories;
using System;
using System.Collections;
using System.Linq;

namespace Application.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private Hashtable _repository;
        public UnitOfWork(AppDbContext _context)
        {
            context = _context;
            _repository = new Hashtable();
        }
        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (!_repository.ContainsKey(typeof(TEntity).Name))
            {
                var repository = new GenericRepository<TEntity, TKey>(context);
                _repository.Add(typeof(TEntity).Name, repository);
            }
            return _repository[typeof(TEntity).Name] as IGenericRepository<TEntity, TKey>;
        }
    }
}