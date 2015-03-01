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

        private static Mainform Main;

        private void button1_Click(object sender, EventArgs e)
        {
            Main = Owner as Mainform;
            if (File.Exists("Settings.txt"))
            {
                File.Delete("Settings.txt");
            }

            using (var file = new StreamWriter(@"Settings.txt"))
            {
                file.WriteLine("CannyHighTh "+TH.Text);
                Main.CannyHighTh = Single.Parse(TH.Text);
                file.WriteLine("CannyLowTL "+TL.Text);
                Main.CannyLowTL = Single.Parse(TL.Text);
                file.WriteLine("CannyMaskSize " + MaskSize.Text);
                Main.CannyMaskSize = Int32.Parse(MaskSize.Text);
                file.WriteLine("CannySigma " + Sigma.Text);
                Main.CannySigma = Single.Parse(Sigma.Text);
                file.WriteLine("DescLengthBetweenObjects " + LengthBetweenObjects.Text);
                Main.DescLengthBetweenObjects = Int32.Parse(LengthBetweenObjects.Text);
                file.WriteLine("DescLengthInsideObject " + LengthInsideObject.Text);
                Main.DescLengthInsideObject = Int32.Parse(LengthInsideObject.Text);
                file.WriteLine("DescPointsCountInsideSegment " + PointsCountInsideSegment.Text);
                Main.DescPointsCountInsideSegment = Int32.Parse(PointsCountInsideSegment.Text);
                file.WriteLine("DescMinSegmentsCount " + MinSegmentsCount.Text);
                Main.DescMinSegmentsCount = Int32.Parse(MinSegmentsCount.Text);
                file.WriteLine("DescCorrelation " + Correlation.Text);
                Main.DescCorrelation = Double.Parse(Correlation.Text);
                file.WriteLine("DescPathToDesctiptors " + PathToDesctiptors.Text);
                Main.DescPathToDesctiptors = PathToDesctiptors.Text;
                file.WriteLine("DescPathToLibrary " + PathToLibrary.Text);
                Main.DescPathToLibrary = PathToLibrary.Text;
                file.WriteLine("OtherDifferenceBetweenTwoArrays" + OtherDifferenceBetweenTwoArrays.Text);
                Main.OtherDifferenceBetweenTwoArrays = Int32.Parse(OtherDifferenceBetweenTwoArrays.Text);
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
