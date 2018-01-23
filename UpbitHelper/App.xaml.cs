using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using QuickHand.Utils;

namespace QuickHand
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            Exception ex = e.Exception;
            while(ex.InnerException != null)
            {
                ex = e.Exception.InnerException;
            }

            string msg = ex.Message;
            string stack = ex.StackTrace;
            MessageBox.Show("알수없는 에러가 발생하였습니다.\n관리자에게 연락주시면 빠른 시일내로 해결해드리도록 하겠습니다.");
            e.Handled = true;
            App.Current.Shutdown();
        }
    }
}
