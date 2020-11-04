using AirportTimetableWPF.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AirportTimetable.Models
{
    public class TimetableHandler
    {
        Localization local = new Localization();
        public List<Flight> GetFlightsFromNodes(HtmlNodeCollection nodes, string lang)
        {
            Localization local = new Localization();
            List<Flight> flights = new List<Flight>();
            for (int i = 0; i < nodes.Count-1; i++)
            {
                var tds = nodes[i].SelectNodes(".//td");
                DateTime dt = new DateTime();
                string name = "";
                string company = "";
                string logopath = "";
                string city = "";
                char terminal = ' ';
                string status = "";
                for (int j = 0; j < tds.Count; j++)
                {
                    var current = tds[j].InnerText.Trim();
                    switch (j) //если возникает ошибка, значит нужной записи нет - устанавливаем значение след свойству
                    {
                        case 0:
                            try
                            {
                                string time = current;
                                dt = TimeHandler(time);
                            }
                            catch (Exception e) { name = current; break; }
                            break;
                        case 1:
                            if (name == "")
                                name = current;
                            break;
                        case 2:
                            try
                            {
                                var img = tds[j].
                                    SelectSingleNode(".//img");
                                if (img != null)
                                    logopath = img.Attributes["src"].Value;
                                else
                                    city = CityHandler(current);
                            }
                            catch (Exception e) { break; }
                            break;
                        case 3:
                            if (city == "")
                            {
                                try
                                {
                                    if (current.Count() == 1)
                                        terminal = current[0];
                                    else
                                        city = CityHandler(current);
                                }
                                catch (Exception e) { break; }
                            }
                            break;
                        case 4:
                            if (terminal == ' ') {
                                try
                                {
                                    if (current.Count() == 1)
                                        terminal = current[0];
                                    else
                                        status = StatusHandler(current);
                                }
                                catch (Exception e) { break; }
                            }
                            break;
                        case 5:
                            if (status == "")
                            {
                                try
                                {
                                    status = StatusHandler(current);
                                }
                                catch (Exception e) { break; }
                            }
                            break;
                    }
                }
                Flight flight = new Flight(
                    dt,
                    name,
                    company,
                    logopath,
                    local.Translate(city, "Cities", lang),
                    terminal,
                    local.Translate(status, "Statuses", lang));
                flights.Add(flight);
            }
            return flights;
        }
        public IEnumerable<Flight> GetTimetable(string depOrArr, string lang, int hours)
        {
            HtmlParser parser = new HtmlParser(depOrArr);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(parser.Node.InnerHtml);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//tr");
            IEnumerable<Flight> flights = GetFlightsFromNodes(nodes, lang).
                Where(e => e.Time > DateTime.Now.AddHours(hours));
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
            if (status.Split(' ').Length < 2)
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
