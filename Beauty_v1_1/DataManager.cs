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
    public class DataManager
    {
        public List<OneString> dataBase;
        public string dataBaseFileName,
                      pricelistFileName;
 
        public DataManager(string dataBaseFileName,
                           string pricelistFileName)
        {
            this.dataBaseFileName = dataBaseFileName;
            this.pricelistFileName = pricelistFileName; ;
            dataBase = new List<OneString>();
            if (File.Exists(dataBaseFileName))
            {
                LoadBase(dataBaseFileName);

            }
        }

        public void LoadBase(string fileAddres)
        {
            try
            {
                bool flag=true;
                StreamReader fileBase = new StreamReader(fileAddres);
                dataBase = new List<OneString>();
                int countOfLine = Convert.ToInt32(fileBase.ReadLine());

                for (int i = 0; i < countOfLine; i++)
                {
                    int number = Convert.ToInt32(fileBase.ReadLine());
                    //DateTime dateAndTime = Convert.ToDateTime(fileBase.ReadLine());

                    string secondName = fileBase.ReadLine();
                    string firstName = fileBase.ReadLine();
                    string patronomic = fileBase.ReadLine();

                    string firstPhone = fileBase.ReadLine();
                    string secondPhone = fileBase.ReadLine();

                    DateTime birthDate = Convert.ToDateTime(fileBase.ReadLine());
                    char sex = fileBase.ReadLine()[0];

                    string discountCardNumber = fileBase.ReadLine();
                    int discountCardValue = Convert.ToInt32(fileBase.ReadLine()); ;

                    int countOfService = Convert.ToInt32(fileBase.ReadLine());
                    List<Service> serviceList = new List<Service>();
                    for (int j = 0; j < countOfService; j++)
                    {
                        DateTime dateAndTime = Convert.ToDateTime(fileBase.ReadLine());
                        string master = fileBase.ReadLine();
                        string name = fileBase.ReadLine();
                        string shortName = fileBase.ReadLine();
                        decimal price = Convert.ToDecimal(fileBase.ReadLine());
                        int discount = Convert.ToInt32(fileBase.ReadLine());
                        int factor = Convert.ToInt32(fileBase.ReadLine());
                        serviceList.Add(new Service(dateAndTime,master, name, shortName, price, discount, factor));
                    }

                    dataBase.Add(new OneString(number, firstName, secondName, patronomic, firstPhone, secondPhone, birthDate, sex, discountCardNumber, discountCardValue, serviceList));

                }
                fileBase.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void saveBase(string fileAddres)
        {
            StreamWriter fileBase;
            try
            {
                fileBase = new StreamWriter(fileAddres);
                fileBase.WriteLine(dataBase.Count);

                foreach (OneString line in dataBase)
                {
                    fileBase.WriteLine(line.number);

                    
                    fileBase.WriteLine(line.client.secondName);
                    fileBase.WriteLine(line.client.firstName);
                    fileBase.WriteLine(line.client.patronymic);

                    fileBase.WriteLine(line.client.firstPhone);
                    fileBase.WriteLine(line.client.secondPhone);

                    fileBase.WriteLine(line.client.birthDate);
                    fileBase.WriteLine(line.client.sex);

                    fileBase.WriteLine(line.client.discountCardNumber);
                    fileBase.WriteLine(line.client.discountCardValue);

                    if (line.serviceList != null)
                    {
                        fileBase.WriteLine(line.serviceList.Count);
                        foreach (Service service in line.serviceList)
                        {
                            fileBase.WriteLine(service.dateAndTime);
                            fileBase.WriteLine(service.master);
                            fileBase.WriteLine(service.name);
                            fileBase.WriteLine(service.shortName);
                            fileBase.WriteLine(service.price);
                            fileBase.WriteLine(service.discount);
                            fileBase.WriteLine(service.factor);
                        }
                    }
                    else fileBase.WriteLine("0");
                }
                fileBase.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void heuristicSearch(ref ListBox listBox1,string subFirstName="", string subSecondName="", string subPatronymic="",string subFirstPhone="",string subSecondPhone="",string subDiscountCardNumber="")
        {
            listBox1.Items.Clear();
           

            foreach (OneString line in dataBase)
            {
                if (((subFirstName != "") && (line.client.firstName.ToLower().IndexOf(subFirstName.ToLower()) != -1))
                    || ((subSecondName != "") && (line.client.secondName.ToLower().IndexOf(subSecondName.ToLower()) != -1))
                    || ((subPatronymic != "") && (line.client.patronymic.ToLower().IndexOf(subPatronymic.ToLower()) != -1))
                    || ((subFirstPhone != "") && (line.client.firstPhone.ToLower().IndexOf(subFirstPhone.ToLower()) != -1))
                    || ((subSecondPhone != "") && (line.client.secondName.ToLower().IndexOf(subSecondPhone.ToLower()) != -1))
                    || ((subDiscountCardNumber != "") && (line.client.secondName.ToLower().IndexOf(subDiscountCardNumber.ToLower()) != -1)))
                {
                    listBox1.Items.Add(String.Format("{0}| {1} {2} {3} | т1:{4}//т2:{5} | card:{6}", line.number,line.client.secondName, line.client.firstName, line.client.patronymic, line.client.firstPhone, line.client.secondPhone, line.client.discountCardNumber));
                }
                
            }
        }

        public DataTable PrintOnTable() //генерация источника данных (виртуальной таблицы) для вывода в таблицу
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("№");
            //dt.Columns.Add("Дата/Время");
            dt.Columns.Add("ФИО");
            dt.Columns.Add("Список услуг (кратко)");
            dt.Columns.Add("Сумма");


            if ((dataBase != null) && (dataBase.Count > 0))
            {
                foreach (OneString line in dataBase)
                {
                    DataRow r = dt.NewRow();
                    r["№"] = line.number.ToString();
                    //r["Дата/Время"] = line.dateAndTime.ToShortDateString() + "/" + line.dateAndTime.ToShortTimeString();
                    r["ФИО"] = line.client.GetFullName;
                    foreach (Service service in line.serviceList)
                    {
                        r["Список услуг (кратко)"] += service.shortName + " | ";
                    }
                    r["Сумма"] = line.GetSummPriceService();
                    dt.Rows.Add(r);
                }
            }
            return dt;
        }

        public bool AddLine() //функция добавления записи
        {
            bool flag = false; 
            Redactor redactorWindow = new Redactor(GetNewNumber(), this);
            DialogResult okFlag = redactorWindow.ShowDialog();
            if (okFlag == DialogResult.OK)
            {
                //dataBase.Add(redactorWindow.GetLine());

                flag = true;
            }
            redactorWindow.Dispose();
            return flag;

        }

        public bool EditLine(int selectNumber) //функции редактирования и удаления записи
        {
            bool flag = false;
            int numberLine = SearchLine(selectNumber); ;
            if (numberLine >=0)
            {
                Redactor redactorWindow = new Redactor(dataBase[numberLine], this);
                DialogResult okFlag = redactorWindow.ShowDialog();
                if (okFlag == DialogResult.OK)
                {
                    //dataBase[numberLine]=redactorWindow.GetLine();
                    //записать изменения в файл?
                    flag = true;
                }
                else
                {
                    if (okFlag==DialogResult.Abort)
                    {
                        //dataBase.RemoveAt(numberLine);
                        flag = true;
                    }
                }
                redactorWindow.Dispose();
            }
            //if (flag) saveBase(dataBaseFileName);
            return flag;
        }

        public int GetNewNumber() //Поиск последнего занятого кода и генерация +1-го
        {
            if (dataBase == null) return 1;
            else
            {
                int maxNumber = 0;
                foreach (OneString line in dataBase)
                {
                    if (line.number > maxNumber) maxNumber = line.number;
                }
                return maxNumber+1;
            }
        }

        public int SearchLine(int number) //Поиск порядкового номера записи в базе по её коду (номеру)
        {
            for (int i = 0; i < dataBase.Count; i++)
            {
                if (dataBase[i].number == number) return i;
            }
            return -1;
        }



    }
}
