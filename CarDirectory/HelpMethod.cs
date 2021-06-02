using System;
using System.Collections.Generic;
using System.Drawing;
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
    
        public static bool IsEmpty(ref MaskedTextBox textBox)
        {
            return textBox.Text.Length == 0;
        }
        public static bool IsEmpty(ref TextBox textBox)
        {
            return textBox.Text.Length == 0;
        }
        public static bool IsCorrectYear(ref MaskedTextBox textBox)
        {
            int result;
            if (int.TryParse(textBox.Text, out result))
                if(result>1967&&result<2022)
                    return true;
            return false;
        }
        //public static bool IsCorrectYear(int god)
        //{
        //    if (god > 1967 && god < 2022)return true;
        //    return false;
        //}

        public static bool IsCorrectEndYear(ref MaskedTextBox textBox)
        {
            int result;
            if (textBox.Text.Length == 0) return true;
            if (int.TryParse(textBox.Text, out result))
                if (result > 1967 && result < 2022)
                    return true;
            return false;
        }

        public static void FixEndCar(ref Car car)
        {
            if (car.End == "")
                car.End = "-";
        }

        public static void CheckTextBox(ref MaskedTextBox ModelTextBox,ref MaskedTextBox BrandTextBox,ref MaskedTextBox StartTextBox,ref MaskedTextBox EndTextBox)
        {
            if (IsEmpty(ref ModelTextBox)) ModelTextBox.BackColor = Color.LightCoral;
            if (IsEmpty(ref BrandTextBox)) BrandTextBox.BackColor = Color.LightCoral;
            if (!IsCorrectYear(ref StartTextBox)) StartTextBox.BackColor = Color.LightCoral;
            if (!IsCorrectEndYear(ref EndTextBox)) EndTextBox.BackColor = Color.LightCoral;
        }
        public static void CheckTextBox(ref MaskedTextBox ModelTextBox, ref MaskedTextBox BrandTextBox)
        {
            if (IsEmpty(ref ModelTextBox)) ModelTextBox.BackColor = Color.LightCoral;
            if (IsEmpty(ref BrandTextBox)) BrandTextBox.BackColor = Color.LightCoral;
        }

    }
}
