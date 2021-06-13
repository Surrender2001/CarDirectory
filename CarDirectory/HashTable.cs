using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CarDirectory
{
    public class BrandAndModel : IEquatable<BrandAndModel>
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool Deleted { get; set; }

        public HashTable HashTable
        {
            get => default;
            set
            {
            }
        }

        public BrandAndModel(string brand, string model)
        {
            Brand = brand;
            Model = model;
            Deleted = false;
        }

        public bool Equals(BrandAndModel other) => other.Brand == Brand && other.Model == Model;

        public override string ToString() => Brand + Model;
    }

    public class HashTable
    {
        private BrandAndModel[] hashtable = new BrandAndModel[DEFAULT_SIZE];
        private const double MAX_FULLNESS = 0.7;
        private const double MIN_FULLNESS = 0.2;
        private const double GOLDEN_RATIO = 0.618033;
        private const int DEFAULT_SIZE = 20;

        private int Size = DEFAULT_SIZE;
        public int Count { get; private set; } = 0;
        public double Fullness => (double)Count / Size;
        public int CurrentSize => Size;

        public void Clear()
        {
            Size = DEFAULT_SIZE;
            Count = 0;
            hashtable = new BrandAndModel[DEFAULT_SIZE];
        }

        private int GetIndex(string key)
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

        public bool Contains(string key) => GetIndex(key) != -1;

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

        private int KeyToInt(string key)
        {
            int value = 0;
            foreach (var ch in key) value += ch;
            return value;
        }

        public int Hash(string key, int i) => (H1(key) + i * H2(key)) % Size;

        private int H1(string key) => (int)Math.Floor(Size * (KeyToInt(key) * GOLDEN_RATIO % 1));

        private int H2(string key)
        {
            int prime = 2;
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

        public void DisplayOnDataGrisView(ref DataGridView dataGridView)
        {
            for (int i = 0; i < Size; ++i)
                if (hashtable[i] != null && !hashtable[i].Deleted)
                    dataGridView.Rows.Add(hashtable[i].Brand, hashtable[i].Model, Hash(hashtable[i].Brand + hashtable[i].Model, 0), GetIndex(hashtable[i].Brand + hashtable[i].Model));
            dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Ascending);
        }
    }
}