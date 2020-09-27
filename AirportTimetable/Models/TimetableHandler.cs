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
                DateTime dt = TimeHandler(time);
                string status = StatusHandler(nodes[j + 5].InnerText.Trim());
                string city = CityHandler(nodes[j + 3].InnerText.Trim());
                Flight flight = new Flight(dt,
                    nodes[j + 1].InnerText.Trim(),
                    nodes[j + 2].InnerText.Trim(),
                    logopath[i].Attributes["src"].Value,
                    city,
                    nodes[j + 4].InnerText[0],
                    status);
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
        public DateTime TimeHandler(string time)
        {
            DateTime dt = new DateTime(Convert.ToInt32(time.Substring(11)),
            Convert.ToInt32(time.Substring(8, 2)),
            Convert.ToInt32(time.Substring(5, 2)),
            Convert.ToInt32(time.Substring(0, 2)),
            Convert.ToInt32(time.Substring(3, 2)), 0); 
            return dt;
        }
        public string StatusHandler(string status)
        {
            string result = "-";
            if (status == "-")
                return result;
            string firstWords = status.Split(' ')[0] + " " + status.Split(' ')[1];
            switch (firstWords)
            {
                case "Совершил посадку":
                    result = "Прилетел";
                    break;
                case "Идёт посадка.":
                    result = "Посадка";
                    break;
                case "Задержан до":
                    result = "Задержан";
                    break;
                case "Регистрация закончена.":
                    result = "Посадка";
                    break;
                case "Идет регистрация.":
                    result = "Регистрация";
                    break;
                case "Регистрация с":
                    result = "-";
                    break;
                case "Посадка закончена.":
                    result = "Вылетает";
                    break;
                case "Закончена выд.":
                    result = "Прилетел";
                    break;
                case "Прилетел в":
                    result = "Прилетел";
                    break;
                case "Ожидается в":
                    result = "Ожидается";
                    break;
                case "Вылетел в":
                    result = "Ожидается";
                    break;
                case "Не вылетел.":
                    result = "Ожидается";
                    break;
            }
            return result;
        }
        public string CityHandler(string city)
        {
            string result = city;
            for (int i = 0; i < city.Length; i++)
            {
                if (city[i] == '(')
                {
                    result = city.Substring(0, i);
                }
            }
            return result;
        }
    }
}
