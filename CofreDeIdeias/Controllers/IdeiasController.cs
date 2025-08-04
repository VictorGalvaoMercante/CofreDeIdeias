using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CofreDeIdeias.Data;
using CofreDeIdeias.Models;

namespace CofreDeIdeias.Controllers
{
    public class IdeiasController : Controller
    {
        private readonly AppDbContext _context;

        public IdeiasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ideias
        public async Task<IActionResult> Index(DateTime? dataInicio, DateTime? dataFim)
        {
            var query = _context.Ideias.AsQueryable();

            if (dataInicio.HasValue)
                query = query.Where(i => i.DataCriacao >= dataInicio.Value);

            if (dataFim.HasValue)
                query = query.Where(i => i.DataCriacao <= dataFim.Value);

            ViewData["DataInicio"] = dataInicio?.ToString("yyyy-MM-dd") ?? "";
            ViewData["DataFim"] = dataFim?.ToString("yyyy-MM-dd") ?? "";

            var listaFiltrada = await query.ToListAsync();

            return View(listaFiltrada);
        }


        // GET: Ideias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ideia = await _context.Ideias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ideia == null)
            {
                return NotFound();
            }

            return View(ideia);
        }

        // GET: Ideias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ideias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,Categoria,Prioridade,Confidencial,Favorita,DataCriacao")] Ideia ideia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ideia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ideia);
        }

        // GET: Ideias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ideia = await _context.Ideias.FindAsync(id);
            if (ideia == null)
            {
                return NotFound();
            }
            return View(ideia);
        }

        // POST: Ideias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Categoria,Prioridade,Confidencial,Favorita,DataCriacao")] Ideia ideia)
        {
            if (id != ideia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ideia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdeiaExists(ideia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ideia);
        }

        // GET: Ideias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ideia = await _context.Ideias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ideia == null)
            {
                return NotFound();
            }

            return View(ideia);
        }

        // POST: Ideias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ideia = await _context.Ideias.FindAsync(id);
            if (ideia != null)
            {
                _context.Ideias.Remove(ideia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdeiaExists(int id)
        {
            return _context.Ideias.Any(e => e.Id == id);
        }
    }
}
