using System;
using System.Windows.Forms;

namespace VJoyTestFeeder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            var service = new VJoyServiceReference.ServiceClient();
            MessageBox.Show(service.GetData(123), "Service Response");
            service.Close();
        }
    }
}
