﻿// (C) Copyright 2013-2016 by Andrew Nicholas
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

namespace SCaddins.ExportManager
{
    using System;
    using System.Windows.Forms;
    using Autodesk.Revit.DB;

    public partial class OptionsDialog : System.Windows.Forms.Form
    {
        private Autodesk.Revit.DB.Document doc;
        private ExportManager scx;

        public OptionsDialog(Autodesk.Revit.DB.Document doc, ExportManager scx)
        {
            this.doc = doc;
            this.scx = scx;
            this.InitializeComponent();
            this.InitializeComponentsMore();
            this.AssignDWGReleaseMenuTags();
            this.PopulateSchemeMenu();
            this.LoadValues();
        }

        private static void SetPrinter(TextBox textBox)
        {
            using (var dialog = new SelectPrinterDialog()) {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK) {
                    textBox.Text = dialog.comboBoxPrinter.SelectedItem.ToString();
                }
            }
        }

        private void LoadValues()
        {
            this.radioPDF.Checked = this.scx.HasExportOption(ExportOptions.PDF);
            this.radioGSPDF.Checked = this.scx.HasExportOption(ExportOptions.GhostscriptPDF);
            this.checkBoxDGN.Checked = this.scx.HasExportOption(ExportOptions.DGN);
            this.checkBoxDWG.Checked = this.scx.HasExportOption(ExportOptions.DWG);
            this.checkBoxDWF.Checked = this.scx.HasExportOption(ExportOptions.DWF);
            this.checkBoxHideTitleblock.Checked =
                this.scx.HasExportOption(ExportOptions.NoTitle);
            this.checkBoxForceDate.Checked = this.scx.ForceRevisionToDateString;
            if (this.scx.FileNameScheme != null) {
                this.comboBoxScheme.Text = this.scx.FileNameScheme.Name;
            }
            this.comboBoxAutocadVersion.SelectedIndex = 
                this.comboBoxAutocadVersion.FindStringExact(ExportManager.AcadVersionToString(this.scx.AcadVersion));
            this.checkBox1.Checked = true;
            this.textBoxScalebarScale.Text = SCaddins.ExportManager.Settings1.Default.ScalebarScaleParameter;
            this.textBoxNorthPointVisibilty.Text = SCaddins.ExportManager.Settings1.Default.NorthPointVisibilityParameter;
            this.textBoxAdobeDriver.Text = this.scx.PdfPrinterName;
            this.textBoxPSPrinter.Text = this.scx.PostscriptPrinterName;
            this.textBoxGSBin.Text = this.scx.GhostscriptBinDirectory;
            this.textBoxGSLib.Text = this.scx.GhostscriptLibDirectory;
            this.textBoxA3Printer.Text = this.scx.PrinterNameA3;
            this.textBoxLargeFormatPrinter.Text = this.scx.PrinterNameLargeFormat;
            textBoxTextEditor.Text = SCaddins.ExportManager.Settings1.Default.TextEditor;
            textBoxExportDir.Text = SCaddins.ExportManager.Settings1.Default.ExportDir;
            this.checkBoxShowExportLog.Checked = this.scx.ShowExportLog;
        }

        private void SaveValues()
        {
            this.scx.GhostscriptBinDirectory = textBoxGSBin.Text;
            this.scx.GhostscriptLibDirectory = textBoxGSLib.Text;
            this.scx.PrinterNameA3 = textBoxA3Printer.Text;
            this.scx.PostscriptPrinterName = textBoxPSPrinter.Text;
            this.scx.PdfPrinterName = textBoxAdobeDriver.Text;
            this.scx.PrinterNameLargeFormat = textBoxLargeFormatPrinter.Text;
            this.scx.ShowExportLog = this.checkBoxShowExportLog.Checked;
            this.scx.ExportDirectory = this.textBoxExportDir.Text;
            SCaddins.ExportManager.Settings1.Default.GSBinDirectory = this.scx.GhostscriptBinDirectory;
            SCaddins.ExportManager.Settings1.Default.AdobePrinterDriver = this.scx.PdfPrinterName;
            SCaddins.ExportManager.Settings1.Default.A3PrinterDriver = this.scx.PrinterNameA3;
            SCaddins.ExportManager.Settings1.Default.LargeFormatPrinterDriver = this.scx.PrinterNameLargeFormat;
            SCaddins.ExportManager.Settings1.Default.PSPrinterDriver = this.scx.PostscriptPrinterName;
            SCaddins.ExportManager.Settings1.Default.GSLibDirectory = this.scx.GhostscriptLibDirectory;
            SCaddins.ExportManager.Settings1.Default.TextEditor = textBoxTextEditor.Text;
            SCaddins.ExportManager.Settings1.Default.ExportDir = this.textBoxExportDir.Text;
            SCaddins.ExportManager.Settings1.Default.AdobePDFMode = radioPDF.Checked;
            SCaddins.ExportManager.Settings1.Default.ForceDateRevision = checkBoxForceDate.Checked;
            SCaddins.ExportManager.Settings1.Default.HideTitleBlocks = checkBoxHideTitleblock.Checked;
            SCaddins.ExportManager.Settings1.Default.AcadExportVersion = this.comboBoxAutocadVersion.SelectedItem.ToString();
            SCaddins.ExportManager.Settings1.Default.ShowExportLog = this.checkBoxShowExportLog.Checked;
            SCaddins.ExportManager.Settings1.Default.NorthPointVisibilityParameter = this.textBoxNorthPointVisibilty.Text;
            SCaddins.ExportManager.Settings1.Default.ScalebarScaleParameter = this.textBoxScalebarScale.Text;
            SCaddins.ExportManager.Settings1.Default.Save();
        }

