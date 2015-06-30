using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty_v1_1
{
    public class Client
    {
        public string firstName;
        public string secondName;
        public string patronymic;

        public string firstPhone;
        public string secondPhone;

        public DateTime birthDate;
        public char sex;

        public string discountCardNumber;
        public int discountCardValue;


        public Client(string firstName, string secondName, string patronymic, string firstPhone, string secondPhone, DateTime birthDate, char sex,  string discountCardNumber, int discountCardValue)
        {
            this.firstName = firstName;
            this.secondName = secondName;
            this.patronymic = patronymic;

            this.firstPhone = firstPhone;
            this.secondPhone = secondPhone;

            this.birthDate = birthDate;
            this.sex = sex;


            this.discountCardNumber = discountCardNumber;
            this.discountCardValue = discountCardValue;
        }

        public String GetFullName
        {
            get
            {
                return string.Format("{0} {1} {2}", secondName, firstName, patronymic);
            }
        }

    }
}
