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
    public partial class Geradverzahnung0 : Window
    {
        public Geradverzahnung0()
        {
            InitializeComponent();
        }

     

        private void bt_3_Click(object sender, RoutedEventArgs e)
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

            string Message1 = md_txt.Text.ToString();
            string Message2 = za_txt.Text.ToString();
            string Message3 = td_txt.Text.ToString();
            string Message4 = bb_txt.Text.ToString();

            Geradverzahnung input = new Geradverzahnung();
            input.getMessage1 = Message1;
            input.getMessage2 = Message2;
            input.getMessage3 = Message3;
            input.getMessage4 = Message4;

            input.ShowDialog();

            Geradverzahnung Brechnung = new Geradverzahnung();

            Brechnung.Show();
        }
       

    }
}
