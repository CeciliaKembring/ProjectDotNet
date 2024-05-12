using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectNet.Data;
using ProjectNet.Models;

namespace ProjectNet.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking
     public async Task<IActionResult> Index()
{
    // Hämta e-postadressen för den inloggade användaren
    var loggedInUserEmail = User.Identity.Name;

    // Hämta bokningar för den inloggade användaren
    var userBookings = await _context.Bookings
                                    .Where(b => b.BookedBy == loggedInUserEmail)
                                    .ToListAsync();

    return View(userBookings);
}

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingModel = await _context.Bookings
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingModel == null)
            {
                return NotFound();
            }

            return View(bookingModel);
        }

  // GET: Booking/Create
        public IActionResult Create()
        {
        ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Service");
        return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,Date,Time,ServiceId,Telephonenumber")] BookingModel bookingModel)
{
    if (ModelState.IsValid)
    {
        bookingModel.BookedBy = User.Identity?.Name ?? "Unknown"; 
        _context.Add(bookingModel);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Service", bookingModel.ServiceId);
    return View(bookingModel);
}


        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingModel = await _context.Bookings.FindAsync(id);
            if (bookingModel == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Service", bookingModel.ServiceId);
            return View(bookingModel);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Time,ServiceId,Telephonenumber")] BookingModel bookingModel)
        {
            if (id != bookingModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingModelExists(bookingModel.Id))
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
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Service", bookingModel.ServiceId);
            return View(bookingModel);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingModel = await _context.Bookings
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingModel == null)
            {
                return NotFound();
            }

            return View(bookingModel);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingModel = await _context.Bookings.FindAsync(id);
            if (bookingModel != null)
            {
                _context.Bookings.Remove(bookingModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingModelExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
