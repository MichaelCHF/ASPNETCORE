using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZFStudio.IService;
using ZFStudio.Web.ViewModels.Acount;

namespace ZFStudio.Web.Controllers
{
    public class AcountController : Controller
    {
        private readonly IUserService _userService;

        public AcountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var row = await _userService.CreateAsync(new Models.UserInfo()
            {
                UserId = model.UserId,
                Password = model.Passwrod,
                UserName = "Test"
            });

            return RedirectToAction("Login", "Acount");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _userService.Login(model.UserId, model.Password))
            {
                if (model.IsRememberMe)
                {
                    //先用Cookie
                    //token待实现
                    Response.Cookies.Append("LoginId", model.UserId, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7)
                    });
                }
                else
                {
                    HttpContext.Session.SetString("LoginId", model.UserId);
                }

                ViewBag.UserId = model.UserId;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
            
        }
    }
}
