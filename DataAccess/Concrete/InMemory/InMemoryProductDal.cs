using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        List<Category> categories;
        public InMemoryProductDal()
        {
            //Oracle,Sql Server,Postgres,MongoDb
            _products = new List<Product> {
            new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
            new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
            new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
            new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
            new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1},
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //Product productToDelete=null;
            //Linq çıktığı için yorum satırı olana gerek yok 
            //foreach (var p in _products)
            //{
            //    if (product.ProductId==p.ProductId)
            //    {
            //        productToDelete = p;

            //    }
            //}

            //LINQ yazılan artık bu kadar kısa
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);//products ları tek tek dolaşmaya yarar  SingleOrDefault()
            //her p için git bak bakiyim benim gönderdiğimin productId sine eşit mi
            //ise git productToDelete eşitle SingleOrDefault yerine  FirstOrDefault,first de yapsan olur
            _products.Remove(productToDelete);

        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            //where içindeki şarta uyan bütün elemanları yeni bir liste haline getirir ve onu durdurur
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün id'sine sahip olan listedeki bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
