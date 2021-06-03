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
        HashSet<string> brandSet = new HashSet<string>();
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
            brandSet.Clear();
            var openFileDialog1 = new OpenFileDialog();
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
                    Car car = new Car
                    {
                        Brand = subs[0],
                        Model = subs[1],
                        Start = int.Parse(subs[2]),
                        End = subs[3]
                    };
                    cars.Add(car);
                    hashTable.Add(new BrandAndModel(car.Brand,car.Model));
                    brandSet.Add(car.Brand);
                }
                RefreshDataGridView(ref cars,ref dataGridView,ref hashTable);
                MessageBox.Show($"Файл успешно считан, кол-во записанных машин {cars.Count}\n" +
                    $"Заполненность хеш-таблицы {Math.Round(hashTable.Fullness,2)*100}%\n" +
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
        private void AddButton_Click(object sender, EventArgs e)
        {
            var addForm = new AddForm(ref brandSet,ref cars, ref hashTable,ref dataGridView);
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
            DialogResult dialogResult = hashForm.ShowDialog();
            hashForm.Dispose();
        }
    }
}
