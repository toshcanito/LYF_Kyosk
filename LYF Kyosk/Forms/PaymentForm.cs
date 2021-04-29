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
    public partial class PaymentForm : Form
    {
        private Action ChangePayment;
        private Account account;
        private double _Payment = 0;

        public PaymentForm()
        {
            InitializeComponent();
            account = AccountManager.GetSessionAccount();
            ChangePayment += RenderAmount;
        }

        private void ChangePaymentAmount(double amount, bool resetPaymentAmount = false)
        {
            if (resetPaymentAmount)
            {
                _Payment = 0.0;
            }
            else 
            {
                _Payment += amount;
            }            
            ChangePayment();
        }

        private double GetRemainingAmount() 
        {
            double remaining = account.Debt - _Payment;
            return remaining < 0 ? 0 : remaining;
        }

        private void AssignPaymentValues(string debt = null, string deposit = null, string remaining = null)
        {
            if (!string.IsNullOrWhiteSpace(debt)) 
            {
                lbDebt.Text = debt;
            }
            if (!string.IsNullOrWhiteSpace(deposit))
            {
                lbDeposit.Text = deposit;
            }
            if (!string.IsNullOrWhiteSpace(remaining))
            {
                lbRemaining.Text = remaining;
            }
        }

        private void Rollback() 
        {
            AssignPaymentValues(null, "0.0", account.Debt.ToString());
        }

        private void RenderAmount() 
        {
            AssignPaymentValues(null, _Payment.ToString(), GetRemainingAmount().ToString());
        }

        private void AfterPaymentProcess() 
        {
            if (MessageBox.Show("Pago realizado con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                ChangePaymentAmount(0.0, true);
                new WelcomeForm().Show();
                this.Hide();
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                Payment payment = new Payment()
                {
                    Account = account.AccountNumber,
                    Paid = _Payment
                };

                ActionsManager.MakePayment(payment, (p) =>
                {
                    Account account = new LYFHttpClient().PostPayment(payment);                    
                    return new Process() { AccountInfo = account, PaymentInfo = payment, TransactionDate = DateTime.Now.ToShortDateString() };
                });
                AfterPaymentProcess();
            }
            catch (Exception ex) 
            {
                Rollback();
                MessageBox.Show("Error al realizar el pago. Descripcion del error: " + ex.Message);
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_Payment != 0) 
            {
                MessageBox.Show
                    ("Accion cancelada, deposito en progreso. Cantida actual $" 
                    + _Payment.ToString());
                return;
            }
            new WelcomeForm().Show();
            this.Hide();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            AssignPaymentValues(account.Debt.ToString(), null, account.Debt.ToString());
        }

        private void btnAmount500_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(500);
        }

        private void btnAmount200_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(200);
        }

        private void btnAmount100_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(100);
        }

        private void btnAmount50_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(50);
        }

        private void btnAmount20_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(20);
        }

        private void btnAmount10_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(10);
        }

        private void btnAmount5_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(5);
        }

        private void btnAmount2_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(2);
        }

        private void btnAmount1_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(1);
        }

        private void btnAmount50c_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(0.5);
        }
    }
}
