using JadKaddor_ASP_task1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace JadKaddor_ASP_task1.Controllers
{
    public class OutletController : ApiController
    {

        static List<Product> products = new List<Product> {};

        // GET: api/Outlet
        public IHttpActionResult Get([FromUri] string name = "")
        {
            List<Product> returnProduct = products.FindAll(s => s.Name.Contains(name));
            if (returnProduct.Count == 0)
            {
                return Content(HttpStatusCode.NotFound, "No product with this name was found");
            }
            return Content(HttpStatusCode.OK, returnProduct);
        }

        // GET: api/Outlet/5
        public IHttpActionResult Get(int id)
        {
            Product product = products.Find(s => s.Id == id);
            if (product == null)
            {
                return Content(HttpStatusCode.NotFound, "Product not found");
            }
            return Content(HttpStatusCode.OK, product);
        }

        // POST: api/Outlet
        public IHttpActionResult Post(Product product)
        {

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "All fields are required!!");
            }
            Product ids = products.Find(p => p.Id == product.Id);
            
            if (ids != null)
            {
                return Content(HttpStatusCode.BadRequest, "ID already exists");
            }

            products.Add(product);
            return Content(HttpStatusCode.Created, "Product was created successfully!");
        }

        // PUT: api/Outlet/5
        public IHttpActionResult Put(int id, [FromBody] Product product)
        {

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "All fields are required");
            }
            bool found = false;
            Product updatedItem = new Product();
            products.ForEach(s => {
                if (s.Id == id)
                {
                    s.Name = product.Name;
                    s.Price = product.Price;
                    s.Quantity = product.Quantity;
                    updatedItem = s;
                    found = true;
                }
            });
            if (!found)
            {
                return Content(HttpStatusCode.NotFound, "ID does not exist");
            }
            return Content(HttpStatusCode.OK, updatedItem);
        }

        // DELETE: api/Outlet/5
        public IHttpActionResult Delete(int id)
        {
            Product prod = products.Find(s => s.Id == id);
            if (prod == null)
            {
                return Content(HttpStatusCode.NotFound, "ID does not exist!");
            }
            products.Remove(prod);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
