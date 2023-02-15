using APIupd8.DataBase;
using APIupd8.Model;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIupd8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        // POST api/<ClienteController>
        [Route("Insert")]
        [HttpPost]
        public bool Insert(Cliente cliente)//[FromBody] string value)
        {
            try
            {
                using (var _context = new _Context())
                {
                    _context.Cliente.Add(cliente);
                    _context.SaveChanges();
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        // PUT api/<ClienteController>/5
        [Route("Update")]
        [HttpPost]
        public bool Update(Cliente cliente)
        {
            try
            {
                using (var _context = new _Context())
                {
                    _context.Cliente.Update(cliente);
                    _context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete]
        public bool Delete(Cliente cliente)
        {
            try
            {
                using (var _context = new _Context())
                {
                    _context.Cliente.Remove(cliente);
                    _context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
