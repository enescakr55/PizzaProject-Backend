using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.CrudOperations.EntityFramework
{
    public class EfCrudBase<QEntity, QContext> : ICrudBase<QEntity> where QEntity : class, IEntity, new() where QContext : DbContext, new()
    {
        public void Add(QEntity entity)
        {
            using (QContext context = new QContext())
            {
                var entry = context.Entry(entity);
                entry.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(QEntity entity)
        {
            using (QContext context = new QContext())
            {
                var entry = context.Entry(entity);
                entry.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public QEntity Get(Expression<Func<QEntity, bool>> filter)
        {
            using (QContext context = new QContext())
            {
                return context.Set<QEntity>().Where(filter).SingleOrDefault();
            }
        }

        public List<QEntity> GetAll(Expression<Func<QEntity, bool>> filter = null)
        {
            using (QContext context = new QContext())
            {
                return filter == null ? context.Set<QEntity>().ToList() : context.Set<QEntity>().Where(filter).ToList();
            }
        }

        public void Update(QEntity entity)
        {
            using (QContext context = new QContext())
            {
                var entry = context.Entry(entity);
                entry.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
