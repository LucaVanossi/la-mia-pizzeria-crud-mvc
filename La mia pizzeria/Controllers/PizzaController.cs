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
            List<Pizza> pizze = PostData.GetPizze();
            
            return View("HomePage", pizze);
        }

        [HttpGet]
        public IActionResult Details (int id)
        {
            Pizza pizzaFound = GetPizzaById(id);

            foreach(Pizza pizza in PostData.GetPizze())
            {
                if (pizza.Id == id)
                {
                    pizzaFound = pizza;
                    break;

                }
            }
        
         if (pizzaFound != null)
            {
            return View("Details", pizzaFound);

            } else
            {
                return NotFound("Il post con id " + id + "non è stato trovato");
            }

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

            Pizza pizzaConId = new Pizza(PostData.GetPizze().Count, nuovaPizza.Name , nuovaPizza.Description , nuovaPizza.Image);

            //Il mio modello è corretto
            PostData.GetPizze().Add(pizzaConId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizza pizzaToEdit = GetPizzaById(id);

            if(pizzaToEdit == null)
            {
                return NotFound();
            } else
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

            Pizza pizzaOriginal = GetPizzaById(id);

            if (pizzaOriginal != null)
            {
                pizzaOriginal.Name = modello.Name;
                pizzaOriginal.Description = modello.Description;
                pizzaOriginal.Image = modello.Image;

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int PizzaIndexToRemove = -1;

            List<Pizza> pizzaList = PostData.GetPizze();
            
            for(int i=0; i < pizzaList.Count; i++)
            {
                if (pizzaList[i].Id == id);
                {
                    PizzaIndexToRemove = i;
                }
            }

            if(PizzaIndexToRemove != -1)
            {
                PostData.GetPizze().RemoveAt(PizzaIndexToRemove);

                return RedirectToAction("Index");
            } else
            {
                return NotFound();
            }
            
        }
        
        
        private Pizza GetPizzaById(int id)
        {
            Pizza pizzaFound = null;

            foreach (Pizza pizza in PostData.GetPizze())
            {
                if (pizza.Id == id)
                {
                    pizzaFound = pizza;
                    break;

                }
            }

            return pizzaFound;
        }
    }
}

