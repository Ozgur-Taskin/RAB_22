#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

#endregion

namespace _06_Building_Your_Ribbon
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            //variables
            string assemblyName = GetAssemblyName();

            //create ribbon tab
            a.CreateRibbonTab("Test Tab");

            //create ribbon panel
            RibbonPanel panel1 = a.CreateRibbonPanel("Test Tab", "Test Panel");
            //use just one argument to place the panel under Add-Ins tab
            RibbonPanel panel2 = a.CreateRibbonPanel("Test Panel");

            //create button data instances
            PushButtonData pData1 = new PushButtonData("button1", "Button 1", assemblyName, "_06_Building_Your_Ribbon.Cmd_Command1");
            PushButtonData pData2 = new PushButtonData("button2", "Button 2", assemblyName, "_06_Building_Your_Ribbon.Cmd_Command2");
            PushButtonData pData3 = new PushButtonData("button3", "Button 3", assemblyName, "_06_Building_Your_Ribbon.Cmd_Command3");
            PulldownButtonData pdownData = new PulldownButtonData("pdownButton", "Pulldown \rButton");
            SplitButtonData sData = new SplitButtonData("sButton", "Split Button");

            //add images to Properties/Resources
            pData1.LargeImage = BitmapToImageSource(_06_Building_Your_Ribbon.Properties.Resources.Blue_32);
            pData1.Image = BitmapToImageSource(_06_Building_Your_Ribbon.Properties.Resources.Blue_16);
            pData2.LargeImage = BitmapToImageSource(_06_Building_Your_Ribbon.Properties.Resources.Green_32);
            pData2.Image = BitmapToImageSource(_06_Building_Your_Ribbon.Properties.Resources.Green_16);
            pData3.LargeImage = BitmapToImageSource(_06_Building_Your_Ribbon.Properties.Resources.Red_32);
            pData3.Image = BitmapToImageSource(_06_Building_Your_Ribbon.Properties.Resources.Red_16);

            //add tooltips
            pData1.ToolTip = "Button 1 Tooltip";
            pData2.ToolTip = "Button 2 Tooltip";
            pData3.ToolTip = "Button 3 Tooltip";

            //create buttons and add them to panels
            panel1.AddItem(pData1);
            panel1.AddItem(pData2);
            panel1.AddItem(pData3);
            panel2.AddItem(pData1);
            panel2.AddItem(pData2);
            panel2.AddItem(pData3);
            //create pulldown button
            PulldownButton pdown = panel1.AddItem(pdownData) as PulldownButton;
            pdown.LargeImage = BitmapToImageSource(_06_Building_Your_Ribbon.Properties.Resources.Yellow_32);
            pdown.Image = BitmapToImageSource(_06_Building_Your_Ribbon.Properties.Resources.Yellow_16);
            pdown.AddPushButton(pData1);
            pdown.AddPushButton(pData2);
            pdown.AddPushButton(pData3);
            //create split button
            SplitButton splitButton1 = panel1.AddItem(sData) as SplitButton;
            splitButton1.AddPushButton(pData1);
            splitButton1.AddPushButton(pData2);
            splitButton1.AddPushButton(pData3);
            //create stacked buttons
            //panel1.AddStackedItems(pData1, pData2, pData3);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

        private string GetAssemblyName()
        {
            string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return assemblyName;
        }

        //save the image file to memory and return the image
        //add references from reference library
        //PresentationCore, PresentationFramework, WindowsBase
        //Add using System.Windows.Media.Imaging;
        private BitmapImage BitmapToImageSource(Bitmap bm)
        {
            using(MemoryStream mem = new MemoryStream())
            {
                bm.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                mem.Position = 0;
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = mem;
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.EndInit();

                return bmi;
            }
        }
    }
}
