﻿// (C) Copyright 2014-2016 by Andrew Nicholas (andrewnicholas@iinet.net.au)
//
// This file is part of SCaddins.
//
// SCaddins is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// SCaddins is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with SCaddins.  If not, see <http://www.gnu.org/licenses/>.

// [assembly: System.CLSCompliant(true)]
namespace SCaddins
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.UI;
    using Newtonsoft.Json;
    using SCaddins.Properties;

    [Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class SCaddinsApp : Autodesk.Revit.UI.IExternalApplication
    {
        public static Version Version
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static void CheckForUpdates(bool newOnly)
        {
            var uri = new Uri("https://api.github.com/repos/acnicholas/scaddins/releases/latest");
            var webRequest = WebRequest.Create(uri) as HttpWebRequest;
            if (webRequest == null) {
                return;
            }

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";
            string latestAsJson = "nothing to see here";

            using (var s = webRequest.GetResponse().GetResponseStream())
            using (var sr = new StreamReader(s)) {
                    latestAsJson = sr.ReadToEnd();
            }

            LatestVersion latestVersion = JsonConvert.DeserializeObject<LatestVersion>(latestAsJson);
            
            var installedVersion = SCaddinsApp.Version;
            Version latestAvailableVersion = new Version(latestVersion.tag_name.Replace("v", string.Empty).Trim());
            string info = latestVersion.body;

            string downloadLink = latestVersion.assets.FirstOrDefault().browser_download_url;
            if (string.IsNullOrEmpty(downloadLink)) {
                downloadLink = SCaddins.Constants.DownloadLink;
            }

            if (latestAvailableVersion > installedVersion || !newOnly) {
                using (var upgradeForm = new SCaddins.Common.UpgradeForm(installedVersion, latestAvailableVersion, info, downloadLink)) {
                    upgradeForm.ShowDialog();
                }
            } 
        }

        public static PushButtonData LoadSCopy(string dll, int iconSize) {
            var pbd = new PushButtonData(
                              "SCopy", Resources.CopySheets, dll, "SCaddins.SheetCopier.Command");
            if (iconSize == 16) {
                AssignPushButtonImage(pbd, "SCaddins.Assets.scopy-rvt-16.png", 16, dll);
            } else {
                AssignPushButtonImage(pbd, "SheetCopier.Assets.scopy-rvt.png", 32, dll);
            }
            pbd.SetContextualHelp(new ContextualHelp(ContextualHelpType.Url, Constants.HelpLink));
            pbd.ToolTip = Resources.CopySheetsToolTip;
            return pbd;
        }

        public Autodesk.Revit.UI.Result OnStartup(
            UIControlledApplication application)
        {
            var ribbonPanel = TryGetPanel(application, "Scott Carver");

            if (ribbonPanel == null) {
                return Result.Failed;
            }

            string scdll =
                new Uri(Assembly.GetAssembly(typeof(SCaddinsApp)).CodeBase).LocalPath;

            ribbonPanel.AddItem(LoadScexport(scdll));
            ribbonPanel.AddStackedItems(
                LoadSCoord(scdll),
                LoadSCulcase(scdll),
                LoadSCwash(scdll));
            ribbonPanel.AddStackedItems(
                LoadSCaos(scdll, 16),
                LoadSCopy(scdll, 16),
                LoadSCloudShed(scdll));
            ribbonPanel.AddStackedItems(
                LoadSCightlines(scdll),
                LoadSCincrement(scdll),
                LoadSCuv(scdll));
            ribbonPanel.AddStackedItems(
                LoadSCasfar(scdll),
                LoadSCam(scdll));

            ribbonPanel.AddSlideOut();

            ribbonPanel.AddStackedItems(
                LoadAbout(scdll),
                LoadSCincrementSettings(scdll),
                LoadSCaddinSettings(scdll));

            if (SCaddins.Scaddins.Default.UpgradeCheckOnStartUp) {    
                CheckForUpdates(true);
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public static PushButtonData LoadSCaos(string dll, int iconSize)
        {
            var pbd = new PushButtonData(
                              "SCaos", Resources.AngleOfSun, dll, "SCaddins.SolarUtilities.Command");
            pbd.SetContextualHelp(
                new ContextualHelp(
                    ContextualHelpType.Url, Constants.HelpLink));
            if (iconSize == 16) {
                AssignPushButtonImage(pbd, "SCaddins.Assets.scaos-rvt-16.png", 16, dll);
            } else {
                AssignPushButtonImage(pbd, "AngleOfSun.Assets.scaos-rvt.png", 32, dll);
            }
            pbd.ToolTip = Resources.AngleOfSunToolTip;
            pbd.LongDescription = Resources.AngleOfSunLongDescription;
            return pbd;
        }

        internal static RibbonPanel TryGetPanel(UIControlledApplication application, string name) {
            if (application == null || string.IsNullOrEmpty(name)) {
                return null;
            }
            List<RibbonPanel> loadedPanels = application.GetRibbonPanels();
            foreach (RibbonPanel p in loadedPanels) {
                if (p.Name.Equals(name)) {
                    return p;
                }
            }
            return application.CreateRibbonPanel(name);
        }

        private static PushButtonData LoadScexport(string dll) {
            var pbd = new PushButtonData(
                          "SCexport", Resources.SCexport, dll, "SCaddins.ExportManager.Command");
            AssignPushButtonImage(pbd, @"SCaddins.Assets.scexport-rvt.png", 32, dll);
            AssignPushButtonImage(pbd, @"SCaddins.Assets.scexport-rvt-16.png", 16, dll);
            pbd.SetContextualHelp(
                new ContextualHelp(ContextualHelpType.Url, Constants.HelpLink));
            pbd.ToolTip = Resources.SCexportToolTip;
            pbd.LongDescription = Resources.SCexportLongDescription;
            return pbd;
        }

        private static PushButtonData LoadSCoord(string dll) {
            var pbd = new PushButtonData(
                           "Scoord", Resources.PlaceCoordinate, dll, "SCaddins.SCoord.Command");
            AssignPushButtonImage(pbd, @"SCaddins.Assets.scoord-rvt-16.png", 16, dll);
            pbd.ToolTip = Resources.PlaceCoordinateToolTip;
            return pbd;
        }

        private static PushButtonData LoadSCulcase(string dll) {
            var pbd = new PushButtonData(
                           "SCulcase", Resources.ChangeCase, dll, "SCaddins.ParameterUtils.EditTextParameters");
            AssignPushButtonImage(pbd, @"SCaddins.Assets.sculcase-rvt-16.png", 16, dll);
            pbd.ToolTip = Resources.ChangeCaseToolTip;
            pbd.LongDescription = Resources.ChangeCaseLongDescription;
            return pbd;
        }

        private static PushButtonData LoadSCwash(string dll) {
            var pbd = new PushButtonData(
                              "SCwash", Resources.DestructivePurge, dll, "SCaddins.SCwash.Command");
            AssignPushButtonImage(pbd, "SCaddins.Assets.scwash-rvt-16.png", 16, dll);
            pbd.ToolTip = Resources.DestructivePurgeToolTip;
            return pbd;
        }

        private static PushButtonData LoadSCightlines(string dll)
        { 
            var pbd = new PushButtonData(
                              "SCightLines", Resources.LineofSight, dll, "SCaddins.LineOfSight.Command");
            AssignPushButtonImage(pbd, "SCaddins.Assets.scightlines-rvt-16.png", 16, dll);
            pbd.ToolTip = Resources.LineofSightToolTip;
            return pbd;
        }

        private static PushButtonData LoadSCloudShed(string dll)
        {
            var pbd = new PushButtonData(
                              "SCloudSChed", Resources.ScheduleClouds, dll, "SCaddins.RevisionUtilities.Command");
            AssignPushButtonImage(pbd, "SCaddins.Assets.scloudsched-rvt-16.png", 16, dll);
            pbd.ToolTip = Resources.ScheduleCloudsToolTip;
            return pbd;
        }

        private static PushButtonData LoadSCincrement(string dll)
        {
            var pbd = new PushButtonData(
                              "SCincrement", Resources.IncrementTool, dll, "SCaddins.ParameterUtils.Command");
            AssignPushButtonImage(pbd, "SCaddins.Assets.scincrement-rvt-16.png", 16, dll);
            pbd.ToolTip = Resources.IncrementToolToolTip;
            return pbd;
        }

        private static PushButtonData LoadSCincrementSettings(string dll)
        {
            var pbd = new PushButtonData(
                              "SCincrementSettings", Resources.IncrementToolSettings, dll, "SCaddins.ParameterUtils.SCincrementSettingsCommand");
            AssignPushButtonImage(pbd, "SCaddins.Assets.scincrement-rvt-16.png", 16, dll);
            pbd.ToolTip = Resources.IncrementToolSettings;
            return pbd;
        }

        private static PushButtonData LoadSCaddinSettings(string dll)
        {
            var pbd = new PushButtonData(
                              "SCaddinsOptions", Resources.Options, dll, "SCaddins.SCaddinsOptions");
            AssignPushButtonImage(pbd, "SCaddins.Assets.gear.png", 16, dll);
            pbd.ToolTip = "SCaddins settings.";
            return pbd;
        }

        private static PushButtonData LoadSCuv(string dll)
        {
            var pbd = new PushButtonData(
                              "SCuv", Resources.UserView, dll, "SCaddins.ViewUtilities.CreateUserViewCommand");
            AssignPushButtonImage(pbd, "SCaddins.Assets.user.png", 16, dll);
            pbd.ToolTip = Resources.UserViewToolTip;
            return pbd;
        }
        
        private static PushButtonData LoadSCam(string dll)
        {
            var pbd = new PushButtonData(
                              "SCam", Resources.CreatePerspective, dll, "SCaddins.ViewUtilities.CameraFromViewCommand");
            AssignPushButtonImage(pbd, "SCaddins.Assets.scam-rvt-16.png", 16, dll);
            pbd.ToolTip = Resources.CreatePerspectiveToolTip;
            return pbd;
        }

        private static PushButtonData LoadSCasfar(string dll)
        {
            var pbd = new PushButtonData(
                              "SCasfar", Resources.RoomTools, dll, "SCaddins.RoomConvertor.RoomConvertorCommand");
            AssignPushButtonImage(pbd, "SCaddins.Assets.scasfar-rvt-16.png", 16, dll);
            pbd.ToolTip = Resources.RoomToolsToolTip;
            return pbd;
        }

        private static PushButtonData LoadAbout(string dll)
        {
            var pbd = new PushButtonData(
                              "SCaddinsAbout", Resources.About, dll, "SCaddins.Common.About");
            AssignPushButtonImage(pbd, "SCaddins.Assets.help.png", 16, dll);
            pbd.ToolTip = "About SCaddins.";
            return pbd;
        }

        private static void AssignPushButtonImage(ButtonData pushButtonData, string iconName, int size, string dll)
        {
            if (size == -1) {
                size = 32;
            }
            ImageSource image = LoadPNGImageSource(iconName, dll);
            if (image != null && pushButtonData != null) {
                if (size == 32) {
                    pushButtonData.LargeImage = image;
                } else {
                    pushButtonData.Image = image;
                }
            }
        }

        // from https://github.com/WeConnect/issue-tracker/blob/master/Case.IssueTracker.Revit/Entry/AppMain.cs
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFrom")]
        private static ImageSource LoadPNGImageSource(string sourceName, string path)
        {
            try {
                Assembly m_assembly = Assembly.LoadFrom(Path.Combine(path));
                Stream m_icon = m_assembly.GetManifestResourceStream(sourceName);
                PngBitmapDecoder m_decoder = new PngBitmapDecoder(m_icon, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                ImageSource m_source = m_decoder.Frames[0];
                return m_source;
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
/* vim: set ts=4 sw=4 nu expandtab: */
