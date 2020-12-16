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
using System.Diagnostics;

namespace Sprint_2_VON_gb
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Geradverzahnung : Window
    {


        public string getMessage1 { get; set; }
        public string getMessage2 { get; set; }
        public string getMessage3 { get; set; }
        public string getMessage4 { get; set; }

        public Geradverzahnung()
        {
            InitializeComponent();

        }








        private void Button_Click(object sender, RoutedEventArgs e)
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

            fd_txt.Content = Zahl.fd();
            gd_txt.Content = Zahl.gd();
        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Excel ofen = new Excel();

            ofen.Exceltabelle();

            string Programmname = "wps.exe";
            Process.Start(Programmname);

            //Das jetzige fenster schlissen
            this.Close();




        }

        public void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ZaehnraderaufCatia zeichnen = new ZaehnraderaufCatia();
            //Catiabedingung
            if (zeichnen.CatiaLaeuft())
            {


                zeichnen.ErzeugePart();

                zeichnen.ErstelleLeereSkizze();

                zeichnen.ErzeugeProfil();

                zeichnen.ErzeugeKreismuster();
            }
            else
            {

                MessageBox.Show("Laufende Catia Application nicht gefunden");



                if (MessageBox.Show("Soll Catia gestartet werden ?", "Catia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //Starten der Catia App 
                    string Programmname = "CNEXT.exe";
                    Process.Start(Programmname);
                }
            }
        }
    }
}










        
