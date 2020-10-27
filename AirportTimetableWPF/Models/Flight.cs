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
        public string TimeString { get; }
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
            TimeString = time.ToString("HH:mm");
        }
    }
}
