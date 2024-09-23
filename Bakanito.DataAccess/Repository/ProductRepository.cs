using Bakanito.DataAccess.Data;
using Bakanito.DataAccess.Repository.IRepository;
using Bakanito.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bakanito.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository 
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var productFromDb = _db.Products.FirstOrDefault(x=> x.Id == product.Id);
            if (productFromDb != null)
            {
                productFromDb.Title = product.Title;
                productFromDb.ISBN = product.ISBN;
                productFromDb.Price = product.Price;
                productFromDb.Description = product.Description;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Price50 = product.Price50;
                productFromDb.Price100 = product.Price100;
                productFromDb.Author = product.Author;
                if (productFromDb.ImageUrl != null)
                {
                    productFromDb.ImageUrl = product.ImageUrl;
                }
            }
            //_db.Products.Update(product);
        }
    }
}