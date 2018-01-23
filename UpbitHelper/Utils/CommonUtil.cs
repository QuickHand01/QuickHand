using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuickHand.Utils.FunctionObject;

namespace QuickHand.Utils
{
    public enum KeyInform
    {
        None = 0,
        Alt = 1,
        Control = 2,
        AltControl = 3,
        Shift = 4,
        ShiftAlt = 5,
        ShiftControl = 6,
        ShiftAltControl = 7
    }

    public class CommonUtil
    {
        private static List<string> defaultFunctionList;

        public static List<string> getDefaultFunctionList()
        {
            if(defaultFunctionList == null){
                defaultFunctionList = new List<string>();
                defaultFunctionList.Add("시장가로 100% 매수");
                defaultFunctionList.Add("시장가로 50% 매수");
                defaultFunctionList.Add("시장가로 25% 매수");
                defaultFunctionList.Add("시장가로 10% 매수");
                defaultFunctionList.Add("------------------");
                defaultFunctionList.Add("시장가로 100% 매도");
                defaultFunctionList.Add("시장가로 50% 매도");
                defaultFunctionList.Add("시장가로 25% 매도");
                defaultFunctionList.Add("시장가로 10% 매도");
                defaultFunctionList.Add("------------------");
                defaultFunctionList.Add("지정가로 100% 매수");
                defaultFunctionList.Add("지정가로 50% 매수");
                defaultFunctionList.Add("지정가로 25% 매수");
                defaultFunctionList.Add("지정가로 10% 매수");
                defaultFunctionList.Add("------------------");
                defaultFunctionList.Add("지정가로 100% 매도");
                defaultFunctionList.Add("지정가로 50% 매도");
                defaultFunctionList.Add("지정가로 25% 매도");
                defaultFunctionList.Add("지정가로 10% 매도");
                defaultFunctionList.Add("------------------");
                defaultFunctionList.Add("주문 취소");
            }

            return defaultFunctionList;
        }

        private static List<FunctionObject> defaultFunctionObjectList;

        public static List<FunctionObject> getDefaultFunctionObjectList()
        {
            if (defaultFunctionObjectList == null)
            {
                defaultFunctionObjectList = new List<FunctionObject>();
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "cb1", Type = CommandType.Buy, AskType = CommandType.Sell, Detail = 3, AType = AmountType.P100, Offset = 1 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "cb2", Type = CommandType.Buy, AskType = CommandType.Sell, Detail = 3, AType = AmountType.P50, Offset = 1 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "cb3", Type = CommandType.Buy, AskType = CommandType.Sell, Detail = 3, AType = AmountType.P25, Offset = 1 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "cb4", Type = CommandType.Buy, AskType = CommandType.Sell, Detail = 3, AType = AmountType.P10, Offset = 1 });
                defaultFunctionObjectList.Add(new FunctionObject());
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "cs1", Type = CommandType.Sell, AskType = CommandType.Buy, Detail = 3, AType = AmountType.P100, Offset = 1 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "cs2", Type = CommandType.Sell, AskType = CommandType.Buy, Detail = 3, AType = AmountType.P50, Offset = 1 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "cs3", Type = CommandType.Sell, AskType = CommandType.Buy, Detail = 3, AType = AmountType.P25, Offset = 1 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "cs4", Type = CommandType.Sell, AskType = CommandType.Buy, Detail = 3, AType = AmountType.P10, Offset = 1 });
                defaultFunctionObjectList.Add(new FunctionObject());
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "sb1", Type = CommandType.Buy, AskType = CommandType.Sell, Detail = 3, AType = AmountType.P100, Offset = 0 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "sb2", Type = CommandType.Buy, AskType = CommandType.Sell, Detail = 3, AType = AmountType.P50, Offset = 0 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "sb3", Type = CommandType.Buy, AskType = CommandType.Sell, Detail = 3, AType = AmountType.P25, Offset = 0 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "sb4", Type = CommandType.Buy, AskType = CommandType.Sell, Detail = 3, AType = AmountType.P10, Offset = 0 });
                defaultFunctionObjectList.Add(new FunctionObject());
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "ss1", Type = CommandType.Sell, AskType = CommandType.Buy, Detail = 3, AType = AmountType.P100, Offset = 0 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "ss2", Type = CommandType.Sell, AskType = CommandType.Buy, Detail = 3, AType = AmountType.P50, Offset = 0 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "ss3", Type = CommandType.Sell, AskType = CommandType.Buy, Detail = 3, AType = AmountType.P25, Offset = 0 });
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "ss4", Type = CommandType.Sell, AskType = CommandType.Buy, Detail = 3, AType = AmountType.P10, Offset = 0 });
                defaultFunctionObjectList.Add(new FunctionObject());
                defaultFunctionObjectList.Add(new FunctionObject() { Name = "cc", Type = CommandType.Cancel, AType = AmountType.P10, Offset = 0 });
            }
            return defaultFunctionObjectList;
        }
    }
}