        private void AssignDWGReleaseMenuTags()
        {
                this.comboBoxAutocadVersion.Items.Add("R2007");
                this.comboBoxAutocadVersion.Items.Add("R2010");
                this.comboBoxAutocadVersion.Items.Add("R2013");
        }

        private void PopulateSchemeMenu()
        {
            foreach (SegmentedSheetName scxn in this.scx.FileNameTypes) {
                if (scxn.Name != null) {
                    this.comboBoxScheme.Items.Add(scxn.Name);
                }
            }
        }

        private void InitializeComponentsMore()
        {
            this.radioGSPDF.Enabled |= this.scx.GSSanityCheck();
            this.radioPDF.Enabled |= this.scx.PDFSanityCheck();

            if (!this.radioPDF.Enabled && !this.radioGSPDF.Enabled) {
                this.checkBox1.Enabled = false;
                this.checkBox1.Text = SCaddins.Properties.Resources.WarningPDFExportDisabled;
            }
            if (!FileUtilities.ConfigFileExists(this.doc)) {
                this.buttonEditConfig.Enabled = false;
            }
            this.radioPDF.Tag = ExportOptions.PDF;
            this.checkBoxDGN.Tag = ExportOptions.DGN;
            this.checkBoxDWF.Tag = ExportOptions.DWF;
            this.checkBoxDWG.Tag = ExportOptions.DWG;
            this.radioGSPDF.Tag = ExportOptions.GhostscriptPDF;
            this.checkBoxHideTitleblock.Tag = ExportOptions.NoTitle;
        }

        private void ToggleCheckBoxValue(object sender, EventArgs e)
        {
            var c = (CheckBox)sender;
            var t = (ExportOptions)c.Tag;
            this.ToggleConversionFlag(c.Checked, t);
            if (this.checkBoxDWG.Checked) {
                this.checkBoxHideTitleblock.Enabled = true;
            } else {
                this.checkBoxHideTitleblock.Enabled = false;
            }
        }

        private void ToggleConversionFlag(
            bool flagged, ExportOptions val)
        {
            if (flagged == true) {
                this.scx.AddExportOption(val);
            } else {
                this.scx.RemoveExportOption(val);
            }
        }

        private void RadioCheckedChanged(object sender, EventArgs e)
        {
            var r = (RadioButton)sender;
            var t = (ExportOptions)r.Tag;
            this.ToggleConversionFlag(r.Checked, t);
        }

        private void ForceDateCheckedChanged(object sender, EventArgs e)
        {
            this.scx.ForceRevisionToDateString = ((CheckBox)sender).Checked;
        }

        private void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = this.comboBoxScheme.Text;
            this.scx.SetFileNameScheme(s);
        }

        private void ComboBoxAutocadVersionSelectedIndexChanged(object sender, EventArgs e)
        {
            this.scx.AcadVersion = 
                ExportManager.AcadVersionFromString(comboBoxAutocadVersion.SelectedItem.ToString());
        }

        private void ButtonCreateConfigClick(object sender, EventArgs e)
        {
            FileUtilities.CreateConfigFile(this.doc);
            this.buttonEditConfig.Enabled = true;
        }

        private void ButtonEditConfigClick(object sender, EventArgs e)
        {
            FileUtilities.EditConfigFile(this.doc);
        }

        private void Button5Click(object sender, EventArgs e)
        {
            SCaddins.SCaddinsApp.CheckForUpdates(false);
        }

        private void CheckBox1CheckedChanged(object sender, EventArgs e)
        {
            if (!this.checkBox1.Checked) {
                this.radioGSPDF.Checked = false;
                this.radioPDF.Checked = false;
                this.radioGSPDF.Enabled = false;
                this.radioPDF.Enabled = false;
            } else {
                this.radioGSPDF.Enabled |= this.scx.GSSanityCheck();
                this.radioPDF.Enabled |= this.scx.PDFSanityCheck();
            }
        }

        private void GSBinDirClick(object sender, System.EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                textBoxGSBin.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void GSLibDirClick(object sender, System.EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                textBoxGSLib.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button1Click(object sender, System.EventArgs e)
        {
            this.SaveValues();
        }

        private void ButtonPSPrinterClick(object sender, System.EventArgs e)
        {
            SetPrinter(this.textBoxPSPrinter);
        }

        private void ButtonA3PrinterClick(object sender, System.EventArgs e)
        {
            SetPrinter(this.textBoxA3Printer);
        }

        private void ButtonAdobePrinterClick(object sender, System.EventArgs e)
        {
            SetPrinter(this.textBoxAdobeDriver);
        }

        private void BtnSelectLargeFormatPrinterClick(object sender, EventArgs e)
        {
            SetPrinter(this.textBoxLargeFormatPrinter);
        }

        private void BtnSelectTextEditorClick(object sender, System.EventArgs e)
        {
            DialogResult result = this.openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                textBoxTextEditor.Text = this.openFileDialog1.FileName;
            }
        }

        private void Button2Click(object sender, System.EventArgs e)
        {
            SCaddins.ExportManager.Settings1.Default.Reset();
            this.scx.LoadSettings();
            this.LoadValues();
        }

        private void BtnDefaultExportDirClick(object sender, System.EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                textBoxExportDir.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }
    }
}

/* vim: set ts=4 sw=4 nu expandtab: */
