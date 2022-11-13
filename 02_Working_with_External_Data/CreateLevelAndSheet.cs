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

namespace _02_Working_with_External_Data
{
    [Transaction(TransactionMode.Manual)]
    public class CreateLevelAndSheet : IExternalCommand
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

            //create filtered element collector for typeID
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_TitleBlocks);
            ElementId titleBlockId = col.FirstElementId();

            Transaction trans = new Transaction(doc);

            trans.Start("Create Level and Sheet");

            //create level
            double levelHeight = ConvertMetersToFeet(3);
            Level newLevel = Level.Create(doc, levelHeight);
            newLevel.Name = "New Level";

            //create sheet
            ViewSheet newSheet = ViewSheet.Create(doc, titleBlockId);
            newSheet.Name = "New Sheet";
            newSheet.SheetNumber = "A101";

            trans.Commit();
            trans.Dispose();


            return Result.Succeeded;
        }

        //create a new method for converting meters to feet
        internal double ConvertMetersToFeet(double meters)
        {
            double feet = meters * 3.28084;

            return feet;
        }
    }
}
