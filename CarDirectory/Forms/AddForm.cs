using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarDirectory
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
           
        }

        private void BrandTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
            toolTip1.ToolTipTitle = "Неверный символ";
            toolTip1.Show("Введите латинские буквы", BrandTextBox, 1000);
        }



        private void BrandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            BrandTextBox.BackColor = Color.Beige;
            if (e.KeyCode ==Keys.Enter)
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
                ActiveControl =StartTextBox;
            }
        }

        private void StartTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            StartTextBox.BackColor = Color.Beige;
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ActiveControl = EndTextBox;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (IsCorrectCar())
            {
                DialogResult = DialogResult.OK;
                Hide();
            }
            else return;
        }

        private bool IsCorrectCar()
        {
            return CheckValues();
        }
        Car car;
        public Car AddNewCar()
        {
            if (IsCorrectCar())
                car = new Car(BrandTextBox.Text, ModelTextBox.Text, int.Parse(StartTextBox.Text), int.Parse(EndTextBox.Text));
            return car;
        }

        private bool CheckValues()
        {
            return checkIsEmpty()&&checkIsCorrectYear();
        }

        private bool checkIsCorrectYear()
        {
            bool start=true, end=true;
            if (!(EndTextBox.Text.Length == 0 || EndTextBox.Text.Length == 4))
            {
                ActiveControl = EndTextBox;
                EndTextBox.BackColor = Color.LightCoral;
                end = false;
            }
            else if (EndTextBox.Text.Length == 4)
                end = isRealDate(int.Parse(EndTextBox.Text));

            if(StartTextBox.Text.Length != 4)
            { 
                ActiveControl = StartTextBox;
                StartTextBox.BackColor = Color.LightCoral;
                start = false;
            }
            else if (StartTextBox.Text.Length == 4)
                start = isRealDate(int.Parse(StartTextBox.Text));

            if(start&&end)
                if(int.Parse(StartTextBox.Text)>int.Parse(EndTextBox.Text))
                {
                    MessageBox.Show("Год начала выпуска больше года конца выпуска", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ActiveControl = EndTextBox;
                    EndTextBox.BackColor = Color.LightCoral;
                    return false;   
                }    



            if (!(start&&end))
            {
                MessageBox.Show("Некорректно введен год", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if(!start)
                {
                    ActiveControl = StartTextBox;
                    StartTextBox.BackColor = Color.LightCoral;
                }
                if (!end)
                {
                    ActiveControl = EndTextBox;
                    EndTextBox.BackColor = Color.LightCoral;
                }
                return false;
            }


            return start &&end;
        }

        private bool isRealDate(int v)
        {
            return v <= 2021 && v >= 1968;
        }

        private bool checkIsEmpty()
        {   
            bool check = true;
            if (BrandTextBox.Text.Length == 0 || ModelTextBox.Text.Length == 0 || StartTextBox.Text.Length == 0)
                check = false;
            BrandTextBox.BackColor = BrandTextBox.Text.Length == 0 ? Color.LightCoral : Color.Beige;
            ModelTextBox.BackColor = ModelTextBox.Text.Length == 0 ? Color.LightCoral : Color.Beige;
            StartTextBox.BackColor = StartTextBox.Text.Length == 0 ? Color.LightCoral : Color.Beige;

            if (!check) MessageBox.Show("Заполните поля, выделенные красным цветом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ActiveControl = BrandTextBox;
            return check;
        }

        private void EndTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            EndTextBox.BackColor = Color.Beige;
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (IsCorrectCar())
                {
                    DialogResult = DialogResult.OK;
                    Hide();
                }
                else return;
            }
        }

        private void InfoPictureBox1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.ToolTipTitle = "Ограничение на год";
            toolTip1.Show(">1968 и <2022",InfoPictureBox1, 5000);

        }
    }
}
