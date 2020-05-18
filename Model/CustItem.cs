using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Customer.Model
{
    class CustItem
    {
        //"https://video.yestar.com/chat_desktop_customer_avatr_img.png"
        public string Avar { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string Time { get; set; }

        public CustItem(string name, string avar, string content, string time)
        {
            this.Avar = avar; this.Content = content; this.Name = name; this.Time = time;
        }
    }
}
