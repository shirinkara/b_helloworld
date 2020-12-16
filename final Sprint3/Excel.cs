using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Sprint_2_VON_gb
{
    class Excel
    {
        _Excel.Application excelApp = new _Excel.Application();
        _Excel._Worksheet mySheet;
        _Excel.Range Xrange;
        _Excel.Borders myBorders;






        internal void Exceltabelle()
        {
            excelApp.Visible = false;
            excelApp.DisplayAlerts = false;
            excelApp.Workbooks.Add();

            mySheet = (_Excel.Worksheet)excelApp.ActiveSheet;

            mySheet.Cells[2, "a"] = "Zahnrad";

            mySheet.Cells[5, "a"] = "Uni";
            mySheet.Cells[7, "a"] = "Addresse";
            mySheet.Cells[9, "a"] = "Plz";
            mySheet.Cells[11, "a"] = "Tel.";

            mySheet.Cells[14, "A"] = "Zahnrader";
            mySheet.Cells[15, "A"] = "Modul : ";
            mySheet.Cells[16, "A"] = "Zähnezahl :";
            mySheet.Cells[17, "A"] = "Teilkreisdurchmesser :";
            mySheet.Cells[18, "A"] = "Breite :";
            mySheet.Cells[24, "a"] = "Zahnkopfhöhe :";
            mySheet.Cells[25, "a"] = "Zahnfußhöhe :";
            mySheet.Cells[26, "a"] = "Teilung :";
            mySheet.Cells[27, "a"] = "Fußkreisdurchmesser :";
            mySheet.Cells[28, "a"] = "Grundkreisdurchmesser :";
        
       

           
           

          

            mySheet.Cells[15, "c"] = "mm";
            mySheet.Cells[17, "c"] = "mm";
            mySheet.Cells[18, "c"] = "mm";
            mySheet.Cells[24, "c"] = "mm";
            mySheet.Cells[25, "c"] = "mm";
            mySheet.Cells[26, "c"] = "mm";
            mySheet.Cells[27, "c"] = "mm";
            mySheet.Cells[28, "c"] = "mm";



            mySheet.Cells[35, "a"] = "Kommentar";


            mySheet.Cells[4, "e"] = "Email";
            mySheet.Cells[5, "e"] = "Tel";

            mySheet.Cells[4, "f"] = "wei.feng.1@student.jade-hs.de";
            mySheet.Cells[5, "f"] = "0123456789";

            mySheet.Range["A2"].Font.Bold = true;
            mySheet.Range["A14"].Font.Bold = true;

            mySheet.Range["A2"].Font.Size = 27;
            mySheet.Range["A14"].Font.Size = 18;
            mySheet.Range["a2:D2"].Font.Underline = XlUnderlineStyle.xlUnderlineStyleSingle;

            mySheet.Range["A5", "A11"].Font.Size = 11;

            mySheet.Range["A15", "A18"].Font.Size = 11;
            mySheet.Range["b15", "b18"].Font.Size = 11;
            mySheet.Range["c15", "c18"].Font.Size = 11;

            mySheet.Range["A24", "A28"].Font.Size = 11;
            mySheet.Range["b24", "b28"].Font.Size = 11;
            mySheet.Range["c24", "c28"].Font.Size = 11;

            Xrange = mySheet.get_Range("A15:c18");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            Xrange = mySheet.get_Range("A24:c28");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            Xrange = mySheet.get_Range("A15:c15");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            Xrange = mySheet.get_Range("A16:c16");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            Xrange = mySheet.get_Range("A17:c17");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            Xrange = mySheet.get_Range("A18:c18");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            Xrange = mySheet.get_Range("A24:c24");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            Xrange = mySheet.get_Range("A25:c25");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            Xrange = mySheet.get_Range("A26:c26");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            Xrange = mySheet.get_Range("A27:c27");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            Xrange = mySheet.get_Range("A28:c28");
            myBorders = Xrange.Borders;
            myBorders.Weight = XlBorderWeight.xlThick;
            myBorders.Weight = 2d;

            mySheet.Range["A1", "g41"].EntireColumn.AutoFit();

        }

        internal void Berechnen(Zahnraderberechnung  Zahl0)
        {
            mySheet.Cells[15, "b"] = Zahl0.m1;
            mySheet.Cells[16, "b"] = Zahl0.z1;
            mySheet.Cells[17, "b"] = Zahl0.t1;
            mySheet.Cells[18, "b"] = Zahl0.b1;
            mySheet.Cells[24, "b"] = Zahl0.zk();
            mySheet.Cells[25, "b"] = Zahl0.zf();
            mySheet.Cells[26, "b"] = Zahl0.tt();
            mySheet.Cells[27, "b"] = Zahl0.fd();
            mySheet.Cells[28, "b"] = Zahl0.gd();


            mySheet.ExportAsFixedFormat(_Excel.XlFixedFormatType.xlTypePDF,
               " " +
                Zahl0.m1  +
                Zahl0.z1 + Zahl0.t1 +Zahl0 .b1 +Zahl0 .zk ()+Zahl0 .zf ()+Zahl0 .tt ()+Zahl0 .fd ()+Zahl0 .gd (),
               _Excel.XlFixedFormatQuality.xlQualityStandard, true, true, 1, 1, true);






            excelApp.Quit();

        }


    }
}
