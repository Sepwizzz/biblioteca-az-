using biblioteca.Models;
using biblioteca.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly bibliotecaContext _context;

        public HomeController(bibliotecaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var libros = _context.Libros.Include(l => l.Autor).ToList();
            return View(libros);
        }
    }
}