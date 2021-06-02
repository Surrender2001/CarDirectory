using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarDirectory.Forms
{
    public partial class DeleteBrandAndModelForm : Form
    {
        public DeleteBrandAndModelForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (checkIsEmpty())
            {
                DialogResult = DialogResult.OK;
                Hide();
            }
        }
        
        public void GetCarName(out string brand,out string model)
        {
                brand = BrandTextBox.Text;
                model = ModelTextBox.Text;
        }

        private void BrandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            BrandTextBox.BackColor = Color.Beige;
            if (e.KeyCode == Keys.Enter)
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
                if(checkIsEmpty())
                {
                    e.SuppressKeyPress = true;
                    DialogResult = DialogResult.OK;
                    Hide();
                }

            }
        }

        private bool checkIsEmpty()
        {
            bool check = true;
            if (BrandTextBox.Text.Length == 0 || ModelTextBox.Text.Length == 0)
                check = false;
            BrandTextBox.BackColor = BrandTextBox.Text.Length == 0 ? Color.LightCoral : Color.Beige;
            ModelTextBox.BackColor = ModelTextBox.Text.Length == 0 ? Color.LightCoral : Color.Beige;
            if (!check) MessageBox.Show("Заполните поля, выделенные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ActiveControl = BrandTextBox;
            return check;
        }
    }
}
