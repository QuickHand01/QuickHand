using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuickHand.Utils;
using QuickHand.View;

namespace UpbitHelper
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr itp, int id, KeyInform fsInform, Keys vk);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr itp, int id);
        const int KEYid = 31197;
        const int HOTKEYGap = 0x0312;

        Storyboard animation;
        List<CommandObject> commandList;

        private IntPtr windowHandle;

        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, messageHandler);
            this.Closing += MainWindow_Closing;
            this.Topmost = Core.getInstance().SaveData.isTop;
            //this.Title += Core.ver;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Core.getInstance().Save();
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(Messages.UpdateDemoCount));
            ChromeUtil.getInstance().StopChrome();
        }

        private void messageHandler(NotificationMessage msg)
        {
            if(msg.Notification == Messages.ActiveMessage)
            {
                this.commandList = Core.getInstance().SaveData.commandList;
                foreach (var item in this.commandList)
                {
                    RegisterHotKey(this.windowHandle, KEYid, item.Modifier, item.Key); //단축키 추가
                }   
            }
            else if(msg.Notification == Messages.DisableMessage)
            {
                for (int i = 0; i < 10; i++)
                {
                    UnregisterHotKey(this.windowHandle, KEYid);
                }
            }
            else if(msg.Notification == Messages.TopMostChanged)
            {
                this.Topmost = Core.getInstance().SaveData.isTop;
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            windowHandle = new WindowInteropHelper(this).Handle;

            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);

            this.animation = (Storyboard)this.FindResource("FadeIn");
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case HOTKEYGap:
                    Keys key = (Keys)(((int)lParam >> 16) & 0xFFFF); //눌러 진 단축키의 키
                    KeyInform modifier = (KeyInform)((int)lParam & 0xFFFF);//눌려진 단축키의 수식어
                    for (int i = 0; i < this.commandList.Count; i++)
                    {
                        CommandObject cmd = this.commandList[i];
                        if(modifier == cmd.Modifier && key == cmd.Key)
                        {
                            animation.Stop();
                            animation.Begin((FrameworkElement)icCommand.ItemContainerGenerator.ContainerFromIndex(i));
                            Messenger.Default.Send<KeyMessage>(new KeyMessage() { CmdIndex = i});
                        }
                    }
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
