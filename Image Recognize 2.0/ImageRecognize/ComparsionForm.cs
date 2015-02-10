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

        private Mainform main;
        private int I;

        public List<string> getAllDifferenceFilesFromLibrary(string pathToLibrary)
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


        public List<string> getALlImageFilesFromLibrary(string pathToLibrary)
        {
            var filesList = new List<string>();
            var dirs = Directory.GetDirectories(pathToLibrary);
            foreach (var dir in dirs)
            {
                var fileAddress = Directory.GetFiles(dir, "*_o.bmp");
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

        public void EqualWithSimmetry(List<double> currentObject)
        {
            var sourceObjectAr = new List<double[]>();
            var fileList = getAllDifferenceFilesFromLibrary(main.DescPathToLibrary);

                for(var i = 0; i<fileList.Count; i++)
                {
                    var libAr = GetOneDescFromLibrary(fileList[i]);
                    sourceObjectAr.Add(new[] { equalTwoArrays(libAr, currentObject), i });
                }

            sourceObjectAr.Sort((a, b) => a[0].CompareTo(b[0]));
            var imagesAr = getImagesFromEqualFunc(sourceObjectAr);
            SetAndDrawPictureboxes(imagesAr);
        }

        public string[] getImagesFromEqualFunc(List<double[]> objectAr)
        {
            var first = (int)objectAr[0][1];
            var second = (int)objectAr[1][1];
            var third = (int)objectAr[2][1];
            var imagesAr = getALlImageFilesFromLibrary(main.DescPathToLibrary);
            return new[] {imagesAr[first], imagesAr[second], imagesAr[third]};
        }

        private double equalTwoArrays(List<double> libAr, List<double> objAr)
        {
            var Sum1 = new List<double>();
            for (var i = 2; i < objAr.Count; i++)
            {
                if (libAr.Count > 2)
                {
                    int address = 0;
                    var n = Double.PositiveInfinity;
                    for (var j = 2; j < libAr.Count; j++)
                    {
                        var dif =
                            Math.Abs(
                                Math.Round(
                                    (libAr[j - 2] - objAr[i - 2]) + (libAr[j - 1] - objAr[i - 1]) +
                                    (libAr[j] - objAr[i]), 3));
                        if (dif < n)
                        {
                            n = dif;
                            address = j;
                        }
                    }
                    libAr.Remove(libAr[address - 2]);
                    Sum1.Add(n);
                }
            }
            return Sum1.Sum();
        }

        private void SetAndDrawPictureboxes(string[] imagesAr)
        {

            var bit = new Bitmap(imagesAr[0]);
            pictureBox2.Image = bit;
            bit = new Bitmap(imagesAr[1]);
            pictureBox3.Image = bit;
            bit = new Bitmap(imagesAr[2]);
            pictureBox4.Image = bit;
        }

        private void ComparsionForm_Load(object sender, EventArgs e)
        {
            main = Owner as Mainform;
            I = 0;
            EqualWithSimmetry(main.descList[I].Difference);
            pictureBox5.Image = main.getOnlyOneBitmap(main.descList[I].SourceCircuit);
        }
    }
}
