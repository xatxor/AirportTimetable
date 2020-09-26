using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AirportTimetable.Models;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Text;
using Microsoft.Extensions.Primitives;
using System.Threading;

namespace AirportTimetable.Controllers
{
    public class HomeController : Controller
    {
        TimetableHandler tt = new TimetableHandler();
        public IActionResult Index()
        {
            TimerCallback tm = new TimerCallback(Count);
            // создаем таймер
            Timer timer = new Timer(tm, null, 5000, 5000);
            return View(tt.GetTimetable("departures"));
        }
        public void Count(object obj)
        {
            Arrivals();
        }
        public IActionResult Arrivals()
        {
            return View(tt.GetTimetable("arrivals"));
        }
    }
}
