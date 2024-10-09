using Lab_5.Middleware;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lab_5.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetCookie(string value, string expiration)
        {
            // Перевірка на правильність формату дати
            if (DateTime.TryParse(expiration, out DateTime expiryDate))
            {
                var options = new CookieOptions
                {
                    Expires = expiryDate,
                    HttpOnly = true, // Додаємо для безпеки
                    Secure = true,   // Вимагається HTTPS
                    SameSite = SameSiteMode.Strict // Політика безпеки
                };
                Response.Cookies.Append("MyValue", value, options);
                ViewBag.CookieStatus = "Cookie has been set successfully!";
            }
            else
            {
                ViewBag.CookieStatus = "Invalid expiration date.";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CheckCookies()
        {
            // Перевірка кукі
            var value = Request.Cookies["MyValue"];
            if (value != null)
            {
                return Content($"Cookie Value: {value}");
            }
            else
            {
                return Content("No cookie found.");
            }
        }
    }
}
