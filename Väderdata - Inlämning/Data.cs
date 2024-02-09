using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Väderdata___Inlämning
{
    internal class Data
    {
        public DateTime DateTime { get; set; }
        public float Temperature { get; set; }
        public int Humidity { get; set; }

        public double MoldIndex { get; set; }
        public Data()
        {
            MoldIndex = ((Humidity - 78) * (Temperature / 15) / 0.22);
        }
    }
}
