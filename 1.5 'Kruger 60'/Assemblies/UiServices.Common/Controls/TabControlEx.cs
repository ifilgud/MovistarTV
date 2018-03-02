﻿// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Controls
{
    [ToolboxBitmap(typeof(TabControl))]
    public sealed class TabControlEx: TabControl
    {
        private bool fieldShowTabs;
        private const int TCM_ADJUSTRECT = 0x1328;

        [DefaultValue(false)]
        public bool ShowTabs
        {
            get
            {
                return fieldShowTabs;
            } // get
            set
            {
                fieldShowTabs = value;
                RecreateHandle();
            } // set
        } // ShowTabs

        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == TCM_ADJUSTRECT && !DesignMode)
            {
                if (!ShowTabs)
                {
                    m.Result = (IntPtr)1;
                } // if
            }
            else
            {
                base.WndProc(ref m);
            } // if-else
        } // WndProc
    } // class
} // namespace