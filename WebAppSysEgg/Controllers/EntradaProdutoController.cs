using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppSysEgg.Models;

namespace WebAppSysEgg.Controllers
{
    public class EntradaProdutoController : Controller
    {
        private readonly AppDbContext _context;

        public EntradaProdutoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EntradaProduto
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EntradaProdutos.Include(e => e.Fornecedor).Include(e => e.Produto);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EntradaProduto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entradaProduto = await _context.EntradaProdutos
                .Include(e => e.Fornecedor)
                .Include(e => e.Produto)
                .FirstOrDefaultAsync(m => m.EntradaProdutoId == id);
            if (entradaProduto == null)
            {
                return NotFound();
            }

            return View(entradaProduto);
        }

        // GET: EntradaProduto/Create
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedors, "FornecedorId", "FornecedorNome");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao");
            return View();
        }

        // POST: EntradaProduto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntradaProdutoId,ProdutoId,Quantidade,Valor,DataEntrada,FornecedorId")] EntradaProduto entradaProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entradaProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedors, "FornecedorId", "FornecedorNome", entradaProduto.FornecedorId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao", entradaProduto.ProdutoId);
            return View(entradaProduto);
        }

        // GET: EntradaProduto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entradaProduto = await _context.EntradaProdutos.FindAsync(id);
            if (entradaProduto == null)
            {
                return NotFound();
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedors, "FornecedorId", "FornecedorNome", entradaProduto.FornecedorId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao", entradaProduto.ProdutoId);
            return View(entradaProduto);
        }

        // POST: EntradaProduto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntradaProdutoId,ProdutoId,Quantidade,Valor,DataEntrada,FornecedorId")] EntradaProduto entradaProduto)
        {
            if (id != entradaProduto.EntradaProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entradaProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntradaProdutoExists(entradaProduto.EntradaProdutoId))
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
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedors, "FornecedorId", "FornecedorNome", entradaProduto.FornecedorId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao", entradaProduto.ProdutoId);
            return View(entradaProduto);
        }

        // GET: EntradaProduto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entradaProduto = await _context.EntradaProdutos
                .Include(e => e.Fornecedor)
                .Include(e => e.Produto)
                .FirstOrDefaultAsync(m => m.EntradaProdutoId == id);
            if (entradaProduto == null)
            {
                return NotFound();
            }

            return View(entradaProduto);
        }

        // POST: EntradaProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entradaProduto = await _context.EntradaProdutos.FindAsync(id);
            _context.EntradaProdutos.Remove(entradaProduto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntradaProdutoExists(int id)
        {
            return _context.EntradaProdutos.Any(e => e.EntradaProdutoId == id);
        }
    }
}
