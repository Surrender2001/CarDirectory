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
using static CarDirectory.HelpMethod;

namespace CarDirectory
{
    public partial class HashForm : Form
    {
        public HashForm()
        {
            InitializeComponent();
        }

        public HashForm(ref HashTable table,ref DataGridView dataGridView,ref List<Car>cars) : this()
        {
            hashTableTemp = table;
            carList = cars;
            gridView = dataGridView;
        }

        private HashTable hashTableTemp = new HashTable();
        List<Car> carList = new List<Car>();
        List<BrandAndModel> cars = new List<BrandAndModel>();
        HashTable hashTable = new HashTable();
        DataGridView gridView = new DataGridView();

        private void ReadDbButton_Click(object sender, EventArgs e)
        {              
            dataGridView.Rows.Clear();
            hashTable.Clear();
            cars.Clear();

            try
            {
                using (var ofd = new OpenFileDialog() { Filter = "txt files (*.txt)|*.txt" })
                    if (ofd.ShowDialog() == DialogResult.OK)
                        using (var sw = new StreamReader(ofd.FileName, Encoding.Default))
                            while (!sw.EndOfStream)
                            {
                                string s = sw.ReadLine();
                                string[] subs = s.Split(new char[] { ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                hashTable.Add(new BrandAndModel(subs[0],subs[1]));
                            }
                RefreshDataGridView(ref cars, ref dataGridView, ref hashTable);
                MessageBox.Show($"Файл успешно считан, кол-во записанных моделей {cars.Count}\n" +
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

            var addForm = new AddBrandModelForm(ref hashTable,ref dataGridView);
            _ = addForm.ShowDialog();
            addForm.Dispose();

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string brand = "", model = "";
            var deleteForm = new DeleteBrandAndModelForm();
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
                            for (int i = 0; i < carList.Count; i++)
                            {
                                if (carList[i].Equals(carList[i].Brand, carList[i].Model))
                                    carList.RemoveAt(i);
                            }
                            RefreshDataGridView(ref carList, ref gridView, ref hashTableTemp);
                        }
                        else return;

                    }
                    //MessageBox.Show("Замечена запись в общем справочнике!!! Хотите удалить?", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    hashTable.Delete(brand + model);
                    cars.Remove(new BrandAndModel(brand,model));
                    dataGridView.Rows.Clear();
                    RefreshDataGridView(ref cars, ref dataGridView, ref hashTable);
                    MessageBox.Show("Удаление элемента из справочника успешно завершено", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Введенный вами элемент в справочнике не найден", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            deleteForm.Dispose();
        }
    }
}
