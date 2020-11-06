using AirportTimetableWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : UserControl, INotifyPropertyChanged
    {
        public Property font = new Property();
        public Property rowCount = new Property(10);
        public Property showInterval = new Property(5000);
        public Property loadInterval = new Property(5000);
        public Property inSpan = new Property(0);
        public Property outSpan = new Property(0);
        public Settings()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            slider1.DataContext = font.Obj;
            showInt.Text = Convert.ToString(showInterval.Obj / 1000);
            loadInt.Text = Convert.ToString(loadInterval.Obj / 1000);
            inSp.Text = inSpan.Obj.ToString();
            outSp.Text = outSpan.Obj.ToString();
            rowCnt.Text = rowCount.Obj.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            font.Obj = (int)slider1.Value;
            rowCount.Obj = Convert.ToInt32(rowCnt.Text);
            showInterval.Obj = Convert.ToInt32(showInt.Text) * 1000;
            loadInterval.Obj = Convert.ToInt32(loadInt.Text) * 1000;
            inSpan.Obj = Convert.ToInt32(inSp.Text);
            outSpan.Obj = Convert.ToInt32(outSp.Text);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
