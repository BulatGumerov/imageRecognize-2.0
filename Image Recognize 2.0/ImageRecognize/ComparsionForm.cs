using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public Mainform main;

        public List<string> getAllFilesFromLibrary(string pathToLibrary)
        {
            var filesList = new List<string>();
            var dirs = Directory.GetDirectories(pathToLibrary);
            foreach (var dir in dirs)
            {
                var fileAddress = Directory.GetFiles(dir, "*_d.txt");
                foreach (var filesPath in fileAddress)
                {
                    filesList.Add(filesPath);
                }
            }
            return filesList;
        }

        public List<double> GetOneDescFromLibrary(string path)
        {
            var libAr = new List<double>();
            string dbls;
            var file = new StreamReader(path);
            while ((dbls = file.ReadLine()) != null)
            {
                libAr.Add(Convert.ToDouble(dbls));
            }
            return libAr;
        }

        public void EqualWithSimmetry()
        {
            var ObjectAr = new List<double>();
            foreach (var obj in main.differences)
            {
                foreach (var objects in getAllFilesFromLibrary(main.DescPathToLibrary))
                {
                    var libAr = GetOneDescFromLibrary(objects);
                    equalTwoArraysWithSimmetry(obj, libAr);
                }
            }
        }

        private double[] equalTwoArraysWithSimmetry(List<double> libAr, List<double> objAr)
        {
            var n = Double.PositiveInfinity;
            var Sum1 = new List<double>();
            var Sum2 = new List<double>();
            for (var i = 2; i < objAr.Count; i++)
            {
                for (var j = 2; j < libAr.Count; j++)
                {
                    n = Double.PositiveInfinity;
                    var dif = Math.Abs(Math.Round((libAr[j - 2] - objAr[i - 2]) + (libAr[j - 1] - objAr[i - 1]) + (libAr[j] - objAr[i]), 3));
                    if (dif < n)
                        n = dif;
                }
                Sum1.Add(n);
            }
            n = Double.PositiveInfinity;
            for (var i = 2; i < libAr.Count; i++)
            {
                for (var j = 2; j < objAr.Count; j++)
                {
                    n = Double.PositiveInfinity;
                    var dif = Math.Abs(Math.Round((libAr[i - 2] - objAr[j - 2]) + (libAr[i - 1] - objAr[j - 1]) + (libAr[i] - objAr[j]), 3));
                    if (dif < n)
                        n = dif;
                }
                Sum2.Add(n);
            }
            return new []{Sum1.Sum(), Sum2.Sum()};
        }

        private
            void ComparsionForm_Load(object sender, EventArgs e)
        {
            main = Owner as Mainform;
            EqualWithSimmetry();
            //pictureBox1.Image = new Bitmap(main.comparsionImg);
            //pictureBox2.Image = new Bitmap(main.pathForComparsion);
            //textBox1.Text = main.pathForComparsionTxt.ToString();
            //pictureBox3.Image = new Bitmap(main.pathForComparsion2);
            //textBox2.Text = main.pathForComparsionTxt2.ToString();
            //pictureBox4.Image = new Bitmap(main.pathForComparsion3);
            //textBox3.Text = main.pathForComparsionTxt3.ToString();
            //pictureBox5.Image = main.bitForComparsion;

        }
    }
}
