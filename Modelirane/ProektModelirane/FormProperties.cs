using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProektModelirane
{
    public partial class FormProperties : Form
    {
        BuildingBlock item;

        public FormProperties()
        {
            InitializeComponent();
        }

        public FormProperties(BuildingBlock item)
        {
            InitializeComponent();

            this.item = item;
            tbExpression.Text = this.item.GetExpression().ToString();
            tbName.Text = this.item.GetName();             
        }
        

        private void btnOK_Click(object sender, EventArgs e)
        {
            item.SetName(tbName.Text);
            //Double result = 0;
            //if(Double.TryParse(tbExpression.Text, out result)) item.SetValue(result);
            item.SetExpression(tbExpression.Text);
            this.Close();
        }

        public void AskForName()
        {
            lblName.Text = "Името \"" + tbName.Text + "\" вече съществува. \nИзберете ново име:";
            lblName.ForeColor = Color.Red;
            tbName.Focus();
        }
    }
}
