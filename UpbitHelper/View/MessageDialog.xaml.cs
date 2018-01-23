using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QuickHand.Utils;

namespace QuickHand.View
{
    /// <summary>
    /// Demo.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MessageDialog : Window
    {
        public MessageDialog(string msg, bool cancelable)
        {
            InitializeComponent();
            this.lbMsg.Content = msg;
            this.Topmost = Core.getInstance().SaveData.isTop;
            if (cancelable)
            {
                this.btnCancel.Visibility = Visibility.Visible;
            }
            else
            {
                this.btnCancel.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
