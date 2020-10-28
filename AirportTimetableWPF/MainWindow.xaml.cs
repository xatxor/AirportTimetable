using AirportTimetable.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirportTimetableWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TimetableHandler tt = new TimetableHandler();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //полноэкранный режим
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            ObservableCollection<Flight> timetable = new ObservableCollection<Flight>(tt.GetTimetable("departures"));
            GetTimetable(timetable);
        }
        private void GetTimetable(ObservableCollection<Flight> timetable)
        {
            date.Content = DateTime.Today.ToString("dd.MM.yyyy");
            time.Content = DateTime.Now.ToString("HH:mm");
            firstColumn.DataContext = timetable;
            if (timetable.Count > 23)
            {
                firstColumn.DataContext = timetable.Take(23);
                if (timetable.Count <= 46)
                    secondColumn.DataContext = timetable.Skip(23).Take(timetable.Count - 23);
                else
                {
                    secondColumn.DataContext = timetable.Skip(23).Take(23);
                    if (timetable.Count <= 69)
                        thirdColumn.DataContext = timetable.Skip(46).Take(timetable.Count - 23);
                    else
                        thirdColumn.DataContext = timetable.Skip(46).Take(23);
                }
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
        }
    }
}
