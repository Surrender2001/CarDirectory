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
    public partial class AddBrandModelForm : Form
    {
        private HashTable hashTable;
        private DataGridView dataGridView;
        public AddBrandModelForm()
        {
            InitializeComponent();
        }

        public AddBrandModelForm(ref HashTable hashTable,ref DataGridView dataGridView)
        {
            this.hashTable = hashTable;
            this.dataGridView = dataGridView;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            CheckTextBox(ref ModelTextBox, ref BrandTextBox);
            if (!IsEmpty(ref ModelTextBox) && !IsEmpty(ref BrandTextBox))
            {

                    if (!hashTable.Contains(BrandTextBox.Text + ModelTextBox.Text))
                    {
                        hashTable.Add(new BrandAndModel(BrandTextBox.Text , ModelTextBox.Text));
                        MessageBox.Show("Введенный вами элемент успешно добавлен в справочник", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Введенный вами элемент уже находится в справочнике", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Исправьте поля, отмеченные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }


        public BrandAndModel GetBrandAndModel()
        {

            return new BrandAndModel(BrandTextBox.Text, ModelTextBox.Text);
        }

        private void BrandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
                CheckTextBox(ref ModelTextBox, ref BrandTextBox);
                if (!IsEmpty(ref ModelTextBox) && !IsEmpty(ref BrandTextBox))
                {
                    DialogResult = DialogResult.OK;
                    Hide();
                }
                else MessageBox.Show("Исправьте поля, отмеченные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BrandTextBox_Click(object sender, EventArgs e)
        {
            BrandTextBox.BackColor = Color.Beige;
        }

        private void ModelTextBox_Click(object sender, EventArgs e)
        {
            ModelTextBox.BackColor = Color.Beige;
        }
    }
}
