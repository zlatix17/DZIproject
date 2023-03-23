using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DZIproject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DZIproject.Controllers
{
    [Authorize]
    public class ShoppingsController : Controller
    {
        private readonly UserManager<Client> _userManager;
        private readonly WebsDbContext _context;

        public ShoppingsController(WebsDbContext context, UserManager<Client> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Shoppings
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
               var websDbContext = _context.Shoppings
                .Include(s => s.Products)
                .Include(s => s.Clients);
               return View(await websDbContext.ToListAsync());
            }
            else
            {
                var currentUser = _userManager.GetUserId(User);
                var websDbContext = _context.Shoppings
               .Include(s => s.Products)
               .Include(s => s.Clients).Where(s => s.ClientId == currentUser);
                return View(await websDbContext.ToListAsync());

            }
        }

        // GET: Shoppings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shoppings == null)
            {
                return NotFound();
            }

            var shopping = await _context.Shoppings
                .Include(s => s.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopping == null)
            {
                return NotFound();
            }

            return View(shopping);
        }

        // GET: Shoppings/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Shoppings/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Quantity")] Shopping shopping)
        {
            if (ModelState.IsValid)
            {
                shopping.ClientId = _userManager.GetUserId(User);
                shopping.RegisterOn = DateTime.Now;
                decimal currentPrice = _context.Products.FirstOrDefault(x => x.Id == shopping.ProductId).Price;
                shopping.TotalSum = shopping.Quantity * currentPrice;
                _context.Shoppings.Add(shopping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", shopping.ProductId);
            return View(shopping);
        }

        // GET: Shoppings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shoppings == null)
            {
                return NotFound();
            }

            var shopping = await _context.Shoppings.FindAsync(id);
            if (shopping == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", shopping.ProductId);
            return View(shopping);
        }

        // POST: Shoppings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Quantity")] Shopping shopping)
        {
            if (id != shopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    shopping.ClientId = _userManager.GetUserId(User);
                    shopping.RegisterOn = DateTime.Now;
                    decimal currentPrice = _context.Products.FirstOrDefault(x => x.Id == shopping.ProductId).Price;
                    shopping.TotalSum = shopping.Quantity * currentPrice;                    
                    _context.Update(shopping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingExists(shopping.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", shopping.ProductId);
            return View(shopping);
        }

        // GET: Shoppings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shoppings == null)
            {
                return NotFound();
            }

            var shopping = await _context.Shoppings
                .Include(s => s.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopping == null)
            {
                return NotFound();
            }

            return View(shopping);
        }

        // POST: Shoppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shoppings == null)
            {
                return Problem("Entity set 'WebsDbContext.Shoppings'  is null.");
            }
            var shopping = await _context.Shoppings.FindAsync(id);
            if (shopping != null)
            {
                _context.Shoppings.Remove(shopping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingExists(int id)
        {
            return _context.Shoppings.Any(e => e.Id == id);
        }
    }
}
