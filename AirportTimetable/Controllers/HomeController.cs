using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AirportTimetable.Models;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Text;
using System.Threading;
using System.Web.Mvc;

namespace AirportTimetable.Controllers
{
    public class HomeController : Controller
    {
        TimetableHandler tt = new TimetableHandler();
        public ActionResult Index()
        {
            return View(tt.GetTimetable("departures"));
        }
        public ActionResult Arrivals()
        {
            return View(tt.GetTimetable("arrivals"));
        }
    }
}
