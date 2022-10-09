using System.ComponentModel.DataAnnotations;

namespace Projekt_ASP.Data
{
    public class Product
    {
        public Product()
        {

        }

        public Product(Guid id, string name, bool isAvailable,string email, string telefon)
        {
            Id = id;
            Name = name;
            IsAvailable = isAvailable;
            Email = email;
            Telefon = telefon;
        }

        public Guid Id { get; set; }
        
        
        
        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        
        public string Email { get; set; }
        
        
        public string Telefon { get; set; }
    }
}
