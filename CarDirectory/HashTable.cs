using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDirectory
{
    /// <summary>
    /// как увеличивать хеш-таблицу: надо ли пересваивать новые хеши
    /// Надо пересваивать 
    /// спросить про двойное хеширование
    /// </summary>
    class HashTable
    {
        
        private const int DEFAULT_SIZE = 20;
        private int Size;
        private const double GOLDEN_RATIO = 0.618033;
        private double Fullness = 0;
        public string[] Cars;
     
        public HashTable()
        {
            Size = DEFAULT_SIZE;
            Cars = new string[Size];
        }

        public int GetNewHash(string key)
        {
            int s = 0;
            for (int i = 0; i < key.Length; i++)
                s += key[i];

            int h1 = Hash1(s);
            int h2 = Hash2(s);
            int hash;
            hash = h1 % Size;
            if(Cars[hash]==null)
                return hash;
            else
            {
                for (int j = 1; j < Size; j++)
                {
                    hash = (h1 + j * h2) % Size;
                    if (Cars[hash] == null)
                        break;
                }
                return hash;
            }     
        }

        private int Hash2(int key)
        {
            for (int i = Size/2; i < Size; i++)
                if (IsCoprime(key, i)) return i;
            return Size - 1;
        }

        private bool IsCoprime(int a, int b)
        {
            return a == b
                   ? a == 1
                   : a > b
                        ? IsCoprime(a - b, b)
                        : IsCoprime(b - a, a);
        }
        private int Hash1(int key)
        {
            return (int)Math.Floor(Size * (key * GOLDEN_RATIO % 1));
        }

        public void Add(string brand,string model)
        {
            if (GetFullness() > 70) Resize();
            Cars[Hash(brand + model)] = brand+model;
            Fullness++;
        }

        public void Clear()
        {
            Array.Clear(Cars,0, Cars.Length);
            Size = DEFAULT_SIZE;
            Cars = new string[Size];
            Fullness = 0;
        }

        public double GetFullness()
        {
            return Fullness/ Size * 100;
        }

        public void Resize()
        {
            Fullness = 0;
            string[] tmpCars=new string[Size];
            Cars.CopyTo(tmpCars, 0);
            Size *= 2;
            string[] newSizeCars = new string[Size];
            Cars = newSizeCars;
            for (int i = 0; i < tmpCars.Length; i++)
                if (tmpCars[i] != null)
                    Add(tmpCars[i]);  
        }

        private void Add(string brandAndModel)
        {
            Cars[Hash(brandAndModel)] = brandAndModel;
            Fullness++;
        }

        public bool IsThere(string brandAndModel)=>FindElem(brandAndModel) != -1;

        private int FindElem(string key)
        {
            throw new NotImplementedException();
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
                hash = (hash1 + j) % Size;
                if (Cars[hash] == brandAndModel) { Cars[hash] = null; return; }
                j++;
            }
            Fullness--;
        }

        public int GetterHash(string brandAndModel)
        {
            int s = 0;

            for (int i = 0; i < brandAndModel.Length; i++)
                s += brandAndModel[i];

            int hash1 = ((int)Math.Floor(Size * (s * GOLDEN_RATIO % 1)));
            int hash2 = Size % 13;
            if (hash2 == 0) hash2 = 13;
            int j = 0, hash;
            while (true)
            {
                hash = (hash1 + j * hash2) % Size;
                if (Cars[hash] == brandAndModel) break;
                j++;
                if (j > Size) break;
            }
            return hash;
        }
    }
}
