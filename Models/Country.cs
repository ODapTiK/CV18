﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV18.Models
{
    internal class Country
    {
        public string Name { get; set; }
        
        public Point Location { get; set; }

        public IEnumerable<ConfirmedInfections> TotalInfections {  get; set; }
    }
}
