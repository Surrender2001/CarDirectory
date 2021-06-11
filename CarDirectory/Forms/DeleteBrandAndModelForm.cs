using System;
using System.Drawing;
using System.Windows.Forms;
using static CarDirectory.HelpMethod;

namespace CarDirectory.Forms
{
    public partial class DeleteBrandAndModelForm : Form
    {
        private RBTree<string, Car> rBTreeCar;
        private RBTree<int, Car> rBTreeYear;
        private RBTree<string, string> rBTreeModel;
        private HashTable hashTable;
        private HashTable hashTableMain;
        private DataGridView dataGridView;
        private DataGridView dataGridViewMain;

        public DeleteBrandAndModelForm()
        {
            InitializeComponent();
        }

        public DeleteBrandAndModelForm(ref RBTree<string, Car> rBTreeCar, ref RBTree<int, Car> rBTreeYear, ref RBTree<string, string> rBTreeModel, ref HashTable hashTable, ref HashTable hashTableMain, ref DataGridView dataGridView, ref DataGridView dataGridViewMain) : this()
        {
            this.rBTreeCar = rBTreeCar;
            this.rBTreeYear = rBTreeYear;
            this.rBTreeModel = rBTreeModel;
            this.hashTable = hashTable;
            this.hashTableMain = hashTableMain;
            this.dataGridView = dataGridView;
            this.dataGridViewMain = dataGridViewMain;
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
                DeleteButton_Click(sender, e);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            CheckTextBox(ref BrandTextBox, ref ModelTextBox);
            if (!IsEmpty(ref BrandTextBox) && !IsEmpty(ref ModelTextBox))
            {
                if (hashTable.Contains(BrandTextBox.Text + ModelTextBox.Text))
                {
                    if (hashTableMain.Contains(BrandTextBox.Text + ModelTextBox.Text))
                    {
                        DialogResult = MessageBox.Show("Обнаружены связанные записи! Желаете удалить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (DialogResult == DialogResult.Yes)
                        {
                            bool isFound = false;
                            Car car = null;
                            var tmpList = rBTreeCar.GetValues(BrandTextBox.Text);
                            foreach (var item in tmpList)
                                if (item.Key.Model == ModelTextBox.Text)
                                {
                                    car = item.Key;
                                    rBTreeCar.Remove(car.Brand, car);
                                    rBTreeYear.Remove(car.Start, car);
                                    rBTreeModel.Remove(car.Brand, "");
                                    isFound = true;
                                }
                            if (!RBTreeContains(ref rBTreeCar, BrandTextBox.Text, ModelTextBox.Text))
                            {
                                hashTable.Delete(car.Brand + car.Model);
                                hashTableMain.Delete(car.Brand + car.Model);
                            }
                            RefreshDataGridView(ref rBTreeCar, ref dataGridViewMain);
                            RefreshDataGridView(ref dataGridView, ref hashTable);
                            Visible = false;
                            DialogResult = DialogResult.OK;
                            if (isFound)
                                MessageBox.Show("Удаление элемента из справочника успешно завершено", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("Введенный вами элемент в справочнике не найден", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        hashTable.Delete(BrandTextBox.Text + ModelTextBox.Text);
                        rBTreeModel.Remove(BrandTextBox.Text, "");
                        Visible = false;
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                    MessageBox.Show("Введенный вами элемент в справочнике не найден", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Исправьте поля, отмеченные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}