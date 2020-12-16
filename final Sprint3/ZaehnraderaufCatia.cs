using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INFITF;
using KnowledgewareTypeLib;
using MECMOD;
using ProductStructureTypeLib;
using PARTITF;
using HybridShapeTypeLib;
using System.Windows;

namespace Sprint_2_VON_gb
{
    class ZaehnraderaufCatia
    {
        INFITF.Application hspZa_catia_App;
        MECMOD.PartDocument hspZa_catia_Part;
        MECMOD.Sketch hspZa_catia_Profil;

        //Wie man eine Feststellung auf Catia erstellen können
        public bool CatiaLaeuft()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");
                hspZa_catia_App = (INFITF.Application)catiaObject;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //Wie man eine Skizze erstellen können
        public bool ErzeugePart()
        {
            INFITF.Documents catDocuments1 = hspZa_catia_App.Documents;
            hspZa_catia_Part = catDocuments1.Add("Part") as MECMOD.PartDocument;
            return true;
        }

        //Wie man eine Skizze erstellen können
        public void ErstelleLeereSkizze()
        {
            HybridBodies catHybridBodies1 = hspZa_catia_Part.Part.HybridBodies;
            HybridBody catHybridBody1;
            try
            {
                catHybridBody1 = catHybridBodies1.Item("Geometrisches Set.1");
            }
            catch (Exception)
            {
                MessageBox.Show("Kein geometrisches Set gefunden! " + Environment.NewLine +
                    "Ein PART manuell erzeugen und ein darauf achten, dass 'Geometisches Set' aktiviert ist.",
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catHybridBody1.set_Name("Profile");
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hspZa_catia_Part.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hspZa_catia_Profil = catSketches1.Add(catReference1);

            // Achsensystem  erstellen 
            ErzeugeAchsensystem();

            // Part aktualisieren
            hspZa_catia_Part.Part.Update();
        }


        private void ErzeugeAchsensystem()
        {
            object[] arr = new object[] {0.0, 0.0, 0.0,
                                         0.0, 1.0, 0.0,
                                         0.0, 0.0, 1.0 };
            hspZa_catia_Profil.SetAbsoluteAxisData(arr);
        }

        internal   void  ErzeugeProfil(Zahnraderberechnung Zahn)
        {
           
            double tr = Zahn.m1 * Zahn.z1 / 2;
            double fr = tr - 1.25 * Zahn.m1;

            
            double hr = tr * 0.94;
            
            double kr = tr + Zahn.m1;
            double vr = 0.35 * Zahn.m1;

            //HilfsWinkel
            double alpha = 20;
            double beta = 90 / Zahn.z1;
            double beta_r = Math.PI * beta / 180;
            double gamma = 90 - (alpha - beta);
            double gamma_r = Math.PI * gamma / 180;
            double ta = 360.0 / Zahn.z1;
            double ta_r = Math.PI * ta / 180;

            

            //MittelPunkt EvolventenKreis
            double MP_EvolventenKreis_x = hr * Math.Cos(gamma_r);
            double MP_EvolventenKreis_y = hr * Math.Sin(gamma_r);

            // SchnittPunkt Evolventenkreis & Teilkreisradius
            double SP_EvolventenTeilKreis_x = -tr * Math.Sin(beta_r);
            double SP_EvolventenTeilKreis_y = tr * Math.Cos(beta_r);

            //Evolventenkreis Radius
            double Evolventenkreis_r = Math.Sqrt(Math.Pow((MP_EvolventenKreis_x - SP_EvolventenTeilKreis_x), 2) + Math.Pow((MP_EvolventenKreis_y - SP_EvolventenTeilKreis_y), 2));

            //SchnittPunkt Evolventenkreis & Kopfkreisradius
            double SP_EvolventenKopfKreis_x = Schnittpunkt_x(0, 0, kr, MP_EvolventenKreis_x, MP_EvolventenKreis_y, Evolventenkreis_r);
            double SP_EvolventenKopfKreis_y = Schnittpunkt_y(0, 0, kr, MP_EvolventenKreis_x, MP_EvolventenKreis_y, Evolventenkreis_r);

            //MittelPunkt VerrundungsRadius
            double MP_Verrundung_x = Schnittpunkt_x(0, 0, fr + vr, MP_EvolventenKreis_x, MP_EvolventenKreis_y, Evolventenkreis_r + vr);
            double MP_Verrundung_y = Schnittpunkt_y(0, 0, fr + vr, MP_EvolventenKreis_x, MP_EvolventenKreis_y, Evolventenkreis_r + vr);

            //SchnittPunkt Evolventenkreis & Verrundungsradius
            double SP_EvolventeVerrundung_x = Schnittpunkt_x(MP_EvolventenKreis_x, MP_EvolventenKreis_y, Evolventenkreis_r, MP_Verrundung_x, MP_Verrundung_y, vr);
            double SP_EvolventeVerrundung_y = Schnittpunkt_y(MP_EvolventenKreis_x, MP_EvolventenKreis_y, Evolventenkreis_r, MP_Verrundung_x, MP_Verrundung_y, vr);

            //SchnittPunkt Fußkreis & Verrundungs Radius
            double SP_FußkreisVerrundungsRadius_x = Schnittpunkt_x(0, 0, fr, MP_Verrundung_x, MP_Verrundung_y, vr);
            double SP_FußkreisVerrundungsRadius_y = Schnittpunkt_y(0, 0, fr, MP_Verrundung_x, MP_Verrundung_y, vr);

            //StartPunkt Fußkreis Radius
            double phi = ta_r - Math.Atan(Math.Abs(SP_FußkreisVerrundungsRadius_x) / Math.Abs(SP_FußkreisVerrundungsRadius_y));
            double StartPkt_Fußkreis_x = -fr * Math.Sin(phi);
            double StartPkt_Fußkreis_y = fr * Math.Cos(phi);

            // Skizze umbenennen
            hspZa_catia_Profil.set_Name("Außen");
            Factory2D catFactory2D1 = hspZa_catia_Profil.OpenEdition();

            Point2D catP2D_Ursprung = catFactory2D1.CreatePoint(0, 0);

            Point2D catP2D_StartPkt_Fußkreis = catFactory2D1.CreatePoint(StartPkt_Fußkreis_x, StartPkt_Fußkreis_y);
            Point2D catP2D_SP_FußkreisVerrundungsRadius1 = catFactory2D1.CreatePoint(SP_FußkreisVerrundungsRadius_x, SP_FußkreisVerrundungsRadius_y);
            Point2D catP2D_SP_FußkreisVerrundungsRadius2 = catFactory2D1.CreatePoint(-SP_FußkreisVerrundungsRadius_x, SP_FußkreisVerrundungsRadius_y);

            Point2D catP2D_MP_EvolventenKreis1 = catFactory2D1.CreatePoint(MP_EvolventenKreis_x, MP_EvolventenKreis_y);
            Point2D catP2D_MP_EvolventenKreis2 = catFactory2D1.CreatePoint(-MP_EvolventenKreis_x, MP_EvolventenKreis_y);

            Point2D catP2D_SP_EvolventenKopfKreis1 = catFactory2D1.CreatePoint(SP_EvolventenKopfKreis_x, SP_EvolventenKopfKreis_y);
            Point2D catP2D_SP_EvolventenKopfKreis2 = catFactory2D1.CreatePoint(-SP_EvolventenKopfKreis_x, SP_EvolventenKopfKreis_y);

            Point2D catP2D_MP_Verrundung1 = catFactory2D1.CreatePoint(MP_Verrundung_x, MP_Verrundung_y);
            Point2D catP2D_MP_Verrundung2 = catFactory2D1.CreatePoint(-MP_Verrundung_x, MP_Verrundung_y);

            Point2D catP2D_SP_EvolventeVerrundung1 = catFactory2D1.CreatePoint(SP_EvolventeVerrundung_x, SP_EvolventeVerrundung_y);
            Point2D catP2D_SP_EvolventeVerrundung2 = catFactory2D1.CreatePoint(-SP_EvolventeVerrundung_x, SP_EvolventeVerrundung_y);


            //Kreise
            Circle2D catC2D_Frußkreis = catFactory2D1.CreateCircle(0, 0, fr, 0, 0);
            catC2D_Frußkreis.CenterPoint = catP2D_Ursprung;
            catC2D_Frußkreis.StartPoint = catP2D_SP_FußkreisVerrundungsRadius1;
            catC2D_Frußkreis.EndPoint = catP2D_StartPkt_Fußkreis;

            Circle2D catC2D_Kopfkreis = catFactory2D1.CreateCircle(0, 0,kr, 0, 0);
            catC2D_Kopfkreis.CenterPoint = catP2D_Ursprung;
            catC2D_Kopfkreis.StartPoint = catP2D_SP_EvolventenKopfKreis2;
            catC2D_Kopfkreis.EndPoint = catP2D_SP_EvolventenKopfKreis1;

            Circle2D catC2D_EvolventenKreis1 = catFactory2D1.CreateCircle(MP_EvolventenKreis_x, MP_EvolventenKreis_y, Evolventenkreis_r, 0, 0);
            catC2D_EvolventenKreis1.CenterPoint = catP2D_MP_EvolventenKreis1;
            catC2D_EvolventenKreis1.StartPoint = catP2D_SP_EvolventenKopfKreis1;
            catC2D_EvolventenKreis1.EndPoint = catP2D_SP_EvolventeVerrundung1;

            Circle2D catC2D_Evolventenkreis2 = catFactory2D1.CreateCircle(-MP_EvolventenKreis_x, MP_EvolventenKreis_y, Evolventenkreis_r, 0, 0);
            catC2D_Evolventenkreis2.CenterPoint = catP2D_MP_EvolventenKreis2;
            catC2D_Evolventenkreis2.StartPoint = catP2D_SP_EvolventeVerrundung2;
            catC2D_Evolventenkreis2.EndPoint = catP2D_SP_EvolventenKopfKreis2;

            Circle2D catC2D_VerrundungsKreis1 = catFactory2D1.CreateCircle(MP_Verrundung_x, MP_Verrundung_y, vr, 0, 0);
            catC2D_VerrundungsKreis1.CenterPoint = catP2D_MP_Verrundung1;
            catC2D_VerrundungsKreis1.StartPoint = catP2D_SP_FußkreisVerrundungsRadius1;
            catC2D_VerrundungsKreis1.EndPoint = catP2D_SP_EvolventeVerrundung1;

            Circle2D catC2D_VerrundungsKreis2 = catFactory2D1.CreateCircle(-MP_Verrundung_x, MP_Verrundung_y, vr, 0, 0);
            catC2D_VerrundungsKreis2.CenterPoint = catP2D_MP_Verrundung2;
            catC2D_VerrundungsKreis2.StartPoint = catP2D_SP_EvolventeVerrundung2;
            catC2D_VerrundungsKreis2.EndPoint = catP2D_SP_FußkreisVerrundungsRadius2;



            hspZa_catia_Profil.CloseEdition();

            hspZa_catia_Part.Part.Update();
        }



       

        private double Schnittpunkt_x(double MP1_x, double MP1_y, double r1, double MP2_x, double MP2_y, double r2)
        {
            double d = Math.Sqrt(Math.Pow((MP1_x - MP2_x), 2) + Math.Pow((MP1_y - MP2_y), 2));
            double l = (Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(d, 2)) / (d * 2);
            double h;
            double ii = 0.00001;

            if (r1 - l < -ii)
            {
                MessageBox.Show("Fehler!\nBitte überprüfen Sie die Eingangsparameter.");
            }
            if (Math.Abs(r1 - l) < ii)
            {
                h = 0;
            }
            else
            {
                h = Math.Sqrt(Math.Pow(r1, 2) - Math.Pow(l, 2));
            }

            return l * (MP2_x - MP1_x) / d - h * (MP2_y - MP1_y) / d + MP1_x;
        }
        private double Schnittpunkt_y(double MP1_x, double MP1_y, double r1, double MP2_x, double MP2_y, double r2)
        {
            double d = Math.Sqrt(Math.Pow((MP1_x - MP2_x), 2) + Math.Pow((MP1_y - MP2_y), 2));
            double l = (Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(d, 2)) / (d * 2);
            double h;
            double ii = 0.00001;

            if (r1 - l < -ii)
            {
                MessageBox.Show("Fehler!\nBitte überprüfen Sie die Eingangsparameter.");
            }
            if (Math.Abs(r1 - l) < ii)
            {
                h = 0;
            }
            else
            {
                h = Math.Sqrt(Math.Pow(r1, 2) - Math.Pow(l, 2));
            }

            return l * (MP2_y - MP1_y) / d + h * (MP2_x - MP1_x) / d + MP1_y;
        }

        internal  void ErzeugeKreismuster(Zahnraderberechnung Zahn)
        {


            //Kreismuster
            ShapeFactory SF = (ShapeFactory)hspZa_catia_Part.Part.ShapeFactory;
            HybridShapeFactory HSF = (HybridShapeFactory)hspZa_catia_Part.Part.HybridShapeFactory;
            Part myPart = hspZa_catia_Part.Part;


            //Referenzen und Skizze
            Factory2D Factory2D1 = hspZa_catia_Profil.Factory2D;

            HybridShapePointCoord Ursprung = HSF.AddNewPointCoord(0, 0, 0);
            Reference RefUrsprung = myPart.CreateReferenceFromObject(Ursprung);
            HybridShapeDirection XDir = HSF.AddNewDirectionByCoord(1, 0, 0);
            Reference RefXDir = myPart.CreateReferenceFromObject(XDir);


            //Kreismuster mit daten füllen
            CircPattern Kreismuster = SF.AddNewSurfacicCircPattern(Factory2D1, 1, 2, 0, 0, 1, 1, RefUrsprung, RefXDir, false, 0, true, false);
            Kreismuster.CircularPatternParameters = CatCircularPatternParameters.catInstancesandAngularSpacing;
            AngularRepartition angularRepartition1 = Kreismuster.AngularRepartition;
            Angle angle1 = angularRepartition1.AngularSpacing;
            angle1.Value = Convert.ToDouble(360 /Zahn.z1 );
            AngularRepartition angularRepartition2 = Kreismuster.AngularRepartition;
            IntParam intParam1 = angularRepartition2.InstancesCount;
            intParam1.Value = Convert.ToInt32(Zahn.z1 ) + 1;


            // Geschlossene kontur
            Reference Ref_Kreismuster = myPart.CreateReferenceFromObject(Kreismuster);
            HybridShapeAssemble Verbindung = HSF.AddNewJoin(Ref_Kreismuster, Ref_Kreismuster);
            Reference Ref_Verbindung = myPart.CreateReferenceFromObject(Verbindung);

            HSF.GSMVisibility(Ref_Verbindung, 0);

            myPart.Update();

            //hspge_catiaPart.Part.Update();

            Bodies bodies = myPart.Bodies;
            Body myBody = bodies.Add();
            myBody.set_Name("Zahnrad");
            myBody.InsertHybridShape(Verbindung);

            myPart.Update();
            //hspge_catiaPart.Part.Update();


            myPart.InWorkObject = myBody;
            Pad myPad = SF.AddNewPadFromRef(Ref_Verbindung, Zahn.b1 );

            myPart.Update();



        }
    }


}
