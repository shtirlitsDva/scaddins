﻿// (C) Copyright 2015 by Andrew Nicholas
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
    using System.Drawing;
    using System.Windows.Forms;
    
    public class MenuButton : Button
    {
        public MenuButton()
        {
            this.Menu = null;
        }

        public MenuButton(ContextMenuStrip menu)
        {
            this.Menu = menu;
        }
        
        public ContextMenuStrip Menu { get; set; }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent != null) {
                base.OnMouseDown(mevent);
                if (this.Menu != null && mevent.Button == MouseButtons.Left) {
                    this.Menu.Show(this, mevent.Location);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (pevent != null) {
                base.OnPaint(pevent);
                int arrowX = ClientRectangle.Width - 14;
                int arrowY = (ClientRectangle.Height / 2) - 1;
                Brush brush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ButtonShadow;
                Point[] arrows = new Point[] { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
                pevent.Graphics.FillPolygon(brush, arrows);
            }
        }
    }
}
/* vim: set ts=4 sw=4 nu expandtab: */
