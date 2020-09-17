using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AirportTimetable.Models
{
    public class HtmlParser
    {
        HtmlDocument htmldoc;
        public HtmlNode Node;
        public HtmlParser(string depOrArr)
        {
            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.UTF8;
            htmldoc = web.Load(@"http://www.vnukovo.ru/flights/online-timetable/#tab-arrivals");
            if (depOrArr == "departures")
                Node = htmldoc.GetElementbyId("sortie");
            else
                Node = htmldoc.GetElementbyId("arrivals");
        }
        public HtmlNodeCollection SelectNodes(string node)
        {
            var nodes = htmldoc.DocumentNode.SelectNodes(node);
            return nodes;
        }
    }
}
