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
        public string getName1 { get; set; }
        public string getName2{ get; set; }
        public string getName3 { get; set; }
        public string getName4 { get; set; }
        public Window1()
        {
            InitializeComponent();
           
        }

        private void SetNum_Click(object sender, RoutedEventArgs e)
        {
            b1_txt.Text = getName1;
            b2_txt.Text = getName2;
            b3_txt.Text = getName3;
            b4_txt.Text = getName4;

            Zahnraderberechnung Zahl = new Zahnraderberechnung();
            string m, z, t, b;

            m = b1_txt.Text;
            z = b2_txt.Text;
            t = b3_txt .Text ;
            b = b4_txt.Text;

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
