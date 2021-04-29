using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using LYF_Kyosk.Models;

namespace LYF_Kyosk
{
    public class Logger
    {
        public static void Log(Process obj)
        {
            using (IDbConnection con = new SQLiteConnection(LoadConnectionString("SQLite")))
            {
                var res = con.Execute("insert into OperationLog (Customer,Account,Debt,Paid,Date) values (@Customer, @Account, @Debt, @Paid, @Date)",
                    new
                    {
                        @Customer = obj.AccountInfo.User,                        
                        @Debt = obj.AccountInfo.Debt,
                        @Account = obj.PaymentInfo.Account,
                        @Paid = obj.PaymentInfo.Paid,
                        @Date = DateTime.Now.ToShortDateString()
                    });
            }
        } 

        private static string LoadConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}
