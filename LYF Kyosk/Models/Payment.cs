using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYF_Kyosk.Models
{
    public class Payment
    {
        [JsonProperty("account")]
        public string Account { get; set; }
        [JsonProperty("paid")]
        public double Paid { get; set; }
    }
}
