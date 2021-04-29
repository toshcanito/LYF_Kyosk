using System;
using System.Windows.Forms;

namespace LYF_Kyosk.Forms
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Click(object sender, EventArgs e)
        {
            new AccountForm().Show();
            this.Hide();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            new DeviceLibrary.DeviceLibrary().Open();
        }
    }
}
