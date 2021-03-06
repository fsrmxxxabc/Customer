﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace Customer.Until
{
    class CommonUtil
    {
        public static void Clicked(Button button)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(button);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        public static Brush GetColorBrush(byte r, byte g, byte b)
        {
            return new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        public static Brush GetColorBrush(byte a, byte r, byte g, byte b)
        {
            return new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }

        public static String GetTimeSecond()
        {
            TimeSpan timeSpan =  DateTime.UtcNow - new DateTime(1970,1,1,0,0,0,0);
            return FormattableString.Invariant($"{Convert.ToInt64(timeSpan.TotalMilliseconds)}");
        }

        public static void ShakeScreen(Window window)
        {
            int i, j, k; //定义三个变量
            for (i = 1; i <= 5; i++) //循环次数
            {
                for (j = 1; j <= 5; j++)
                {

                    window.Top += 1;
                    window.Left += 1;
                    System.Threading.Thread.Sleep(3); //当前线程指定挂起的时间
                }
                for (k = 1; k <= 5; k++)
                {
                    window.Top -= 1;
                    window.Left -= 1;
                    System.Threading.Thread.Sleep(3);
                }
            }
        }

        public static string GetFileExtra(string filename)
        {
            return filename.Split(".")[1];

        }

        public static int GetRichTextBoxWidth(string content,string text)
        {
            int maxWidth = 450 ,count = 0 ,counti = 0, width;
            string str = "chat_desktop_face",msg = "_msg";

            if (content.Contains(str,StringComparison.OrdinalIgnoreCase))
            {
                string replace = content.Replace(str, "",StringComparison.OrdinalIgnoreCase);
                count = ((content.Length - replace.Length)/str.Length)/ 2;
            }

            if (content.Contains(msg, StringComparison.OrdinalIgnoreCase))
            {
                string rep = content.Replace(msg, "", StringComparison.OrdinalIgnoreCase);
                counti = (content.Length - rep.Length) / msg.Length;
            }

            width = text.Length * 15 + count * 22 + counti * 200 + 15;

            Trace.WriteLine(width);

            if (width>maxWidth)
            {
                return maxWidth;
            }

            return width;
        }

        public static void SetGifImage(string path)
        {
            CommonUtil.SetGIfImg = path;
        }

        public static void SetImage(string path)
        {
            CommonUtil.SetImg = path;
        }

        private static string SetImg
        {
            set
            {
                if(value != null)
                {
                    Image img = new Image()
                    {
                        Source = new BitmapImage(new Uri(value)),
                        Width = 300,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    AddToRichTextBox = img;
                }
            }
        }

        private static Image AddToRichTextBox
        {
            set
            {
                if(value != null)
                {
                    /*App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, (ThreadStart)delegate ()
                    {
                        //View.Index.ChatingContMsg.Document.Blocks.Add(value);
                        _ = new InlineUIContainer(value, View.Index.ChatingContMsg.CaretPosition);
                        View.Index.ChatingContMsg.Focus();
                        //View.Index.ChatingContMsg.CaretPosition = pointer.DocumentEnd;
                        //View.Index.ChatingContMsg.Focus();
                    });*/
                    View.Index.ChatingContMsg.Dispatcher.Invoke(new Action(() =>
                    {
                        _ = new InlineUIContainer(value, View.Index.ChatingContMsg.CaretPosition);
                        View.Index.ChatingContMsg.Focus();
                    }));
                }
            }
        }

        private static string SetGIfImg
        {
            set
            {
                if (value != null )
                {
                    Image img = new Image
                    {
                        Width = 20,
                        Source = SetBitImageInfo(value)
                    };
                    //ImageBehavior.SetAnimatedSource(img, img.Source);
                    img.Dispatcher.Invoke(new Action(() =>
                    {
                        //ImageBehavior.SetAnimatedSource(img, img.Source);
                        AddToRichTextBox = img;
                    }));
                    //ImageBehavior.SetAnimatedSource(img, img.Source);
                    
                }
            }
        }

        private static BitmapImage SetBitImageInfo(string path)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path);
            image.EndInit();
            return image;
        }


    }
}
