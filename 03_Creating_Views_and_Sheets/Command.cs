#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Forms = System.Windows.Forms;

#endregion

namespace _03_Creating_Views_and_Sheets
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

            //create dialog
            Forms.OpenFileDialog selectFile = new Forms.OpenFileDialog();
            //fixed folder location
            //selectFile.InitialDirectory = "C:\\";
            // OR restore last folder path
            selectFile.RestoreDirectory = false;
            //set file type
            selectFile.Filter = "CSV file|*.csv";
            //set multiple file selection
            selectFile.Multiselect = false;

            //open file dialog
            //create a variable for filename
            string fileName = "";
            //check if dialog opened and OK clicked
            if (selectFile.ShowDialog() == Forms.DialogResult.OK)
            {
                fileName = selectFile.FileName;
            }

            if(fileName != "")
            {
                //do something
            }

            //call struct and reference variables
            myStruct struct1 = new myStruct();
            struct1.Name = "test name1";
            struct1.Description = "This is a description";
            struct1.Distance = 100;

            //create a new struct with constructor
            myStruct struct2 = new myStruct("test name2", "This is a description", 100);

            //add structs to a list
            List<myStruct> myList = new List<myStruct>();
            myList.Add(struct1);
            myList.Add(struct2);

            //print the name of the structs
            foreach(myStruct curStruct in myList)
            {
                TaskDialog.Show("Struct", curStruct.Name);
            }

            //filter viewFamilyTypes
            FilteredElementCollector vftCollector = new FilteredElementCollector(doc);
            vftCollector.OfClass(typeof(ViewFamilyType));

            //collector for title block type id
            FilteredElementCollector tCollector = new FilteredElementCollector(doc);
            tCollector.OfCategory(BuiltInCategory.OST_TitleBlocks);
            ElementId tBlockId = tCollector.FirstElementId();

            //create variables for viewFamilyTypes
            ViewFamilyType planVFT = null;
            ViewFamilyType rcpVFT = null;

            //create a lopp and make sure you have plan view and rcp view
            foreach (ViewFamilyType vft in vftCollector)
            {
                if(vft.ViewFamily == ViewFamily.FloorPlan)
                {
                    planVFT = vft;
                }

                if(vft.ViewFamily == ViewFamily.CeilingPlan)
                {
                    rcpVFT = vft;
                }
            }

            //create and start transaction
            Transaction tran = new Transaction(doc);
            tran.Start("Create View");

            //create a new level
            Level newLevel = Level.Create(doc, 10);

            //create plan and rcp views
            ViewPlan newPlanView = ViewPlan.Create(doc, planVFT.Id, newLevel.Id);
            ViewPlan newRCPPView = ViewPlan.Create(doc, rcpVFT.Id, newLevel.Id);

            //create sheet
            ViewSheet newSheet = ViewSheet.Create(doc, tBlockId);
            newSheet.Name = "New Sheet";
            newSheet.SheetNumber = "A101";

            ViewSheet newCeilingSheet = ViewSheet.Create(doc, tBlockId);
            newCeilingSheet.Name = "New Ceiling Sheet";
            newCeilingSheet.SheetNumber = "C101";

            //create insertion point
            XYZ insertPoint = new XYZ(0, 0, 0);

            //create viewport
            Viewport newViewPort = Viewport.Create(doc, newSheet.Id, newPlanView.Id, insertPoint);
            Viewport newCeilingViewPort = Viewport.Create(doc, newCeilingSheet.Id, newRCPPView.Id, insertPoint);


            tran.Commit();
            tran.Dispose();

            return Result.Succeeded;
        }

        //create a struct
        struct myStruct
        {
            public string Name;
            public string Description;
            public double Distance;

            //constructor
            public myStruct(string name, string description, double distance)
            {
                Name = name;
                Description = description;
                Distance = distance;
            }
        }
    }
}
