using JadKaddor_ASP_task1.Functions;
using JadKaddor_ASP_task1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace JadKaddor_ASP_task1.Controllers
{
    public class UserController : ApiController
    {
        static List<Userauth> users = new List<Userauth> { };

        // GET: api/User
        public IHttpActionResult Get([FromUri] string name = "")
        {
            List<Userauth> returnUser = users.FindAll(s => s.firstName.Contains(name));
            if (returnUser.Count == 0)
            {
                return Content(HttpStatusCode.NotFound, "No User with this name was found");
            }
            return Content(HttpStatusCode.OK, returnUser);
        }

        // GET: api/User/5
        public IHttpActionResult Get(int id)
        {
            Userauth userauth = users.Find(s => s.Id == id);
            if (userauth == null)
            {
                return Content(HttpStatusCode.NotFound, "User not found");
            }
            userauth.Tkn = "";
            return Content(HttpStatusCode.OK, userauth);
        }

        // POST: api/User
        [APIauth]
        public IHttpActionResult Post(Userauth userauth)
        {

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "All fields are required!!");
            }
            Userauth ids = users.Find(p => p.Id == userauth.Id);

            if (ids != null)
            {
                return Content(HttpStatusCode.BadRequest, "ID already exists");
            }
            int id = 0;
            if (users.Count > 0)
            {
                users.ForEach(s =>
                {
                    if (s.Id >= 0)
                    {
                        id = s.Id + 1;
                    }
                });
            }
            userauth.Id = 0;
            Gtokens gtokens = new Gtokens();
            string token = gtokens.generateJwtToken(userauth.Id.ToString(), userauth.Email);
            userauth.Tkn = token;
            users.Add(userauth);
            return Content(HttpStatusCode.Created, "User was created successfully!");
        }

        // PUT: api/User/5
        [APIauth]
        public IHttpActionResult Put(int id, [FromBody] Userauth userauth)
        {

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "All fields are required");
            }
            bool found = false;
            Userauth updatedItem = new Userauth();
            users.ForEach(s => {
                if (s.Id == id)
                {
                    s.firstName = userauth.firstName;
                    s.lastName = userauth.lastName;
                    s.Email = userauth.Email;
                    s.Tkn = userauth.Tkn;
                    s.phoneNumber = userauth.phoneNumber;
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

        // DELETE: api/User/5
        [APIauth]
        public IHttpActionResult Delete(int id)
        {
            Userauth prod = users.Find(s => s.Id == id);
            if (prod == null)
            {
                return Content(HttpStatusCode.NotFound, "ID does not exist!");
            }
            users.Remove(prod);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }


}
