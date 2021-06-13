using System;

namespace CarDirectory
{
    public class Car : IEquatable<Car>
    {
        public Car()
        {
        }

        public Car(string brand, string model, int start, string end)
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
            return Brand.Equals(other.Brand) && Model.Equals(other.Model) && Start == other.Start && End == other.End;
        }

        public override string ToString()
        {
            return Brand + "\t" + Model + "\t" + Start + "\t" + End;
        }
    }
}