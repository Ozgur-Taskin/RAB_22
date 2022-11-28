#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Forms = System.Windows.Forms;

#endregion

namespace _03_Project_Setup_Challenge
{
    [Transaction(TransactionMode.Manual)]
    public class Project_Setup : IExternalCommand
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

            //read data from csv
            //create dialog
            Forms.OpenFileDialog selectFile = new Forms.OpenFileDialog();
            //restore last folder path
            selectFile.RestoreDirectory = false;
            //set file type
            selectFile.Filter = "CSV File|*.csv";
            //set multiple file selection
            selectFile.Multiselect = false;

            //create empty filepaths
            string filepathLevels = "";
            string filepathSheets = "";

            //ask for level data file
            TaskDialog.Show("File Selection", "Please select file for Level Data");

            //check if dialog opened and OK clicked
            if (selectFile.ShowDialog() == Forms.DialogResult.OK)
            {
                filepathLevels = selectFile.FileName;
            }

            //ask for sheet data file
            TaskDialog.Show("File Selection", "Please select file for Sheet Data");

            //check if dialog opened and OK clicked
            if (selectFile.ShowDialog() == Forms.DialogResult.OK)
            {
                filepathSheets = selectFile.FileName;
            }

            //string filepathLevels = "C:\\Personal\\Revit Add-In Bootcamp\\_Resources\\02_Challenge\\RAB_Session_02_Challenge_Levels.csv";
            //string filepathSheets = "C:\\Personal\\Revit Add-In Bootcamp\\_Resources\\02_Challenge\\RAB_Session_02_Challenge_Sheets.csv";

            //split each line to an array item
            string[] arrayLevels = System.IO.File.ReadAllLines(filepathLevels);
            string[] arraySheets = System.IO.File.ReadAllLines(filepathSheets);
            //OR we can create Lists and use ReadAllLines()
            //List<string[]> listLevels = new List<string[]>();
            //List<string[]> listSheets = new List<string[]>();

            //remove header from array and create array from the other items
            arrayLevels = arrayLevels.Skip(1).ToArray();
            arraySheets = arraySheets.Skip(1).ToArray();

            //create filtered element collector for titleblock typeID
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_TitleBlocks);
            ElementId titleBlockID = col.FirstElementId();

            //transaction
            Transaction trans = new Transaction(doc);
            trans.Start("Create Levels and Sheets");

            //create levels
            foreach (string levelString in arrayLevels)
            {
                //split each string into an array element
                string[] arrayStrings = levelString.Split(',');

                //get the value from array element
                string levelName = arrayStrings[0];
                string levelHeight = arrayStrings[1];

                //change data type to double for height
                double levelHeightAsDouble = 0;
                bool isParse = double.TryParse(levelHeight, out levelHeightAsDouble);

                //create levels if the data can be converted ti double
                //otherwise show an error and continue with the other levels
                if (isParse)
                {
                    Level newLevel = Level.Create(doc, levelHeightAsDouble);
                    newLevel.Name = levelName;
                }
                else
                {
                    TaskDialog.Show("Error", "Level Height is not valid at: " + levelName);
                }

            }

            //create sheets
            foreach (string sheetString in arraySheets)
            {
                //split each string into an array element
                string[] arrayStrings = sheetString.Split(',');

                //get the value from array element
                string sheetNumber = arrayStrings[0];
                string sheetName = arrayStrings[1];

                //create sheets
                ViewSheet newSheet = ViewSheet.Create(doc, titleBlockID);
                newSheet.Name = sheetName;
                newSheet.SheetNumber = sheetNumber;
            }

            //commit transaction
            trans.Commit();
            trans.Dispose();

            return Result.Succeeded;
        }
    }
}
