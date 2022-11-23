using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoMute.Web.Data;
using CoMute.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace CoMute.Web.Controllers
{
    public class CarPoolsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CarPoolsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CarPools
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarPools.ToListAsync());
        }

        // GET: CarPools/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carPool = await _context.CarPools
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPool == null)
            {
                return NotFound();
            }

            return View(carPool);
        }

        // GET: CarPools/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarPools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartureTime,ExpectedArrivalTime,Origin,DaysAvailable,Destination,AvailableSeats,OwnerId,Note")] CarPool carPool)
        {
            if (ModelState.IsValid)
            {
                carPool.Id = Guid.NewGuid();
                carPool.OwnerId = new Guid(_userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult().Id);


                //startB < startA || startB > endA
                var checkOverlap = await _context.CarPools.Where(x=>carPool.DepartureTime < x.DepartureTime ||
                                                                    carPool.ExpectedArrivalTime > x.ExpectedArrivalTime
                                                                    ).ToListAsync();

              if (checkOverlap.Any())
                  return BadRequest("Overlapping car pools");

                _context.Add(carPool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carPool);
        }

        // GET: CarPools/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carPool = await _context.CarPools.FindAsync(id);
            if (carPool == null)
            {
                return NotFound();
            }
            return View(carPool);
        }

        public async Task<IActionResult> Join(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = new Guid(_userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult().Id);

            var carPool = await _context.CarPools.FindAsync(id);
            if (carPool == null)
            {
                return NotFound();
            }

            var linkedData = await _context.LinkedCarPools
                .Where(x=>x.UserId ==  userId  && x.CarPoolId == id)
                .FirstOrDefaultAsync();

            if (linkedData == null)
                return BadRequest("User Already linked");

            var linkPool = new LinkedCarPools()
            {
                CarPoolId = id,
                UserId = userId,
                Id = Guid.NewGuid(),
            };

            _context.LinkedCarPools.Add(linkPool);
            await _context.SaveChangesAsync();


            return Ok(await _context.CarPools.ToListAsync());
        }


        // POST: CarPools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DepartureTime,ExpectedArrivalTime,Origin,DaysAvailable,Destination,AvailableSeats,OwnerId,Note")] CarPool carPool)
        {
            if (id != carPool.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carPool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarPoolExists(carPool.Id))
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
            return View(carPool);
        }

        // GET: CarPools/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carPool = await _context.CarPools
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPool == null)
            {
                return NotFound();
            }

            return View(carPool);
        }

        // POST: CarPools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carPool = await _context.CarPools.FindAsync(id);
            _context.CarPools.Remove(carPool);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarPoolExists(Guid id)
        {
            return _context.CarPools.Any(e => e.Id == id);
        }

        public bool CheckOverLapping(DateTime startA, DateTime startB,
            DateTime endA, DateTime endB)
        {
            return startB < startA || startB > endA;
        }
    }
}
