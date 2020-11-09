using AirportTimetable.Models;
using AirportTimetableWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Configuration;
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
using System.Windows.Threading;

namespace AirportTimetableWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TimetableHandler tt = new TimetableHandler();
        Context context;
        ObservableCollection<Flight> timetable;
        public Property font = new Property();
        public Property rowCount = new Property();
        public Property loadInterval;
        public Property showInterval;
        public Property inSpan;
        public Property outSpan;
        private int loadEnumerator = 1;
        Timer loadTimer;
        Timer showTimer;
        string title;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //полноэкранный режим
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            font = settings.font;
            rowCount = settings.rowCount;
            showInterval = settings.showInterval;
            loadInterval = settings.loadInterval;
            inSpan = settings.inSpan;
            outSpan = settings.outSpan;
            loadTimer = new Timer(loadInterval.Obj);
            showTimer = new Timer(showInterval.Obj);
            LoadTimetable(null, null);
            context = new Context(timetable);
            ShowTimetable(null, null);
            loadTimer.Elapsed += LoadTimetable;
            showTimer.Elapsed += ShowTimetable;
            loadTimer.Start();
            showTimer.Start();
            settings.Visibility = Visibility.Hidden;
        }
        private void LoadTimetable(Object source, ElapsedEventArgs e)
        {
            loadTimer.Interval = loadInterval.Obj;
            string lang = "Ru";
            string depOrArr = "departures";
            int hours = 0;
            switch (loadEnumerator)
            {
                case 1:
                    lang = "Ru";
                    depOrArr = "departures";
                    hours = outSpan.Obj;
                    loadEnumerator++;
                    title = "Вылеты";
                    break;
                case 2:
                    lang = "Ru";
                    depOrArr = "arrivals";
                    hours = inSpan.Obj;
                    loadEnumerator++;
                    title = "Прилеты";
                    break;
                case 3:
                    lang = "En";
                    depOrArr = "departures";
                    hours = outSpan.Obj;
                    loadEnumerator++;
                    title = "Departures";
                    break;
                case 4:
                    lang = "En";
                    depOrArr = "arrivals";
                    hours = inSpan.Obj;
                    loadEnumerator++;
                    title = "Arrivals";
                    break;
                case 5:
                    lang = "Ch";
                    depOrArr = "departures";
                    hours = outSpan.Obj;
                    loadEnumerator++;
                    title = "出港";
                    break;
                case 6:
                    lang = "Ch";
                    depOrArr = "arrivals";
                    hours = inSpan.Obj;
                    loadEnumerator = 1;
                    title = "到达";
                    break;
            }
            timetable = new ObservableCollection<Flight>(tt.GetTimetable(depOrArr, lang, hours));
            context = new Context(timetable);
        }
        private void ShowTimetable(Object source, ElapsedEventArgs e)
        {
            showTimer.Interval = showInterval.Obj;
            mainWindow.Dispatcher.Invoke(() =>
            {
                titleLbl.Content = title;
                context.FillTimeTable(rowCount.Obj);
                date.Content = DateTime.Today.ToString("dd.MM.yyyy");
                time.Content = DateTime.Now.ToString("HH:mm");
//                timetablegrid.Children.Clear();
//                TimeTableColumn firstColumn, secondColumn, thirdColumn = new TimeTableColumn();
 //               timetablegrid.Children.Add(firstColumn = new TimeTableColumn());
//                firstColumn.Margin = new Thickness(10, 110, 0, 0);
  //              firstColumn.HorizontalAlignment = HorizontalAlignment.Left;
    //            firstColumn.VerticalAlignment = VerticalAlignment.Top;
  //              timetablegrid.Children.Add(secondColumn = new TimeTableColumn());
   //             secondColumn.Margin = new Thickness(0, 110, 0, 0);
     //           secondColumn.HorizontalAlignment = HorizontalAlignment.Center;
       //         secondColumn.VerticalAlignment = VerticalAlignment.Top;
    //            timetablegrid.Children.Add(thirdColumn = new TimeTableColumn());
   //             thirdColumn.Margin = new Thickness(0, 110, 10, 0);
     //           thirdColumn.HorizontalAlignment = HorizontalAlignment.Right;
       //         thirdColumn.VerticalAlignment = VerticalAlignment.Top;
                firstColumn.context = context.first;
                secondColumn.context = context.second;
                thirdColumn.context = context.third;
                firstColumn.font.Obj = font.Obj;
                secondColumn.font.Obj = font.Obj;
                thirdColumn.font.Obj = font.Obj;
                firstColumn.rowCount.Obj = rowCount.Obj;
                secondColumn.rowCount.Obj = rowCount.Obj;
                thirdColumn.rowCount.Obj = rowCount.Obj;
            });
        }
        private void Window_Closed(object sender, EventArgs e)
        {
        }
        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (settings.Visibility == Visibility.Visible)
                settings.Visibility = Visibility.Hidden;
            else
                settings.Visibility = Visibility.Visible;
        }
    }
}
