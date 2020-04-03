using Customer.until;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Customer.Controller
{
    class LoginController
    {

        public JObject Submit(String username, String passwd, String code)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("name=" + username + "&passwd=" + passwd + "&code=" + code);
            return SaveToken(JObject.Parse(HttpUntil.PostClient("login", stringBuilder)));
        }


        private static JObject SaveToken(JObject jObject)
        {
            if (jObject["code"].ToString() != "200")
            {
                return null;
            }
            JObject data = (JObject)jObject["data"];
            ConfigUntil.UpdateSettingString("token", data["token"].ToString());
            return jObject;
        }
    }
}
