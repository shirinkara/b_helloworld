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

namespace Sprint_2_VON_gb
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bt_1_Click(object sender, RoutedEventArgs e)
        {
           

            

            Zahnraderberechnung Zahl = new Zahnraderberechnung();
            string m, z, t, b;
            m = md_txt.Text;
            z = za_txt.Text;
            t = td_txt.Text;
            b = bb_txt.Text;

            Zahl.Umwandlung1(m);
            Zahl.Umwandlung2(z);
            Zahl.Umwandlung3(t);
            Zahl.Umwandlung4(b);


        
        }

        private void bt_2_Click(object sender, RoutedEventArgs e)
        {
            Window1 Brechnung = new Window1();

            Brechnung.Show();
        }

        private void bt_3_Click(object sender, RoutedEventArgs e)
        {
            string Message1 = md_txt.Text;
            string Message2 = za_txt.Text;
            string Message3 = td_txt.Text;
            string Message4 = bb_txt.Text;

            Window1 input = new Window1();
            input.getMessage1 = Message1;
            input.getMessage2 = Message2;
            input.getMessage3 = Message3;
            input.getMessage4 = Message4;

            input.ShowDialog();
        }
    }
}
