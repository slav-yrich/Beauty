using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Beauty_v1_1
{
    public class OneString
    {
        public int number;
        
        public Client client;
        public List<Service> serviceList;

        //

        public OneString(int number, string firstName, string secondName, string patronymic, string firstPhone, string secondPhone, DateTime birthDate,char sex, string discountCardNumber, int discountCardValue, List<Service> serviceList)
        {
            this.number = number;
            
            client = new Client(firstName,secondName,patronymic,firstPhone,secondPhone,birthDate,sex, discountCardNumber, discountCardValue);
            this.serviceList = serviceList;
        }

        //При добавлении услуги описанной в файле услуг
        public void addService(Service service)
        {
            serviceList.Add(service);
        }
        

        public decimal GetSummPriceService()
        {
            decimal summ = 0;
            if (serviceList!=null)
            {
                foreach(Service service in serviceList)
                {
                    summ += service.GetTotalPrice();
                }
            }
            summ = summ*(100 - client.discountCardValue) / 100;
            return summ;
        }

        public int SearchService(string name)
        {
            for (int i = 0; i < serviceList.Count; i++)
            {
                if (serviceList[i].name == name) return i;
            }
            return -1;
        }

        public List<Service> SearchServiceKeyMaster(string master)
        {
            List<Service> findServiceList = new List<Service>();
            for (int i = 0; i < serviceList.Count; i++)
            {
                if (serviceList[i].master == master) findServiceList.Add(serviceList[i]);
            }
            return findServiceList;
        }
    }
}
