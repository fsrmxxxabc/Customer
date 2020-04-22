using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Qiniu.Storage;
using Customer.Until.Qiniu;
using System.Windows.Media;
using System.IO;
using System.Windows.Interop;
using System;
using Newtonsoft.Json.Linq;

namespace Customer.Until
{
    public static class Interaction
    {
        private static Bitmap screenSnapshot { get; set; }


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int callBackHandle(int a, [MarshalAs(UnmanagedType.LPStr)]StringBuilder b);

        private static callBackHandle callBack { get; set; }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void callScreentHandle(string msg);

        private static callScreentHandle CallSreent { get; set; }

        private static int LocalFun(int a, [MarshalAs(UnmanagedType.LPStr)]StringBuilder b)

        {

            MessageBox.Show(b.ToString());

            return 0;

        }

        private static Bitmap CopyFromScreenSnapshot(Rect region)
        {
            Rectangle sourceRect = region.ToRectangle();

            Trace.WriteLine(sourceRect);

            Rectangle destRect = new Rectangle(0, 0, sourceRect.Width, sourceRect.Height);

            var bitmap = new Bitmap(sourceRect.Width, sourceRect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(screenSnapshot, destRect, sourceRect, GraphicsUnit.Pixel);
            }

            return bitmap;
        }

        private static Rectangle ToRectangle(this Rect rect)
        {
            return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        private static void CallBackScreent(string msg)
        {
            QiniuUtil qiniuUtil = new QiniuUtil()
            {
                ConfigUtil = new Config(),
                TokenUtil = null
            };

            string ret = qiniuUtil.ChunkUpload("chat_" + "screent_shot_"+CommonUtil.GetTimeSecond() + "_msg." + CommonUtil.GetFileExtra(msg), msg);

            JObject jObject = JObject.Parse(ret);

            Trace.WriteLine(jObject);

            App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, (ThreadStart)delegate ()
            {
                View.Index.ChatingContMsg.Focus();
                CommonUtil.SetImage(QiniuUtil.Domain + jObject["key"]);
            });
        }

        public static void StartScreenShot()
        {
            CallSreent = new callScreentHandle(CallBackScreent);
            SafeNativeMethods.finishedCapture(CallSreent);
            SafeNativeMethods.StartScreenShot(1, new char[10]);
        }

        public static void StartScreenShots()
        {
            callBack = new callBackHandle(LocalFun);
            SafeNativeMethods.SetCB(callBack);
            SafeNativeMethods.CallTheCB();
            //Process.Start("E:\\wechat\\WeChat.exe");
        }

    }

    //[SuppressUnmanagedCodeSecurityAttribute]
    internal static class SafeNativeMethods
    {
        [DllImport("D:\\Desktop\\QTwindows\\qt-solutions-master\\qtwinmigrate\\examples\\build-qtdll-Desktop_x86_windows_msvc2019_pe_64bit-Debug\\debug\\ScreenShot.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void StartScreenShot(int argc,char[] argv);

        [DllImport("D:\\Desktop\\QTwindows\\qt-solutions-master\\qtwinmigrate\\examples\\build-qtdll-Desktop_x86_windows_msvc2019_pe_64bit-Debug\\debug\\ScreenShot.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void finishedCapture(Interaction.callScreentHandle callBack);

        [DllImport("D:\\Desktop\\QTwindows\\qt-solutions-master\\qtwinmigrate\\examples\\build-qtdll-Desktop_x86_windows_msvc2019_pe_64bit-Debug\\debug\\ScreenShot.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetCB(Interaction.callBackHandle callBack);

        [DllImport("D:\\Desktop\\QTwindows\\qt-solutions-master\\qtwinmigrate\\examples\\build-qtdll-Desktop_x86_windows_msvc2019_pe_64bit-Debug\\debug\\ScreenShot.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CallTheCB();
    }

    /*[SuppressUnmanagedCodeSecurityAttribute]
    internal static class UnsafeNativeMethods
    {
        [DllImport("D:\\Desktop\\QTwindows\\qt-solutions-master\\qtwinmigrate\\examples\\build-qtdll-Desktop_x86_windows_msvc2019_pe_64bit-Debug\\debug\\qtdialog.dll", CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void startCapture(int argc, char[] argv);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern int ShowCursor([MarshalAs(UnmanagedType.Bool)]bool bShow);
    }*/
}
