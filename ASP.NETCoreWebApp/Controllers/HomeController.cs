using ASP.NETCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP.NETCoreWebApp.Data;
using System.Dynamic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP.NETCoreWebApp.Manager;
using Microsoft.AspNetCore.Http;
using System;

namespace ASP.NETCoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopContext _context;

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void setCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append(key, value, option);
        }
        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void removeCookie(string key)
        {
            Response.Cookies.Delete(key);
        }

        public async Task<User> isLoginAsync()
        {
            string token = Request.Cookies["token"];
            if (token != null)
            {
                Token tkn = await _context.Tokens.Where<Token>(t => t.token == token).FirstOrDefaultAsync();
                if (tkn != null)
                {
                    return await _context.Users.FindAsync(tkn.UserID);
                }
            }
            return null;
        }
        public HomeController(ILogger<HomeController> logger, ShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            ViewBag.brands = await _context.Brands.ToListAsync();
            ViewBag.categoris = await _context.Categoris.ToListAsync();
            ViewBag.Objects = await _context.Objects.ToListAsync();
            ViewBag.subCategoris = await _context.SubCategoris.ToListAsync();
            return View(await isLoginAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Categories(string style, int? page)
        {
            SubCategori subCategori = await _context.SubCategoris.Where<SubCategori>(sc => sc.title == style).FirstOrDefaultAsync();
            var obj = _context.Objects.Where<Models.Object>(o => o.SubCategori == subCategori).Include(o=>o.SubCategori).Include(o=>o.Attribute);
            return View(await PaginatedList<Models.Object>.CreateAsync(obj, page ?? 1, 9));
        }

        [HttpGet]
        public async Task<IActionResult> OrderBy(string style, int? page)
        {
            IQueryable<Models.Object> obj = null;
            switch (style)
            {
                case "-price":
                    obj = _context.Objects.OrderBy(o => o.price);
                    break;
                case "price":
                    obj = _context.Objects.OrderByDescending(o => o.price);
                    break;
                case "-published":
                    obj = _context.Objects.OrderByDescending(o => o.ObjectID);
                    break;
                case "published":
                    obj = _context.Objects.OrderBy(o => o.ObjectID);
                    break;
                case "discount":
                    obj = _context.Objects.OrderByDescending(o => o.discount);
                    break;
                default:
                    return View();
            }
            await _context.Attributes.ToListAsync();
            return View(await PaginatedList<Models.Object>.CreateAsync(obj, page ?? 1, 9));
        }

        [HttpGet]
        public async Task<IActionResult> ObjectsDetail(int attID)
        {
            await _context.Objects.Where(o => o.AttributeID == attID).FirstOrDefaultAsync();
            return View(await _context.Attributes.FindAsync(attID));
        }

        [HttpGet]
        public async Task<IActionResult> Basket()
        {
            User user = await isLoginAsync();
            if (user != null)
            {
                return View(await _context.Sells.Where(s=>s.User == user && s.PaymentID == null).Include(s=>s.Object).ToListAsync());
            }

            return StatusCode(405);
        }

        [HttpPost]
        public async Task<IActionResult> Basket(int productID)
        {
            User user = await isLoginAsync();
            if (user != null)
            {
                await _context.AddAsync(new Sell { ObjectID = productID, User = user });
                await _context.SaveChangesAsync();
                return StatusCode(200);
            }

            return StatusCode(405);
        }

        [HttpGet]
        public async Task<IActionResult> Payment(int productID)
        {
            User user = await isLoginAsync();
            if (user != null)
            {
                _context.Sells.Remove(await _context.Sells.Where(s => s.ObjectID == productID).FirstOrDefaultAsync());
                await _context.SaveChangesAsync();
                return StatusCode(200);
            }

            return StatusCode(405);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(string discountCode)
        {
            User user = await isLoginAsync();
            DisCountCode d = await _context.disCountCodes.Where(d => d.code == discountCode).FirstOrDefaultAsync();
            if (user != null && d != null)
            {
                Payment payment = new Payment { DisCountCode = d};
                await _context.AddAsync(payment);
                await _context.SaveChangesAsync();
                return StatusCode(200);
            }

            return StatusCode(405);
        }

        public async Task<IActionResult> Logout()
        {
            User user = await isLoginAsync();
            if (user != null)
            {
                Token token = await _context.Tokens.Where(t => t.User == user).FirstOrDefaultAsync();
                if (token != null)
                {
                    _context.Tokens.Remove(token);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DiscountCode(string discountCode,double price)
        {
            dynamic obj = new ExpandoObject();
            DisCountCode discount = await _context.disCountCodes.Where(d => d.code == discountCode).FirstOrDefaultAsync();
            if (discount != null)
            {
                obj.resualt = true;
                obj.price = price - (price * discount.discount / 100.0f);
            }
            else
            {
                obj.resualt = false;
                obj.price = price;
            }
            return Json(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("UserID,username,email,first_name,last_name,address,phone_number,password")] User user)
        {
            if (ModelState.IsValid && user != null)
            {
                user.password = UserManager.makeHashPassword(user.password);
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                Token token = await UserManager.loginAsync(user, _context);
                setCookie("token", token.token, null);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync<User>(u => u.username == username);
                if (user != null && UserManager.isValidPassword(user, password))
                {
                    Token token = await UserManager.loginAsync(user, _context);
                    setCookie("token", token.token, null);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
