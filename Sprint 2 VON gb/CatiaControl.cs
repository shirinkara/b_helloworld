using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sprint_2_VON_gb
{
    class CatiaControl
    {
        CatiaControl()
        {
            try
            {

                ZaehnraderaufCatia cc = new ZaehnraderaufCatia();

                // Finde Catia Prozess
                if (cc.CatiaLaeuft())
                {


                    // Öffne ein neues Part
                    cc.ErzeugePart();


                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();

                    cc.ErzeugeProfil( );

                    cc.ErzeugeKreismuster();
                       


                    



                








                }
                else
                {
                    Console.WriteLine("Laufende Catia Application nicht gefunden");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception aufgetreten");
            }
            Console.WriteLine("Fertig - Taste drücken.");
            Console.Read();

        }

        static void Main (string[] args)
        {
            new CatiaControl();
        }
    }
}
