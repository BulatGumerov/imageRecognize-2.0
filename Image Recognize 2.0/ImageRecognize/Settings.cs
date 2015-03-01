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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("Settings.txt"))
            {
                File.Delete("Settings.txt");
            }

            using (var file = new StreamWriter(@"Settings.txt"))
            {
                file.WriteLine("CannyHighTh "+TH.Text);
                file.WriteLine("CannyLowTL "+TL.Text);
                file.WriteLine("CannyMaskSize " + MaskSize.Text);
                file.WriteLine("CannySigma " + Sigma.Text);
                file.WriteLine("DescLengthBetweenObjects " + LengthBetweenObjects.Text);
                file.WriteLine("DescLengthInsideObject " + LengthInsideObject.Text);
                file.WriteLine("DescPointsCountInsideSegment " + PointsCountInsideSegment.Text);
                file.WriteLine("DescMinSegmentsCount " + MinSegmentsCount.Text);
                file.WriteLine("DescCorrelation " + Correlation.Text);
                file.WriteLine("DescPathToDesctiptors " + PathToDesctiptors.Text);
                file.WriteLine("DescPathToLibrary " + PathToLibrary.Text);
                file.WriteLine("OtherDifferenceBetweenTwoArrays" + OtherDifferenceBetweenTwoArrays.Text);
                file.Close();
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            const string bCannyHighTh = "CannyHighTh";
            const string bCannyLowTL = "CannyLowTL";
            const string bCannyMaskSize = "CannyMaskSize";
            const string bCannySigma = "CannySigma";
            const string bDescLengthBetweenObjects = "DescLengthBetweenObjects";
            const string bDescLengthInsideObject = "DescLengthInsideObject";
            const string bDescPointsCountInsideSegment = "DescPointsCountInsideSegment";
            const string bDescMinSegmentsCount = "DescMinSegmentsCount";
            const string bDescCorrelation = "DescCorrelation";
            const string bDescPathToDesctiptors = "DescPathToDesctiptors";
            const string bDescPathToLibrary = "DescPathToLibrary";
            const string bOtherDifferenceBetweenTwoArrays = "OtherDifferenceBetweenTwoArrays";

            using (var sr = new StreamReader("Settings.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    if (line.Contains(bCannyHighTh))
                    {
                        TH.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bCannyLowTL))
                    {
                        TL.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bCannyMaskSize))
                    {
                        MaskSize.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bCannySigma))
                    {
                        Sigma.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bDescLengthBetweenObjects))
                    {
                        LengthBetweenObjects.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bDescLengthInsideObject))
                    {
                        LengthInsideObject.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bDescPointsCountInsideSegment))
                    {
                        PointsCountInsideSegment.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bDescMinSegmentsCount))
                    {
                        MinSegmentsCount.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bDescCorrelation))
                    {
                        Correlation.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bDescPathToDesctiptors))
                    {
                        PathToDesctiptors.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bDescPathToLibrary))
                    {
                        PathToLibrary.Text = line.Substring(line.IndexOf(' '));
                    }
                    else if (line.Contains(bOtherDifferenceBetweenTwoArrays))
                    {
                        OtherDifferenceBetweenTwoArrays.Text = line.Substring(line.IndexOf(' '));
                    }
                    else MessageBox.Show("error" + line);
                }
            }
        }
    }
}
