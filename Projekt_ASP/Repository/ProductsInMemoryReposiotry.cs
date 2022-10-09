using Projekt_ASP.Data;
using Projekt_ASP.Interfaces;

namespace Projekt_ASP.Repository
{
    public class ProductsInMemoryReposiotry : IProductsRepository
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product(Guid.NewGuid(), "test", true,"test@wp.pl","531234543"),
         new Product(Guid.NewGuid(), "test1", true,"test1@wp.pl","531234541"),
         new Product(Guid.NewGuid(), "test2", true,"test2@wp.pl","531234542"),
         new Product(Guid.NewGuid(), "test3", true,"test3@wp.pl","531234543"),
         new Product(Guid.NewGuid(), "test4", true,"test4@wp.pl","531234544"),
         new Product(Guid.NewGuid(), "test5", true,"test5@wp.pl","531234545"),
        };

        public ProductsInMemoryReposiotry()
        {

        }

        public Product Add(Product product)
        {
            var existsProduct = _products.SingleOrDefault(x => x.Id == product.Id);
            if (existsProduct != null)
                throw new Exception($"Product with id: {product.Id} is exosts!");
            else
            {
                _products.Add(product);
            }

            return product;
        }

        public void Delete(Guid id)
        {
            var del = _products.SingleOrDefault(x=>x.Id == id);
             _products.Remove(del);
        }

        public bool Exists(Guid id)
        {
            return _products.Any(x => x.Id.Equals(id));
        }

        public Product Get(Guid id)
        {
            
            var findObj = _products
               .FirstOrDefault(x => x.Id.Equals(id));
            if (findObj == null)
            {
                Console.WriteLine("Product not exists!");
                //throw new Exception("Product not exists!");
            }
            return findObj;
        }

        public List<Product> GetAll()
        {
            return _products;
        }

   

        public void Update(Product product)
        {
            var updateProduct = _products.SingleOrDefault(x=>x.Id == product.Id);
            if(updateProduct != null)
            {
                updateProduct.Name = product.Name;
                updateProduct.IsAvailable = product.IsAvailable;
            }
            else
            {
                Console.WriteLine($"Product with id: {product.Id} is not exists!");
                
            }
           
        }
    }
}
