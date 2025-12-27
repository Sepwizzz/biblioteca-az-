using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using biblioteca.Data;
using biblioteca.Models;
using System.Threading.Tasks;


namespace biblioteca
{
    public class LibrosController : Controller
    {
        private readonly bibliotecaContext _context;

        public LibrosController(bibliotecaContext context)
        {
            _context = context;
        }

        // GET: Libros
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _context.Libros.Include(l => l.Autor);
            return View(await bibliotecaContext.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (libros == null)
            {
                return NotFound();
            }

            return View(libros);

        }
        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (libro == null)
                return NotFound();

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para comprobar existencia
        private bool LibrosExists(int id)
        {
            return _context.Libros.Any(e => e.ID == id);
        }







        // GET: Libros/Create
        public IActionResult Create()
        {
            // Lista de autores para el select
            ViewData["AutorID"] = new SelectList(_context.Autores, "AutorID", "Nombre");
            return View();
        }

        // POST: Libros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,AutorID")] Libros libro)
        {
            Console.WriteLine($"POST recibido -> Titulo: {libro.Titulo}, AutorID: {libro.AutorID}");
            Console.WriteLine($"paso 1"); // Debe ser 1
            foreach (var error in ModelState)
            {
                if (error.Value.Errors.Count > 0)
                {
                    Console.WriteLine($"{error.Key} -> {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
            }



            if (ModelState.IsValid)
            {
                Console.WriteLine($"pasa if ??"); // Debe ser 1

                // Forzar ID en 0 para que EF Core lo trate como nuevo
                libro.ID = 0;
                _context.Libros.Add(libro);
                var cambios = await _context.SaveChangesAsync();
                Console.WriteLine($"Filas afectadas: {cambios}"); // Debe ser 1
                return RedirectToAction(nameof(Index));
            }

            // Si hay error, recargar autores para el select
            ViewData["AutorID"] = new SelectList(_context.Autores, "AutorID", "Nombre", libro.AutorID);
            return View(libro);
        }


        //GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = await _context.Libros.FindAsync(id);
            if (libros == null)
            {
                return NotFound();
            }
             ViewData["AutorID"] = new SelectList(_context.Autores, "AutorID", "Nombre", libros.AutorID);
            return View(libros);
         }

    //POST: Libros/Edit/5
     //To protect from overposting attacks, enable the specific properties you want to bind to.
     //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, [Bind("ID,Titulo,AutorID")] Libros libros)
                {
                    if (id != libros.ID)
                    {
                        return NotFound();
            }

            if (ModelState.IsValid)
                    {
                        try
                        {
                _context.Update(libros);
                            await _context.SaveChangesAsync();
            }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (LibrosExists(libros.ID))
                            {
                                throw;
            }
                            else
                            {
                                return NotFound();
            }
            }
            return RedirectToAction(nameof(Index));
            }
            ViewData["AutorID"] = new SelectList(_context.Autores, "AutorID", "Nombre", libros.AutorID);
                    return View(libros);
            }






            }


}
