using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace CannyEdgeDetectionCSharp
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }

        //settings
        public float CannyHighTh;
        public float CannyLowTl;
        public int CannyMaskSize;
        public float CannySigma;
        public int DescLengthBetweenObjects;
        public int DescLengthInsideObject;
        public int DescMinSegmentsCount;
        public double DescCorrelation;
        public int DescPointsCountInsideSegment;
        public string DescPathToDesctiptors;
        public string DescPathToLibrary;
        public int OtherDifferenceBetweenTwoArrays;
        public double DescTailCorrelation=0.7;
        public int DescTailLength=10;

        
        public List<List<double>> Differences;
        public List<Description> DescList;
        private Canny _cannyData;
        private List<Point> _reducingList;
        private OpenFileDialog _ofd;
        private bool _endObj;
        private Point _nextPoint;
        private Point _beginPoint;
        private Bitmap _inputImage;
        public String FileName;
        public int I;
        //public List<List<double[]>> TempArray = new List<List<double[]>>();
        private string _pathBmp;

        public class Point
        {
            public int X;
            public int Y;

            public Point()
            {
                X = 0;
                Y = 0;
            }
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class PointWithLength
        {
            public double Length;
            public Point Point;

            public PointWithLength(Point point, double length)
            {
                Point = point;
                Length = length;
            }

        }

        public class Description
        {
            public Description (string pathToImage, string pathToRegressionDescription, string pathToDifferenceDescription, List<Point> sourceCircuit, List<double> difference)
            {
                PathToImage = pathToImage;
                PathToRegressionDescription = pathToRegressionDescription;
                PathToDifferenceDescription = pathToDifferenceDescription;
                SourceCircuit = sourceCircuit;
                Difference = difference;
                //Corners = corners;
            }

            public string PathToImage { get; set; }
            public string PathToRegressionDescription { get; set; }
            public string PathToDifferenceDescription { get; set; }
            public List<Point> SourceCircuit { get; set; }
            public List<double> Difference { get; set; }
            //public List<Point> Corners { get; set; } 
        }

        private void ClickOpen(object sender, EventArgs e)
        {
            _ofd = new OpenFileDialog
            {
                Filter =
                    @"PNG files (*.png)|*.png|Bitmap files (*.bmp)|*.bmp|TIFF files (*.tif)|*tif|JPEG files (*.jpg)|*.jpg",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (_ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                _inputImage = new Bitmap(_ofd.FileName);
                pictureBox1.Image = _inputImage;
                FileName = Path.GetFileNameWithoutExtension(_ofd.FileName);

            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Step1Canny(object sender, EventArgs e)
        {
                try
                {
                    _cannyData = new Canny(_inputImage, CannyHighTh, CannyLowTl, CannyMaskSize, CannySigma);
                    CannyEdges.Image = _cannyData.DisplayImage(_cannyData.EdgeMap);

                    CheckCreateDirectories();
                    I = 0;
                    new Bitmap(CannyEdges.Image).Save(DescPathToDesctiptors + FileName + "\\circuit_" + I+".bmp");
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show(@"Выберите изображение");
                    return;
                }

            _reducingList = new List<Point>();
                for (var i = 0; i < _cannyData.EdgeMap.GetLength(0); i++)
                    for (var j = 0; j < _cannyData.EdgeMap.GetLength(1); j++)
                    {
                        if (_cannyData.EdgeMap[i, j] > 0)
                            _reducingList.Add(new Point(i, j));
                    }
                //_beginPoint = GetMaxLength(_reducingList, ShapeCenter());
            //var rand = new Random();
            _beginPoint = _reducingList[0];

        }



        private void Step2Circuit(object sender, EventArgs e)
        {
            if (_inputImage == null)
            {
                MessageBox.Show(@"Выберите изображение");
                return;
            }
            if (CannyEdges.Image == null)
            {
                MessageBox.Show(@"Обработайте изображение");
                return;
            }
            if (_reducingList.Count == 0)
            {
                MessageBox.Show(@"Вначале необходимо оконтурить изображениеи");
                return;
            }

            Differences = new List<List<double>>();
            DescList = new List<Description>();

            while (true)
            {
                if (_reducingList.Count > DescPointsCountInsideSegment + 1)
                {
                    _nextPoint = _beginPoint;
                    var obj = GetOneObject();
                    if (obj != null)
                    {
                        DrawApproxBit(obj);
                        DrawOnlyOneObject(obj);
                        var desc = MakeDescriptions(obj);
                        DescList.Add(desc);
                        I++;
                    }
                }
                else
                {
                    MessageBox.Show(@"Сканирование завершено");
                    break;
                }
            }
        }


        private void Step3Identify(object sender, EventArgs e)
        {
            if (_inputImage == null)
            {
                MessageBox.Show(@"Выберите изображение");
                return;
            }
            if (CannyEdges.Image == null)
            {
                MessageBox.Show(@"Обработайте изображение");
                return;
            }
            if (DescList.Count.Equals(0))
            {
                MessageBox.Show(@"Не распознано ниодного объекта");
                return;
            }
            var identify = new identifyForm {Owner = this};
            identify.ShowDialog();

        }



        private void Step4Comparsion(object sender, EventArgs e)
        {
            I = 0;
            var comparsion = new ComparsionForm() { Owner = this };
            comparsion.ShowDialog();
        }

        private void Settings(object sender, EventArgs e)
        {
            var settings = new Settings() { Owner = this };
            settings.ShowDialog();
        }


        private List<List<Point>> GetOneObject()
        {
            var objectArray = new List<List<Point>>();
            var resultArray = new List<Point>();
            while (true)
            {
                var subPointsWithLength = new List<PointWithLength>();
                while (LineCorrelation(resultArray))
                {
                    if (_reducingList.Count < DescPointsCountInsideSegment+1)
                    {
                        if (objectArray.Count < DescMinSegmentsCount)
                        {
                            objectArray.Clear();
                            resultArray.Clear();
                            return null;
                        }
                        //DrawApproxBit(objectsMassive);
                        //I++;
                        return objectArray;
                    }


                    var pointsWithLength = _reducingList.Select(point => new PointWithLength(point, GetLength(_nextPoint, point))).ToList();
                    pointsWithLength.Sort((x,y)=>x.Length.CompareTo(y.Length));
                    _reducingList.RemoveAll(x => x.Equals(pointsWithLength.First().Point));
                    pointsWithLength.RemoveAt(0);

                    subPointsWithLength.AddRange(pointsWithLength.GetRange(0, DescPointsCountInsideSegment));

                    //var min = subPointsWithLength.OrderBy(x => x.Length).First();
                    var min = subPointsWithLength.First();
                    subPointsWithLength.RemoveAll(x=>x.Point.Equals(min.Point));

                    if (min.Length < DescLengthInsideObject)
                    {
                        _nextPoint = min.Point;
                        resultArray.Add(min.Point);
                    }
                    else if (min.Length >= DescLengthInsideObject && min.Length <= DescLengthBetweenObjects)
                    {
                        subPointsWithLength.Clear();
                        _nextPoint = min.Point;
                    }
                    else if (min.Length >= DescLengthBetweenObjects && !_endObj)
                    {
                        subPointsWithLength.Clear();
                        _endObj = true;
                        _nextPoint = _beginPoint;
                    }
                    else if (min.Length >= DescLengthBetweenObjects && _endObj)
                    {
                        subPointsWithLength.Clear();
                        _endObj = false;
                        _beginPoint = min.Point;
                        if (objectArray.Count < DescMinSegmentsCount)
                        {
                            objectArray.Clear();
                            _nextPoint = min.Point;
                            resultArray.Clear();
                            continue;
                        }
                        return new List<List<Point>>(objectArray);
                    }
                }
                if (Correlation(resultArray) >= DescCorrelation)
                {
                    objectArray.Add(new List<Point>(resultArray));
                }
                var nextP = new Point(resultArray[resultArray.Count - 1].X, resultArray[resultArray.Count - 1].Y);
                _nextPoint = nextP;
                resultArray.Clear();
            }
        }

        bool LineCorrelation(List<Point> cloud)
        {
            if (cloud.Count < DescTailLength)
            {
                return true;
            }
            var subAr = cloud.GetRange(cloud.Count - DescTailLength, DescTailLength);
            return !(Correlation(subAr) < DescTailCorrelation);
        }

        private static double GetLength(Point nextP, Point p)
        {
            return Math.Sqrt(Math.Pow(nextP.X - p.X, 2) + Math.Pow(nextP.Y - p.Y, 2));
        }


        private double[] GetRegression(List<Point> cloud)
        {
            double meanXy = 0, meanSqrX = 0;
            var meanX = (double)cloud.Sum(x => x.X)/cloud.Count;
            var meanY = (double)cloud.Sum(x => x.Y)/cloud.Count;
            foreach (var p in cloud)
            {
                meanXy += p.X*p.Y;
                meanSqrX += Math.Pow(p.X, 2);
            }
            meanSqrX /= cloud.Count;
            meanXy /= cloud.Count;

            var a = (meanXy - meanX*meanY)/(meanSqrX - Math.Pow(meanX, 2));
            var b = meanY - a*meanX;

            if (Double.IsNaN(a))
            {
                a = Double.PositiveInfinity;
            }
            if (Double.IsNaN(b))
            {
                b = Double.PositiveInfinity;
            }
            return new[] {a, b};
        }


        private double Correlation(List<Point> cloud)
        {
            double meanX = 0, meanY = 0, upCor = 0, downCor1 = 0, downCor2 = 0;
            foreach (var point in cloud)
            {
                meanX += point.X;
                meanY += point.Y;
            }

            meanX /= DescPointsCountInsideSegment;
            meanY /= DescPointsCountInsideSegment;

            foreach (var point in cloud)
            {
                upCor += (point.X - meanX) * (point.Y - meanY);
                downCor1 += Math.Pow(point.X - meanX, 2);
                downCor2 += Math.Pow(point.Y - meanY, 2);
            }

            return upCor.Equals(0) ? 1 : Math.Abs(upCor/Math.Sqrt(downCor1*downCor2));
        }


        public Point GetMinPoint(IReadOnlyList<Point> cloud)
        {
            return new Point(cloud.Min(x => x.X), cloud.Min(x => x.Y));
        }

        private static Point GetMaxPoint(List<Point> cloud)
        {
            return new Point(cloud.Max(x => x.X), cloud.Max(x => x.Y));
        }


        private void DrawApproxBit(List<List<Point>> obj)
        {
            var approxBit = new Bitmap(_inputImage);
            foreach (var cloud in obj)
            {
                if (Correlation(cloud) >= DescCorrelation)
                {
                    var minP = GetMinPoint(cloud);
                    var maxP = GetMaxPoint(cloud);

                    var regression = GetRegression(cloud);
                    var a = regression[0];
                    var b = regression[1];

                    if (Math.Abs(a) <= 1)
                    {
                        for (var i = minP.X; i < maxP.X; i++)
                        {
                            var number = Convert.ToInt32(Math.Round(i*a + b));
                            if (number >= _inputImage.Height)
                            {
                                number = _inputImage.Height - 1;
                            }
                            approxBit.SetPixel(i, number, Color.Red);
                        }
                    }
                    else if (Math.Abs(a) > 1 && Math.Abs(a) < 10)
                    {
                        for (var i = minP.Y; i <= maxP.Y; i++)
                        {
                            var number = Convert.ToInt32(-b/a + i/a);
                            if (number >= _inputImage.Width)
                            {
                                number = _inputImage.Width - 1;
                            }
                            approxBit.SetPixel(number, i, Color.Red);
                        }
                    }
                    else
                    {
                        for (var i = minP.Y; i < maxP.Y; i++)
                        {
                            approxBit.SetPixel(minP.X, i, Color.Red);
                        }
                    }
                }
            }
            _pathBmp = DescPathToDesctiptors + FileName + "\\" + I + "_b.bmp";
            if (File.Exists(_pathBmp))
                File.Delete(_pathBmp);
            approxBit.Save(_pathBmp);
        }


        private Description MakeDescriptions(List<List<Point>> obj)
        {
            var mainDescPath = DescPathToDesctiptors + FileName + "\\" + I + ".txt";
            var mainDescFile = new StreamWriter(mainDescPath);
            foreach (var cloud in obj)
            {
                var regression = GetRegression(cloud);
                mainDescFile.WriteLine(@"корреляция=" + Correlation(cloud) + " a=" + Math.Atan(regression[0])*57.3 + " b=" + regression[1]+" "+cloud.Count);

            }
            mainDescFile.Close();

            var differenceDescPath = DescPathToDesctiptors + FileName + "\\" + I + "_d.txt";
            var differenceDescFile = new StreamWriter(differenceDescPath);
            Differences.Add(new List<double>());
            for (var i = 1; i < obj.Count; i++)
            {
                var regressionNext = GetRegression(obj[i])[0];
                var regressionPrev = GetRegression(obj[i - 1])[0];
                var difference = Math.Atan(regressionNext) * 57.3 - Math.Atan(regressionPrev) * 57.3;
                difference = Math.Round(difference);
                differenceDescFile.WriteLine(difference);
                Differences[Differences.Count - 1].Add(difference);
            }
            differenceDescFile.Close();

            var desc = new Description(_pathBmp,
                differenceDescPath,
                mainDescPath, GetSimpleList(obj), Differences[Differences.Count-1]);
            return desc;
        }


        private void CheckCreateDirectories()
        {
            if (Directory.Exists(DescPathToDesctiptors + FileName))
            {
                MessageBox.Show(
                    @"Папка с таким названием уже существует. Прекратите выполнение программы и измените название входного файла, иначе выходные файлы будут перезаписаны.");
            }
            else
                Directory.CreateDirectory(DescPathToDesctiptors + FileName);
        }



        public List<Point> GetSimpleList(List<List<Point>> sourceList)
        {
            return sourceList.SelectMany(ar => ar).ToList();
        }


        public Bitmap GetBitmap(List<Point> cloud)
        {
            var minP = new Point(cloud.Min(x => x.X), cloud.Min(x => x.Y));
            var maxP = new Point(cloud.Max(x => x.X), cloud.Max(x => x.Y));

            return new Bitmap(maxP.X-minP.X+1, maxP.Y-minP.Y+1);
        }

        public void DrawOnlyOneObject(List<List<Point>> objs)
        {
            var currObject = GetSimpleList(objs);
            var bit = GetBitmap(currObject);
            var minP = GetMinPoint(currObject);
            foreach (var point in currObject)
            {
                bit.SetPixel(point.X - minP.X,point.Y - minP.Y, Color.Black);
            }
            bit.Save(DescPathToDesctiptors + FileName + "\\" + I + "_o.bmp");
        }

        public Bitmap GetOnlyOneBitmap(List<Point> obj)
        {
            var bit = GetBitmap(obj);
            var minP = GetMinPoint(obj);
            foreach (var point in obj)
            {
                bit.SetPixel(point.X - minP.X, point.Y - minP.Y, Color.Black);
            }
            return bit;
        }

        private void ReadSettings(object sender, EventArgs e)
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
            //const string bOtherDifferenceBetweenTwoArrays = "OtherDifferenceBetweenTwoArrays";

            using (var sr = new StreamReader("Settings.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    if (line.Contains(bCannyHighTh))
                    {
                        CannyHighTh = Single.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                    else if (line.Contains(bCannyLowTL))
                    {
                        CannyLowTl = Single.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                    else if (line.Contains(bCannyMaskSize))
                    {
                        CannyMaskSize = Int32.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                    else if (line.Contains(bCannySigma))
                    {
                        CannySigma = Single.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                    else if (line.Contains(bDescLengthBetweenObjects))
                    {
                        DescLengthBetweenObjects = Int32.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                    else if (line.Contains(bDescLengthInsideObject))
                    {
                        DescLengthInsideObject = Int32.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                    else if (line.Contains(bDescPointsCountInsideSegment))
                    {
                        DescPointsCountInsideSegment = Int32.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                    else if (line.Contains(bDescMinSegmentsCount))
                    {
                        DescMinSegmentsCount = Int32.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                    else if (line.Contains(bDescCorrelation))
                    {
                        DescCorrelation = Double.Parse(line.Substring(line.IndexOf(' ') + 1));
                    }
                    else if (line.Contains(bDescPathToDesctiptors))
                    {
                        DescPathToDesctiptors = line.Substring(line.IndexOf(' ') + 1);
                    }
                    else if (line.Contains(bDescPathToLibrary))
                    {
                        DescPathToLibrary = line.Substring(line.IndexOf(' ') + 1);
                    }
                    else
                    {
                        OtherDifferenceBetweenTwoArrays = Int32.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                }
            }
        }




}
}