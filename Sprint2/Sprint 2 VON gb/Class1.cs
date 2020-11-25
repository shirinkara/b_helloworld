using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sprint_2_VON_gb
{
    class Zahnraderberechnung

    {
        //Zahnradparameter
        public double m1 { get; set; } //Modul
        public double z1 { get; set; } //Zaehnzahl
        public double t1 { get; set; } //Teilkreisdurchmesser
        public double b1 { get; set; } //Bereite

        internal void Umwandlung1(string m)
        {
            double mx;
            if (double.TryParse(m, out mx))
            {
                m1 = mx;
            }
            else
            {
                MessageBox.Show("Fehler:Bitte geben Sie eine gueltige Zahl");
                return;

            }
        }
        internal void Umwandlung2(string z)
        {
            double zx;
            if (double.TryParse(z, out zx))
            {
                z1 = zx;
            }
            else
            {
                MessageBox.Show("Fehler:Bitte geben Sie eine gueltige Zahl");
                return;

            }
        }
        internal void Umwandlung3(string t)
        {
            double tx;
            if (double.TryParse(t, out tx))
            {
                t1 = tx;
            }
            else
            {
                MessageBox.Show("Fehler:Bitte geben Sie eine gueltige Zahl");
                return;

            }
        }
        internal void Umwandlung4(string b)
        {
            double bx;
            if (double.TryParse(b, out bx))
            {
                b1 = bx;
            }
            else
            {
                MessageBox.Show("Fehler:Bitte geben Sie eine gueltige Zahl");
                return;

            }
        }

      internal double zf()
        {
            double c = 0.167 * m1;
            double hf = m1 + c;
            return hf;

        }
      internal double zk()
        {
            double ha = m1;
            return ha;

        }
       internal double tt()
        {
            double p = Math.PI * m1;
            return p;
        }

        internal double fd()
        {
            double c = 0.167 * m1;
            double df = t1 - 2 * (m1 + c);
            return df;

        }
        
        internal double gd()
        {
            double db = m1 * z1 * Math.Cos(20 * Math.PI / 180);
            return db;
        }
    }
}
