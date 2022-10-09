using Projekt_ASP.Data;

namespace Projekt_ASP.Interfaces
{
    public interface IProductsRepository
    {
        bool Exists(Guid id);
        public Product Get(Guid id);
        public List<Product> GetAll();
        public Product Add(Product product);
        public void Update(Product product);
        public void Delete(Guid id);
    }
}
