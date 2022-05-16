using La_mia_pizzeria.Data;
using La_mia_pizzeria.Models;
using La_mia_pizzeria.Utils;
using Microsoft.AspNetCore.Mvc;

namespace La_mia_pizzeria.Controllers
{
    
    public class PizzaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> pizze = new List<Pizza>();

            using (BlogContext db = new BlogContext())
            {
                pizze = db.Pizze.ToList<Pizza>();
            }
            
            //List<Pizza> pizze = PostData.GetPizze();
            
            return View("HomePage", pizze);
        }

        [HttpGet]
        public IActionResult Details (int id)
        {

            using (BlogContext db = new BlogContext())
            {
                try
                {
                    Pizza pizzaFound = db.Pizze
                         .Where(pPizza => pPizza.Id == id)
                         .First();

                    return View("Details", pizzaFound);

                }
                catch (InvalidOperationException ex)
                {
                    return NotFound("Il post con id " + id + " non è stato trovato");
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }


            }


            //    Pizza pizzaFound = GetPizzaById(id);

            //    foreach(Pizza pizza in PostData.GetPizze())
            //    {
            //        if (pizza.Id == id)
            //        {
            //            pizzaFound = pizza;
            //            break;

            //        }
            //    }

            // if (pizzaFound != null)
            //    {
            //    return View("Details", pizzaFound);

            //    } else
            //    {
            //        return NotFound("Il post con id " + id + "non è stato trovato");
            //    }

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("FormPizza");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza nuovaPizza)
        {
            if(!ModelState.IsValid)
            {
                return View("FormPizza" , nuovaPizza);
            }

            using (BlogContext db = new BlogContext())
            {
                Pizza pizzaToCreate = new Pizza(nuovaPizza.Name, nuovaPizza.Description, nuovaPizza.Image);

                db.Pizze.Add(pizzaToCreate);
                db.SaveChanges();
            }

            //Pizza pizzaConId = new Pizza(PostData.GetPizze().Count, nuovaPizza.Name , nuovaPizza.Description , nuovaPizza.Image);

            //Il mio modello è corretto
            //PostData.GetPizze().Add(pizzaConId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizza pizzaToEdit = null;

            using (BlogContext db = new BlogContext())
            {
                pizzaToEdit = db.Pizze
                         .Where(pPizza => pPizza.Id == id)
                         .First();
            }

            if (pizzaToEdit == null)
            {
                return NotFound();
            }
            else
            {
                return View("Update", pizzaToEdit);
            }
        }

        [HttpPost]
        public IActionResult Update(int id, Pizza modello)
        {
            if (!ModelState.IsValid)
            {
                return View("FormPizza", modello);
            }

            Pizza pizzaToEdit = null;

            using (BlogContext db = new BlogContext())
            {
                pizzaToEdit = db.Pizze
                         .Where(pPizza => pPizza.Id == id)
                         .First();

                db.SaveChanges();

            if (pizzaToEdit != null)
            {
                pizzaToEdit.Name = modello.Name;
                pizzaToEdit.Description = modello.Description;
                pizzaToEdit.Image = modello.Image;

                

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }

            }

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            using (BlogContext db = new BlogContext())
            {
                Pizza pizzaToDelete = db.Pizze
                         .Where(pPizza => pPizza.Id == id)
                         .First();

                if(pizzaToDelete != null)
                {
                    db.Pizze.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }

        }

            

    }
        
        
}
    
