using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDirectory
{
    class Car
    {
        public Car(object[] cell)
        {
            Brand = (string)cell[0];
            Model = (string)cell[1];
            Start = (int)cell[2];
            End = (string)cell[3];
            Hash = (int)cell[4]; 
        }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Start { get; set; }
        public string End { get; set; }
        public int Hash { get; set; }
    }
}
