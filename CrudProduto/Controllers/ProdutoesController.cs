using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudProduto.Models;

namespace CrudProduto.Controllers
{
    public class ProdutoesController : Controller
    {
        private readonly DbLojaSenaiContext _context;

        public ProdutoesController(DbLojaSenaiContext context)
        {
            _context = context;
        }

        // GET: Produtoes
        public async Task<IActionResult> Index()
        {
            var dbLojaSenaiContext = _context.Produtos.Include(p => p.IdCategoriaNavigation).Include(p => p.IdFornecedoresNavigation);
            return View(await dbLojaSenaiContext.ToListAsync());
        }

        // GET: Produtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdFornecedoresNavigation)
                .FirstOrDefaultAsync(m => m.IdProduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Categoria1");
            ViewData["IdFornecedores"] = new SelectList(_context.Fornecedores, "IdFornecedores", "Nome");
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduto,Descricao,IdCategoria,IdFornecedores")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Categoria1", produto.IdCategoria);
            ViewData["IdFornecedores"] = new SelectList(_context.Fornecedores, "IdFornecedores", "Nome", produto.IdFornecedores);
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Categoria1", produto.IdCategoria);
            ViewData["IdFornecedores"] = new SelectList(_context.Fornecedores, "IdFornecedores", "Nome", produto.IdFornecedores);
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduto,Descricao,IdCategoria,IdFornecedores")] Produto produto)
        {
            if (id != produto.IdProduto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.IdProduto))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Categoria1", produto.IdCategoria);
            ViewData["IdFornecedores"] = new SelectList(_context.Fornecedores, "IdFornecedores", "Nome", produto.IdFornecedores);
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdFornecedoresNavigation)
                .FirstOrDefaultAsync(m => m.IdProduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'DbLojaSenaiContext.Produtos'  is null.");
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
          return (_context.Produtos?.Any(e => e.IdProduto == id)).GetValueOrDefault();
        }
    }
}
