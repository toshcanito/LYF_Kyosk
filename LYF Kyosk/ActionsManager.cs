using LYF_Kyosk.Models;

namespace LYF_Kyosk
{
    public class ActionsManager
    {
        public delegate Process MakePaymentAction(Payment payment);
        public delegate Account FindAccountAction(string accountNumber);

        public static void MakePayment(Payment payment, MakePaymentAction paymentAction) 
        {
            Process processInfo = paymentAction(payment);
            Logger.Log(processInfo);
        }
    }
}
