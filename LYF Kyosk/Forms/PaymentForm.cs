using System;
using System.Windows.Forms;
using LYF_Kyosk.Models;
using DeviceLibrary.Models;
using DeviceLibrary.Models.Enums;

namespace LYF_Kyosk.Forms
{
    public partial class PaymentForm : Form
    {
        private Action ChangePayment;
        private Account account;
        private decimal _Payment = 0;
        private decimal _Change = 0;
        DeviceLibrary.DeviceLibrary device;

        public PaymentForm()
        {
            InitializeComponent();
            account = AccountManager.GetSessionAccount();
            device = new DeviceLibrary.DeviceLibrary();
            ChangePayment += RenderAmount;
        }

        private void ChangePaymentAmount(decimal amount, DocumentType? type, bool resetPaymentAmount = false)
        {
            if (resetPaymentAmount)
            {
                _Payment = 0.0m;
            }
            else
            {
                Document document = new Document(amount, type.Value, 1);
                _Payment += amount;
            }
            ChangePayment();
        }

        private decimal GetRemainingAmount() 
        {
            decimal remaining = account.Debt - _Payment;
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
            string clossingMessage = "Pago realizado con exito. ";
            _Change = _Payment - account.Debt;
            if (_Change > 0)
            {
                clossingMessage += "Cambio entregado " + _Change;
                device.Dispense(_Change);
            }
            if (MessageBox.Show(clossingMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                ChangePaymentAmount(0.0m, null, true);
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
            finally 
            {
                device.Close();
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

        #region Form Events
        private void PaymentForm_Load(object sender, EventArgs e)
        {
            device.Enable();
            AssignPaymentValues(account.Debt.ToString(), null, account.Debt.ToString());
        }

        private void btnAmount500_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(500, DocumentType.Bill);
        }

        private void btnAmount200_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(200, DocumentType.Bill);
        }

        private void btnAmount100_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(100, DocumentType.Bill);
        }

        private void btnAmount50_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(50, DocumentType.Bill);
        }

        private void btnAmount20_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(20, DocumentType.Bill);
        }

        private void btnAmount10_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(10, DocumentType.Coin);
        }

        private void btnAmount5_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(5, DocumentType.Coin);
        }

        private void btnAmount2_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(2, DocumentType.Coin);
        }

        private void btnAmount1_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(1, DocumentType.Coin);
        }

        private void btnAmount50c_Click(object sender, EventArgs e)
        {
            ChangePaymentAmount(0.5m, DocumentType.Coin);
        }
        #endregion
    }
}
