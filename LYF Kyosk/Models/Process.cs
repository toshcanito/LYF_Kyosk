using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYF_Kyosk.Models
{
    public class Process
    {
        public Payment PaymentInfo { get; set; }
        public Account AccountInfo { get; set; }
        public string TransactionDate { get; set; }
    }
}
