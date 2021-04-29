using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using LYF_Kyosk.Models;
using Newtonsoft.Json;

namespace LYF_Kyosk
{
    public class LYFHttpClient
    {
        private const string _ServiceToken = "ac01a4bea06a01461f830486a20cd2f9";
        private const string _BaseAddress = "https://api.xenterglobal.com:2053/";

        private T Get<T>(string uri)
        {
            using (HttpClient client = new HttpClient())
            {
                var task = client.GetAsync(uri);
                task.Wait();

                var taskReq = task.Result;
                if (!taskReq.IsSuccessStatusCode)
                {
                    throw new ArgumentException("Error obteniendo la informacion desde servicio");
                }

                var resultInfo = taskReq.Content.ReadAsStringAsync();
                resultInfo.Wait();

                T formatedResult = JsonConvert.DeserializeObject<T>(resultInfo.Result);
                return formatedResult;
            }
        }

        private T Post<T>(string uri, object body)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent data = new StringContent(JsonConvert.SerializeObject(body),
                    Encoding.UTF8, "application/json");

                var task = client.PostAsync(uri, data);
                task.Wait();

                var taskReq = task.Result;
                if (!taskReq.IsSuccessStatusCode)
                {
                    throw new ArgumentException("Error obteniendo la respuesta desde el servicio POST");
                }

                var resultInfo = taskReq.Content.ReadAsStringAsync();
                resultInfo.Wait();

                T formatedResult = JsonConvert.DeserializeObject<T>(resultInfo.Result);
                return formatedResult;
            }
        }

        public Account GetSingleAccount(string account)
        {
            string uri =
                _BaseAddress
                + "account_balance?token="
                + _ServiceToken
                + "&account="
                + account;

            return Get<Account>(uri);
        }

        public List<Account> GetAllAccounts()
        {
            string uri =
                _BaseAddress
                + "accounts?token="
                + _ServiceToken;

            return Get<List<Account>>(uri);
        }

        public Account PostPayment(Payment payment)
        {
            string uri = 
                _BaseAddress 
                + "transaction?token=" 
                + _ServiceToken;

            Account account = Post<Account>(uri, payment);
            return account;
        }
    }
}
