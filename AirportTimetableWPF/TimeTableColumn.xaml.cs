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
        public List<Flight> context = new List<Flight>();
        public Property font = new Property();
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
            list.DataContext = font;
            list.ItemsSource = context;
        }
    }
}
