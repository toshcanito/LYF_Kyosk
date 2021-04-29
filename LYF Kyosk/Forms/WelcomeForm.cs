using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LYF_Kyosk.Models;

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
    }
}
