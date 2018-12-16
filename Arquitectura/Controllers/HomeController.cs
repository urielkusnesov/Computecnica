using System;
using System.IO;
using System.Web.Mvc;

namespace Arquitectura.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public FileResult DownloadApp()
        {
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files\\app-debug.apk");
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                string fileName = "computecnica.apk";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Message = "No se pudo descargar la aplicación. Intente mas tarde";
                return null;
            }
        }
    }
}
