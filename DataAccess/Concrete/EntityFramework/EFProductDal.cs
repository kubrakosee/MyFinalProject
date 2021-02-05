using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDisposable pattern implementation of c#
            //burda bellekte işi bitince atılacak
            using (NorthwindContext context=new NorthwindContext())
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

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
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
        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);

            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                return filter == null
                    ? context.Set<Product>().ToList()
                    :context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                 context.SaveChanges();
            }
        }
    }
}
