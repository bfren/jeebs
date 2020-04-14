﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Mvc.Models
{
    /// <summary>
    /// MenuItem Text
    /// </summary>
    public class MenuItemText : MenuItem
    {
        /// <summary>
        /// Set IsLink to be false
        /// </summary>
        public MenuItemText(string text)
        {
            IsLink = false;
            Text = text;
        }
    }
}
