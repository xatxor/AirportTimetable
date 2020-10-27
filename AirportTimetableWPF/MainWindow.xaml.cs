using AirportTimetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        List<Flight> timetable;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //полноэкранный режим
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            date.Content = DateTime.Today.ToString("dd.MM.yyyy");
            time.Content = DateTime.Now.ToString("HH:mm");
            timetable = tt.GetTimetable("departures").ToList();
            firstColumn.DataContext = timetable;
            if (timetable.Count > 23)
            {
                firstColumn.DataContext = timetable.GetRange(0, 23);
                if (timetable.Count <= 46)
                    secondColumn.DataContext = timetable.GetRange(23, timetable.Count - 23);
                else
                {
                    secondColumn.DataContext = timetable.GetRange(23, 23);
                    if (timetable.Count <= 69)
                        thirdColumn.DataContext = timetable.GetRange(46, timetable.Count - 46);
                    else
                        thirdColumn.DataContext = timetable.GetRange(46, 23);
                }
            }
        }
    }
}
