using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using INFITF;
using MECMOD;
using PARTITF;
using HybridShapeTypeLib;
using KnowledgewareTypeLib;
using ProductStructureTypeLib;




namespace MinimalCatia
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

        public void ErzeugeProfil(Double df, Double h)
        {
            // Skizze umbenennen
            hspZa_catia_Profil.set_Name("Rechteck");

            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = hspZa_catia_Profil.OpenEdition();

            //Endpunkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(-3.470708, 39.221739);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(3.470708, 39.221739);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(-1.168219, 47.235556);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(1.168219, 47.235556);

            //Kreise unten
            Circle2D circle2D1 = catFactory2D1.CreateClosedCircle(0.000000, 0.000000, df);
            circle2D1.StartPoint = catPoint2D1;
            circle2D1.EndPoint = catPoint2D2;

            //Kreise oben
            Circle2D circle2D2 = catFactory2D1.CreateClosedCircle(0.000000, 0.000000, 47.25);
            circle2D2.StartPoint = catPoint2D4;
            circle2D2.EndPoint = catPoint2D3;

            //Kreise links
            Circle2D circle2D3 = catFactory2D1.CreateClosedCircle(11.611292, 39.451787, 14.963387);
            circle2D3.StartPoint = catPoint2D3;
            circle2D3.EndPoint = catPoint2D1;

            //Kreise rechts
            Circle2D circle2D4 = catFactory2D1.CreateClosedCircle(-11.611292, 39.451787, 14.963387);
            circle2D4.StartPoint = catPoint2D2;
            circle2D4.EndPoint = catPoint2D4;

            // Skizzierer verlassen
            hspZa_catia_Profil.CloseEdition();
            // Part aktualisieren
            hspZa_catia_Part.Part.Update();
        }

        public void ErzeugeBalken(Double l)
        {
            // Hauptkoerper in Bearbeitung definieren
            hspZa_catia_Part.Part.InWorkObject = hspZa_catia_Part.Part.MainBody;

            // Block(Balken) erzeugen
            ShapeFactory catShapeFactory1 = (ShapeFactory)hspZa_catia_Part.Part.ShapeFactory;
            Pad catPad1 = catShapeFactory1.AddNewPad(hspZa_catia_Profil, l);

            // Block umbenennen
            catPad1.set_Name("Balken");

            // Part aktualisieren
            hspZa_catia_Part.Part.Update();
        }
       
        public void ErzeugeMuster(double Zaehnezahl)
        {
            ShapeFactory SF = (ShapeFactory)hspZa_catia_Part.Part.ShapeFactory;
            HybridShapeFactory HSF = (HybridShapeFactory)hspZa_catia_Part.Part.HybridShapeFactory;
            Part myPart = hspZa_catia_Part.Part;

            Factory2D Factory2D1 = hspZa_catia_Profil.Factory2D;
            HybridShapePointCoord Ursprung = HSF.AddNewPointCoord(0, 0, 0);
            Reference RefUrsprung = myPart.CreateReferenceFromObject(Ursprung);
            HybridShapeDirection XDir = HSF.AddNewDirectionByCoord(1, 0, 0);
            Reference RefXDir = myPart.CreateReferenceFromObject(XDir);

            CircPattern Kreismuster = SF.AddNewSurfacicCircPattern(Factory2D1, 1, 2, 0, 0, 1, 1, RefUrsprung, RefXDir, false, 0, true, false);
            Kreismuster.CircularPatternParameters = CatCircularPatternParameters.catInstancesandAngularSpacing;
            AngularRepartition angularRepartition1 = Kreismuster.AngularRepartition;
            Angle angle1 = angularRepartition1.AngularSpacing;
            angle1.Value = Convert.ToDouble(360 / Convert.ToDouble(Zaehnezahl));
            AngularRepartition angularRepartition2 = Kreismuster.AngularRepartition;
            IntParam intParam1 = angularRepartition2.InstancesCount;
            intParam1.Value = Convert.ToInt32(Zaehnezahl) + 1;

            Reference Ref_Kreismuster = myPart.CreateReferenceFromObject(Kreismuster);
            HybridShapeAssemble Verbindung = HSF.AddNewJoin(Ref_Kreismuster, Ref_Kreismuster);
            Reference Ref_Verbindung = myPart.CreateReferenceFromObject(Verbindung);
            HSF.GSMVisibility(Ref_Verbindung, 0);
            myPart.Update();
            Bodies bodies = myPart.Bodies;
            Body myBody = bodies.Add();
            myBody.set_Name("Zahnrad");
            myBody.InsertHybridShape(Verbindung);

            myPart.Update();
        }
    }

    }





    
