using Customer.Controller.Customer;
using Customer.until;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Customer.Controller
{
    class CustomerUser
    {
        /**
         * 获取访客信息列表
         * 
         */
        private static JObject GetCustomerList()
        {
            //return JObject.Parse(HttpUntil.GetClient("customer/list", ConfigUntil.GetSettingString("token")));
            return JObject.Parse(HttpUntil.GetClient("customer/list", "140c69e80f384b588be"));
        }

        public List<CustomerInfo> LoadCustomerList()
        {
            var jObjects = GetCustomerList().SelectToken("data").ToList();
            List<CustomerInfo> customerInfos = new List<CustomerInfo>();
            foreach (var jObject in jObjects)
            {
                customerInfos.Add(new CustomerInfo()
                {
                    CustImage = "https://video.yestar.com/chat_desktop_customer_avatr_img.png",
                    CustMsg = "null",
                    CustName = jObject["cust_name"].ToString(),
                    CustTime = jObject["create_time"].ToString()
                });
            }
            return customerInfos;
        }
    }
}
