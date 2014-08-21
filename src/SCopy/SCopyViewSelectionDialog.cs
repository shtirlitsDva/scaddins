﻿namespace SCaddins.SCopy
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Description of Form1.
    /// </summary>
    public partial class SCopyViewSelectionDialog : Form
    {
        public SCopyViewSelectionDialog()
        {
            this.InitializeComponent();
        }
        
        public void Add(Autodesk.Revit.DB.View view)
        {
            listBox1.Items.Add(view.Name);
            
            // TODO just do this once.
            listBox1.Sorted = true;
        }
        
        public string SelectedView() {
            return listBox1.Text;
        }
    }
}
/* vim: set ts=4 sw=4 nu expandtab: */
