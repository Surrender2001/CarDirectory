using System;
using System.Drawing;
using System.Windows.Forms;
using static CarDirectory.HelpMethod;

namespace CarDirectory
{
    public partial class AddBrandModelForm : Form
    {
        private HashTable hashTable;
        private DataGridView dataGridView;
        private RBTree<string, string> rBTreeModel;

        public AddBrandModelForm()
        {
            InitializeComponent();
        }

        public AddBrandModelForm(ref HashTable hashTable, ref RBTree<string, string> rBTreeModel, ref DataGridView dataGridView) : this()
        {
            this.hashTable = hashTable;
            this.dataGridView = dataGridView;
            this.rBTreeModel = rBTreeModel;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            CheckTextBox(ref ModelTextBox, ref BrandTextBox);
            if (!IsEmpty(ref ModelTextBox) && !IsEmpty(ref BrandTextBox))
            {
                if (rBTreeModel.Contains(BrandTextBox.Text))
                {
                    if (!hashTable.Contains(BrandTextBox.Text + ModelTextBox.Text))
                    {
                        hashTable.Add(new BrandAndModel(BrandTextBox.Text, ModelTextBox.Text));
                        RefreshDataGridView(ref dataGridView, ref hashTable);
                        Visible = false;
                        MessageBox.Show("Введенный вами элемент успешно добавлен в справочник", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Введенный вами элемент уже содержится в справочнике", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Введенная вами марка не найдена в справочнике", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Исправьте поля, отмеченные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                AddButton_Click(sender, e);
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

        private void BrandTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTip1.ToolTipTitle = "Неверный символ";
            toolTip1.Show("Введите буквы", BrandTextBox, 1000);
        }
    }
}