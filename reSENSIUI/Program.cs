using System;
using System.Net;
using System.Windows;

namespace reSENSIUI
{
    internal static class Program
    {
        public static string PRODUCT_USERAGENT = "reSENSI " + 6 + "." + 7 + "." + 0;

        public static IWebProxy DefaultWebProxy = WebRequest.DefaultWebProxy;

        [STAThread]
        private static void Main()
        {
            ((Application)(object)new App()).Run();
        }
    }
}
