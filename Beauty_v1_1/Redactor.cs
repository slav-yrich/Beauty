using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Beauty_v1_1
{
    public partial class Redactor : Form
    {
        int number;
        List<Service> serviceList;
        DataManager dataManager;

        public Redactor(int number, DataManager dataManager)
        {
            InitializeComponent();
            this.number = number;
            tbNumber.Text = number.ToString();
            this.dataManager = dataManager;

            

            dtpBirthDate.Value = DateTime.Now;
            dtpBirthDate.Enabled = true;
            radioButton1.Checked = false;
            radioButton1.Enabled = true;
            radioButton2.Checked = true;
            radioButton2.Enabled = true;
            tbFirstName.Text = "";
            tbFirstName.ReadOnly = false;
            tbSecondName.Text = "";
            tbSecondName.ReadOnly = false;
            tbPatronymic.Text = "";
            tbPatronymic.ReadOnly = false;
            tbFirstPhone.Text = "";
            tbFirstPhone.ReadOnly = false;
            tbSecondPhone.Text = "";
            tbTotal.Text = "";
            tbTotal.ReadOnly = true;
            dataGridView1.Rows.Clear();
            serviceList = new List<Service>();
            dataGridView1.DataSource = PrintOnTable();
            dataGridView1.Enabled = true;

            tbDiscountCardNumber.Text = "";
            tbDiscountCardNumber.ReadOnly = false;
            cbDiscountCardValue.Items.Clear();
            for (int i = 0; i <= 100; i++)
            {
                cbDiscountCardValue.Items.Add(i.ToString());
            }
            cbDiscountCardValue.SelectedIndex = 0;
            cbDiscountCardValue.Enabled = true;

            tbSecondPhone.ReadOnly = false;
            buttonDeleteLine.Visible = false;
            buttonEditLine.Visible = false;
            buttonSaveLine.Visible = false;
            buttonAddLine.Visible = true;
            buttonCancel.Visible = false;

            buttonAddService.Enabled = true;
        }

        public Redactor(OneString line, DataManager dataManager)
        {
            InitializeComponent();
            number = line.number;
            tbNumber.Text = line.number.ToString();
            this.dataManager = dataManager;

            groupBox1.Visible = false;

            dtpBirthDate.Value = line.client.birthDate;
            dtpBirthDate.Enabled = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            if (line.client.sex=='М')
            {
                radioButton1.Checked = true;
            }
            else
                {
                    radioButton2.Checked = true;
                }
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            tbFirstName.Text = line.client.firstName;
            tbFirstName.ReadOnly = true;
            tbSecondName.Text = line.client.secondName;
            tbSecondName.ReadOnly = true;
            tbPatronymic.Text = line.client.patronymic;
            tbPatronymic.ReadOnly = true;
            
            tbFirstPhone.Text = line.client.firstPhone;
            tbFirstPhone.ReadOnly = true;
            tbSecondPhone.Text = line.client.secondPhone;
            tbSecondPhone.ReadOnly = true;
            tbTotal.Text = line.GetSummPriceService().ToString();
            tbTotal.ReadOnly = true;
            dataGridView1.Rows.Clear();
            serviceList = line.serviceList;
            dataGridView1.DataSource = PrintOnTable();
            //dataGridView1.Enabled = false;

            tbDiscountCardNumber.Text = line.client.discountCardNumber.ToString();
            tbDiscountCardNumber.ReadOnly = true;
            cbDiscountCardValue.Items.Clear();
            for (int i = 0; i <= 100; i++)
            {
                cbDiscountCardValue.Items.Add(i.ToString());
            }
            cbDiscountCardValue.SelectedIndex = line.client.discountCardValue;
            cbDiscountCardValue.Enabled = false;

            buttonDeleteLine.Visible = true;
            buttonEditLine.Visible = true;
            buttonSaveLine.Visible = false;
            buttonAddLine.Visible = false;
            buttonCancel.Visible = false;

            buttonAddService.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Redactor_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            buttonEditLine.Location = buttonSaveLine.Location = buttonAddLine.Location;
        }

        public OneString GetLine()
        {
            char sex;
            
            if (radioButton1.Checked) sex = 'М';
            else  sex = 'Ж';
            
            
            //Заполнить сервисЛист
            
            return (new OneString(number,
                                  tbFirstName.Text, tbSecondName.Text, tbPatronymic.Text,
                                  tbFirstPhone.Text, tbSecondPhone.Text, dtpBirthDate.Value, sex,   
                                  tbDiscountCardNumber.Text, cbDiscountCardValue.SelectedIndex,
                                  serviceList));
        }


        private void buttonSaveLine_Click(object sender, EventArgs e)
        {
            

            if ((tbFirstName.Text != "") && ((tbFirstPhone.Text != "") || (tbSecondPhone.Text != "")))
            {
                //number = dataManager.GetNewNumber();
                //tbNumber.Text = number.ToString();
                dataManager.dataBase[dataManager.SearchLine(number)] = GetLine();
                dataManager.saveBase(dataManager.dataBaseFileName);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("Необходимо записать Имя и хотя бы один контактный номер телефона.");
        }

        private void buttonAddLine_Click(object sender, EventArgs e)
        {
            if ((tbFirstName.Text != "") && ((tbFirstPhone.Text != "") || (tbSecondPhone.Text != "")))
            {
                //number = dataManager.GetNewNumber();
                //tbNumber.Text = number.ToString();
                dataManager.dataBase.Add(GetLine());
                dataManager.saveBase(dataManager.dataBaseFileName);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("Необходимо записать Имя и хотя бы один контактный номер телефона.");
        }

        private void buttonEditLine_Click(object sender, EventArgs e)
        {

            
            
            dtpBirthDate.Enabled = true;

            tbFirstName.ReadOnly = false;
            tbSecondName.ReadOnly = false;
            tbPatronymic.ReadOnly = false;
            
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;

            tbFirstPhone.ReadOnly = false;
            tbSecondPhone.ReadOnly = false;

            dataGridView1.Enabled = true;
            tbTotal.ReadOnly = true;

            tbDiscountCardNumber.ReadOnly = true;
            cbDiscountCardValue.Enabled = true;

            buttonDeleteLine.Visible = true;
            buttonEditLine.Visible = false;
            buttonSaveLine.Visible = true;
            buttonAddLine.Visible = false;

            buttonAddService.Enabled = true;
            buttonCancel.Visible = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0)&&(!buttonEditLine.Visible))
            {
                string selectName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (EditServiceLine(selectName))
                {
                    dataGridView1.DataSource = PrintOnTable();
                }
            }
        }

        public DataTable PrintOnTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Дата/Время");
            dt.Columns.Add("Мастер");
            dt.Columns.Add("Наименование");
            dt.Columns.Add("Сокращение");
            dt.Columns.Add("Стоимость");
            dt.Columns.Add("Количество");
            dt.Columns.Add("Скидка");
            dt.Columns.Add("Итого");

            decimal totalPrice=0;

            if ((serviceList != null) && (serviceList.Count > 0))
            {
                foreach (Service serviceline in serviceList)
                {
                    DataRow r = dt.NewRow();
                    r["Дата/Время"] = serviceline.dateAndTime.ToShortDateString() + "/" + serviceline.dateAndTime.ToShortTimeString();
                    r["Мастер"] = serviceline.master;
                    r["Наименование"] = serviceline.name;
                    r["Сокращение"] = serviceline.shortName;
                    r["Стоимость"] = serviceline.price.ToString();                    
                    r["Количество"] = serviceline.factor.ToString();
                    r["Скидка"] = serviceline.discount.ToString();
                    r["Итого"] = serviceline.GetTotalPrice().ToString();
                    totalPrice += serviceline.GetTotalPrice();
                    dt.Rows.Add(r);
                }
            }
            tbTotal.Text = (totalPrice*(100-cbDiscountCardValue.SelectedIndex)/100).ToString();
            return dt;
        }



        private void buttonAddService_Click(object sender, EventArgs e)
        {
            ServiceWindow serviceWindow = new ServiceWindow();
            DialogResult okFlag = serviceWindow.ShowDialog();
            if (okFlag == DialogResult.OK)
            {

                serviceList.Add(serviceWindow.GetLine());
                dataGridView1.DataSource = PrintOnTable();
            }
            serviceWindow.Dispose();
        }

        public bool EditServiceLine(string selectName)
        {
            bool flag = false;
            int numberLine = SearchService(selectName); ;
            if (numberLine >= 0)
            {
                ServiceWindow serviceWindow = new ServiceWindow(serviceList[numberLine]);
                DialogResult okFlag = serviceWindow.ShowDialog();
                if (okFlag == DialogResult.OK)
                {
                    serviceList[numberLine] = serviceWindow.GetLine();
                    //записать изменения в файл?
                    flag = true;
                }
                else
                {
                    if (okFlag == DialogResult.Abort)
                    {
                        serviceList.RemoveAt(numberLine);
                        flag = true;
                    }
                }
                serviceWindow.Dispose();
            }

            return flag;
        }

        public int SearchService(string selectName)
        {
            for (int i = 0; i < serviceList.Count; i++)
            {
                if (serviceList[i].name == selectName) return i;
            }
            return -1;
        }


        private void cbDiscountCardValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = PrintOnTable();
        }

        private void tbSecondName_TextChanged(object sender, EventArgs e)
        {
            if (groupBox1.Visible)
            {
                dataManager.heuristicSearch(ref listBox1, tbFirstName.Text, tbSecondName.Text, tbPatronymic.Text, tbFirstPhone.Text, tbSecondPhone.Text, tbDiscountCardNumber.Text);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex!=-1)
            {
                string currentLine = listBox1.SelectedItem.ToString();
                int i = dataManager.SearchLine(Convert.ToInt32(currentLine.Substring(0, currentLine.IndexOf("|"))));
                tbNumber.Text = dataManager.dataBase[i].number.ToString();
                number = dataManager.dataBase[i].number;
                tbSecondName.Text = dataManager.dataBase[i].client.secondName;
                tbFirstName.Text = dataManager.dataBase[i].client.firstName;
                tbPatronymic.Text = dataManager.dataBase[i].client.patronymic;
                tbFirstPhone.Text = dataManager.dataBase[i].client.firstPhone;
                tbSecondPhone.Text = dataManager.dataBase[i].client.secondPhone;
                dtpBirthDate.Value = dataManager.dataBase[i].client.birthDate;
                if (dataManager.dataBase[i].client.sex == 'М')
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                tbDiscountCardNumber.Text = dataManager.dataBase[i].client.discountCardNumber;
                cbDiscountCardValue.SelectedIndex = dataManager.dataBase[i].client.discountCardValue;
                serviceList = dataManager.dataBase[i].serviceList;
                dataGridView1.DataSource = PrintOnTable();

                buttonDeleteLine.Visible = true;
                buttonEditLine.Visible = false;
                buttonSaveLine.Visible = true;
                buttonAddLine.Visible = false;
            }
        }

        private void buttonDeleteLine_Click(object sender, EventArgs e)
        {
            dataManager.dataBase.RemoveAt(dataManager.SearchLine(number));
            dataManager.saveBase(dataManager.dataBaseFileName);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataManager.heuristicSearch(ref listBox1, tbFirstName.Text, tbSecondName.Text, tbPatronymic.Text, tbFirstPhone.Text, tbSecondPhone.Text, tbDiscountCardNumber.Text);
        }
        
    }
}
