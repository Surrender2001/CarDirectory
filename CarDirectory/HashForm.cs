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

namespace CarDirectory
{
    public partial class HashForm : Form
    {
        public HashForm()
        {
            InitializeComponent();
        }
        public HashForm(ref HashTable table)
        {
            
        }

        List<BrandAndModel> cars = new List<BrandAndModel>();
        HashTable hashTable = new HashTable();

        private void ReadDbButton_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog();                
            dataGridView.Rows.Clear();
            hashTable.Clear();
            cars.Clear();
            openFileDialog1.Filter = "Справочник (*.txt)|*.txt";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "")
            {
                return;
            }

            StreamReader input = null;
            try
            {
                int hash = 0;
                input = new StreamReader(openFileDialog1.FileName, Encoding.Default);

                while (!input.EndOfStream)
                {
                    string s = input.ReadLine();
                    string[] subs = s.Split(new char[] { ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    //check correct values
                    var car = new BrandAndModel(subs[0], subs[1]);
                    
                    cars.Add(car);
                    hashTable.Add(new BrandAndModel(car.Brand, car.Model));
                    //dataGridView.Rows.Add(car.Brand, car.Model, car.Start, car.End, hash);                        
                }
                RefreshDataGridView();
                MessageBox.Show($"Файл успешно считан, кол-во записанных машин {cars.Count}\n" +
                    $"Заполненность хеш-таблицы {Math.Round(hashTable.Fullness, 2) * 100}%\n" +
                    $"Вместительность {hashTable.CurrentSize}",
                    "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {
                MessageBox.Show($"где-то ошибка, кол-во записанных машин {cars.Count}", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                input.Close();
            }


        }            
        private void RefreshDataGridView()
        {
            int hash = 0;
            dataGridView.Rows.Clear();
            foreach (var car in cars)
            {
                hash = hashTable.GetHash(car.Brand + car.Model);
                dataGridView.Rows.Add(car.Brand, car.Model,hashTable.GetHash(car.Brand + car.Model));
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {

            var addForm = new AddForm();
            DialogResult dialogResult = addForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Car car = addForm.AddNewCar();
                //if(car!=null)
                //{

                //    //if (!hashTable.IsThere(car.Brand + " " + car.Model))
                //    //{
                //    //    if (hashTable.GetFullness() > 70)
                //    //    {
                //    //        hashTable.Resize();
                //    //        RefreshDataGridView();
                //    //    }
                //    //    if (car.End == "") car.End = "-";
                //    //    cars.Add(car);
                //    //    hashTable.Add(car.Brand, car.Model, out int hash);
                //    //    dataGridView.Rows.Add(car.Brand, car.Model, car.Start, car.End, hash);
                //    //    MessageBox.Show("Введенный вами элемент успешно добавлен в справочник", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    //}
                //    //else MessageBox.Show("Введенный вами элемент уже находится в справочнике", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}

            }
            addForm.Dispose();

        }
    }
}
