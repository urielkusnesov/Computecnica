using System.Web.Mvc;
using Model;
using log4net;
using Service.Users;

namespace Arquitectura.Controllers
{
    public class LoginController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(LoginController));
        private IUserService service;

        public LoginController(IUserService service)
        {
            this.service = service;
        }

        public ActionResult Index(string redirect)
        {
            ViewBag.Redirect = redirect;
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user, string redirect)
        {
            string token = service.Login(user);
            if(token != "")
            {
                Response.Cookies["UserSettings"]["username"] = user.Username;
                Response.Cookies["UserSettings"]["token"] = token;
                return RedirectToAction("Index", redirect, null);
            }

            ModelState.AddModelError("", "Usuario o contraseña invalida");
            return View(user);
        }

    }
}
