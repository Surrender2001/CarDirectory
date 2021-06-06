using CarDirectory.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CarDirectory.HelpMethod;

namespace CarDirectory
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent(); 
        }
        
        List<Car> cars = new List<Car>();
        HashTable hashTable = new HashTable();
        RBTree<int, Car> rBTreeYear = new RBTree<int, Car>();
        RBTree<string, string> rBTreeModel = new RBTree<string, string>(); 
        private void CloseLabel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseLabel_MouseEnter(object sender, EventArgs e)
        {
            CloseLabel.ForeColor = Color.Red;
        }


        private void CloseLabel_MouseLeave(object sender, EventArgs e)
        {
            CloseLabel.ForeColor = Color.Lime;
        }

        private void ReadDbButton_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            hashTable.Clear();
            cars.Clear();
            rBTreeYear.Clear();
            rBTreeModel.Clear();
            try
            {
                using (var ofd = new OpenFileDialog() { Filter = "txt files (*.txt)|*.txt" })
                    if (ofd.ShowDialog() == DialogResult.OK)
                        using (var sw = new StreamReader(ofd.FileName, Encoding.Default))
                            while (!sw.EndOfStream)
                            {
                                string s = sw.ReadLine();
                                string[] subs = s.Split(new char[] { ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                if (!int.TryParse(subs[2], out int result) || !IsCorrectEndYear(subs[2]))
                                    throw new Exception("Ошибка чтения файла");
                                var car = new Car
                                {
                                    Brand = subs[0],
                                    Model = subs[1],
                                    Start = int.Parse(subs[2]),
                                    End = subs[3]
                                };
                                cars.Add(car);
                                hashTable.Add(new BrandAndModel(car.Brand, car.Model));
                                rBTreeYear.Add(car.Start, car);
                                rBTreeModel.Add(car.Brand, car.Model);
                            }
                RefreshDataGridView(ref cars, ref dataGridView, ref hashTable);
                MessageBox.Show($"Файл успешно считан, кол-во записанных машин {cars.Count}\n" +
                    $"Заполненность хеш-таблицы {Math.Round(hashTable.Fullness, 2) * 100}%\n" +
                    $"Вместительность {hashTable.CurrentSize}",
                    "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}, кол-во записанных машин {cars.Count}", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            var addForm = new AddForm( ref cars, ref hashTable,ref rBTreeModel,ref rBTreeYear, ref dataGridView);
            _ = addForm.ShowDialog();
            addForm.Dispose();
        }

        Point lastPoint;
        private void NameOfProjectLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }
        private void NameOfProjectLabel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteCarForm deleteForm = new DeleteCarForm(ref cars, ref hashTable, ref dataGridView);
            _ = deleteForm.ShowDialog();
            deleteForm.Dispose();
        }
        private void HashButton_Click(object sender, EventArgs e)
        {
            var hashForm = new HashForm(ref hashTable,ref dataGridView,ref cars);
            _ = hashForm.ShowDialog();
            hashForm.Dispose();
        }
        DoubleLinkedList<Car> dlListCars = new DoubleLinkedList<Car>();
        private void FindButton_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            DoubleLinkedList<Car> dlListCarsTemp;

            for (int i = 2005; i < 2010; i++)
            {
                dlListCarsTemp = rBTreeYear.GetValues(i);
                foreach (var item in dlListCarsTemp)
                    dlListCars.AddLast(item.Key);
            }
            RefreshDataGridView(ref dlListCars, ref dataGridView, ref hashTable);

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog() {Filter = "txt files (*.txt)|*.txt"})
            if (sfd.ShowDialog() == DialogResult.OK)
                using (var sw = new StreamWriter(sfd.FileName))
                    foreach (var car in cars)
                        sw.WriteLine(car);
        }
    }
}
