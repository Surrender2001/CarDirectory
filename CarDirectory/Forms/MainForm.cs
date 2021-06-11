using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static CarDirectory.HelpMethod;

namespace CarDirectory
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private HashTable hashTable = new HashTable();
        private RBTree<int, Car> rBTreeYear = new RBTree<int, Car>();
        private RBTree<string, Car> rBTreeCar = new RBTree<string, Car>();

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseLabel_MouseEnter(object sender, EventArgs e)
        {
            CloseLabel.ForeColor = Color.Red;
        }

        private void CloseLabel_MouseLeave(object sender, EventArgs e)
        {
            CloseLabel.ForeColor = Color.Lime;
        }

        private void ReadDbButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ofd = new OpenFileDialog() { Filter = "txt files (*.txt)|*.txt" })
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        dataGridView.Rows.Clear();
                        hashTable.Clear();
                        rBTreeYear.Clear();
                        using (var sw = new StreamReader(ofd.FileName, Encoding.Default))
                            while (!sw.EndOfStream)
                            {
                                string s = sw.ReadLine();
                                string[] subs = s.Split(new char[] { ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                if (!int.TryParse(subs[2], out int result) || !IsCorrectYear(result) || !IsCorrectEndYear(subs[3]) || !Regex.IsMatch(subs[0], @"^[a-zA-ZА-Яа-я- ]+$") || !Regex.IsMatch(subs[1], @"^[A-Za-zА-Яа-я0-9-&()/+ ]+$"))
                                    throw new Exception($"Ошибка чтения файла brand: {subs[0]} model: {subs[1]}");
                                var car = new Car
                                {
                                    Brand = subs[0],
                                    Model = subs[1],
                                    Start = int.Parse(subs[2]),
                                    End = subs[3]
                                };
                                hashTable.Add(new BrandAndModel(car.Brand, car.Model));
                                rBTreeYear.Add(car.Start, car);
                                rBTreeCar.Add(car.Brand, car);
                            }
                        RefreshDataGridView(ref rBTreeCar, ref dataGridView);
                        MessageBox.Show($"Заполненность хеш-таблицы {Math.Round(hashTable.Fullness, 2) * 100}%\n" +
                            $"Вместительность {hashTable.CurrentSize}",
                            "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} ", "Информация об элементе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var addForm = new AddForm(ref hashTable, ref rBTreeCar, ref rBTreeYear, ref dataGridView);
            _ = addForm.ShowDialog();
            addForm.Dispose();
        }

        private Point lastPoint;

        private void NameOfProjectLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }

        private void NameOfProjectLabel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteCarForm deleteForm = new DeleteCarForm(ref hashTable, ref rBTreeCar, ref rBTreeYear, ref dataGridView);
            _ = deleteForm.ShowDialog();
            deleteForm.Dispose();
        }

        private void HashButton_Click(object sender, EventArgs e)
        {
            var hashForm = new HashForm(ref hashTable, ref rBTreeCar, ref rBTreeYear, ref dataGridView);
            _ = hashForm.ShowDialog();
            hashForm.Dispose();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            var findForm = new FindForm(ref rBTreeYear, ref dataGridView);
            _ = findForm.ShowDialog();
            findForm.Dispose();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog() { Filter = "txt files (*.txt)|*.txt" })
                if (sfd.ShowDialog() == DialogResult.OK)
                    using (var sw = new StreamWriter(sfd.FileName))
                        for (int i = 0; i < dataGridView.Rows.Count; ++i)
                            sw.WriteLine($"{dataGridView.Rows[i].Cells[0].Value}\t{dataGridView.Rows[i].Cells[1].Value}\t{dataGridView.Rows[i].Cells[2].Value}\t{dataGridView.Rows[i].Cells[3].Value}");
        }
    }
}