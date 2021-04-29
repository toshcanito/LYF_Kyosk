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
    public partial class BalanceForm : Form
    {
        Account _AccountInformation;
        public BalanceForm()
        {
            _AccountInformation = AccountManager.GetSessionAccount();
            InitializeComponent();
            RenderAccountInformation();
        }

        private void RenderAccountInformation() 
        {
            lblAccountNumber.Text = _AccountInformation.AccountNumber;
            lblDebt.Text = _AccountInformation.Debt.ToString();
            lblUserName.Text = _AccountInformation.User;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            new WelcomeForm().Show();
            this.Hide();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            new PaymentForm().Show();
            this.Hide();
        }
    }
}
