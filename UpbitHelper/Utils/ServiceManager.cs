using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QuickHand.Utils
{
    public class ServiceManager
    {
        private static ServiceManager instance;

        public static ServiceManager GetInstance()
        {
            if(instance == null)
            {
                instance = new ServiceManager();
            }

            return instance;
        }

        QuickHandService.ServiceClient client;
        

        public ServiceManager()
        {
            try
            {
                client = new QuickHandService.ServiceClient();
                
            }catch(Exception e)
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(Messages.ServerErrorMessage));
                Debug.Print(e.Message);
            }
        }

        public string getVersion()
        {
            string result = client.GetVersion();
            return result;
        }

        public string getNotification()
        {
            string result = client.GetNotification();
            return result;
        }

    }
}
