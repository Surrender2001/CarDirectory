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
            BrandTextBox.BackColor = Color.Beige;
            if (e.KeyCode ==Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = ModelTextBox;
            }
        }

        private void ModelTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            ModelTextBox.BackColor = Color.Beige;
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl =StartTextBox;
            }
        }

        private void StartTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            StartTextBox.BackColor = Color.Beige;
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = EndTextBox;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddCar();
        }

        private void AddCar()
        {
            if (CheckValues())
            {
                Hide();
                Dispose();
            }
            else return;
        }

        private bool CheckValues()
        {
            return checkIsEmpty()&&checkIsCorrectYear();
        }

        private bool checkIsCorrectYear()
        {
            bool start=true, end=true;

            if (!(EndTextBox.Text.Length == 0 || EndTextBox.Text.Length == 4))
            {
                //MessageBox.Show("Некорректно введен год", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ActiveControl = EndTextBox;
                EndTextBox.BackColor = Color.LightCoral;
                end = false;
            }

            if(StartTextBox.Text.Length != 4)
            {
                //MessageBox.Show("Некорректно введен год", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                ActiveControl = StartTextBox;
                StartTextBox.BackColor = Color.LightCoral;
                start = false;
            }

            if(!(start&&end))
                MessageBox.Show("Некорректно введен год", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            return start &&end;
        }

        private bool checkIsEmpty()
        {   
            bool check = true;
            if (BrandTextBox.Text.Length == 0 || ModelTextBox.Text.Length == 0 || StartTextBox.Text.Length == 0)
                check = false;
            BrandTextBox.BackColor = BrandTextBox.Text.Length == 0 ? Color.LightCoral : Color.Beige;
            ModelTextBox.BackColor = ModelTextBox.Text.Length == 0 ? Color.LightCoral : Color.Beige;
            StartTextBox.BackColor = StartTextBox.Text.Length == 0 ? Color.LightCoral : Color.Beige;

            if (!check) MessageBox.Show("Заполните поля, выделенные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ActiveControl = BrandTextBox;
            return check;
        }

        private void EndTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EndTextBox.BackColor = Color.Beige;
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                AddCar();
            }
        }
    }
}
