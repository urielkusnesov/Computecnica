using System.Web.Mvc;

namespace Arquitectura.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Abonado", null);
        }

    }
}
