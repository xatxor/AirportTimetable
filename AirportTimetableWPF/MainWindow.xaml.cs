using AirportTimetable.Models;
using AirportTimetableWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        Context context;
        public Property Font = new Property();
        ObservableCollection<Flight> timetable;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //полноэкранный режим
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            timetable = new ObservableCollection<Flight>(tt.GetTimetable("departures", "Ru"));
            context = new Context(timetable);
            context.FillTimeTable(10);
            GetTimetable(timetable);
            Font = settings.Font;
        }
        private void GetTimetable(ObservableCollection<Flight> timetable)
        {
            date.Content = DateTime.Today.ToString("dd.MM.yyyy");
            time.Content = DateTime.Now.ToString("HH:mm");
            timetablegrid.Children.Clear();
            TimeTableColumn firstColumn, secondColumn, thirdColumn = new TimeTableColumn();
            timetablegrid.Children.Add(firstColumn = new TimeTableColumn());
            firstColumn.Margin = new Thickness(10, 110, 0, 0);
            firstColumn.HorizontalAlignment = HorizontalAlignment.Left;
            firstColumn.VerticalAlignment = VerticalAlignment.Top;
            timetablegrid.Children.Add(secondColumn = new TimeTableColumn());
            secondColumn.Margin = new Thickness(0, 110, 0, 0);
            secondColumn.HorizontalAlignment = HorizontalAlignment.Center;
            secondColumn.VerticalAlignment = VerticalAlignment.Top;
            timetablegrid.Children.Add(thirdColumn = new TimeTableColumn());
            thirdColumn.Margin = new Thickness(0, 110, 10, 0);
            thirdColumn.HorizontalAlignment = HorizontalAlignment.Right;
            thirdColumn.VerticalAlignment = VerticalAlignment.Top;
            firstColumn.context = context.first;
            secondColumn.context = context.second;
            thirdColumn.context = context.third;
            firstColumn.Font.Obj = Font.Obj;
            secondColumn.Font.Obj = Font.Obj;
            thirdColumn.Font.Obj = Font.Obj;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetTimetable(timetable);
        }
    }
}
