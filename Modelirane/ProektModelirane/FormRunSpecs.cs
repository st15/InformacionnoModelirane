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
    public partial class FormRunSpecs : Form
    {
        Form1 mainForm;
        public FormRunSpecs()
        {
            InitializeComponent();
        }

        public FormRunSpecs(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            mainForm.SetTime(Double.Parse(this.tbTime.Text));
            mainForm.SetStep(Double.Parse(this.tbStep.Text));
            this.Close();
        }
    }
}
