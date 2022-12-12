#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

#endregion

namespace _05_Challenge
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

            //insert families to room location point
            //filter rooms
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_Rooms);

            //get furniture set
            List<string[]> furnitureSetList = Utils.GetFurnitureSets();

            //get furniture types
            List<string[]> furnitureTypesList = Utils.GetFurnitureTypes();

            //create a list for the list items
            string furnitureSetListItems = "";

            //create a string array for funitures
            string[] tempFurnitureListArray;
            List<string>  furnitureList = new List<string>();

            //create variables for families
            string revFamName = "";
            string revFamType = "";

            //transaction
            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Insert Family");


                //get all the room location poins
                foreach(SpatialElement room in col)
                {
                    //get room location
                    Location loc = room.Location;
                    //get location point
                    LocationPoint locPoint = loc as LocationPoint;
                    //assign location point value to a variable to use later
                    XYZ roomPoint = locPoint.Point;

                    //read furniture set from the revit room
                    string furnitureSetModelItem = Utils.GetParameterValueAsString(room, "Furniture Set");
                    if(furnitureSetModelItem != null && furnitureSetModelItem != "-")
                    {
                        foreach (string[] fSet in furnitureSetList)
                        {
                            //check if the furniture set in list matching with room furniture set in model
                            if (fSet[0] == furnitureSetModelItem)
                            {
                                //get furnitures from furniture set list
                                furnitureSetListItems = fSet[2];

                                //split furniture list
                                tempFurnitureListArray = furnitureSetListItems.Split(',');

                                //trim spaces
                                foreach (string fl in tempFurnitureListArray)
                                {                                    
                                    furnitureList.Add(fl.Trim());
                                }

                                //get items in the furniture list from furniture set list
                                foreach (string furnitureName in furnitureList)
                                {
                                    //check for a matching furniture name
                                    foreach (string[] furniture in furnitureTypesList)
                                    {
                                        if (furnitureName.Equals(furniture[0]))
                                        {
                                            revFamName = furniture[1];
                                            revFamType = furniture[2];

                                            //get family symbol
                                            FamilySymbol famSym = Utils.GetFamilySymbolByName(doc,
                                                revFamName, revFamType);

                                            //place family
                                            FamilyInstance newInstance = doc.Create.NewFamilyInstance(roomPoint, famSym,
                                                StructuralType.NonStructural);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                tran.Commit();
            }
            return Result.Succeeded;
        }


    }
}
