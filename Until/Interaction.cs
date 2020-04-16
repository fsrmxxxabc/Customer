using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace Customer.Until
{
    public static class Interaction
    {
        public static void StartScreenShot()
        {
            SafeNativeMethods.StartScreenShot(1,new char[10]);
        }
    }

    [SuppressUnmanagedCodeSecurityAttribute]
    internal static class SafeNativeMethods
    {
        [DllImport("D:\\Desktop\\QTwindows\\qt-solutions-master\\qtwinmigrate\\examples\\build-qtdll-Desktop_x86_windows_msvc2019_pe_64bit-Debug\\debug\\ScreenShot.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        internal static extern void StartScreenShot(int argc,char[] argv);
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
