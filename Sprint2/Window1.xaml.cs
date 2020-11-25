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
using System.Windows.Shapes;

namespace Sprint_2_VON_gb
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
           
        }
        public string getMessage1 { get; set; }
        public string getMessage2 { get; set; }
        public string getMessage3 { get; set; }
        public string getMessage4 { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            b1_txt.Text = getMessage1;
            b2_txt.Text = getMessage2;
            b3_txt.Text = getMessage3;
            b4_txt.Text = getMessage4;

            Zahnraderberechnung Zahl = new Zahnraderberechnung();
            string m, z, t, b;

            m = getMessage1;
            z = getMessage2;
            t = getMessage3;
            b = getMessage4;

            Zahl.Umwandlung1(m);
            Zahl.Umwandlung2(z);
            Zahl.Umwandlung3(t);
            Zahl.Umwandlung4(b);

            zf_txt.Content = Zahl.zf();
            zk_txt.Content = Zahl.zk();
            tt_txt.Content = Zahl.tt();
            zf_txt.Content = Zahl.zf();
            fd_txt.Content = Zahl.fd();
            gd_txt.Content = Zahl.gd();

        }





    }
}
