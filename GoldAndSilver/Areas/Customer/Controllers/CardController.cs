using GoldAndSilver.Data;
using GoldAndSilver.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GoldAndSilver.Utility;

namespace GoldAndSilver.Areas.Customer.Controllers
{
    public class CardController : Controller
    {
        [Area("Customer")]
        public class CartController : Controller
        {
            private readonly ApplicationDbContext _db;
           

            [BindProperty]
            public OrderDetailsCart detailCart { get; set; }



            public async Task<IActionResult> Index()
            {
                // ADD the detail cart
                detailCart = new OrderDetailsCart()
                {
                    OrderHeader = new Models.OrderHeader()
                };

                // the total order zero
                detailCart.OrderHeader.OrderTotal = 0;

                //retrieve the userId of the logged IN  user
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                //find all the items user has added them to shopping cart from database
                var cart = _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);
                //if the card is null
                if (cart != null)
                {
                    detailCart.listCart = cart.ToList();
                }

                // we need foreach to calculate the order totat

                foreach (var list in detailCart.listCart)
                {
                    list.Collection = await _db.Collection.FirstOrDefaultAsync(m => m.Id == list.CollectionId);
                    detailCart.OrderHeader.OrderTotal = detailCart.OrderHeader.OrderTotal + (list.Collection.Price * list.Count);
                    //calculate the total order and don't have HTML tag
                    list.Collection.Description = StaticDetail.ConvertToRawHtml(list.Collection.Description);
                    if (list.Collection.Description.Length > 100)
                    {
                        list.Collection.Description = list.Collection.Description.Substring(0, 99) + "...";
                    }
                }
                detailCart.OrderHeader.OrderTotalOriginal = detailCart.OrderHeader.OrderTotal;


                return View(detailCart);

            }


            public async Task<IActionResult> Summary()
            {

                detailCart = new OrderDetailsCart()
                {
                    OrderHeader = new Models.OrderHeader()
                };

                detailCart.OrderHeader.OrderTotal = 0;

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                ApplicationUser applicationUser = await _db.ApplicationUser.Where(c => c.Id == claim.Value).FirstOrDefaultAsync();
                var cart = _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);
                if (cart != null)
                {
                    detailCart.listCart = cart.ToList();
                }

                foreach (var list in detailCart.listCart)
                {
                    list.Collection = await _db.Collection.FirstOrDefaultAsync(m => m.Id == list.CollectionId);
                    detailCart.OrderHeader.OrderTotal = detailCart.OrderHeader.OrderTotal + (list.Collection.Price * list.Count);

                }




                return View(detailCart);

            }

            }
        }
    }

