using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickHand.Utils
{
    public class SaveUtil
    {
        public static void saveFile(SaveObject data)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(SaveObject));

                var path = Directory.GetCurrentDirectory() + "\\save.dat";
                System.IO.FileStream file = System.IO.File.Create(path);

                writer.Serialize(file, data);
                file.Close();
            }catch(Exception e)
            {
                Debug.Print(e.Message);
            }
        }

        public static SaveObject loadFile()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(SaveObject));
                var path = Directory.GetCurrentDirectory() + "\\save.dat";
                if (!File.Exists(path))
                {
                    SaveObject saveObject = new SaveObject();
                    saveObject.init();
                    saveObject.updateFunctionList();
                    return saveObject;
                }
                else
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(path);
                    SaveObject data = (SaveObject)reader.Deserialize(file);
                    if(data.commandList.Count == 0)
                    {
                        data.init();
                    }
                    data.updateFunctionList();
                    file.Close();
                    return data;
                }
            }catch(Exception e)
            {
                Debug.Print(e.Message);
                return new SaveObject();
            }
        }
    }
}
