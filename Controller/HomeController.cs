using Customer.Model;
using Customer.Until;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Customer.Controller
{
    class HomeController
    {
        private static ObservableCollection<CustItem> custItmes = new ObservableCollection<CustItem>();

        public static ObservableCollection<CustItem> CustItems
        {
            get
            {
                custItmes.Clear();
                JObject jObject = JObject.Parse(HttpUntil.GetClient("customer/list", ConfigUntil.GetSettingString("token")));
                JEnumerable<JToken> jTokens =  jObject["body"].Children();
                foreach (JToken cust in jTokens)
                {
                    Trace.WriteLine(cust);
                    JObject jOb1 = (JObject)cust;
                    custItmes.Add(new CustItem(jOb1["cust_name"].ToString(), jOb1["avar"].ToString(), "暂时还未开始聊天", jOb1["create_time"].ToString()));
                }
                return custItmes;
            }
        }
    }
}
