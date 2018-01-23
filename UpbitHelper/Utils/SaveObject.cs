using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuickHand.Utils
{
    [Serializable]
    public class SaveObject
    {
        public bool isTop { get; set; }
        public List<CommandObject> commandList { get; set; }

        public SaveObject()
        {
            commandList = new List<CommandObject>();
        }

        public void init()
        {
            commandList.Add(new CommandObject() { Command = "시장가로 25% 매수", Modifier = KeyInform.Control, Key = System.Windows.Forms.Keys.Q });
            commandList.Add(new CommandObject() { Command = "시장가로 50% 매수", Modifier = KeyInform.Control, Key = System.Windows.Forms.Keys.W });
            commandList.Add(new CommandObject() { Command = "시장가로 100% 매수", Modifier = KeyInform.Control, Key = System.Windows.Forms.Keys.A });
            commandList.Add(new CommandObject() { Command = "시장가로 50% 매도", Modifier = KeyInform.Control, Key = System.Windows.Forms.Keys.S });
            commandList.Add(new CommandObject() { Command = "시장가로 100% 매도", Modifier = KeyInform.Control, Key = System.Windows.Forms.Keys.Z });
            commandList.Add(new CommandObject() { Command = "주문 취소", Modifier = KeyInform.Control, Key = System.Windows.Forms.Keys.X });
        }

        public void updateFunctionList()
        {
            foreach (var item in commandList)
            {
                string temp = item.Command;
                item.FunctionList.Clear();
                foreach (var item2 in CommonUtil.getDefaultFunctionList())
                {
                    item.FunctionList.Add(item2);
                }
                item.Command = temp;
            }
        }
    }
}
