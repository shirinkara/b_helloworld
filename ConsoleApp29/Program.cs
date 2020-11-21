using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp29
{
    class Program
    {

        static void Main(string[] args)
        {

            //Zahnradparameter:
            Console.WriteLine("Bitte geben Sie den Modul an in [mm]");
            double m = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Bite geben Sie die Zähnezahl an");
            double z = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Bitte geben Sie den Teilkreisdurchmesser in [mm] an");
            double d = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Bitte geben Sie die Breite in [mm] an");
            double b = Convert.ToDouble(Console.ReadLine());


            //Spezielle Eigenschaften:

            //1.Zahnhöhe(Zahnfußhöhe,Zahnkopfhöhe):
            double c = 0.167 * m;

            double ha = m;
            Console.WriteLine("Zahnkopfhöhe:" + ha + " mm");

            double hf = m + c;
            Console.WriteLine("Zahnfußhöhe:" + hf + " mm");

            //2.Teilung
            double p = Math.PI * m;
            Console.WriteLine("Teilung:" + p + " mm");

            //3.Grundkreisdurchmesser;
            double db = m * z * Math.Cos(20 * Math.PI / 180);
            Console.WriteLine("Grundkreisdurchmesser:" + db + " mm");

            //4.Kopfkreisdurchmesser außenverzahnt;
            double da = m * (z + 2);
            Console.WriteLine("Kopfkreisdurchmesser:" + da + " mm");

            //5.Fußkreisdurchmesser außenverzahnt;
            double df = d - 2 * (m + c);
            Console.WriteLine("Fußkreisdurchmesser:" + df + " mm");

            //Kopfkkreisdurchmesser innenverzahnt;
            double dai = m * (z - 2);
            Console.WriteLine("Kopfkreisdurchmesser innenverzahnt:" + dai + " mm");

            //Fußkreisdurchmesser innenverzahnt;
            double dfi = d + 2 * (m + c);
            Console.WriteLine("Fußkreisdurchmesser innenverzahnt:" + dfi + " mm");

            //Volumen außenverzahnt;
            double va = Math.Pow(da / 2, 2) * b;
            Console.WriteLine("Volumen außenverzahnt:" + va + " mm^3");

            //Volumen innenverzahnt;
            double vi = Math.Pow(dai / 2, 2) * b;
            Console.WriteLine("Volumen innenverzahnt:" + vi + " mm^3");

            //Achsabstand mit außenliegendes Gegenrad;
            

        
        } 








    }
}
