﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAI
{
    public static class ApplicationSettings
    {
        public static Random Random = new Random(0);
        public static Size MapVisibilityCells = new Size(60, 40);
    }
}
