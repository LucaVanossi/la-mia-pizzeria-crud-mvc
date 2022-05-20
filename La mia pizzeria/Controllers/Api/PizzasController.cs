using La_mia_pizzeria.Data;
using La_mia_pizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace La_mia_pizzeria.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<Pizza> pizze = new List<Pizza>();

            using (BlogContext db = new BlogContext())
            {
                pizze = db.Pizze.ToList<Pizza>();
            }

            return Ok(pizze);
        }
        
        
    }
}
