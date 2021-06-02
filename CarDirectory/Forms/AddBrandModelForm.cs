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
    public partial class AddBrandModelForm : Form
    {
        public AddBrandModelForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
        }


        public BrandAndModel GetBrandAndModel()
        {

            return new BrandAndModel(BrandTextBox.Text, ModelTextBox.Text);
        }

    }
}
