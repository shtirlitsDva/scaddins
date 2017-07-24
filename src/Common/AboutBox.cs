﻿// (C) Copyright 2012-2014 by Andrew Nicholas
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

namespace SCaddins.Common
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    public partial class AboutBox1 : Form
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        public AboutBox1()
        {
            this.InitializeComponent();
            this.Text = string.Format(
                CultureInfo.InvariantCulture,
                "About {0}",
                AboutBox1.AssemblyTitle);
            this.labelProductName.Text = AboutBox1.AssemblyTitle;
            this.labelVersion.Text = string.Format(
                CultureInfo.InvariantCulture,
                "Version {0}",
                AboutBox1.AssemblyVersion);
            this.labelCopyright.Text = AboutBox1.AssemblyCopyright;
            this.textBoxDescription.Text = Constants.License;
        }

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0) {
                    var titleAttribute =
                        (AssemblyTitleAttribute)attributes[0];
                    if (!string.IsNullOrEmpty(titleAttribute.Title)) {
                        return titleAttribute.Title;
                    }
                }

                return System.IO.Path.GetFileNameWithoutExtension(Assembly
                        .GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName()
                    .Version.ToString();
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) {
                    return string.Empty;
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        private void LinkLabel2_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(SCaddins.Constants.SourceLink);
        }

        private void LinkLabel1_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(SCaddins.Constants.HelpLink);    
        }
        
        private void Button1Click(object sender, EventArgs e)
        {
            SCaddins.SCaddinsApp.CheckForUpdates(false);
        }
    }
}

/* vim: set ts=4 sw=4 nu expandtab: */
