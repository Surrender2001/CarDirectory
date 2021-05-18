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
        HashSet<string> setBrand=new HashSet<string>();
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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "")
                return;


            using (StreamReader sr = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Car car = new Car
                    {
                        Brand = s.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[0],
                        Model = s.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[1],
                        Start = int.Parse(s.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[2]),
                        End= s.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[3]
                    };
                    car.Hash = hashTable.GetHash(car.Brand + car.Model);
                    cars.Add(car);
                    if (hashTable.GetFullness() > 70) hashTable.Resize();
                    hashTable.Add(car.Brand, car.Model);
                    dataGridView.Rows.Add(car.Brand, car.Model, car.Start, car.End, hashTable.GetHash(car.Brand + car.Model));
                }
            }


        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            DialogResult dialogResult = addForm.ShowDialog();
            if(dialogResult==DialogResult.OK)
            {
                Car car=addForm.AddNewCar();
                if(car!=null)
                {
                    if(!setBrand.Contains(car.Brand))
                    {
                        MessageBox.Show("Введенная марка автомобиля не содержится в справочнике", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        addForm.Dispose();
                        return;
                    }    
                    if (!hashTable.IsThere(car.Brand+" "+car.Model))
                    {
                        if (car.End == "") car.End = "-";
                        car.Hash= hashTable.GetHash(car.Brand + car.Model);
                        cars.Add(car);
                        hashTable.Add(car.Brand,car.Model);
                        dataGridView.Rows.Add(car.Brand,car.Model,car.Start,car.End,car.Hash);
                        MessageBox.Show("Введенный вами элемент успешно добавлен в справочник", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Введенный вами элемент уже находится в справочнике", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
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
            string brand = "", model = "";
            DeleteForm deleteForm = new DeleteForm();
            DialogResult dialogResult = deleteForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                deleteForm.GetCarName(out brand,out  model);
                if (hashTable.IsThere(brand +" "+ model))
                {
                    hashTable.Delete(brand + " "+ model);
                    cars.Remove(new Car() { Brand = brand, Model = model });
                    dataGridView.Rows.Clear();
                    RefreshDataGridView();
                    MessageBox.Show("Удаление элемента из справочника успешно завершено", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Введенный вами элемент в справочнике не найден", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

            }
            deleteForm.Dispose();
        }

        private void RefreshDataGridView()
        {
            foreach (var car in cars)
                dataGridView.Rows.Add(car.Brand, car.Model, car.Start, car.End, car.Hash);
        }
    }
}
