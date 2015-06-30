using System;
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
    public partial class Form1 : Form
    {
        public DataManager dataManager;
        public string settingsFileName = "settingsFile.bsf";
        public string dataBaseFileName = "dataBase.bdb",
                      pricelistFileName = "pricelist.bpl",
                      clientlistFileName = "clientlist.bpl";
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            bool flag=true;
            if (File.Exists(settingsFileName))
            {
                StreamReader settingFile = new StreamReader(settingsFileName);
                string str = settingFile.ReadLine();
                if (str == "") flag = false;
                else { dataBaseFileName = str; }

                settingFile.Close();
            }
            else
            {
                flag = false;
            }
            if(!flag)
            {
                //Здесь добавить вызов окна настроек
                StreamWriter settingFile = new StreamWriter(settingsFileName);
                settingFile.WriteLine("dataBase.bdb");
                settingFile.Close();
            }
            dataManager = new DataManager(dataBaseFileName,pricelistFileName);
            dataGridView1.DataSource = dataManager.PrintOnTable();
            
        }

        private void загрузитьБазуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((openFileDialog1.ShowDialog()) == DialogResult.OK)
            {
                dataBaseFileName = openFileDialog1.FileName;
                dataManager.LoadBase(dataBaseFileName);
                dataGridView1.DataSource = dataManager.PrintOnTable();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void buttonAddLine_Click(object sender, EventArgs e)
        {
            if (dataManager.AddLine())
            {
                dataGridView1.DataSource = dataManager.PrintOnTable();
            }
        }

        private void настройкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((saveFileDialog1.ShowDialog()) == DialogResult.OK)
            {
                dataBaseFileName = saveFileDialog1.FileName;
                dataManager.saveBase(dataBaseFileName);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataManager.saveBase(dataBaseFileName);
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filtersForm = new Filters(dataManager);
            filtersForm.Owner = this;
            filtersForm.ShowDialog();
            dataGridView1.DataSource = dataManager.PrintOnTable();
        }

        private void сгенерироватьБазуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i=0;i<=10000;i++)
            {
                List<Service> serviceList = new List<Service>();
                for (int j = 0; j <= 100;j++ )
                {
                    serviceList.Add(new Service(DateTime.Now, "master" + i.ToString(), "Service" + i.ToString(), "Serv" + i.ToString(), (new Random()).Next(10,10000), (new Random()).Next(99)));
                }
                dataManager.dataBase.Add(new OneString(dataManager.GetNewNumber(), "FirstName" + i.ToString(), "SecondName" + i.ToString(), "Patronymic" + i.ToString(),
                                                            (i * 10000).ToString(), (i * 10000 + i).ToString(), DateTime.Now, 'Ж', i.ToString(), (new Random()).Next(99), serviceList));
            }
            dataManager.saveBase(dataBaseFileName);
            dataGridView1.DataSource = dataManager.PrintOnTable();
        }
    }
}
