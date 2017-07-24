// (C) Copyright 2014 by Andrew Nicholas
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

namespace SCaddins.SheetCopier
{
    using System;
    using System.Linq;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class Command : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(
            ExternalCommandData commandData,
            ref string message,
            Autodesk.Revit.DB.ElementSet elements)
        {
            if (commandData == null) {
                return Result.Failed;
            }

            Document doc = commandData.Application.ActiveUIDocument.Document;
            SCaddins.ExportManager.DialogHandler.AddRevitDialogHandler(commandData.Application);
        
            Autodesk.Revit.DB.ViewSheet viewSheet = SheetCopierManager.ViewToViewSheet(doc.ActiveView);
            if (viewSheet == null) {
                using (TaskDialog td = new TaskDialog("SCopy")) {
                    td.MainIcon = TaskDialogIcon.TaskDialogIconWarning;
                    td.MainInstruction = "The Copy Sheets add-in needs to be started in a sheet view.";

                    // FIXME add sheet selection to SheetCopier
                    td.MainContent = "Please open the sheet you wish to copy before running...";
                    td.Show();
                }
                return Autodesk.Revit.UI.Result.Failed;    
            }
            var scopy = new SheetCopierManager(commandData.Application.ActiveUIDocument);
            using (var form = new MainForm(doc, viewSheet, scopy)) {
                form.Enabled = true;
                System.Windows.Forms.DialogResult result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK) {
                    scopy.CreateSheets();
                }
            }
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
/* vim: set ts=4 sw=4 nu expandtab: */
