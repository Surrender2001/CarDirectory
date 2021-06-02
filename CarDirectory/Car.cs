using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDirectory
{
    public class Car :IEquatable<Car>
    {
        public Car()
        {
        }

        public Car(object[] cell)
        {
            Brand = (string)cell[0];
            Model = (string)cell[1];
            Start = (int)cell[2];
            End = (string)cell[3];
        }
        public Car(string brand,string model,int start,string end)
        {
            Brand = brand;
            Model = model;
            Start = start;
            End = end;
        }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Start { get; set; }
        public string End { get; set; }

        public bool Equals(Car other)
        {
            if (other == null) return false;
            return Brand.Equals(other.Brand) && Model.Equals(other.Model)&&Start==other.Start&&End==other.End;
        }
        public bool Equals(string brand,string model)
        {
            return Brand.Equals(brand) && Model.Equals(model);
        }
    }
}
