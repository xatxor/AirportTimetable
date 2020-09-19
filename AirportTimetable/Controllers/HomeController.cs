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
        Flight flight = new Flight();
        HtmlParser parser;
        public IActionResult Index()
        {
            parser = new HtmlParser("departures");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(parser.Node.InnerHtml);
            var nodes = doc.DocumentNode.SelectNodes("//tr/td");
            IEnumerable<Flight> departures = flight.GetFlightsFromNodes(nodes).Where(e => e.Time > DateTime.Now);
            return View(departures);
        }
        public IActionResult Arrivals()
        {
            parser = new HtmlParser("arrivals");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(parser.Node.InnerHtml);
            var nodes = doc.DocumentNode.SelectNodes("//tr/td");
            IEnumerable<Flight> arrivals = flight.GetFlightsFromNodes(nodes).Where(e => e.Time > DateTime.Now);
            return View(arrivals);
        }
    }
}
