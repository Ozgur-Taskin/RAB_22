#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace _05_Inserting_Families
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
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

            //call the static class and static method
            //it's a static class we don't need to create an instance of the class
            Utils.StaticMethodTest("This is the value");

            //insert family
            //we willinsert the family to the room point
            //filter rooms
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_Rooms);

            //transaction
            using(Transaction t = new Transaction(doc))
            {
                t.Start("Inset Family");
                //get location point and insert a family to all the rooms
                foreach (SpatialElement room in col)
                {
                    //get room location
                    Location loc = room.Location;
                    //get the location point
                    //room has locationpoint 
                    LocationPoint locPoint = loc as LocationPoint;
                    //assignt the location point value to a variable to use it inserting family
                    XYZ roomPoint = locPoint.Point;

                    //get family symbol
                    FamilySymbol myFS = Utils.GetFamilySymbolByName_Family(doc, "Furniture_Desk", "1830x915mm");
                    //place family
                    FamilyInstance myInstance = doc.Create.NewFamilyInstance(roomPoint, 
                        myFS, StructuralType.NonStructural);

                    //set parameter value
                    Utils.SetParameterValue(room, "Ceiling Finish", "ACT");
                    string roomName =  Utils.GetParameterValueAsString(room, "Name");
                    TaskDialog.Show("Room Name", roomName);
                }

                t.Commit();
                //don't need to dispose, it will dipose automatically
            }
            return Result.Succeeded;
        }
    }
}
