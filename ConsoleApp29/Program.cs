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

            //3.Fußkreisdurchmesser
            double df = d - 2 * (m + c);
            Console.WriteLine("Fußkreisdurchmesser:" + df + " mm");

            //4.Grundkreisdurchmesser;
            double db= m * z * Math.Cos(20 * Math.PI / 180);
            Console.WriteLine("Grundkreisdurchmesser:" + db + " mm");

            Console.Read();



        }
    }
}
