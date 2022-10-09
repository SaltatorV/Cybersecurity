namespace Projekt_ASP.Extensions
{
    public class ProductsAlreadyExistsException : Exception
    {
        public ProductsAlreadyExistsException(Guid id ) : base($"Product with id {id} already exists.")
        {
            id = id;
        }
        public Guid Id { get; }
    }
}
