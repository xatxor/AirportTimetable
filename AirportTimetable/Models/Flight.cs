using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace AirportTimetable.Models
{
    public class Flight
    {
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string CompanyLogoPath { get; set; }
        public string City { get; set; }
        public char Terminal { get; set; }
        public string Status { get; set; }
        public Flight() { }
        public Flight (DateTime time, string name, string company, string companylogopath, string city, char terminal, string status)
        {
            Time = time;
            Name = name;
            Company = company;
            CompanyLogoPath = @"http://www.vnukovo.ru" + companylogopath;
            City = city;
            Terminal = terminal;
            Status = status;
        }
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
    }
}
