using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Inserting_Families
{
    internal static class Utils
    {
        public static void StaticMethodTest(string value)
        {
            TaskDialog.Show("Test", "This is a static method\n" + value);
        }

        //get familySymbol with filtering family symbols
        public static FamilySymbol GetFamilySymbolByName_FS(Document doc, string familyName, string familySymbolName)
        {
            //filter family symbols
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfClass(typeof(FamilySymbol));

            //return matching family symbol
            foreach(FamilySymbol fs in col)
            {
                //check if there is a matching name
                if(fs.FamilyName == familyName && fs.Name == familySymbolName)
                {
                    return fs;
                }
            }

            return null;
        }

        public static FamilySymbol GetFamilySymbolByName_Family(Document doc, string familyName, string familySymbolName)
        {
            //filter families
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfClass(typeof(Family));

            //return matching family symbol
            foreach (Family family in col)
            {
                //check for matching family name
                if(family.Name == familyName)
                {
                    //check if family symbol ids matching
                    foreach(ElementId id in family.GetFamilySymbolIds())
                    {
                        //get element from family symbol id
                        FamilySymbol curFS = doc.GetElement(id) as FamilySymbol;

                        if(curFS.Name == familySymbolName)
                        {
                            return curFS;
                        }
                    }
                }
            }
            
            return null;
        }

        public static void SetParameterValue(Element curElement, string paramName, string paramValue)
        {
            //get parameter. Returns a list
            IList<Parameter> paramList = curElement.GetParameters(paramName);

            //If you have multiple parameters with the same name you can assign the value to all
            foreach(Parameter param in paramList)
            {
                param.Set(paramValue);
            }
        }

        public static void SetParameterValueBuiltIn(Element curElement, string paramName, string paramValue)
        {
            //get parameter.
            Parameter curParam = curElement.get_Parameter(BuiltInParameter.ROOM_AREA);

            //check if the parameter exist
            //set the parameter
            if(curParam != null)
            {
                curParam.Set(paramValue);
            }
        }

        public static string GetParameterValueAsString(Element curElement, string paramName)
        {
            //create return value variable
            string returnValue = "";

            //get parameter. Returns a list
            IList<Parameter> paramList = curElement.GetParameters(paramName);

            //get the first element from parameter list.
            //Most likely there will be just one parameter in the list
            Parameter myParam = paramList[0];

            //get the return value
            returnValue = myParam.AsValueString();

            return returnValue;
        }
    }
}
