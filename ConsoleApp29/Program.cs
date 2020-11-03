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
            Console.WriteLine("Modul");
             m = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Zähnezahl");
            double z = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Teilkreisdurchmesser");
            double d = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Breite");
            double b = Convert.ToDouble(Console.ReadLine());

            //Spezielle Eigenschaften:

            //1.Zahnhohe(Zahnfusshohe,Zahnkopfhohe):
            double c = 0.167 * m;

            double ha = m;
            Console.WriteLine("Zahnkopfhohe:" + ha);

            double hf = m + c;
            Console.WriteLine("Zahnfusshohe:" + hf);

            //2.Teilung
            double p = Math.PI * m;
            Console.WriteLine("Teilung:" + p);

            //3.Fusskreisdurchmesser
            double df = d - 2 * (m + c);
            Console.WriteLine("Fusskreisdurchmesser:" + df);

            //4.Grundkreisdurchmesser;
            double db= m * z * Math.Cos(20 * Math.PI / 180);
            Console.WriteLine("Grundkreisdurchmesser:" + db);

            Console.Read();



        }
    }
}
