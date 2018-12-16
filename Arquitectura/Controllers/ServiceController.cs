using System.Web.Mvc;
using log4net;
using Service.Services;
using System;
using System.Net.Mail;
using Service.Abonados;
using System.Net;
using Service.Users;
using Model;
using System.Linq;

namespace Arquitectura.Controllers
{
    public class ServiceController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AbonadoController));
        private IServiceService service;
        private IAbonadoService abonadoService;
        private IUserService userService;

        public ServiceController(IServiceService service, IAbonadoService abonadoService, IUserService userService)
        {
            this.service = service;
            this.abonadoService = abonadoService;
            this.userService = userService;
        }

        public ActionResult Particular()
        {
            var service = new Model.Service();
            ViewBag.Result = "";
            return View(service);
        }

        [HttpPost]
        public ActionResult Particular(Model.Service model)
        {
            if (ModelState.IsValid)
            {
                var sendOk = SendMail(model);
                if (sendOk)
                {
                    ViewBag.Result = "ok";
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo enviar la solicitud");
                    ViewBag.Result = "error";
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "No se pudo enviar la solicitud");
                ViewBag.Result = "error";
                return View(model);
            }
        }

        public ActionResult AbonadoService()
        {
            var service = new Model.Service();
            ViewBag.Result = "";
            return View(service);
        }

        [HttpPost]
        public ActionResult AbonadoService(Model.Service model)
        {
            if (ModelState.IsValid)
            {
                var abonado = abonadoService.List(x => x.Number == model.Abonado.Number).FirstOrDefault();
                if(abonado != null)
                {
                    model.Abonado = abonado;
                    var sendOk = SendMail(model);
                    if (sendOk)
                    {
                        ViewBag.Result = "ok";
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se pudo enviar la solicitud");
                        ViewBag.Result = "error";
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo enviar la solicitud");
                    ViewBag.Result = "error";
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "No se pudo enviar la solicitud");
                ViewBag.Result = "error";
                return View(model);
            }
        }

        private bool SendMail(Model.Service model)
        {
            //mando mail
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("computecnica.arg@gmail.com", "serv2018"),
                    EnableSsl = true
                };
                var subjet = model.Abonado.Id > 0 ? "Solicitud de servicio - Abonado" : "Solicitud de servicio - Particular";
                var body = "Nombre: " + model.Abonado.Name + "\n" + "Tel: " + model.Abonado.Phone + "\n" + "Email: " + model.Abonado.Email
                    + "\n" + "Detalle: " + model.Issue;
                client.Send("computecnica.arg@gmail.com", "service@computecnica.com.ar", subjet, body);
                return true;
            }
            catch (Exception ex)
            {
                return false;
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
