using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beauty_v1_1
{
    public partial class Filters : Form
    {
        List<OneString> selection;
        DataManager dataManager;

        public Filters(DataManager dataManager)
        {
            InitializeComponent();
            
            this.dataManager=dataManager;

            ClearFilters();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Filters_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool flag;
            selection = new List<OneString>();
            foreach (OneString line in dataManager.dataBase)
            {
                //Порядковый номер
                flag= true;
                if ((flag)&&(tbBeginNumber.Text!=""))
                {
                    flag &= (line.number >= Convert.ToInt32(tbBeginNumber.Text));
                }
                if ((flag)&&(tbEndNumber.Text!=""))
                {
                    flag &= (line.number <= Convert.ToInt32(tbEndNumber.Text));
                }
                ////Дата добавление
                //if ((flag)&&(dtpBeginDate.Value!=dtpBeginDate.MinDate))
                //{
                //    flag &= (line.dateAndTime >= dtpBeginDate.Value);
                //}
                //if ((flag) && (dtpEndDate.Value != dtpEndDate.MinDate))
                //{
                //    flag &= (line.dateAndTime <= dtpEndDate.Value);
                //}
                //ФИО
                if ((flag) && (tbKeyFirstName.Text != ""))
                {
                    flag &= (line.client.firstName == tbKeyFirstName.Text);
                }
                if ((flag) && (tbKeySecondName.Text != ""))
                {
                    flag &= (line.client.secondName == tbKeySecondName.Text);
                }
                if ((flag) && (tbKeyPatronomic.Text != ""))
                {
                    flag &= (line.client.patronymic == tbKeyPatronomic.Text);
                }
                //Телефоны
                if ((flag) && (tbKeyPhone.Text != ""))
                {
                    flag &= ((line.client.firstPhone == tbKeyPhone.Text) || (line.client.secondPhone == tbKeyPhone.Text));
                }
                //Поиск по мастеру
                if ((flag) && (tbKeyMaster.Text!=""))
                {
                    flag &= (line.SearchServiceKeyMaster(tbKeyMaster.Text).Count>0);
                }


                if (flag) selection.Add(line);
            }

            if (tbKeyMaster.Text != "")
            {
                dataGridView1.DataSource = PrintOnTableService();
            }
            else
            {
                dataGridView1.DataSource = PrintOnTable();
            }
        }
        public void ClearFilters()
        {
            tbBeginNumber.Text = "";
            tbEndNumber.Text = "";
            dtpBeginDate.Value = dtpBeginDate.MinDate;
            dtpEndDate.Value = dtpEndDate.MaxDate;
            tbKeyFirstName.Text = "";
            tbKeySecondName.Text = "";
            tbKeyPatronomic.Text = "";
            tbKeyPhone.Text = "";
            selection = dataManager.dataBase;
            dataGridView1.DataSource = PrintOnTable();
        }

        public DataTable PrintOnTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("№");
            //dt.Columns.Add("Дата/Время");
            dt.Columns.Add("ФИО клиента");
            dt.Columns.Add("Список услуг (кратко)");
            dt.Columns.Add("Сумма");


            if ((selection != null) && (selection.Count > 0))
            {
                foreach (OneString line in selection)
                {
                    DataRow r = dt.NewRow();
                    r["№"] = line.number.ToString();
                    //r["Дата/Время"] = line.dateAndTime.ToShortDateString() + "/" + line.dateAndTime.ToShortTimeString();
                    r["ФИО клиента"] = line.client.GetFullName;
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

        public DataTable PrintOnTableService()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("№");
            //dt.Columns.Add("Дата/Время");
            dt.Columns.Add("ФИО клиента");
            dt.Columns.Add("Услуга");
            dt.Columns.Add("Итоговая стоимость");

            double TotalPrice = 0;
            int TotalNumberService = 0;

            if ((selection != null) && (selection.Count > 0))
            {
                foreach (OneString line in selection)
                {
                    foreach (Service serv in line.SearchServiceKeyMaster(tbKeyMaster.Text))
                    {
                        DataRow r = dt.NewRow();
                        r["№"] = line.number.ToString();
                        //r["Дата/Время"] = line.dateAndTime.ToShortDateString() + "/" + line.dateAndTime.ToShortTimeString();
                        r["ФИО клиента"] = line.client.GetFullName;
                        r["Услуга"] = serv.name;
                        r["Итоговая стоимость"] = serv.GetTotalPrice().ToString();
                        dt.Rows.Add(r);

                        TotalPrice += (double)serv.GetTotalPrice();
                        TotalNumberService++;
                    }
                }
            }

            DataRow total = dt.NewRow();
            total["№"] = "-1";
            //r["Дата/Время"] = line.dateAndTime.ToShortDateString() + "/" + line.dateAndTime.ToShortTimeString();
            total["ФИО клиента"] = "Итого";
            total["Услуга"] = "Всего услуг: " + TotalNumberService.ToString();
            total["Итоговая стоимость"] = TotalPrice.ToString();
            dt.Rows.Add(total);

            return dt;
        }

        private void buttonClearFilter_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int selectNumber = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                if (dataManager.EditLine(selectNumber))
                {
                    dataGridView1.DataSource = dataManager.PrintOnTable();
                }
            }
            
        }
    }
}
