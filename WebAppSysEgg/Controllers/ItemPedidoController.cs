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
    public class ItemPedidoController : Controller
    {
        private readonly AppDbContext _context;

        public ItemPedidoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ItemPedido
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ItemPedidos.Include(i => i.Pedido).Include(i => i.Produto);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ItemPedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ItemPedidoId == id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // GET: ItemPedido/Create
        public IActionResult Create()
        {
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "PedidoId");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao");
            return View();
        }

        // POST: ItemPedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemPedidoId,PedidoId,ProdutoId,Item,Quantidade,ValorUnitario,SubTotal")] ItemPedido itemPedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "PedidoId", itemPedido.PedidoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao", itemPedido.ProdutoId);
            return View(itemPedido);
        }

        // GET: ItemPedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos.FindAsync(id);
            if (itemPedido == null)
            {
                return NotFound();
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "PedidoId", itemPedido.PedidoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao", itemPedido.ProdutoId);
            return View(itemPedido);
        }

        // POST: ItemPedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemPedidoId,PedidoId,ProdutoId,Item,Quantidade,ValorUnitario,SubTotal")] ItemPedido itemPedido)
        {
            if (id != itemPedido.ItemPedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemPedidoExists(itemPedido.ItemPedidoId))
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
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "PedidoId", itemPedido.PedidoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Descricao", itemPedido.ProdutoId);
            return View(itemPedido);
        }

        // GET: ItemPedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ItemPedidoId == id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // POST: ItemPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemPedido = await _context.ItemPedidos.FindAsync(id);
            _context.ItemPedidos.Remove(itemPedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemPedidoExists(int id)
        {
            return _context.ItemPedidos.Any(e => e.ItemPedidoId == id);
        }
    }
}
