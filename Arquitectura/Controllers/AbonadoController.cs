using System.Web.Mvc;
using log4net;
using Model;
using Service.Abonados;
using System;
using System.Net.Mail;
using System.Net;
using Service.Users;
using Service.Mails;

namespace Arquitectura.Controllers
{
    public class AbonadoController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AbonadoController));
        private IAbonadoService service;
        private IUserService userService;
        private IMailService mailService;

        public AbonadoController(IAbonadoService service, IUserService userService, IMailService mailService)
        {
            this.service = service;
            this.userService = userService;
            this.mailService = mailService;
        }

        public ActionResult Index(string username, string token)
        {
            if (!ValidateCredentials())
            {
                return RedirectToAction("Index", "Login", new { redirect = "Abonado" });
            }

            var abonados = service.List();
            return View(abonados);
        }

        public ActionResult Create(string username, string token)
        {
            if (!ValidateCredentials())
            {
                return RedirectToAction("Index", "Login", new { redirect = "Abonado/Create" });
            }

            var abonado = new Abonado();
            return View(abonado);
        }

        [HttpPost]
        public ActionResult Create(string username, string token, Abonado model)
        {
            if (!ValidateCredentials())
            {
                return RedirectToAction("Index", "Login", new { redirect = "Abonado/Create" });
            }

            if (ModelState.IsValid)
            {
                var result = service.Add(model);
                if (String.IsNullOrEmpty(result.Error))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Error);
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Modify(string username, string token, int id)
        {
            if (!ValidateCredentials())
            {
                return RedirectToAction("Index", "Login", new { redirect = "Abonado/Modify?id="+id });
            }

            Abonado abonado = service.Get(id);
            return View(abonado);
        }

        [HttpPost]
        public ActionResult Modify(string username, string token, Abonado model)
        {
            if (!ValidateCredentials())
            {
                return RedirectToAction("Index", "Login", new { redirect = "Abonado/Modify?id=" + model.Id });
            }

            if (ModelState.IsValid)
            {
                var result = service.Update(model.Id, model);
                if (String.IsNullOrEmpty(result.Error))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Error);
                return View(model);
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Remove(string username, string token, int id)
        {
            if (!ValidateCredentials())
            {
                return RedirectToAction("Index", "Login", new { redirect = "Abonado/Modify?id=" + id });
            }

            var result = service.Remove(id);
            if (!String.IsNullOrEmpty(result.Error))
            {
                ModelState.AddModelError("", result.Error);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Cotizar(string username, string token)
        {
            var abonado = new Abonado();
            ViewBag.Result = "";
            return View(abonado);
        }

        [HttpPost]
        public ActionResult Cotizar(string username, string token, Abonado model, string pcs, string notebooks, string servers, string printers, bool network)
        {
            if (ModelState.IsValid)
            {
                //mando mail
                Mail mail = new Mail();
                mail.Subject = "Solicitud de cotización";
                mail.Body = "Nombre: " + model.Name + "\n" + "Tel: " + model.Phone + "\n" + "Email: " + model.Email + "\n" + "Dirección: "
                    + model.Address + "\n" + "Pcs: " + pcs + ", Notebooks: " + notebooks + ", Servers: " + servers + ", Impresoras: " + printers
                    + ", En red? " + (network ? "Si" : "No");

                var result = mailService.Send(mail);
                if (!String.IsNullOrEmpty(result.Error))
                {
                    ModelState.AddModelError("", "No se pudo enviar la solicitud");
                    ViewBag.Result = "error";
                    return View(model);
                }

                ViewBag.Result = "ok";
                return View(model);
            }
            else
            {
                ViewBag.Result = "error";
                return View(model);
            }
        }

        private bool ValidateCredentials()
        {
            if (Request.Cookies["UserSettings"] != null)
            {
                if (!userService.ValidateToken(Request.Cookies["UserSettings"]["username"], Request.Cookies["UserSettings"]["username"]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
