using GoldAndSilver.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GoldAndSilver.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            // if the claim is null that means the user has not logged in
            var claimsidentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);

            //passing all the application user except the looged in user to the view
            return View(await _db.ApplicationUser.Where(u => u.Id != claim.Value).ToListAsync());
        }
    }
}
