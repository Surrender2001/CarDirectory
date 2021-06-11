using CarDirectory.Forms;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static CarDirectory.HelpMethod;

namespace CarDirectory
{
    public partial class HashForm : Form
    {
        public HashForm()
        {
            InitializeComponent();
        }

        public HashForm(ref HashTable hashTable, ref RBTree<string, Car> rBTreeCar, ref RBTree<int, Car> rBTreeYear, ref DataGridView dataGridView) : this()
        {
            this.hashTableMain = hashTable;
            this.rBTreeCar = rBTreeCar;
            this.rBTreeYear = rBTreeYear;
            this.dataGridViewMain = dataGridView;
        }

        private HashTable hashTableMain;
        private HashTable hashTable = new HashTable();
        private DataGridView dataGridViewMain = new DataGridView();
        private RBTree<string, Car> rBTreeCar;
        private RBTree<int, Car> rBTreeYear;
        private RBTree<string, string> rBTreeModel = new RBTree<string, string>();

        private void ReadDbButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ofd = new OpenFileDialog() { Filter = "txt files (*.txt)|*.txt" })
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        dataGridView.Rows.Clear();
                        hashTable.Clear();
                        rBTreeModel.Clear();
                        int i = 0;
                        using (var sw = new StreamReader(ofd.FileName, Encoding.Default))
                            while (!sw.EndOfStream)
                            {
                                ++i;
                                string s = sw.ReadLine();
                                string[] subs = s.Split(new char[] { ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                if (!Regex.IsMatch(subs[0], @"^[a-zA-ZА-Яа-я- ]+$") || !Regex.IsMatch(subs[1], @"^[A-Za-zА-Яа-я0-9-&()/+ ]+$"))
                                    throw new Exception($"Ошибка чтения файла brand: {subs[0]} model: {subs[1]}");
                                hashTable.Add(new BrandAndModel(subs[0], subs[1]));
                                rBTreeModel.Add(subs[0], "");
                            }
                        RefreshDataGridView(ref dataGridView, ref hashTable);
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
            var addForm = new AddBrandModelForm(ref hashTable, ref rBTreeModel, ref dataGridView);
            _ = addForm.ShowDialog();
            addForm.Dispose();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var deleteForm = new DeleteBrandAndModelForm(ref rBTreeCar, ref rBTreeYear, ref rBTreeModel, ref hashTable, ref hashTableMain, ref dataGridView, ref dataGridViewMain);
            _ = deleteForm.ShowDialog();
            deleteForm.Dispose();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog() { Filter = "txt files (*.txt)|*.txt" })
                if (sfd.ShowDialog() == DialogResult.OK)
                    using (var sw = new StreamWriter(sfd.FileName))
                        for (int i = 0; i < dataGridView.Rows.Count; ++i)
                            sw.WriteLine($"{dataGridView.Rows[i].Cells[0].Value}\t{dataGridView.Rows[i].Cells[1].Value}");
        }
    }
}