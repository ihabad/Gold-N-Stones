using GoldAndSilver.Data;
using GoldAndSilver.Models;
using GoldAndSilver.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace GoldAndSilver.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CollectionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        //This Controller has this binded property attached with it
        [BindProperty]
        public CollectionViewModel CollectionVM { get; set; }


        public CollectionController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;

            CollectionVM = new CollectionViewModel()
            {
                Category = _db.Category,
                Collection = new Models.Collection()

            };
        }

        public async Task<IActionResult> Index()
        {
            var Collections = await _db.Collection.Include(m => m.Category).ToListAsync();
            return View(Collections);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View(CollectionVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreatePOST()
        {
            //CollectionVM.Collection = Convert.ToInt32();

            if (!ModelState.IsValid)
            {
                return View(CollectionVM);
            }

            _db.Collection.Add(CollectionVM.Collection);
            await _db.SaveChangesAsync();

            //image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var collectionFromDb = await _db.Collection.FindAsync(CollectionVM.Collection.Id);

            if (files.Count > 0)
            {
                // file has been uploaded

                var uploads = Path.Combine(webRootPath, "images");
                var extensions = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, CollectionVM.Collection.Id + extensions), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                collectionFromDb.Image = @"\images\" + CollectionVM.Collection.Id + extensions;
            }
            else
            {
                //no file was uploaded , use default

                var uploads = Path.Combine(webRootPath, @"images\" + StaticDetail.DefaultGoldImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\" + CollectionVM.Collection.Id + ".jpy");
                collectionFromDb.Image = @"\image\" + CollectionVM.Collection.Id + ".jpy";
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // EDIT Collection
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CollectionVM.Collection = await _db.Collection.Include(m => m.Category).SingleOrDefaultAsync(m => m.Id == id);


            if (CollectionVM.Collection == null)
            {
                return NotFound();
            }
            return View(CollectionVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          

            if (!ModelState.IsValid)
            {
                
                return View(CollectionVM);
            }



            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var CollectionFromDb = await _db.Collection.FindAsync(CollectionVM.Collection.Id);

            if (files.Count > 0)
            {
                //New Image has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension_new = Path.GetExtension(files[0].FileName);

                //Delete the original file
                var imagePath = Path.Combine(webRootPath, CollectionFromDb.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                //we will upload the new file
                using (var filesStream = new FileStream(Path.Combine(uploads, CollectionVM.Collection.Id + extension_new), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                CollectionFromDb.Image = @"\images\" + CollectionVM.Collection.Id + extension_new;
            }

            CollectionFromDb.Name = CollectionVM.Collection.Name;
            CollectionFromDb.Description = CollectionVM.Collection.Description;
            CollectionFromDb.Price = CollectionVM.Collection.Price;
            CollectionFromDb.CategoryId = CollectionVM.Collection.CategoryId;


            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET : Details Collection
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CollectionVM.Collection = await _db.Collection.Include(m => m.Category).SingleOrDefaultAsync(m => m.Id == id);

            if (CollectionVM.Collection == null)
            {
                return NotFound();
            }

            return View(CollectionVM);
        }

        //GET : Delete Collection
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CollectionVM.Collection = await _db.Collection.Include(m => m.Category).SingleOrDefaultAsync(m => m.Id == id);

            if (CollectionVM.Collection == null)
            {
                return NotFound();
            }

            return View(CollectionVM);
        }

        //POST Delete Collection
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Collection collection = await _db.Collection.FindAsync(id);

            if (collection != null)
            {
                

                if (System.IO.File.Exists(""))
                {
                    System.IO.File.Delete("");
                }
                _db.Collection.Remove(collection);
                await _db.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }
    }

}
