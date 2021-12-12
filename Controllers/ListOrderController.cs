using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FwB.Models;
using ListOrderModel = FwB.Models.ListOrder;

namespace FwB.ListOrder.Controllers
{
    public class ListOrderController : Controller
    {
        private readonly AppDbContext _context;

        public ListOrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ListOrder
        public async Task<IActionResult> Index()
        {
            var listOrder = await _context.ListOrder.ToListAsync();
            return View(listOrder);
        }

        // GET: ListOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listOrder = await _context.ListOrder
                .FirstOrDefaultAsync(m => m.ListOrderId == id);
            if (listOrder == null)
            {
                return NotFound();
            }

            return View(listOrder);
        }

        // GET: ListOrder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListOrderId")] ListOrderModel listOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listOrder);
        }

        // GET: ListOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listOrder = await _context.ListOrder.FindAsync(id);
            if (listOrder == null)
            {
                return NotFound();
            }
            return View(listOrder);
        }

        // POST: ListOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListOrderId")] ListOrderModel listOrder)
        {
            if (id != listOrder.ListOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListOrderExists(listOrder.ListOrderId))
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
            return View(listOrder);
        }

        // GET: ListOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listOrder = await _context.ListOrder
                .FirstOrDefaultAsync(m => m.ListOrderId == id);
            if (listOrder == null)
            {
                return NotFound();
            }

            return View(listOrder);
        }

        // POST: ListOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listOrder = await _context.ListOrder.FindAsync(id);
            _context.ListOrder.Remove(listOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListOrderExists(int id)
        {
            return _context.ListOrder.Any(e => e.ListOrderId == id);
        }
    }
}
