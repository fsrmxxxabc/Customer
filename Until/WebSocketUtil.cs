using Customer.Until.Chat;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using WebSocket4Net;

namespace Customer.Until
{
	public class WebSocketUtil
	{
		private String Content { get; set; }

		public WebSocketUtil()
		{
			ConnectWebsocket = new WebSocket("ws://localhost:8080/socket/c511355a1013483e9a5");
		}

		public static WebSocket WebSockets { get; set; }

		/// <summary>
		/// 接收到服务端推送的消息时自动触发（异步触发）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Websocket_MessageReceivedAsync(object sender, MessageReceivedEventArgs e)
		{
			//接收服务端发来的消息
			App.Current.Dispatcher.Invoke((Action)(() =>
			{
				MessageReceivedEventArgs responseMsg = (MessageReceivedEventArgs)e;
				string strMsg = responseMsg.Message;
				GetCont = strMsg;
				IndexUtil indexUtil = new IndexUtil(new CustInfo(SetCustparam(GetCont,400)), new DockPanel());
				View.Index.Chatingmsg.Children.Add(indexUtil.DockPanel);
			}));
		}

		private CustParam SetCustparam(String content)
		{
			return new CustParam()
			{
				Url = "https://video.yestar.com/chat_desktop_visitor.png",
				Time = DateTime.Now.ToLongTimeString(),
				UrlIcon = "pack://application:,,,/Resources/chat_desktop_triangle_icon.png",
				Content = content,
			};
		}

		private CustParam SetCustparam(string content, int width)
		{
			return new CustParam()
			{
				UserImage = "https://video.yestar.com/chat_desktop_visitor.png",
				UserTime = DateTime.Now.ToLongTimeString(),
				MsgCont = content,
				RichTextBoxWidth = width
			};
		}

		public String GetCont
		{
			get { return Content; }
			set
			{
				JObject jObject = JObject.Parse(value);
				JToken token = JToken.Parse(jObject["data"].ToString());
				Content = token["content"].ToString();
				//Trace.WriteLine(token);
			}
		}

		private void Websocket_Closed(object sender, EventArgs e)
		{
			//DisplayStatusInfo("websocket connect failed!");
			Trace.WriteLine("websocket connect failed!");
		}

		private void Websocket_Opened(object sender, EventArgs e)
		{
			//DisplayStatusInfo("websocket connect success!");
			Trace.WriteLine("websocket connect success!");
		}

		public WebSocket ConnectWebsocket
		{
			get { return WebSockets; }
			set
			{
				if (value != null) 
				{
					WebSockets = value;
					WebSockets.Opened += Websocket_Opened;
					WebSockets.Closed += Websocket_Closed;
					WebSockets.MessageReceived += Websocket_MessageReceivedAsync;
					WebSockets.Open();
				}
			}
		}
	}
}
