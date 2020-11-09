using AirportTimetable.Models;
using AirportTimetableWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
    /// Логика взаимодействия для TimeTableColumn.xaml
    /// </summary>
    public partial class TimeTableColumn : UserControl
    {
        public static readonly DependencyProperty FontProperty;
        public static readonly DependencyProperty RowsProperty;

        static TimeTableColumn()
        {
            FontProperty = DependencyProperty.Register("Font", typeof(int), typeof(TimeTableColumn));
            RowsProperty = DependencyProperty.Register("Rows", typeof(int), typeof(TimeTableColumn));
        }
        public int Font
        {
            get { return (int)GetValue(FontProperty); }
            set { SetValue(FontProperty, value);
                list.DataContext = value;
                list.FontSize = value;
            }
        }
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }


        public List<Flight> context = new List<Flight>();
        public Property rowCount = new Property();
        public Property rowHeight = new Property(100);
        public TimeTableColumn()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = 890 / rowCount.Obj;
            rowHeight.Obj = (int)temp;
            Row.DataContext = rowHeight.Obj;
            list.ItemsSource = context;
        }
        public void SetFlights(List<Flight> flights)
        {
            context = flights;
            list.ItemsSource = context;
        }

    }
}
