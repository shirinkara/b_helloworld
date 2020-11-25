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

        private void bt_3_Click(object sender, RoutedEventArgs e)
        {
            string name1 = md_txt.Text.ToString();
            string name2 = za_txt.Text.ToString();
            string name3 = td_txt.Text.ToString();
            string name4 = bb_txt.Text.ToString();

            Window1 about = new Window1();
            about.getName1 = name1;
            about.getName2 = name2;
            about.getName3 = name3;
            about.getName4 = name4;
            about.ShowDialog();
        }

       
    }
}
