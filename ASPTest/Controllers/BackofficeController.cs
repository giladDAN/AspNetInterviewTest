using ASPTest.BL;
using ASPTest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPTest.Controllers
{
    public class BackofficeController : Controller
    {
        IData _DataRepository = new DataRepository();

        // GET: Backoffice
        public ActionResult Index()
        {
            ViewBag.data = _DataRepository.GetItemsAndCount();
            return View();
        }

        // GET: date
        public ActionResult Accroding(DateTime date)
        {
            ViewBag.data = _DataRepository.GetItemsAccroding(date);
            return View();
        }
    }
}
