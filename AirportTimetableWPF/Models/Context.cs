using AirportTimetable.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTimetableWPF.Models
{
    public class Context : INotifyPropertyChanged
    {
        ObservableCollection<Flight> Timetable { get; set; }
        public Property Font;
        public List<Flight> first = new List<Flight>();
        public List<Flight> second = new List<Flight>();
        public List<Flight> third = new List<Flight>();
        public Context()
        {
        }
        public Context(ObservableCollection<Flight> timetable)
        {
            Timetable = timetable;
        }
        public void FillTimeTable(int rowCount)
        {
            if (Timetable.Count > rowCount)
            {
                first = Timetable.Take(rowCount).ToList();
                if (Timetable.Count <= rowCount * 2)
                    second = Timetable.Skip(rowCount).Take(Timetable.Count - rowCount).ToList();
                else
                {
                    second = Timetable.Skip(rowCount).Take(rowCount).ToList();
                    if (Timetable.Count <= rowCount * 3)
                        third = Timetable.Skip(rowCount).Take(Timetable.Count - rowCount).ToList();
                    else
                        third = Timetable.Skip(rowCount*2).Take(rowCount).ToList();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
