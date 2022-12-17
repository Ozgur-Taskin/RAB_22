#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

#endregion

namespace _06_Challenge
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            //variables
            String assemblyName = GetAssemblyName();
            //create ribbon tab
            a.CreateRibbonTab("Revit Add-in Bootcamp");

            //create ribbon panel
            RibbonPanel panel1 = a.CreateRibbonPanel("Revit Add-in Bootcamp", "Revit Tools");

            //create button data instances
            PushButtonData pData1 = new PushButtonData("pushButton1", "Tool 1", assemblyName, "_06_Challenge.Cmd_1");
            PushButtonData pData2 = new PushButtonData("pushButton2", "Tool 2", assemblyName, "_06_Challenge.Cmd_2");
            PushButtonData pData3 = new PushButtonData("pushButton3", "Tool 3", assemblyName, "_06_Challenge.Cmd_3");
            PushButtonData pData4 = new PushButtonData("pushButton4", "Tool 4", assemblyName, "_06_Challenge.Cmd_4");
            PushButtonData pData5 = new PushButtonData("pushButton5", "Tool 5", assemblyName, "_06_Challenge.Cmd_5");
            PushButtonData pData6 = new PushButtonData("pushButton6", "Tool 6", assemblyName, "_06_Challenge.Cmd_6");
            PushButtonData pData7 = new PushButtonData("pushButton7", "Tool 7", assemblyName, "_06_Challenge.Cmd_7");
            PushButtonData pData8 = new PushButtonData("pushButton8", "Tool 8", assemblyName, "_06_Challenge.Cmd_8");
            PushButtonData pData9 = new PushButtonData("pushButton9", "Tool 9", assemblyName, "_06_Challenge.Cmd_9");
            PushButtonData pData10 = new PushButtonData("pushButton10", "Tool 10", assemblyName, "_06_Challenge.Cmd_10");
            SplitButtonData sData1 = new SplitButtonData("splitButton1", "Split Button");
            PulldownButtonData pdData1 = new PulldownButtonData("pulldownButton1", "Pulldown Button");

            //add images
            pData1.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources._1_32);
            pData1.Image = BitmapToImageSource(_06_Challenge.Properties.Resources._1_16);
            pData2.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources._2_32);
            pData2.Image = BitmapToImageSource(_06_Challenge.Properties.Resources._2_16);
            pData3.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources._3_32);
            pData3.Image = BitmapToImageSource(_06_Challenge.Properties.Resources._3_16);
            pData4.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources._4_32);
            pData4.Image = BitmapToImageSource(_06_Challenge.Properties.Resources._4_16);
            pData5.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources._5_32);
            pData5.Image = BitmapToImageSource(_06_Challenge.Properties.Resources._5_16);
            pData6.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources._6_32);
            pData6.Image = BitmapToImageSource(_06_Challenge.Properties.Resources._6_16);
            pData7.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources._7_32);
            pData7.Image = BitmapToImageSource(_06_Challenge.Properties.Resources._7_16);
            pData8.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources._8_32);
            pData8.Image = BitmapToImageSource(_06_Challenge.Properties.Resources._8_16);
            pData9.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources._9_32);
            pData9.Image = BitmapToImageSource(_06_Challenge.Properties.Resources._9_16);
            pData10.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources._10_32);
            pData10.Image = BitmapToImageSource(_06_Challenge.Properties.Resources._10_16);

            //add tooltips
            pData1.ToolTip = "This is a tooltip";

            //create buttons and add to panel
            panel1.AddItem(pData1);
            panel1.AddItem(pData2);
            panel1.AddStackedItems(pData3, pData4, pData5);

            SplitButton splitButton1 = panel1.AddItem(sData1) as SplitButton;
            splitButton1.AddPushButton(pData6);
            splitButton1.AddPushButton(pData7);

            PulldownButton pulldownButton1 = panel1.AddItem(pdData1) as PulldownButton;
            pulldownButton1.LargeImage = BitmapToImageSource(_06_Challenge.Properties.Resources.Green_32);
            pulldownButton1.Image = BitmapToImageSource(_06_Challenge.Properties.Resources.Green_16);
            pulldownButton1.AddPushButton(pData8);
            pulldownButton1.AddPushButton(pData9);
            pulldownButton1.AddPushButton(pData10);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
        
        private string GetAssemblyName()
        {
            string assemblyName = Assembly.GetExecutingAssembly().Location;
            return assemblyName;
        }

        private BitmapImage BitmapToImageSource(Bitmap bm)
        {
            using (MemoryStream mem = new MemoryStream())
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
