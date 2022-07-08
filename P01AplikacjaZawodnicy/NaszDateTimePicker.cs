using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P01AplikacjaZawodnicy
{
    public partial class NaszDateTimePicker : UserControl
    {
        public DateTime? Value
        {
            get
            {
                if (cbAktrywny.Checked)
                    return dtpData.Value;
                return null;
            }
            set
            {
                if (value == null)
                {
                    cbAktrywny.Checked = false;
                    dtpData.Enabled = false;
                }
                else
                {
                    dtpData.Value = (DateTime)value;
                }
                
            }
        }


        public NaszDateTimePicker()
        {
            InitializeComponent();
        }

        private void cbAktrywny_CheckedChanged(object sender, EventArgs e)
        {
            dtpData.Enabled = cbAktrywny.Checked;
        }
    }
}
