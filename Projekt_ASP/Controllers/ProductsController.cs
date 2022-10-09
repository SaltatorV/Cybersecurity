using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_ASP.Data;
using Projekt_ASP.DTO;
using Projekt_ASP.Extensions;
using Projekt_ASP.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Projekt_ASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _products;

        public ProductsController(IProductsRepository products)
        {
            _products = products;
        }


        [HttpGet]
        public IActionResult GetAll()
        {


            return Ok(_products.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {


            return Ok(_products.Get(id));
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Post(Product product)
        {

            if (_products.Exists(product.Id))
                throw new ProductsAlreadyExistsException(product.Id);

            var produt = new Product(product.Id, product.Name, product.IsAvailable, product.Email, product.Telefon);
            return Ok(_products.Add(produt));

        }
        [HttpPut]
        public IActionResult Update(UpdateProductDTO product)
        {
            /*     if (!_products.Exists(product.Id))
                     return NotFound();
                 _products.Update(product);*/

            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (!_products.Exists(id))
                return NotFound();
            _products.Delete(id);
            return NoContent();
        }



    }
}
