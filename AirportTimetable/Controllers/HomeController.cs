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
        HtmlDocument doc = new HtmlDocument();
        HtmlNodeCollection nodes;
        Flight flight = new Flight();
        HtmlParser parser;
        IEnumerable<Flight> flights;
        public IActionResult Index()
        {
            parser = new HtmlParser("departures");
            doc.LoadHtml(parser.Node.InnerHtml);
            nodes = doc.DocumentNode.SelectNodes("//tr/td");
            flights = flight.GetFlightsFromNodes(nodes).Where(e => e.Time > DateTime.Now);
            return View(flights);
        }
        public IActionResult Arrivals()
        {
            parser = new HtmlParser("arrivals");
            doc.LoadHtml(parser.Node.InnerHtml);
            nodes = doc.DocumentNode.SelectNodes("//tr/td");
            flights = flight.GetFlightsFromNodes(nodes).Where(e => e.Time > DateTime.Now);
            return View(flights);
        }
    }
}
