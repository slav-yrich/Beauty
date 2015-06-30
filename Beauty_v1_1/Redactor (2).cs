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
    public partial class Redactor : Form
    {

        public Redactor(int number)
        {
            InitializeComponent();
            tbNumber.Text = number.ToString();
            tbNumber.ReadOnly = true;
            dtpServiceDate.Value = DateTime.Now;
            dtpServiceTime.Value = DateTime.Now;
            tbFirstName.Text = "";
            tbFirstName.ReadOnly = false;
            tbSecondName.Text = "";
            tbSecondName.ReadOnly = false;
            tbPatronymic.Text = "";
            tbPatronymic.ReadOnly = false;
            tbAddres.Text = "";
            tbAddres.ReadOnly = false;
            tbFirstPhone.Text = "";
            tbFirstPhone.ReadOnly = false;
            tbSecondPhone.Text = "";
            tbTotal.Text = "";
            tbTotal.ReadOnly = true;
            dataGridView1.Rows.Clear();
            tbSecondPhone.ReadOnly = false;
            buttonDeleteLine.Visible = false;
            buttonChangeLine.Visible = false;
            buttonSaveLine.Visible = false;
            buttonAddLine.Visible = true;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Redactor_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            buttonChangeLine.Location = buttonSaveLine.Location = buttonAddLine.Location;
        }

        public OneString GetLine()
        {
            
            return (new OneString(Convert.ToInt32(tbNumber.Text),
                                  dtpServiceDate.Value,
                                  tbFirstName,tbSecondName,tbPatronymic,
                                  tbFirstPhone,tbSecondPhone,);
        }

        
    }
}
