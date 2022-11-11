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
    public class Read_Data : IExternalCommand
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

            //read data
            //create filepath
            string filepath = "C:\\Personal\\Revit Add-In Bootcamp\\_Resources\\02\\Room List.csv";

            //read all data to a single string
            string fileText = System.IO.File.ReadAllText(filepath);

            TaskDialog.Show("All Data", fileText);

            //read all lines to an array item
            string[] fileArray = System.IO.File.ReadAllLines(filepath);

            //split line to array items
            foreach (string file in fileArray)
            {
                //split
                string[] cellString = file.Split(',');

                string roomNumber = cellString[0];
                string roomName = cellString[1];
                string roomArea = cellString[2];

                //change data type string to double
                //this works only if we are sure we can do the change
                //in this case, it will read the header and give an error
                //double roomAreaAsDouble = double.Parse(roomArea);

                //other option - try changing the data type
                double roomAreaAsDouble2 = 0;
                bool isParse = double.TryParse(roomArea, out roomAreaAsDouble2);


            }

            return Result.Succeeded;
        }
    }
}
