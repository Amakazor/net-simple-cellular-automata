using System;
using System.Collections.Generic;
using System.Text;

namespace Amakazor.Cellular
{
    internal class Sides
    {
        internal bool Top { get; set; }
        internal bool Left { get; set; }
        internal bool Bottom { get; set; }
        internal bool Right { get; set; }

        internal Sides()
        {
            Top = false;
            Left = false;
            Bottom = false;
            Right = false;
        }
    }
}
