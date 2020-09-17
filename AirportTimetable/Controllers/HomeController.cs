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

namespace AirportTimetable.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Flight> flights = new List<Flight>();
            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.UTF8;
            var htmldoc = web.Load(@"http://www.vnukovo.ru/flights/online-timetable/#tab-sortie");
            var nodes = htmldoc.DocumentNode.SelectNodes("//tr/td");
            for (int i = 0; i < nodes.Count / 6; i++)
            {
                int j = 6 * i;
                if (nodes[j].InnerText == "" || nodes[j].InnerText == " ")
                    nodes.Remove(nodes[j]);
                var logopath = nodes[j + 3].SelectNodes("//td/img");
                string time = nodes[j].InnerText.Trim();
                DateTime dt = new DateTime(Convert.ToInt32(time.Substring(11)), 
                    Convert.ToInt32(time.Substring(8, 2)), 
                    Convert.ToInt32(time.Substring(5, 2)), 
                    Convert.ToInt32(time.Substring(0, 2)), 
                    Convert.ToInt32(time.Substring(3, 2)), 0);
                Flight flight = new Flight(dt,
                    nodes[j + 1].InnerText.Trim(),
                    nodes[j + 2].InnerText.Trim(),
                    logopath[i].Attributes["src"].Value,
                    nodes[j + 3].InnerText.Trim(),
                    nodes[j + 4].InnerText[0],
                    nodes[j + 5].InnerText.Trim());;
                flights.Add(flight);
            }
            return View(flights);
        }

        public IActionResult Departure()
        {
            return View();
        }
    }
}
