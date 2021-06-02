using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CarDirectory.HelpMethod;

namespace CarDirectory
{
    public partial class DeleteCarForm : Form
    {
        public DeleteCarForm()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            CheckTextBox(ref ModelTextBox, ref BrandTextBox, ref StartTextBox, ref EndTextBox);
            if (!IsEmpty(ref ModelTextBox) && !IsEmpty(ref BrandTextBox) && IsCorrectYear(ref StartTextBox) && IsCorrectEndYear(ref EndTextBox))
            {
                DialogResult = DialogResult.OK;
                Hide();
            }
            else MessageBox.Show("Исправьте поля, отмеченные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public Car GetCar()
        {
            return new Car() { Brand=BrandTextBox.Text, Model =ModelTextBox.Text, Start=int.Parse(StartTextBox.Text), End=EndTextBox.Text };
        }

        private void InfoPictureBox1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.ToolTipTitle = "Ограничение на год";
            toolTip1.Show(">1967 и <2022", InfoPictureBox1, 5000);
        }

        private void BrandTextBox_Click(object sender, EventArgs e)
        {
            BrandTextBox.BackColor = Color.Beige;
        }

        private void ModelTextBox_Click(object sender, EventArgs e)
        {
            ModelTextBox.BackColor = Color.Beige;
        }

        private void StartTextBox_Click(object sender, EventArgs e)
        {
            StartTextBox.BackColor = Color.Beige;
        }

        private void EndTextBox_Click(object sender, EventArgs e)
        {
            EndTextBox.BackColor = Color.Beige;
        }

        private void BrandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            BrandTextBox.BackColor = Color.Beige;
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = ModelTextBox;
                ModelTextBox.BackColor = Color.Beige;
            }
        }

        private void ModelTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = StartTextBox;
                StartTextBox.BackColor = Color.Beige;
            }
        }

        private void StartTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = EndTextBox;
                EndTextBox.BackColor = Color.Beige;
            }
        }

        private void EndTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CheckTextBox(ref ModelTextBox, ref BrandTextBox, ref StartTextBox, ref EndTextBox); 
                if (!IsEmpty(ref ModelTextBox) && !IsEmpty(ref BrandTextBox) && IsCorrectYear(ref StartTextBox) && IsCorrectEndYear(ref EndTextBox))
                {
                    DialogResult = DialogResult.OK;
                    Hide();
                }
                else MessageBox.Show("Исправьте поля, отмеченные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
