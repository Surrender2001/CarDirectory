using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        HashTable hashtable = new HashTable();
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
            cars.Clear();
            try
            {
                DB db = new DB();// database
                DataTable table = new DataTable();// table for reading

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `car_dictionary`", db.getConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);
                object[] cell = new object[5];
                foreach (DataRow row in table.Rows)
                {
                    row.ItemArray.CopyTo(cell,0);
                    if(!setBrand.Contains((string)cell[0]))
                        setBrand.Add((string)cell[0]);
                    cell[4] = hashtable.GetHash((string)cell[0]+(string)cell[1]);              
                    dataGridView.Rows.Add(cell);
                    cars.Add(new Car(cell));
                    hashtable.Add((string)cell[0], (string)cell[1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    if (!hashtable.IsThere(car.Brand+car.Model))
                    {
                        car.Hash= hashtable.GetHash(car.Brand + car.Model);
                        cars.Add(car);
                        hashtable.Add(car.Brand,car.Model);
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


    }
}
