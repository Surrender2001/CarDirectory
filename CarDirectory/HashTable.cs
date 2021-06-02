using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDirectory
{
    public class BrandAndModel : IEquatable<BrandAndModel>
    {

      
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool Deleted { get; set; }
        public BrandAndModel(string brand, string model)
        {
            Brand = brand;
            Model = model;
            Deleted = false;
        }

        public bool Equals(BrandAndModel other)
        {
            return other.Brand == Brand && other.Model == Model;
        }

        public override string ToString() 
        {
            return Brand + Model;
        }
    }

    public class HashTable //: System.Collections.Generic.IEnumerable<BrandAndModel>
    {        
        private BrandAndModel[] hashtable = new BrandAndModel[DEFAULT_SIZE];
        private const double MAX_FULLNESS = 0.7;
        private const double MIN_FULLNESS = 0.2;
        private const double GOLDEN_RATIO = 0.618033;
        private const int DEFAULT_SIZE = 8;

        private int Size = DEFAULT_SIZE;
        private int Count = 0;
        public double Fullness => (double)Count / Size;
        public int Stores => Count;
        public int CurrentSize => Size;
        public void Clear()
        {
            Size = DEFAULT_SIZE;
            Count = 0;
            hashtable = new BrandAndModel[DEFAULT_SIZE];
        }

        protected int GetIndex(string key)
        {
            int i = 0;
            int hash = Hash(key, i);
            while (hashtable[hash] != null && !hashtable[hash].ToString().Equals(key))
            {
                i++;
                hash = Hash(key, i);
            }
            return hashtable[hash] != null && !hashtable[hash].Deleted ? hash : -1;
        }

        //public int GetHash(string key)
        //{
        //    if (Fullness > MAX_FULLNESS) Expand();
        //    int i = 0;
        //    int hash = Hash(key, i);
        //    while (!hashtable[hash].Equals(key))
        //    {
        //        i++;
        //        hash = Hash(key, i);
        //    }
        //    //if (hashtable[hash] == null || hashtable[hash].Deleted || !hashtable[hash].Equals(key))
        //    //{
        //    //    hashtable[hash] = bam;
        //    //    Count++;
        //    //}
        //    //hash1 = hash;

        //    return hash;
        //}
        //
        public int GetHash(string key)
        {
            int s = 0, hash1, hash2, hash = 0;
            for (int i = 0; i < key.Length; i++)
                s += key[i];

            hash1 = H1(key);
            if (hashtable[hash1].ToString() == key)
                return hash1;
            else
            {
                hash2 = H2(key);
                for (int i = 1; i < Size; ++i)
                {
                    hash = (hash1 + i * hash2) % Size;
                    if (hashtable[hash].ToString() == key)
                        return hash;
                }
            }
            return -1;
        }

        public bool Contains(string key) => GetIndex(key) != -1;
        //public string GetValue(string key)
        //{

        //    int index = GetIndex(key);
        //    return index != -1 ? hashtable[index].Brand+ hashtable[index].Model : default;
        //}
        //public BrandAndModel GetPair(string key)
        //{
        //    int index = GetIndex(key);
        //    return index != -1 ? hashtable[index] : null;
        //}
        public void Delete(BrandAndModel bam)
        {
            int index = GetIndex(bam.ToString());
            if (index != -1)
            {
                hashtable[index].Deleted = true;
                Count--;
                if (Fullness < MIN_FULLNESS) Reduce();
            }
        }
        public void Delete(string key)
        {
            int index = GetIndex(key);
            if (index != -1)
            {
                hashtable[index].Deleted = true;
                Count--;
                if (Fullness < MIN_FULLNESS) Reduce();
            }

        }

        //public void Add(string key, out index)
        //{
        //    if (Fullness > MAX_FULLNESS) Expand();
        //    int i = 0;
        //    int hash = Hash(key, i);
        //    while (hashtable[hash] != null && !hashtable[hash].Deleted && !hashtable[hash].Key.Equals(key))
        //    {
        //        i++;
        //        hash = Hash(key, i);
        //    }
        //    if (hashtable[hash] == null || hashtable[hash].Deleted || !hashtable[hash].Key.Equals(key))
        //    {
        //        hashtable[hash] = new BrandAndModel(key, value);
        //        Count++;
        //    }
        //}
        public void Add(BrandAndModel bam)
        {
            if (Fullness > MAX_FULLNESS) Expand();
            int i = 0;
            int hash = Hash(bam.ToString(), i);
            while (hashtable[hash] != null && !hashtable[hash].Deleted && !hashtable[hash].Equals(bam))
            {
                i++;
                hash = Hash(bam.ToString(), i);
            }
            if (hashtable[hash] == null || hashtable[hash].Deleted || !hashtable[hash].Equals(bam))
            {
                hashtable[hash] = bam;
                Count++;
            }
        }
        //public void Add(BrandAndModel bam, out int hash1)
        //{
        //    if (Fullness > MAX_FULLNESS) Expand();
        //    int i = 0;
        //    int hash = Hash(bam.ToString(), i);
        //    while (hashtable[hash] != null && !hashtable[hash].Deleted && !hashtable[hash].Equals(bam))
        //    {
        //        i++;
        //        hash = Hash(bam.ToString(), i);
        //    }
        //    if (hashtable[hash] == null || hashtable[hash].Deleted || !hashtable[hash].Equals(bam))
        //    {
        //        hashtable[hash] = bam;
        //        Count++;
        //    }
        //    hash1 = hash;
        //}
        private int KeyToInt(string key)
        {
            int value = 0;
            foreach (var ch in key) value += ch;
            return value;
        }
        private int Hash(string key, int i) => (H1(key) + i * H2(key)) % Size;
        private int H1(string key)
        {
            return (int)Math.Floor(Size * (KeyToInt(key) * GOLDEN_RATIO % 1)); 
        }
        private int H2(string key)
        {
            int prime=2;
            for (int i = Size - 1; i > 2; --i)
                if (IsPrime(i))
                {
                    prime = i;
                    break;
                }
           return prime;
        }

        private bool IsPrime(int a)
        {
            int count = 0;
            for (int i = 2; i * i < a; ++i)
                if (a % i == 0)
                    ++count;
            if (a == 1 || count != 0)
                return false;
            else
                return true;
        }

        private void Expand()
        {
            var oldtable = hashtable;
            Count = 0;
            Size *= 2;
            hashtable = new BrandAndModel[Size];
            foreach (var item in oldtable) 
                if (item != null) 
                    Add(item);
        }
        private void Reduce()
        {
            if (Size <= DEFAULT_SIZE) return;
            var oldtable = hashtable;
            Count = 0;
            Size /= 2;
            hashtable = new BrandAndModel[Size];
            foreach (var item in oldtable) 
                if (item != null && !item.Deleted) 
                    Add(item);
        }

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    int i = 0;
        //    while (i < Size && hashtable[i] == null) i++;
        //    if (hashtable[i] != null && !hashtable[i].Deleted) yield return hashtable[i];
        //}
        //System.Collections.Generic.IEnumerator<BrandAndModel> System.Collections.Generic.IEnumerable<BrandAndModel>.GetEnumerator()
        //{
        //    int i = 0;
        //    while (i < Size)
        //    {
        //        while (i + 1 < Size && hashtable[i] == null) i++;
        //        if (hashtable[i] != null && !hashtable[i].Deleted) yield return hashtable[i];
        //        i++;
        //    }
        //}
    }
}
