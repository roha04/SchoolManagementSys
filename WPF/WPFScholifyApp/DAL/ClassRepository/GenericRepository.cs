﻿// <copyright file="GenericRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.DAL.ClassRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private SchoolContext? context = null;
        private DbSet<T>? table = null;

        public GenericRepository()
        {
            this.context = new SchoolContext();
            this.table = this.context.Set<T>();
        }

        public GenericRepository(SchoolContext context)
        {
            this.context = context;
            this.table = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.table.ToList();
        }

        public virtual IQueryable<T> GetAllq()
        {
            return this.table!;
        }

        public T GetById(object id)
        {
            return this.table?.Find(id) ?? throw new InvalidOperationException("Entity not found");
        }

        public virtual void Insert(T obj)
        {
            this.table?.Add(obj);
        }

        public virtual void Update(T obj)
        {
            this.table?.Attach(obj);
            if (this.context != null)
            {
                this.context.Entry(obj).State = EntityState.Modified;
            }
        }

        public virtual void Delete(object id)
        {
            T? existing = this.table?.Find(id);
            this.table?.Remove(existing);
        }

        public virtual void Save()
        {
            this.context?.SaveChanges();
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            if (this.table == null)
            {
                throw new InvalidOperationException("Table not initialized");
            }

            IQueryable<T> query = this.table;

            foreach (var include in includes)
            {
                if (include.Body is MemberExpression memberExpression && memberExpression.Type.IsClass)
                {
                    query = memberExpression.Type.IsNullableReferenceType()
                        ? query.Include(include)
                        : query;
                }
            }

            return query;
        }
    }
}
