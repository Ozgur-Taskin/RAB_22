using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;

public static class Utils
{
    public static FamilySymbol GetFamilySymbolByName(Document doc, string familyName,
        string familySymbolName)
    {
        //filter family symbols
        FilteredElementCollector col = new FilteredElementCollector(doc);
        col.OfClass(typeof(FamilySymbol));

        //return matching family symbol
        foreach(FamilySymbol fs in col)
        {
            //check for mathing name
            if(fs.FamilyName == familyName && fs.Name == familySymbolName)
            {
                return fs;
            }
        }

        return null;
    }

    public static List<string[]> GetFurnitureTypes()
    {
        List<string[]> returnList = new List<string[]>();
        //returnList.Add(new string[] { "Furniture Name", "Revit Family Name", "Revit Family Type" });
        returnList.Add(new string[] { "desk", "Desk", "60in x 30in" });
        returnList.Add(new string[] { "task chair", "Chair-Task", "Chair-Task" });
        returnList.Add(new string[] { "side chair", "Chair-Breuer", "Chair-Breuer" });
        returnList.Add(new string[] { "bookcase", "Shelving", "96in x 12in x 84in" });
        returnList.Add(new string[] { "loveseat", "Sofa", "54in" });
        returnList.Add(new string[] { "teacher desk", "Table-Rectangular", "48in x 30in" });
        returnList.Add(new string[] { "student desk", "Desk", "60in x 30in Student" });
        returnList.Add(new string[] { "computer desk", "Table-Rectangular", "48in x 30in" });
        returnList.Add(new string[] { "lab desk", "Table-Rectangular", "72in x 30in" });
        returnList.Add(new string[] { "lounge chair", "Chair-Corbu", "Chair-Corbu" });
        returnList.Add(new string[] { "coffee table", "Table-Coffee", "30in x 60in x 18in" });
        returnList.Add(new string[] { "sofa", "Sofa-Corbu", "Sofa-Corbu" });
        returnList.Add(new string[] { "dining table", "Table-Dining", "30in x 84in x 22in" });
        returnList.Add(new string[] { "dining chair", "Chair-Breuer", "Chair-Breuer" });
        returnList.Add(new string[] { "stool", "Chair-Task", "Chair-Task" });

        return returnList;
    }

    public static List<string[]> GetFurnitureSets()
    {
        List<string[]> returnList = new List<string[]>();
        //returnList.Add(new string[] { "Furniture Set", "Room Type", "Included Furniture" });
        returnList.Add(new string[] { "A", "Office", "desk, task chair, side chair, bookcase" });
        returnList.Add(new string[] { "A2", "Office", "desk, task chair, side chair, bookcase, loveseat" });
        returnList.Add(new string[] { "B", "Classroom - Large", "teacher desk, task chair, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk" });
        returnList.Add(new string[] { "B2", "Classroom - Medium", "teacher desk, task chair, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk" });
        returnList.Add(new string[] { "C", "Computer Lab", "computer desk, computer desk, computer desk, computer desk, computer desk, computer desk, task chair, task chair, task chair, task chair, task chair, task chair" });
        returnList.Add(new string[] { "D", "Lab", "teacher desk, task chair, lab desk, lab desk, lab desk, lab desk, lab desk, lab desk, lab desk, stool, stool, stool, stool, stool, stool, stool" });
        returnList.Add(new string[] { "E", "Student Lounge", "lounge chair, lounge chair, lounge chair, sofa, coffee table, bookcase" });
        returnList.Add(new string[] { "F", "Teacher's Lounge", "lounge chair, lounge chair, sofa, coffee table, dining table, dining chair, dining chair, dining chair, dining chair, bookcase" });
        returnList.Add(new string[] { "G", "Waiting Room", "lounge chair, lounge chair, sofa, coffee table" });

        return returnList;
    }

    internal static string GetParameterValueAsString(Element curElement, string paramName)
    {
        //create return value variable
        string returnValue = "";

        //get parameter. returns a list
        IList<Parameter> paramList = curElement.GetParameters(paramName);

        //get the first element from the list
        Parameter myPram = paramList[0];

        //return value
        returnValue = myPram.AsValueString();

        return returnValue;
    }
}
