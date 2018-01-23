using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using QuickHand.View;

namespace QuickHand.Utils
{
    [Serializable]
    public class CommandObject : ObservableObject
    {
        #region Modifier 
        private KeyInform modifier;
        public KeyInform Modifier
        {
            get { return this.modifier; }
            set
            {
                if (this.modifier != value)
                {
                    this.modifier = value;
                    this.RaisePropertyChanged("Modifier");
                    this.RaisePropertyChanged("KeyString");
                }
            }
        }
        #endregion

        #region Key 
        private Keys key;
        public Keys Key
        {
            get { return this.key; }
            set
            {
                if (this.key != value)
                {
                    this.key = value;
                    this.RaisePropertyChanged("Key");
                    this.RaisePropertyChanged("KeyString");
                }
            }
        }
        #endregion

        #region KeyString 
        public string KeyString
        {
            get {
                return optionToString(this.Modifier, this.Key);

            }
        }
        #endregion

        #region FunctionList
        #region FunctionList
        private ObservableCollection<String> functionList;
        [XmlIgnore]
        public ObservableCollection<String> FunctionList
        {
            get { return (this.functionList) ?? (this.functionList = new ObservableCollection<String>()); }
        }
        #endregion
        #endregion

        #region Command 
        private string command;
        public string Command
        {
            get { return this.command; }
            set
            {
                if (this.command != value)
                {
                    this.command = value;
                    this.RaisePropertyChanged("Command");
                }
            }
        }
        #endregion

        #region IsOff 
        private bool isOff;
        public bool IsOff
        {
            get { return this.isOff; }
            set
            {
                if (this.isOff != value)
                {
                    this.isOff = value;
                    this.RaisePropertyChanged("IsOff");
                }
            }
        }
        #endregion

        private RelayCommand editCommand;

        /// <summary>
        /// Gets the EditCommand.
        /// </summary>
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand
                    ?? (editCommand = new RelayCommand(ExecuteEditCommand));
            }
        }

        private void ExecuteEditCommand()
        {
            if (IsOff)
            {
                ChangeKeyWindow dialog = new ChangeKeyWindow();
                dialog.Owner = App.Current.MainWindow;
                if (dialog.ShowDialog() == true)
                {
                    this.Modifier = dialog.Modifier;
                    this.Key = dialog.KeyValue;
                }
            }
        }

        public CommandObject()
        {
            Messenger.Default.Register<NotificationMessage>(this, messageHandler);
        }

        private void messageHandler(NotificationMessage msg)
        {
            if(msg.Notification == Messages.ActiveMessage)
            {
                this.IsOff = false;
            }
            else if(msg.Notification == Messages.DisableMessage)
            {
                this.IsOff = true;
            }
        }

        private String optionToString(KeyInform option, Keys key)
        {
            if (option == KeyInform.Alt)
            {
                return "Alt + " + key.ToString();
            }
            else if (option == KeyInform.Control)
            {
                return "Ctrl + " + key.ToString();
            }
            else if (option == KeyInform.Shift)
            {
                return "Shift + " + key.ToString();
            }
            else if(option == KeyInform.AltControl)
            {
                return "Alt + Ctrl + " + key.ToString();
            }
            else if (option == KeyInform.ShiftAlt)
            {
                return "Shift + Alt + " + key.ToString();
            }
            else if (option == KeyInform.ShiftControl)
            {
                return "Sifht + Ctrl + " + key.ToString();
            }
            else if (option == KeyInform.ShiftAltControl)
            {
                return "Shift + Alt + Ctrl + " + key.ToString();
            }
            else if (option == KeyInform.None)
            {
                return key.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
