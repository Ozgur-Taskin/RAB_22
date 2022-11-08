#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace _01
{
    [Transaction(TransactionMode.Manual)]
    public class CmdSkills01 : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            //Create a taskdialog
            //TaskDialog.Show("Test", "It's working");

            //Create a point
            XYZ mypoint = new XYZ(10, 10, 0);
            XYZ mypoint2 = new XYZ();

            //Create filtered element collector for textnote type id
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            //collector.OfCategory(BuiltInCategory.OST_TextNotes);
            //collector.WhereElementIsElementType();
            collector.OfClass(typeof(TextNoteType));

            Transaction trans = new Transaction(doc);
            trans.Start("Create Text Note");

            //Create a textnote
            //TextNote myTextNote = TextNote.Create(doc, doc.ActiveView.Id, mypoint, "Note", collector.FirstElementId());

            //Create multiple text notes with for loop
            XYZ offset = new XYZ(0, 2, 0);

            for (int i = 0; i <= 10; i++)
            {
                mypoint2 = mypoint2.Add(offset);
                string textNumber = i.ToString();

                TextNote myTextNote = TextNote.Create(doc,
                    doc.ActiveView.Id, mypoint2, "Note" + textNumber, collector.FirstElementId());
            }

            trans.Commit();
            trans.Dispose();

            return Result.Succeeded;
        }
    }
}
