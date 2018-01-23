using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;
using QuickHand.Utils;
using QuickHand.View;
using System.Windows;

namespace QuickHand.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        ChromeUtil chromeUtil;
        ServiceManager service;
        Core core;

        #region Properties

        #region OptionList
        private ObservableCollection<string> optionList;
        public ObservableCollection<string> OptionList
        {
            get { return (this.optionList) ?? (this.optionList = new ObservableCollection<string>()); }
        }
        #endregion

        #region IsLogined 
        private bool isLogined;
        public bool IsLogined
        {
            get { return this.isLogined; }
            set
            {
                if (this.isLogined != value)
                {
                    this.isLogined = value;
                    this.RaisePropertyChanged("IsLogined");
                }
            }
        }
        #endregion

        #region IsActived 
        private bool isActived;
        public bool IsActived
        {
            get { return this.isActived; }
            set
            {
                if (this.isActived != value)
                {
                    this.isActived = value;
                    this.RaisePropertyChanged("IsActived");
                    this.RaisePropertyChanged("ActiveBtnText");
                    
                }
            }
        }
        #endregion

        #region ActiveBtnText 
        public string ActiveBtnText
        {
            get {
                if (IsActived)
                {
                    return "ON";
                }
                else
                {
                    return "OFF";
                }
            }
        }
        #endregion



        #region StatusText 
        private string statusText;
        public string StatusText
        {
            get { return this.statusText; }
            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;
                    this.RaisePropertyChanged("StatusText");
                }
            }
        }
        #endregion

        #region CommandList
        private ObservableCollection<CommandObject> commandList;
        public ObservableCollection<CommandObject> CommandList
        {
            get { return (this.commandList) ?? (this.commandList = new ObservableCollection<CommandObject>()); }
        }
        #endregion
        #endregion

        #region Commands
        #region RefreshCommand
        private RelayCommand refreshCommand;

        /// <summary>
        /// Gets the RefreshCommand.
        /// </summary>
        public RelayCommand RefreshCommand
        {
            get
            {
                return refreshCommand ?? (refreshCommand = new RelayCommand(
                    ExecuteRefreshCommand,
                    CanExecuteRefreshCommand));
            }
        }

        private void ExecuteRefreshCommand()
        {
            //this.SelectedIE = null;
            //this.IEList.Clear();
            //this.IsActived = false;
            //Messenger.Default.Send<NotificationMessage>(new NotificationMessage(Messages.DisableMessage));
            //List<IEObject> list = ieUtil.getIEs();
            //foreach (var item in list)
            //{
            //    IEList.Add(item);
            //}
            //if(IEList.Count > 0)
            //{
            //    this.SelectedIE = IEList[0];
            //}
            //this.updateStatus(IEList.Count + "개의 Internet Explorer가 감지되었습니다.");

            try
            {
                chromeUtil.StartChrome("https://upbit.com");
            }catch(Exception e)
            {
                updateStatus(e.Message);
            }
        }

        private bool CanExecuteRefreshCommand()
        {
            return true;
        }
        #endregion

        #region ActiveCommand

        private RelayCommand activeCommand;

        /// <summary>
        /// Gets the ActiveCommand.
        /// </summary>
        public RelayCommand ActiveCommand
        {
            get
            {
                return activeCommand ?? (activeCommand = new RelayCommand(
                    ExecuteActiveCommand,
                    CanExecuteActiveCommand));
            }
        }

        private void ExecuteActiveCommand()
        {
            if (IsActived)
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(Messages.ActiveMessage));
                //this.IsActived = true;
                this.updateStatus("단축키가 활성화 되었습니다.");
            }
            else
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(Messages.DisableMessage));
                //this.IsActived = false;
                this.updateStatus("단축키가 비활성화 되었습니다.");
            }
        }

        private bool CanExecuteActiveCommand()
        {
            return true;
            //return this.SelectedIE != null;
        }

        #endregion

        #region DonationCommand

        private RelayCommand donationCommand;

        /// <summary>
        /// Gets the DonationCommand.
        /// </summary>
        public RelayCommand DonationCommand
        {
            get
            {
                return donationCommand
                    ?? (donationCommand = new RelayCommand(ExecuteDonationCommand));
            }
        }

        private void ExecuteDonationCommand()
        {
            DonationDialog dialog = new DonationDialog();
            dialog.Owner = App.Current.MainWindow;
            dialog.ShowDialog();
        }

        #endregion

        #endregion

        public MainViewModel()
        {
            chromeUtil = ChromeUtil.getInstance();
            service = ServiceManager.GetInstance();
            core = Core.getInstance();

            this.updateSaveData();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();

            Messenger.Default.Send<NotificationMessage>(new NotificationMessage(Messages.DisableMessage));
            Messenger.Default.Register<KeyMessage>(this, KeyHandler);
            Messenger.Default.Register<NotificationMessage>(this, MessageHandler);

            updateStatus("Ready.");

            string version = service.getVersion();
            if(version != Core.ver)
            {
                MessageDialog dialog = new MessageDialog("새로운 버전이 업데이트 되었습니다", false);
                dialog.ShowDialog();
            }

            string notification = service.getNotification();
            if (!string.IsNullOrEmpty(notification))
            {
                MessageDialog dialog = new MessageDialog(notification, false);
                dialog.ShowDialog();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            chromeUtil.CheckChrome();
        }

        private void MessageHandler(NotificationMessage obj)
        {
            if (obj.Notification == Messages.LoginMessage)
            {
                this.IsLogined = true;
            }
            else if (obj.Notification == Messages.ServerErrorMessage)
            {
                updateStatus("서버와 통신에 실패하였습니다.");
            }
        }

        private void KeyHandler(KeyMessage obj)
        {
            CommandObject cmd = this.core.SaveData.commandList[obj.CmdIndex];
            FunctionObject func = this.core.findFunction(cmd.Command);
            if(func == null || string.IsNullOrEmpty(func.Name))
            {
                updateStatus("선택된 명령이 없습니다.");
            }
            else
            {
                try
                {
                    this.runCommand(func);
                    updateStatus(cmd.Command + " 실행.");
                }
                catch (Exception e)
                {
                    updateStatus(cmd.Command + " 실행 실패.");
                }
            }
        }


        private void runCommand(FunctionObject func)
        {
            if (func.Type == FunctionObject.CommandType.Buy)
            {
                if(func.Offset == 0)
                {
                    this.chromeUtil.BuyPercent(func.AType, func.Amount);
                }
                else
                {
                    this.chromeUtil.CurrentBuyPercent(func.AskType, func.Offset, func.Detail, func.AType, func.Amount);
                }
                
            }
            else if (func.Type == FunctionObject.CommandType.Sell)
            {
                if (func.Offset == 0)
                {
                    this.chromeUtil.SellPercent(func.AType, func.Amount);
                }
                else
                {
                    this.chromeUtil.CurrentSellPercent(func.AskType, func.Offset, func.Detail, func.AType, func.Amount);
                }
            }
            else if (func.Type == FunctionObject.CommandType.Cancel)
            {
                this.chromeUtil.CancelOrder();
            }
            else if (func.Type == FunctionObject.CommandType.Close)
            {

            }
        }

        private void updateStatus(string msg)
        {
            DateTime now = DateTime.Now;
            this.StatusText = now.ToString("HH:mm:ss") + " : " + msg;
        }

        private void updateSaveData()
        {
            this.CommandList.Clear();
            foreach (var item in this.core.SaveData.commandList)
            {
                this.CommandList.Add(item);
                item.IsOff = !this.IsActived;
            }
        }
        
    }
}