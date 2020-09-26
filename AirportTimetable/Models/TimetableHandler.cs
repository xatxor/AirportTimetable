using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportTimetable.Models
{
    public class TimetableHandler
    {
        public List<Flight> GetFlightsFromNodes(HtmlNodeCollection nodes)
        {
            List<Flight> flights = new List<Flight>();
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
                    nodes[j + 5].InnerText.Trim()); ;
                flights.Add(flight);
            }
            return flights;
        }
        public IEnumerable<Flight> GetTimetable(string depOrArr)
        {
            HtmlParser parser = new HtmlParser(depOrArr);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(parser.Node.InnerHtml);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//tr/td");
            IEnumerable<Flight> flights = GetFlightsFromNodes(nodes).Where(e => e.Time > DateTime.Now);
            return flights;
        }
    }
}
