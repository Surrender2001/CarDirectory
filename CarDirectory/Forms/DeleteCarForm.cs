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
    public partial class DeleteCarForm : Form
    {
        public DeleteCarForm()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
        }
        public Car GetCar()
        {
            return new Car() { Brand=BrandTextBox.Text, Model =ModelTextBox.Text, Start=int.Parse(StartTextBox.Text), End=EndTextBox.Text };
        }

    }
}
