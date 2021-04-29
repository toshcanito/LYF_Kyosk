using System;
using System.Windows.Forms;
using LYF_Kyosk.Models;

namespace LYF_Kyosk.Forms
{
    public partial class AccountForm : Form
    {        
        public AccountForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            new WelcomeForm().Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNumAccount.Text))
            {
                return;
            }
            tbNumAccount.Text = tbNumAccount.Text.Remove(tbNumAccount.Text.Length - 1);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            tbNumAccount.Text += 1;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            tbNumAccount.Text += 2;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            tbNumAccount.Text += 3;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            tbNumAccount.Text += 4;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            tbNumAccount.Text += 5;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            tbNumAccount.Text += 6;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            tbNumAccount.Text += 7;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            tbNumAccount.Text += 8;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            tbNumAccount.Text += 9;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            tbNumAccount.Text += 0;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNumAccount.Text))
            {
                MessageBox.Show("Número de cuenta no es valido");
                return;
            }
            try 
            {
                LoadAccountInfo(tbNumAccount.Text);
                new BalanceForm().Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error obteniendo informacion de la cuenta. " + ex.Message);
                return;
            }
        }

        private void LoadAccountInfo(string accountNumber)
        {
            Account client = new LYFHttpClient().GetSingleAccount(accountNumber);

            client.AccountNumber = accountNumber;
            AccountManager.LoadSessionAccount(client);
        }
    }
}
