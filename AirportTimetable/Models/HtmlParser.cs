using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AirportTimetable.Models
{
    public class HtmlParser
    {
        HtmlDocument htmldoc;
        public HtmlParser(string sortOrArr)
        {
            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.UTF8;
            if (sortOrArr == "sortie")
                htmldoc = web.Load(@"http://www.vnukovo.ru/flights/online-timetable/#tab-sortie");
            else
                htmldoc = web.Load(@"http://www.vnukovo.ru/flights/online-timetable/#tab-arrivals");
        }
        public HtmlNodeCollection SelectNodes(string node)
        {
            var nodes = htmldoc.DocumentNode.SelectNodes(node);
            return nodes;
        }
    }
}
