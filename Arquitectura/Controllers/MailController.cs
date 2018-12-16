using log4net;
using Model;
using Service.Mails;
using System;
using System.Web.Mvc;

namespace Arquitectura.Controllers
{
    public class MailController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MailController));
        private IMailService service;

        public MailController(IMailService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Mail model)
        {
            model.Subject = "Consulta - Web";
            model.Body = "Nombre: " + model.Name + "\n" + "Mail: " + model.Email + "\n \n" + model.Body;
            var result = service.Send(model);
            if (!String.IsNullOrEmpty(result.Error))
            {
                ModelState.AddModelError("", result.Error);
                return View(model);
            }

            ViewBag.Result = "ok";
            return View(model);
        }
    }
}
