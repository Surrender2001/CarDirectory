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
    public partial class FindBrandAndModelForm : Form
    {
        private HashTable hashTable;
        private DataGridView dataGridView;

        public FindBrandAndModelForm()
        {
            InitializeComponent();
        }

        public FindBrandAndModelForm(ref HashTable hashTable, ref DataGridView dataGridView) : this()
        {
            this.hashTable = hashTable;
            this.dataGridView = dataGridView;
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            CheckTextBox(ref BrandTextBox, ref ModelTextBox);
            if (!IsEmpty(ref ModelTextBox) && !IsEmpty(ref BrandTextBox))
            {
                if (hashTable.Contains(BrandTextBox.Text + ModelTextBox.Text))
                {
                    for (int i = 0; i < dataGridView.Rows.Count; ++i)
                        if (dataGridView.Rows[i].Cells[0].Value.ToString() == BrandTextBox.Text && dataGridView.Rows[i].Cells[1].Value.ToString() == ModelTextBox.Text)
                        {
                            dataGridView.Rows[i].Selected = true;
                            Visible = false;
                            break;
                        }
                }
                else MessageBox.Show("Введенная вами марка не найдена в справочнике", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Исправьте поля, отмеченные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                e.SuppressKeyPress = true;
                ActiveControl = BrandTextBox;
                FindButton_Click(sender, e);
            }
        }
    }
}