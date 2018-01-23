using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickHand.Utils
{
    public class FunctionObject : ObservableObject
    {
        public enum CommandType
        {
            [Description("매수")]
            Buy = 0,
            [Description("매도")]
            Sell = 1,
            [Description("취소")]
            Cancel = 2,
            [Description("닫기")]
            Close = 3
        }

        public enum AmountType
        {
            [Description("100%")]
            P100 = 0,
            [Description("50%")]
            P50 = 1,
            [Description("25%")]
            P25 = 2,
            [Description("10%")]
            P10 = 3,
            [Description("\\")]
            KRW = 4,
            [Description("BTC")]
            BTC = 5
        }

        #region Name 
        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        #endregion

        #region Type 
        private CommandType type;
        public CommandType Type
        {
            get { return this.type; }
            set
            {
                if (this.type != value)
                {
                    this.type = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        #endregion

        #region AskType 
        private CommandType askType;
        public CommandType AskType
        {
            get { return this.askType; }
            set
            {
                if (this.askType != value)
                {
                    this.askType = value;
                    this.RaisePropertyChanged("AskType");
                }
            }
        }
        #endregion

        #region Offset 
        private int offset;
        public int Offset
        {
            get { return this.offset; }
            set
            {
                if (this.offset != value)
                {
                    this.offset = value;
                    this.RaisePropertyChanged("Offset");
                }
            }
        }
        #endregion

        #region Detail 
        private int detail;
        public int Detail
        {
            get { return this.detail; }
            set
            {
                if (this.detail != value)
                {
                    this.detail = value;
                    this.RaisePropertyChanged("Detail");
                }
            }
        }
        #endregion

        #region AType 
        private AmountType aType;
        public AmountType AType
        {
            get { return this.aType; }
            set
            {
                if (this.aType != value)
                {
                    this.aType = value;
                    this.RaisePropertyChanged("AType");
                }
            }
        }
        #endregion

        #region Amount 
        private double amount;
        public double Amount
        {
            get { return this.amount; }
            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        #endregion

        public static string getAmountTypeText(AmountType type)
        {
            if (type == AmountType.P100)
            {
                return "100%최대";
            }
            else if (type == AmountType.P50)
            {
                return "50%";
            }
            else if (type == AmountType.P25)
            {
                return "25%";
            }
            else if (type == AmountType.P10)
            {
                return "10%";
            }
            else
            {
                return "";
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
