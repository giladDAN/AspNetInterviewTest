using ASPTest.BL;
using ASPTest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            IEvents _eventRep = new EventsRepository();
            ViewBag.items = _eventRep.GetAllItems();
            ViewBag.nextPositions = _eventRep.getNextPositions();
            return View();
        }
    }
}