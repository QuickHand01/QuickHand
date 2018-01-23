using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickHand.Utils
{
    public class Core
    {
        public static string ver = "1.1";

        private static Core instance;
        public static Core getInstance()
        {
            if(instance == null)
            {
                instance = new Core();
            }
            return instance;
        }

        public SaveObject SaveData { get; set; }

        public Core()
        {
            this.Load();
        }

        public void Load()
        {
            this. SaveData = SaveUtil.loadFile();
        }

        public void Save()
        {
            SaveUtil.saveFile(this.SaveData);
        }

        public FunctionObject findFunction(string cmd)
        {
            if (string.IsNullOrEmpty(cmd))
            {
                return null;
            }

            for (int i = 0; i < CommonUtil.getDefaultFunctionList().Count; i++)
            {
                if(CommonUtil.getDefaultFunctionList()[i] == cmd)
                {
                    return CommonUtil.getDefaultFunctionObjectList()[i];
                }
            }

            return null;
        }

    }
}
