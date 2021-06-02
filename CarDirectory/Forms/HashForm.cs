using CarDirectory.Forms;
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
            InitializeComponent();
            hashTableTemp = table;
        }

        private HashTable hashTableTemp = new HashTable();

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

            var addForm = new AddBrandModelForm();
            DialogResult dialogResult = addForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var car = addForm.GetBrandAndModel();
                if (car != null)
                {
                    
                    if (!hashTable.Contains(car.Brand + car.Model))
                    {
                        //if (hashTable.GetFullness() > 70)
                        //{
                        //    hashTable.Resize();
                        //}
                        cars.Add(car);
                        hashTable.Add(new BrandAndModel(car.Brand,car.Model));
                        //dataGridView.Rows.Add(car.Brand, car.Model, hash);
                        MessageBox.Show("Введенный вами элемент успешно добавлен в справочник", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Введенный вами элемент уже находится в справочнике", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDataGridView();
                }

            }
            addForm.Dispose();

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string brand = "", model = "";
            var deleteForm = new DeleteForm();
            DialogResult dialogResult = deleteForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                deleteForm.GetCarName(out brand, out model);



                if (hashTable.Contains(brand + model))
                {
                    if(hashTableTemp.Contains(brand + model))
                    {
                        DialogResult result = MessageBox.Show("Замечена запись в общем справочнике!!! Хотите удалить?", 
                            "Информация об элементе", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            hashTableTemp.Delete(new BrandAndModel(brand, model));
                        }
                        else return;

                    }
                    //MessageBox.Show("Замечена запись в общем справочнике!!! Хотите удалить?", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    hashTable.Delete(brand + model);
                    cars.Remove(new BrandAndModel(brand,model));
                    dataGridView.Rows.Clear();
                    RefreshDataGridView();
                    MessageBox.Show("Удаление элемента из справочника успешно завершено", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Введенный вами элемент в справочнике не найден", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            deleteForm.Dispose();
        }
    }
}
