using GoldAndSilver.Data;
using GoldAndSilver.Models;
using GoldAndSilver.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GoldAndSilver.Areas.Admin.Controllers
{
  
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        // Get Action Method  it will retrieve everything from the database and pass it to the view

        public async Task<IActionResult> Index()
        {
            return View(await _db.Category.ToListAsync());
        }

        //Get for Create 
        public IActionResult Create()
        {
            return View();
        }


        //Post for Create

        [HttpPost]

        // ALL HTTP must include validate AntiForgeryToken
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            // If it finds the name is empty it wants to go inside because it says the model is invalid
            if (ModelState.IsValid)
            {
                // if valid

                _db.Category.Add(category);
                await _db.SaveChangesAsync();

                //once it's successful will return back to index
                return RedirectToAction(nameof(Index));
            }
            //if it's not valid will return back to the view
            return View(category);

        }

        // Get Edit for Category
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();

            }
            return View(category); //this will display for the user so they can edit it if they want
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            //model state will check if there any requirements on any properties on this model
            if (ModelState.IsValid)
            {
                _db.Update(category);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //if not valid
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();

            }
            return View(category);
        }

        // Delete button for Category Name

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _db.Category.FindAsync(id);

            if (category == null)
            {
                return View();
            }
            _db.Category.Remove(category);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
