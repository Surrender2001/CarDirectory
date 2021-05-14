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

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseLabel_MouseEnter(object sender, EventArgs e)
        {

        }

        private void CloseLabel_MouseEnter_1(object sender, EventArgs e)
        {
            CloseLabel.ForeColor = Color.Red;
        }

        private void CloseLabel_MouseLeave(object sender, EventArgs e)
        {
            CloseLabel.ForeColor = Color.Lime;
        }

        private void ReadDbButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();// database
            DataTable table = new DataTable();// table for reading

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `car_dictionary`",db.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            
            object[] cell = new object[5];

            foreach (DataRow row in table.Rows)
            {
                // получаем все ячейки строки
                
                row.ItemArray.CopyTo(cell,0);
                cell[4] = "fdgg";              
           
                dataGridView.Rows.Add(cell);
            }


        }
    }
}
