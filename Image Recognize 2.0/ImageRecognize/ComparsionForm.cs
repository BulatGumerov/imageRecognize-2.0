using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CannyEdgeDetectionCSharp
{
    public partial class ComparsionForm : Form
    {
        public ComparsionForm()
        {
            InitializeComponent();
        }



        private void ComparsionForm_Load(object sender, EventArgs e)
        {
            var main = Owner as Mainform;
            pictureBox1.Image = new Bitmap(main.comparsionImg);
            pictureBox2.Image=new Bitmap(main.pathForComparsion);
            textBox1.Text = main.pathForComparsionTxt.ToString();
            pictureBox3.Image = new Bitmap(main.pathForComparsion2);
            textBox2.Text = main.pathForComparsionTxt2.ToString();
            pictureBox4.Image = new Bitmap(main.pathForComparsion3);
            textBox3.Text = main.pathForComparsionTxt3.ToString();
            pictureBox5.Image = main.bitForComparsion;

        }
    }
}
