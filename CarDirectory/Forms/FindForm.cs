using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static CarDirectory.HelpMethod;

namespace CarDirectory
{
    public partial class FindForm : Form
    {
        private readonly RBTree<int, Car> rBTreeYear;
        private DataGridView dataGridView;

        public FindForm()
        {
            InitializeComponent();
            ActiveControl = StartTextBox;
        }

        public FindForm(ref RBTree<int, Car> rBTreeYear, ref DataGridView dataGridView) : this()
        {
            this.rBTreeYear = rBTreeYear;
            this.dataGridView = dataGridView;
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            CheckTextBox(ref StartTextBox, ref EndTextBox);
            if (int.TryParse(StartTextBox.Text, out int res1) && int.TryParse(EndTextBox.Text, out int res2) && IsCorrectYear(res1) && IsCorrectYear(res2) && res1 <= res2)
            {
                DoublyLinkedList<Car> dlListCars = new DoublyLinkedList<Car>();
                DoublyLinkedList<Car> dlListCarsTemp;
                dataGridView.Rows.Clear();
                int sum = 0;
                for (int i = res1; i <= res2; ++i)
                {
                    dlListCarsTemp = rBTreeYear.GetValues(i, out int count);
                    sum += count;
                    foreach (var item in dlListCarsTemp)
                        dlListCars.AddLast(item.Key);
                }
                RefreshDataGridView(ref dlListCars, ref dataGridView);
                dataGridView.Sort(dataGridView.Columns[2], ListSortDirection.Ascending);
                Visible = false;
                MessageBox.Show($"Количество сравнений: {sum}", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Некорректные данные!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ActiveControl = StartTextBox;
                FindButton_Click(sender, e);
            }
        }
    }
}