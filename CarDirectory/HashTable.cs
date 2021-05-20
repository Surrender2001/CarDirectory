using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDirectory
{
    class HashTable
    {
        
        private const int DEFAULT_SIZE = 5;
        public int Size { get; private set; }
        private const double GOLDEN_RATIO = 0.618033;
        private double Fullness = 0;
        private string[] Cars;
     
        public HashTable()
        {
            Size = DEFAULT_SIZE;
            Cars = new string[Size];
        }

        public int GetNewHash(string key)
        {
            int s = 0, prime = 0,hash1,hash2,hash=0;
            for (int i = 0; i < key.Length; i++)
                s += key[i];

            hash1 = (int)Math.Floor(Size * (s * GOLDEN_RATIO % 1)) ;
            if (Cars[hash1] == null)
                return hash1;
            else
            {
                for (int i = Size-1; i > 2; --i)
                    if (IsPrime(i))
                    {
                        prime = i;
                        break;
                    }
                hash2=prime;
                for (int i = 1; i < Size; ++i)
                {
                    hash = (hash1 + i * hash2) % Size;
                    if (Cars[hash] == null)
                        return hash;
                }
            }


            return -1;
        }
        
        public int GetHash(string key)
        {
            int s = 0, prime = 0, hash1, hash2, hash=0;
            for (int i = 0; i < key.Length; i++)
                s += key[i];

            hash1 = (int)Math.Floor(Size * (s * GOLDEN_RATIO % 1));
            if (Cars[hash1] == key)
                return hash1;
            else
            {
                for (int i = Size - 1; i > 2; --i)
                    if (IsPrime(i))
                    {
                        prime = i;
                        break;
                    }
                hash2 = prime;
                for (int i = 1; i < Size; ++i)
                {
                    hash = (hash1 + i * hash2) % Size;
                    if (Cars[hash] == key)
                        return hash;
                }
            }
            return -1;
        }

        private bool IsPrime(int a)
        {
            int count = 0;
            for (int i = 2; i*i < a; ++i)
                if (a % i == 0)
                    ++count;  
            if (a == 1 || count != 0) 
                return false;
            else 
                return true;
        }

        public void Add(string brand,string model,out int hash)
        {
            hash = GetNewHash(brand + model);
            Cars[hash] = brand+" "+model;
            Fullness++;
        }

        public void Clear()
        {
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
            string[] tmpCars = new string[Size];
            Cars.CopyTo(tmpCars, 0);
            Size *= 2;
            Cars =new string[Size];
            for (int i = 0; i < tmpCars.Length; i++)
                if (tmpCars[i] != null)
                    Add(tmpCars[i]);
        }

        private void Add(string brandAndModel)
        {
            Cars[GetNewHash(brandAndModel)] = brandAndModel;
            Fullness++;
        }

        public bool IsThere(string brandAndModel)=>GetHash(brandAndModel) != -1;


        public void Delete(string key)
        {
            int s = 0, prime = 0, hash1, hash2, hash = 0;
            for (int i = 0; i < key.Length; i++)
                s += key[i];

            hash1 = (int)Math.Floor(Size * (s * GOLDEN_RATIO % 1));
            if (Cars[hash1] == key)
                Cars[hash1] = null;
            else
            {
                for (int i = Size - 1; i > 2; --i)
                    if (IsPrime(i))
                    {
                        prime = i;
                        break;
                    }
                hash2 = prime;
                for (int i = 1; i < Size; ++i)
                {
                    hash = (hash1 + i * hash2) % Size;
                    if (Cars[hash] == key)
                        Cars[hash] = null;
                }
            }
            Fullness--;
        }
    }
}
