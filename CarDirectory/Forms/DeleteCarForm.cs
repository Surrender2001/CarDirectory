using System;
using System.Drawing;
using System.Windows.Forms;
using static CarDirectory.HelpMethod;

namespace CarDirectory
{
    public partial class DeleteCarForm : Form
    {
        public DeleteCarForm()
        {
            InitializeComponent();
            ActiveControl = BrandTextBox;
        }

        public DeleteCarForm(ref RBTree<string, Car> rBTreeCar, ref RBTree<int, Car> rBTreeYear, ref DataGridView dataGridView) : this()
        {
            //this.hashTable = hashTable;
            this.rBTreeCar = rBTreeCar;
            this.rBTreeYear = rBTreeYear;
            this.dataGridView = dataGridView;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            CheckTextBox(ref BrandTextBox, ref ModelTextBox, ref StartTextBox, ref EndTextBox);
            if (!IsEmpty(ref ModelTextBox) && !IsEmpty(ref BrandTextBox) && IsCorrectStartYear(ref StartTextBox) && IsCorrectEndYear(ref EndTextBox))
                if (IsCorrectStartAndEndYear(ref StartTextBox, ref EndTextBox))
                    DeleteCar();
                else
                    MessageBox.Show("Исправьте поля, отмеченные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else MessageBox.Show("Исправьте поля, отмеченные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //private HashTable hashTable;
        private DataGridView dataGridView;

        private RBTree<int, Car> rBTreeYear;
        private RBTree<string, Car> rBTreeCar;

        public void DeleteCar()
        {
            string text = EndTextBox.Text;
            FixEndCar(ref text, out string endYear);
            bool isFound = false;
            var tmpList = rBTreeCar.GetValues(BrandTextBox.Text);
            foreach (var item in tmpList)
                if (item.Key.Model == ModelTextBox.Text && item.Key.Start == int.Parse(StartTextBox.Text) && item.Key.End == endYear)
                {
                    Car car = item.Key;
                    rBTreeCar.Remove(car.Brand, car);
                    rBTreeYear.Remove(car.Start, car);
                    isFound = true;
                }
            RefreshDataGridView(ref rBTreeCar, ref dataGridView);
            Visible = false;
            if (isFound)
                MessageBox.Show("Удаление элемента из справочника успешно завершено", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Введенный вами элемент в справочнике не найден", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BrandTextBox_Click(object sender, EventArgs e)
        {
            BrandTextBox.BackColor = ColorTranslator.FromHtml("#EEEFF7");
        }

        private void ModelTextBox_Click(object sender, EventArgs e)
        {
            ModelTextBox.BackColor = ColorTranslator.FromHtml("#EEEFF7");
        }

        private void StartTextBox_Click(object sender, EventArgs e)
        {
            StartTextBox.BackColor = ColorTranslator.FromHtml("#EEEFF7");
        }

        private void EndTextBox_Click(object sender, EventArgs e)
        {
            EndTextBox.BackColor = ColorTranslator.FromHtml("#EEEFF7");
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
                ActiveControl = StartTextBox;
                ;
            }
        }

        private void StartTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            StartTextBox.BackColor = ColorTranslator.FromHtml("#EEEFF7");
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = EndTextBox;
            }
        }

        private void EndTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EndTextBox.BackColor = ColorTranslator.FromHtml("#EEEFF7");
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = BrandTextBox;
                DeleteButton_Click(sender, e);
            }
        }

        private void BrandTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTip1.ToolTipTitle = "Неверный символ";
            toolTip1.Show("Введите буквы", BrandTextBox, 1000);
        }
    }
}