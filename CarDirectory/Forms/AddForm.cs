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
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
           
        }

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BrandTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
            toolTip1.ToolTipTitle = "Неверный символ";
            toolTip1.Show("Введите латинские буквы", BrandTextBox, 1000);
        }



        private void BrandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = ModelTextBox;
            }
        }

        private void ModelTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl =StartTextBox;
            }
        }

        private void StartTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = EndTextBox;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (CheckValues())
                Close();
            else return;
        }

        private bool CheckValues()
        {
            bool check = true;
            if (BrandTextBox.Text.Length == 0)
            {
                BrandTextBox.BackColor = Color.LightCoral;
                MessageBox.Show("empty field");
                check = false;
            }
            if (ModelTextBox.Text.Length == 0)
            {
                ModelTextBox.BackColor = Color.LightCoral;
                MessageBox.Show("empty field");              
                check = false;
            }
            if (StartTextBox.Text.Length == 0)
            {
                StartTextBox.BackColor = Color.LightCoral;
                MessageBox.Show("empty field");
                check = false;
            }
            return check;
        }
    }
}
