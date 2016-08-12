using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InformationRetrieval
{
    public partial class ShowRetrievalResult : Form
    {
        public ShowRetrievalResult(string rm, double rc, double pr, double iap, double niap)
        {
            InitializeComponent();
            textBoxRetrievalMethod.Text = rm;
            textBoxRecall.Text = rc.ToString();
            textBoxPrecision.Text = pr.ToString();
            textBoxIAP.Text = iap.ToString();
            textBoxNIAP.Text = niap.ToString();
        }
    }
}
