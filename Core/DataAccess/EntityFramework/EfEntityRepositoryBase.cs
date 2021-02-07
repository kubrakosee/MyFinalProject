using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>:IEntityRepository<TEntity>
         where TEntity : class, IEntity, new()
         where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            //burda bellekte işi bitince atılacak
            using (TContext context = new TContext())
            {
                //tamam şimdi ben veri kaynağımla ilişkilendirdim bunuda ama ben bunu napayım
                //state demek durum demek
                //referanssı yakala

                var addedEntity = context.Entry(entity);

                //eklenecek bir nesne
                addedEntity.State = EntityState.Added;

                //ve şimdi ekle
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                //tamam şimdi ben veri kaynağımla ilişkilendirdim bunuda ama ben bunu napayım
                //state demek durum demek
                //referanssı yakala

                var deletedEntity = context.Entry(entity);

                //eklenecek bir nesneyi sil
                deletedEntity.State = EntityState.Deleted;

                //ve şimdi ekle
                context.SaveChanges();
            }
        }

        //tek data getirecekti
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);

            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
