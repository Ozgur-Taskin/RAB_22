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

            //create an array
            string[] myStringArray = new string[4];
            //add items to array
            myStringArray[0] = "Array 1";
            myStringArray[1] = "Array 2";
            myStringArray[2] = "Array 3";
            myStringArray[3] = "Array 4";

            //create a list
            List<string> myStringList = new List<string>();
            //add items to list
            myStringList.Add("List 1");
            myStringList.Add("List 2");
            myStringList.Add("List 3");
            myStringList.Add("List 4");
            //remove item from list
            myStringList.RemoveAt(0);

            //create a list contains array items
            List<string[]> myArrayList = new List<string[]>();
            //add an array item to the list
            myArrayList.Add(myStringArray);

            //show items in array
            foreach(string str in myStringArray)
            {
                TaskDialog.Show("Array", str);
            }

            //show items in list
            foreach(string str in myStringList)
            {
                TaskDialog.Show("List", str);
            }

            //merge items in Array List
            string fullStr = "";

            foreach (string[] strArray in myArrayList)
            {
                

                foreach(string str in strArray)
                {
                    fullStr = fullStr + " - " + str;
                } 
                
                //show string on immediate window
                Debug.Print(fullStr);
            }

            //show merged string
            TaskDialog.Show("Merged Array", fullStr);


            return Result.Succeeded;
        }
    }
}
