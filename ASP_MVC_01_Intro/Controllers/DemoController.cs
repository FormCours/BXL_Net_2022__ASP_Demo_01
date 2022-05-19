using ASP_MVC_01_Intro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASP_MVC_01_Intro.Controllers
{
    public class DemoController : Controller
    {
        // ↓ Donnée Statique (Simulation de donnée d'un Model -> DB, API,...)
        private static IEnumerable<Person> _People = new List<Person>
        {
            new Person() { Id=1, Firstname= "Zaza", Lastname="Vanderquack", Email= "z.vanderquack@test.bxl", Birthdate= new DateTime(2003, 2, 1) },
            new Person() { Id=2, Firstname= "Balthazar", Lastname="Picsou", Email= "b.picsou@money.org", Birthdate= new DateTime(1972, 12, 22) },
            new Person() { Id=3, Firstname= "Della", Lastname="Duck", Email= "della.duck@canard.com", Birthdate= new DateTime(1985, 6, 2) },
            new Person() { Id=4, Firstname= "Archibald", Lastname="Gripsou", Email= "a.gripsou@demo.test", Birthdate= new DateTime(1968, 5, 9) }
        };


        [ViewData]
        public string ExempleProps { get; set; }

        public DemoController()
        {
            ExempleProps = "Vincent";
        }


        public string Index()
        {
            return "Hello World";
        }

        public string Exemple()
        {
            return "Salut :)";
        }

        [Route("Demo/Info/Zaza")]
        public string Custom()
        {
            return "Une route custom -> /Demo/Info/Zaza";
        }

        [NonAction]
        public string No()
        {
            return "...";
        }

        public IActionResult Page()
        {
            ViewData["firstname"] = "Della";
            ViewBag.lastname = "Duck";

            return View();
        }

        [Route("Demo/DisplayData/{id}")]
        public IActionResult DisplayData([FromRoute] int id)
        {
            ViewData["IdData"] = id;
            return View();
        }


        public IActionResult Redirection([FromQuery] string name)
        {
            // ↓ Les données du ViewBag/ViewData ne sont conservé durant une seul requte
            ViewData["ExempleRedirection"] = "Salut !!!";

            // ↓ Utilisation du TempData -> Capacité de conservé la donnée après la redirection !
            TempData["Message"] = "Ceci sont des données créer dans la méthode \"Redirection\"";

            // ↓ "RedirectToAction" provoque une redirection par le navigateur
            //   Une seconde requete sera donc envoyé (donc le donnée du ViewData sont reset)
            return RedirectToAction(nameof(RedirectionResult), new { name = name, number = 42 });
        }

        public IActionResult RedirectionResult([FromQuery] string name, [FromQuery] int number)
        {
            Console.WriteLine($"QueryString : {name} / {number}");
            Console.WriteLine($"ViewData : {ViewData["ExempleRedirection"]}");
            Console.WriteLine($"TempData : {TempData["Message"]}");

            return View();
        }


        public IActionResult Information([FromRoute] int id)
        {
            Person target = _People.SingleOrDefault(p  => p.Id == id);

            if(target is null)
            {
                return NotFound();
            }

            return View(target);
        }
    }
}
