using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CarDirectory
{
    public static class HelpMethod
    {
        public static void RefreshDataGridView(ref DataGridView dataGridView, ref HashTable hashTable)
        {
            dataGridView.Rows.Clear();
            hashTable.DisplayOnDataGrisView(ref dataGridView);
        }

        public static void RefreshDataGridView(ref DoubleLinkedList<Car> cars, ref DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            foreach (var car in cars)
                dataGridView.Rows.Add(car.Key.Brand, car.Key.Model, car.Key.Start, car.Key.End);
            dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Ascending);
        }

        public static void RefreshDataGridView(ref RBTree<string, Car> rBTreeCar, ref DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            rBTreeCar.Inorder(ref dataGridView, rBTreeCar.root);
            dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Ascending);
        }

        public static bool IsEmpty(ref MaskedTextBox textBox) => textBox.Text.Length == 0;

        public static bool IsCorrectStartYear(ref MaskedTextBox textBox)
        {
            if (int.TryParse(textBox.Text, out int result))
                if (result > 1967 && result < 2022)
                    return true;
            return false;
        }

        public static bool IsCorrectYear(int v) => v > 1967 && v < 2022;

        public static bool IsCorrectEndYear(ref MaskedTextBox textBox)
        {
            if (textBox.Text.Length == 0) return true;
            if (int.TryParse(textBox.Text, out int result))
                if (result > 1967 && result < 2022)
                    return true;
            return false;
        }

        public static bool IsCorrectEndYear(string end)
        {
            if (end == "-") return true;
            if (int.TryParse(end, out int result))
                if (result > 1967 && result < 2022)
                    return true;
            return false;
        }

        public static void FixEndCar(ref string text, out string endYear)
        {
            if (text == "")
                endYear = "-";
            else endYear = text;
        }

        public static void FixEndCar(ref Car car)
        {
            if (car.End == "")
                car.End = "-";
        }

        public static void CheckTextBox(ref MaskedTextBox BrandTextBox, ref MaskedTextBox ModelTextBox, ref MaskedTextBox StartTextBox, ref MaskedTextBox EndTextBox)
        {
            if (IsEmpty(ref ModelTextBox)) ModelTextBox.BackColor = Color.LightCoral;
            if (IsEmpty(ref BrandTextBox)) BrandTextBox.BackColor = Color.LightCoral;
            if (!IsCorrectStartYear(ref StartTextBox)) StartTextBox.BackColor = Color.LightCoral;
            if (!IsCorrectEndYear(ref EndTextBox)) EndTextBox.BackColor = Color.LightCoral;
            if (!IsCorrectStartAndEndYear(ref StartTextBox, ref EndTextBox))
            {
                StartTextBox.BackColor = Color.LightCoral;
                EndTextBox.BackColor = Color.LightCoral;
            }
        }

        public static void CheckTextBox(ref MaskedTextBox BrandTextBox, ref MaskedTextBox ModelTextBox)
        {
            if (IsEmpty(ref ModelTextBox)) ModelTextBox.BackColor = Color.LightCoral;
            if (IsEmpty(ref BrandTextBox)) BrandTextBox.BackColor = Color.LightCoral;
        }

        public static bool IsCorrectDates(ref RBTree<string, Car> rBTreeCar, ref Car car)
        {
            var templist = rBTreeCar.GetValues(car.Brand);
            foreach (var item in templist)
                if (item.Key.Brand == car.Brand && item.Key.Model == car.Model)
                {
                    if (item.Key.End != "-" && car.End != "-")
                    {
                        if (!((car.Start < item.Key.Start && int.Parse(car.End) < item.Key.Start) || (car.Start > int.Parse(item.Key.End) && int.Parse(car.End) > int.Parse(item.Key.End))))
                            return false;
                    }
                    else if (item.Key.End == "-" && car.End != "-")
                    {
                        if (!(car.Start < item.Key.Start && int.Parse(car.End) < item.Key.Start))
                            return false;
                    }
                    else if (item.Key.End != "-" && car.End == "-")
                    {
                        if (!(car.Start > int.Parse(item.Key.End)))
                            return false;
                    }
                    else return false;
                }
            return true;
        }

        public static bool IsCorrectStartAndEndYear(ref MaskedTextBox StartTextBox, ref MaskedTextBox EndTextBox)
        {
            if (!IsEmpty(ref EndTextBox))
                return int.Parse(StartTextBox.Text) < int.Parse(EndTextBox.Text);
            return true;
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static bool RBTreeContains(ref RBTree<string, Car> rBTree, string brand, string model)
        {
            var tmpList = rBTree.GetValues(brand);
            foreach (var item in tmpList)
                if (item.Key.Model == model)
                    return true;
            return false;
        }
    }
}