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
    public partial class ServiceWindow : Form
    {
        public ServiceWindow()
        {
            InitializeComponent();

            dtpServiceDate.Value = DateTime.Now;
            dtpServiceDate.Enabled = true;
            dtpServiceTime.Value = DateTime.Now;
            dtpServiceTime.Enabled = true;

            tbMaster.Text = "";
            tbMaster.ReadOnly = false;
            tbServiceName.Text = "";
            tbServiceShortName.Text = "";
            tbPrice.Text = "";
            tbTotalPrice.Text = "";


            cbFactor.Items.Clear();
            for (int i = 1; i <= 1000; i++)
            {
                cbFactor.Items.Add(i.ToString());
            }
            cbDiscount.Items.Clear();
            for (int i = 0; i <= 100; i++)
            {
                cbDiscount.Items.Add(i.ToString());
            }
            cbFactor.SelectedIndex = 0;
            cbDiscount.SelectedIndex = 0;

            tbServiceShortName.ReadOnly = false;
            tbServiceName.ReadOnly = false;
            tbPrice.ReadOnly = false;
            tbTotalPrice.ReadOnly = true;
            cbDiscount.Enabled = true;
            cbFactor.Enabled = true;
            cbConstService.Enabled = true;

            buttonDeleteLine.Visible = false;
            buttonCancel.Visible = false;
            buttonEditLine.Visible = false;
            buttonSaveLine.Visible = false;
            buttonAddLine.Visible = true;

        }


        public ServiceWindow(Service service)
        {
            InitializeComponent();

            dtpServiceDate.Value = service.dateAndTime;
            dtpServiceDate.Enabled = false;
            dtpServiceTime.Value = service.dateAndTime;
            dtpServiceTime.Enabled = false;

            tbMaster.Text = service.master;
            
            tbServiceName.Text = service.name;
            tbServiceShortName.Text = service.shortName;
            tbPrice.Text = service.price.ToString();
            tbTotalPrice.Text = service.GetTotalPrice().ToString();

            cbFactor.Items.Clear();
            for (int i = 1; i <= 1000; i++)
            {
                cbFactor.Items.Add(i.ToString());
            }
            cbDiscount.Items.Clear();
            for (int i = 0; i <= 100; i++)
            {
                cbDiscount.Items.Add(i.ToString());
            }
            cbFactor.SelectedIndex = service.factor-1;
            cbDiscount.SelectedIndex = service.discount;

            tbMaster.ReadOnly = true; 
            tbServiceShortName.ReadOnly = true;
            tbServiceName.ReadOnly = true;
            tbPrice.ReadOnly = true;
            tbTotalPrice.ReadOnly = true;
            cbDiscount.Enabled = false;
            cbFactor.Enabled = false;
            cbConstService.Enabled = false;

            buttonDeleteLine.Visible = true;
            buttonCancel.Visible = false;
            buttonEditLine.Visible = true;
            buttonSaveLine.Visible = false;
            buttonAddLine.Visible = false;

        }

        private void ServiceWindow_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            buttonEditLine.Location = buttonSaveLine.Location = buttonAddLine.Location;

        }

        private void buttonAddLine_Click(object sender, EventArgs e)
        {
            if (CheckData()) this.DialogResult = DialogResult.OK;
        }

        private bool CheckData()
        {
            bool flag=false;
            char ch;

            if ((tbMaster.Text != "") && (tbServiceName.Text != "")
                && (tbServiceShortName.Text != "") && (tbPrice.Text != ""))
            {
                flag = true;
                for (int i = 0; ((flag) && (i < tbPrice.Text.Length)); i++)
                {
                    ch = tbPrice.Text[i];
                    flag = ((ch >= '0') && (ch <= '9')
                        ||((ch=='.')&&(i!=0)&&(i!=tbPrice.Text.Length-1)
                        &&(tbPrice.Text.IndexOf('.')==tbPrice.Text.LastIndexOf('.'))));
                    if (!flag) MessageBox.Show("Неверно введена стоимость.");
                }
                
            }
            else MessageBox.Show("Необходимо заполнить все поля.");
             
            return flag;
        }

        public Service GetLine()

        {
            DateTime dateTime = new DateTime(dtpServiceDate.Value.Year, dtpServiceDate.Value.Month, dtpServiceDate.Value.Day, dtpServiceTime.Value.Hour, dtpServiceTime.Value.Minute, dtpServiceTime.Value.Second);
            return (new Service(dateTime, tbMaster.Text,
                                tbServiceName.Text, tbServiceShortName.Text,
                                Convert.ToDecimal(tbPrice.Text), 
                                Convert.ToInt32(cbDiscount.Text), Convert.ToInt32(cbFactor.Text)));
        }

        private void tbPrice_Leave(object sender, EventArgs e)
        {
            //tbTotalPrice.Text = ((Convert.ToDecimal(tbPrice.Text) - Convert.ToDecimal(tbPrice.Text) * Convert.ToInt32(cbDiscount.Text) / 100) * Convert.ToInt32(cbFactor.Text)).ToString();
        }

        private void cbFactor_Leave(object sender, EventArgs e)
        {
            //tbTotalPrice.Text = ((Convert.ToDecimal(tbPrice.Text) - Convert.ToDecimal(tbPrice.Text) * Convert.ToInt32(cbDiscount.Text) / 100) * Convert.ToInt32(cbFactor.Text)).ToString();
        }

        private void tbServiceName_Leave(object sender, EventArgs e)
        {
            if (tbServiceShortName.Text == "")
            {
                tbServiceShortName.Text = (tbServiceName.Text.Length>=10)?(tbServiceName.Text.Substring(0, 10)):(tbServiceName.Text);
            }
        }

        private void buttonEditLine_Click(object sender, EventArgs e)
        {
            dtpServiceDate.Enabled = true;
            dtpServiceTime.Enabled = true;

            tbMaster.ReadOnly = false; 
            tbServiceShortName.ReadOnly = false;
            tbServiceName.ReadOnly = false;
            tbPrice.ReadOnly = false;
            tbTotalPrice.ReadOnly = true;
            cbDiscount.Enabled = true;
            cbFactor.Enabled = true;
            cbConstService.Enabled = true;

            buttonDeleteLine.Visible = true;
            buttonCancel.Visible = true;
            buttonEditLine.Visible = false;
            buttonSaveLine.Visible = true;
            buttonAddLine.Visible = false;
        }

        private void tbPrice_TextChanged(object sender, EventArgs e)
        {
            if ((tbPrice.Text != "")&&(cbDiscount.SelectedIndex!=-1)&&(cbFactor.SelectedIndex!=-1))
            {
                bool flag = true;
                char ch;
                for (int i = 0; ((flag) && (i < tbPrice.Text.Length)); i++)
                {
                    ch = tbPrice.Text[i];
                    flag = ((ch >= '0') && (ch <= '9')
                        ||((ch=='.')&&(i!=0)&&(i!=tbPrice.Text.Length-1)
                        &&(tbPrice.Text.IndexOf('.')==tbPrice.Text.LastIndexOf('.'))));
                }
                if (flag)
                {
                    tbTotalPrice.Text = ((Convert.ToDecimal(tbPrice.Text)* (100-  cbDiscount.SelectedIndex) / 100) * (cbFactor.SelectedIndex+1)).ToString();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbConstService_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonSaveLine_Click(object sender, EventArgs e)
        {
            if (CheckData()) this.DialogResult = DialogResult.OK;
        }
    }
}
