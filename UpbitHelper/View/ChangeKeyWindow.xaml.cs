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
    /// ChangeKeyWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChangeKeyWindow : Window
    {
        public KeyInform Modifier { get; set; }
        public System.Windows.Forms.Keys KeyValue { get; set; }

        public ChangeKeyWindow()
        {
            InitializeComponent();
            this.Topmost = Core.getInstance().SaveData.isTop;
            this.tbShortcut.Focus();
        }

        private void ShortcutTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // The text box grabs all input.
            e.Handled = true;

            // Fetch the actual shortcut key.
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            key = key.Equals(Key.ImeProcessed) ? e.ImeProcessedKey : key;
            // Ignore modifier keys.
            if (key == Key.LeftShift || key == Key.RightShift
                || key == Key.LeftCtrl || key == Key.RightCtrl
                || key == Key.LeftAlt || key == Key.RightAlt
                || key == Key.LWin || key == Key.RWin)
            {
                return;
            }

          

            this.Modifier = 0;
            // Build the shortcut key name.
            StringBuilder shortcutText = new StringBuilder();
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                shortcutText.Append("Ctrl+");
                this.Modifier = (int)this.Modifier + KeyInform.Control;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
            {
                shortcutText.Append("Shift+");
                this.Modifier  = (int)this.Modifier + KeyInform.Shift;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
            {
                shortcutText.Append("Alt+");
                this.Modifier = (int)this.Modifier + KeyInform.Alt;
            }
            shortcutText.Append(key.ToString());
            this.KeyValue = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), key.ToString());
            // Update the text box.
            tbShortcut.Text = shortcutText.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
