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
                cell[4] = 0;              
                dataGridView.Rows.Add(cell);
                cars.Add(new Car(cell));
                hashtable.Add((string)cell[0], (string)cell[1]);
            }
        }



        private void testButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cars.Count.ToString());
        }
    }
}
