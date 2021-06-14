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
        private DataGridView dataGridView;

        private DataGridView dataGridViewMain;

        public DeleteBrandAndModelForm()
        {
            InitializeComponent();
        }

        public DeleteBrandAndModelForm(ref RBTree<string, Car> rBTreeCar, ref RBTree<int, Car> rBTreeYear, ref RBTree<string, string> rBTreeModel, ref HashTable hashTable, ref DataGridView dataGridView, ref DataGridView dataGridViewMain) : this()
        {
            this.rBTreeCar = rBTreeCar;
            this.rBTreeYear = rBTreeYear;
            this.rBTreeModel = rBTreeModel;
            this.hashTable = hashTable;
            this.dataGridView = dataGridView;
            this.dataGridViewMain = dataGridViewMain;
        }

        private void BrandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            BrandTextBox.BackColor = ColorTranslator.FromHtml("#EEEFF7");
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = ModelTextBox;
            }
        }

        private void ModelTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            ModelTextBox.BackColor = ColorTranslator.FromHtml("#EEEFF7");
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = BrandTextBox;
                DeleteButton_Click(sender, e);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            CheckTextBox(ref BrandTextBox, ref ModelTextBox);
            if (!IsEmpty(ref BrandTextBox) && !IsEmpty(ref ModelTextBox))
            {
                if (hashTable.Contains(BrandTextBox.Text + ModelTextBox.Text))
                {
                    bool foundDublicate = false;
                    var lst = rBTreeCar.GetValues(BrandTextBox.Text);
                    foreach (var item in lst)
                        if (item.Key.Brand == BrandTextBox.Text && item.Key.Model == ModelTextBox.Text)
                        {
                            DialogResult = MessageBox.Show("Обнаружены связанные записи! Желаете удалить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (DialogResult == DialogResult.Yes)
                            {
                                bool isFound = false;
                                Car car = null;
                                var tmpList = rBTreeCar.GetValues(BrandTextBox.Text);
                                foreach (var it in tmpList)
                                    if (it.Key.Model == ModelTextBox.Text)
                                    {
                                        car = it.Key;
                                        rBTreeCar.Remove(car.Brand, car);
                                        rBTreeYear.Remove(car.Start, car);
                                        rBTreeModel.Remove(car.Brand, "");
                                        isFound = true;
                                        foundDublicate = true;
                                    }
                                if (!RBTreeContains(ref rBTreeCar, BrandTextBox.Text, ModelTextBox.Text))
                                    hashTable.Delete(car.Brand + car.Model);
                                RefreshDataGridView(ref rBTreeCar, ref dataGridViewMain);
                                RefreshDataGridView(ref dataGridView, ref hashTable);
                                Visible = false;
                                if (isFound)
                                    MessageBox.Show("Удаление элемента из справочника успешно завершено", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Введенный вами элемент в справочнике не найден", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    if (!foundDublicate)
                    {
                        hashTable.Delete(BrandTextBox.Text + ModelTextBox.Text);
                        rBTreeModel.Remove(BrandTextBox.Text, "");
                        RefreshDataGridView(ref dataGridView, ref hashTable);
                        Visible = false;
                        MessageBox.Show("Удаление элемента из справочника успешно завершено", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("Введенный вами элемент в справочнике не найден", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Исправьте поля, отмеченные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BrandTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTip1.ToolTipTitle = "Неверный символ";
            toolTip1.Show("Введите буквы", BrandTextBox, 1000);
        }
    }
}