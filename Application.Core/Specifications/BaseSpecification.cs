﻿using Application.Core.Entities;
using Application.Core.Interfaces.Specifications;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Core.Specifications
{
    public class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public BaseSpecification(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
        }
        public BaseSpecification()
        {

        }
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderByDescending { get; set; } = null;
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagingEnabled { get; set; }

        public void AddOrderBy(Expression<Func<TEntity, object>> Expression)
        {
            OrderBy = Expression;
        }
        public void AddOrderByDescending(Expression<Func<TEntity, object>> Expression)
        {
            OrderByDescending = Expression;
        }
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}