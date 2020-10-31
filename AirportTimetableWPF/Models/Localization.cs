using AirportTimetableWPF.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace AirportTimetableWPF.Models
{
    public class Localization
    {
        public Dictionary<string, string> EnCities { get; private set; }
        public Dictionary<string, string> EnStatuses { get; private set; }
        public Dictionary<string, string> ChCities { get; private set; }
        public Dictionary<string, string> ChStatuses { get; private set; }
        public Localization()
        {
            EnCities = GetTranslations(AirportTimetableWPF.Properties.Resources.EnCities);
            EnStatuses = GetTranslations(AirportTimetableWPF.Properties.Resources.EnStatuses);
            ChCities = GetTranslations(AirportTimetableWPF.Properties.Resources.ChCities);
            ChStatuses = GetTranslations(AirportTimetableWPF.Properties.Resources.ChStatuses);
        }
        private Dictionary<string, string> GetTranslations(string res)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (res != null & res != "")
            {
                string[] lines = res.Split('\n');
                foreach (var line in lines)
                {
                    var words = line.Split('*');
                    result.Add(words[0], words[1]);
                }
            }
            return result;
        }
        public string Translate(string word, string theme, string lang)
        {
            if (lang == "Ru")
                return word;
            if (lang == "En")
            {
                if (theme == "Cities")
                    return Contains(word, EnCities);
                if (theme == "Statuses")
                    return Contains(word, EnStatuses);
            }
            if (lang == "Ch")
            {
                if (theme == "Cities")
                    return Contains(word, ChCities);
                if (theme == "Statuses")
                    return Contains(word, ChStatuses);
            }
            return word;
        }
        public string Contains(string word, Dictionary<string, string> dict)
        {
            if (dict.ContainsKey(word))
                return dict.Where(e => e.Key == word).First().Value.Trim();
            return word;
        }
    }
}
