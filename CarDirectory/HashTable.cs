using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDirectory
{
    /// <summary>
    /// как увеличивать хеш-таблицу: надо ли пересваивать новые хеши
    /// спросить про двойное хеширование
    /// </summary>
    class HashTable
    {
        private int Size=500;
        private const double GOLDEN_RATIO = 0.618033;
        private double Fullness = 0;
        public string[] Cars;
     
        public HashTable()
        {
            Cars = new string[Size];
        }

        public int GetHash(string brandAndModel)
        {
            int s = 0;

            for (int i = 0; i < brandAndModel.Length; i++)
                s += brandAndModel[i];

            int hash1 = ((int)Math.Floor(Size * (s * GOLDEN_RATIO % 1)));
            int hash2 = s % 50;
            if (hash2 == 0) hash2 =50;
            int j = 0, hash;
            while (true)
            {
                hash = (hash1 + j * hash2) % Size;
                if (Cars[hash] == null) break;
                j++;
            }

            return hash;
        }

        public void Add(string brand, string model)
        {
            if (GetFullness() > 70) Resize();
            Cars[GetHash(brand + model)] = brand +" "+ model;
            Fullness++;
        }

        public void Clear()
        {
            Array.Clear(Cars,0, Cars.Length);
            Size = 500;
            Fullness = 0;
        }

        public double GetFullness()
        {
            return Fullness / Size * 100;
        }

        public void Resize()
        {
            Size *= 2;
            string[] newCars = new string[Size];
            for (int i = 0; i < Cars.Length; i++)
                if (Cars[i] != null) newCars[i] = Cars[i];
            Cars = newCars;
        }

        public bool IsThere(string brandAndModel)
        {
            int s = 0;

            for (int i = 0; i < brandAndModel.Length; i++)
                s += brandAndModel[i];

            int hash1 = ((int)Math.Floor(Size * (s * GOLDEN_RATIO % 1)));
            int hash2 = s % 20;
            if (hash2 == 0) hash2 = 20;
            int j = 0, hash;
            while (j<Size)
            {
                hash = (hash1 + j) % Size;//А так можно?
                if (Cars[hash] == brandAndModel) return true;
                j++;
            }
            return false;
        }

        public void Delete(string brandAndModel)
        {
            int s = 0;

            for (int i = 0; i < brandAndModel.Length; i++)
                s += brandAndModel[i];

            int hash1 = ((int)Math.Floor(Size * (s * GOLDEN_RATIO % 1)));
            int hash2 = s % 20;
            if (hash2 == 0) hash2 = 20;
            int j = 0, hash;
            while (j < Size)
            {
                hash = (hash1 + j) % Size;//А так можно?
                if (Cars[hash] == brandAndModel) { Cars[hash] = null; return; }
                j++;
            }
        }
    }
}
