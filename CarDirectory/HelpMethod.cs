using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarDirectory
{
    public static class HelpMethod
    { 

        public static void RefreshDataGridView(ref List<Car> cars,ref DataGridView dataGridView,ref HashTable hashTable)
        {
            int hash;
            dataGridView.Rows.Clear();
            foreach (var car in cars)
            {
                hash = hashTable.GetHash(car.Brand + car.Model);
                dataGridView.Rows.Add(car.Brand, car.Model, car.Start, car.End, hash);
            }
        }

        public static void RefreshDataGridView(ref List<BrandAndModel> cars, ref DataGridView dataGridView, ref HashTable hashTable)
        {
            int hash;
            dataGridView.Rows.Clear();
            foreach (var car in cars)
            {
                hash = hashTable.GetHash(car.Brand + car.Model);
                dataGridView.Rows.Add(car.Brand, car.Model,hash);
            }
        }
    }
}
