using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agregator_linków.Data;
using agregator_linków.Models;
using agregator_linków.Repository;
using agregator_linków.Viewmodel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using agregator_linków.Attribute;

namespace agregator_linków.Controllers
{
    public class UserController : Controller
    {
       private MainRepository repository;

       public UserController(Dbcontext dbcontext)
        {
            this.repository = new MainRepository(dbcontext);
        }


       [AuthorizeUser]
       public IActionResult Index(int page=1)
        {
            string message = TempData["data"] as string;
            ViewData["Register"] = message;
            var model= repository.link.ownerLinks(User.Identity.Name,page);
            page = repository.link.CheckPage(page);
            ViewBag.Page = page;
            ViewBag.maxPage = repository.link.MaxPageUser;
            return View(model);
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmail(string email)
        {
            if (repository.user.CheckEmail(email))
            {
                return Json($"Email {email} jest juz zajęty");
            }

            return Json(true);
        }

    

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(ViewLogin viewUser)
        {
           
            if (ModelState.IsValid)
            {
                User user = repository.user.MapViewUserToUser(viewUser);
                if (repository.user.CheckLogin(user))
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.eamil));
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("index", "Link");
                }
                else
                {
                    ModelState.AddModelError("Errorlogin", "Login lub hasło jest nie prawidłowe");
                    return View(viewUser);
                }

            }
            return View(viewUser);
        }
        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public IActionResult Register(ViewRegister register)
        {
            if (ModelState.IsValid)
            {
                User user = repository.user.MapRegisterToUser(register);
                var salt = repository.user.GenerateSalt();
                user.salt = salt;
                user.password = repository.user.Haskpassword(user.password, user.salt);
                repository.user.AddUser(user);
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, register.email));
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                TempData["data"] = "Succest Register";
                return RedirectToAction("index", "Link");
            }
            return View(register);
        }

    }
}