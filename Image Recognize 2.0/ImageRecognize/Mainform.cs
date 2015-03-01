using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Xml.Schema;

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
        public float CannyLowTL;
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

        
        public List<List<double>> differences;
        public List<Description> descList;
        private Canny _cannyData;
        private List<double[]> _reducingList = new List<double[]>();
        private OpenFileDialog _ofd;
        private bool _endObj;
        private double[] _nextPoint = new double[3];
        private double[] _beginPoint = new double[2];
        private Bitmap _inputImage;
        public String FileName;
        private int I;
        public List<List<double[]>> TempArray = new List<List<double[]>>();
        private string _pathBmp;
        public List<double[]> resultArray;


        public struct Description
        {
            public Description (string pathToImage, string pathToRegressionDescription, string pathToDifferenceDescription, List<double[]> sourceCircuit, List<double> difference) : this()
            {
                PathToImage = pathToImage;
                PathToRegressionDescription = pathToRegressionDescription;
                PathToDifferenceDescription = pathToDifferenceDescription;
                SourceCircuit = sourceCircuit;
                Difference = difference;
            }

            public string PathToImage { get; set; }
            public string PathToRegressionDescription { get; set; }
            public string PathToDifferenceDescription { get; set; }
            public List<double[]> SourceCircuit { get; set; }
            public List<double> Difference { get; set; }
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

        private string[] readFolder()
        {
            return Directory.GetFiles(@"C:\\ellipse");
        }

        private void Step1Canny(object sender, EventArgs e)
        {

            var fold = readFolder();
            foreach (var file in fold)
            {
                _inputImage = new Bitmap(file);
                try
                {
                    _cannyData = new Canny(_inputImage, CannyHighTh, CannyLowTL, CannyMaskSize, CannySigma);
                    CannyEdges.Image = _cannyData.DisplayImage(_cannyData.EdgeMap);

                    CheckCreateDirectories();
                    new Bitmap(CannyEdges.Image).Save(@"C:\2\ellipse\" + I + "_" + file.Substring(19));
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show(@"Выберите изображение");
                    return;
                }
                for (var i = 0; i < _cannyData.EdgeMap.GetLength(0); i++)
                    for (var j = 0; j < _cannyData.EdgeMap.GetLength(1); j++)
                    {
                        if (_cannyData.EdgeMap[i, j] > 0)
                            _reducingList.Add(new[] {i, (double) j});
                    }
                //_beginPoint = GetMaxLength(_reducingList, ShapeCenter());
                var rand = new Random();
                _beginPoint = _reducingList[rand.Next(0, _reducingList.Count)];
                Step2Circuit(sender, e);
            }
        }



        private void Step2Circuit(object sender, EventArgs e)
        {
            differences = new List<List<double>>();

            descList = new List<Description>();

            if (_reducingList.Count == 0)
            {
                MessageBox.Show(@"Вначале необходимо оконтурить изображениеи");
                return;
            }

            try
            {
                while (true)
                {
                    if (_reducingList.Count > DescPointsCountInsideSegment + 1)
                    {
                        resultArray = new List<double[]>();
                        _nextPoint = _beginPoint;
                        var obj = GetOneObject();
                        if (obj != null)
                        {
                            DrawApproxBit(obj);
                            DrawOnlyOneObject(obj);
                            var desc = MakeDescriptions(obj);
                            descList.Add(desc);
                            I++;
                        }
                    }
                    else
                    {
                        //MessageBox.Show(@"Сканирование завершено");
                        break;
                    }
                }
            }
            catch (NullReferenceException)
            {
                if (_inputImage == null)
                {
                    MessageBox.Show(@"Выберите изображение");
                    return;
                }
                if (CannyEdges.Image == null)
                {
                    MessageBox.Show(@"Обработайте изображение");
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
            var identify = new identifyForm {Owner = this};
            identify.ShowDialog();

        }



        private void Step4Comparsion(object sender, EventArgs e)
        {
            var comparsion = new ComparsionForm() { Owner = this };
            comparsion.ShowDialog();
        }

        private void Settings(object sender, EventArgs e)
        {
            var settings = new Settings() { Owner = this };
            settings.ShowDialog();
        }


        private List<List<double[]>> GetOneObject()
        {
            var objectsMassive = new List<List<double[]>>();
            while (true)
            {
                while (resultArray.Count < DescPointsCountInsideSegment)
                {
                    if (_reducingList.Count < DescPointsCountInsideSegment + 1)
                    {
                        if (objectsMassive.Count <= DescMinSegmentsCount)
                        {
                            objectsMassive.Clear();
                            resultArray.Clear();
                            return null;
                        }
                        //DrawApproxBit(objectsMassive);
                        //I++;
                        return objectsMassive;
                    }
                    var pointsLength = new List<double[]>();
                    foreach (var elem in _reducingList)
                    {
                        pointsLength.Add(new[]
                        {
                            elem[0], elem[1], get_length(_nextPoint[0], elem[0], _nextPoint[1], elem[1])
                        });
                    }
                   

                    pointsLength.Sort((x, y) => x[2].CompareTo(y[2]));
                    FuckingEquals(new[] {pointsLength[0][0], pointsLength[0][1]});


                    TempArray.Add(new List<double[]>());
                    for (var j = 1; j < DescPointsCountInsideSegment + 1; j++)
                    {
                        TempArray[TempArray.Count - 1].Add(pointsLength[j]);
                    }

                    var min = new[] {0, 0, Double.PositiveInfinity};
                    foreach (var minArray in TempArray)
                    {
                        foreach (var elem in minArray)
                        {
                            if (elem[2] < min[2])
                            {
                                min = elem;
                            }
                        }
                    }
                    FuckingEquals(new[] {min[0], min[1]}, TempArray);

                    if (min[2] < DescLengthInsideObject)
                    {
                        _nextPoint = min;
                        resultArray.Add(min);
                    }
                    else if (min[2] >= DescLengthInsideObject && min[2] <= DescLengthBetweenObjects)
                    {
                        TempArray.Clear();
                        _nextPoint = min;
                    }
                    else if (min[2] >= DescLengthBetweenObjects && !_endObj)
                    {
                        TempArray.Clear();
                        _endObj = true;
                        _nextPoint = _beginPoint;
                    }
                    else if (min[2] >= DescLengthBetweenObjects && _endObj)
                    {
                        TempArray.Clear();
                        _endObj = false;
                        _beginPoint = min;
                        if (objectsMassive.Count <= DescMinSegmentsCount)
                        {
                            objectsMassive.Clear();
                            _nextPoint = min;
                            resultArray.Clear();
                            continue;
                        }
                        return new List<List<double[]>>(objectsMassive);
                    }
                }
                TempArray.Clear();
                if (Correlation(resultArray) >= DescCorrelation)
                {
                    objectsMassive.Add(new List<double[]>());
                    foreach (var t in resultArray)
                    {
                        objectsMassive[objectsMassive.Count - 1].Add(t);
                    }
                }
                _nextPoint = new[] {resultArray[resultArray.Count - 1][0], resultArray[resultArray.Count - 1][1]};
                resultArray.Clear();
            }
        }


        private double[] ShapeCenter()
        {
            try
            {
                double sumx = 0, sumy = 0;
                foreach (var t in _reducingList)
                {
                    sumx += t[0];
                    sumy += t[1];
                }
                return new[] {sumx/_reducingList.Count, sumy/_reducingList.Count};
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(@"массив точек пуст");
                return null;
            }
        }



        private static double get_length(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Abs(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }



        private static double[] GetMaxLength(IReadOnlyList<double[]> cloud, IList<double> point)
        {
            var max = new double[] {0, 0, 0};
            for (var i = 1; i < cloud.Count; i++)
            {
                if (get_length(cloud[i][0], point[0], cloud[i][1], point[1]) > max[2])
                {
                    max[0] = cloud[i][0];
                    max[1] = cloud[i][1];
                    max[2] = get_length(cloud[i][0], point[0], cloud[i][1], point[1]);
                }
            }
            return new[] {max[0], max[1]};
        }

        private void FuckingEquals(double[] point)
        {
            for (var i = 0; i < _reducingList.Count; i++)
            {
                if (point.SequenceEqual(_reducingList[i]))
                {
                    _reducingList.Remove(_reducingList[i]);
                }
            }
        }



        private static void FuckingEquals(IList<double> point, IEnumerable<List<double[]>> cloud)
        {
            foreach (var t in cloud)
                for (var j = 0; j < t.Count; j++)
                {
                    if (point[0] == t[j][0] && point[1] == t[j][1])
                    {
                        t.Remove(t[j]);
                    }
                }
        }

        private double[] get_regression(IReadOnlyCollection<double[]> cloud)
        {
            double meanXy = 0, meanX = 0, meanY = 0, meanSqrX = 0;
            foreach (double[] t in cloud)
            {
                meanXy += t[0]*t[1];
                meanX += t[0];
                meanY += t[1];
                meanSqrX += Math.Pow(t[0], 2);
            }
            meanSqrX /= cloud.Count;
            meanX /= cloud.Count;
            meanY /= cloud.Count;
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


        private double Correlation(IReadOnlyList<double[]> cloud)
        {
            double meanX = 0, meanY = 0, upCor = 0, downCor1 = 0, downCor2 = 0;
            for (var i = 0; i < cloud.Count; i++)
            {
                meanX += cloud[i][0];
                meanY += cloud[i][1];
            }
            meanX /= DescPointsCountInsideSegment;
            meanY /= DescPointsCountInsideSegment;

            for (var i = 0; i < cloud.Count; i++)
            {
                upCor += (cloud[i][0] - meanX)*(cloud[i][1] - meanY);
                downCor1 += Math.Pow(cloud[i][0] - meanX, 2);
                downCor2 += Math.Pow(cloud[i][1] - meanY, 2);
            }

            return upCor == 0 ? 1 : Math.Abs(upCor/Math.Sqrt(downCor1*downCor2));
        }


        private static List<int[]> GetMinMax(IReadOnlyList<double[]> cloud)
        {
            double minx = cloud[0][0], miny = cloud[0][1], maxx = cloud[0][0], maxy = cloud[0][1];
            var result = new List<int[]>();
            foreach (double[] t in cloud)
            {
                if (minx > t[0])
                    minx = t[0];
                else if (maxx < t[0])
                    maxx = t[0];

                if (miny > t[1])
                    miny = t[1];
                else if (maxy < t[1])
                    maxy = t[1];
            }
            result.Add(new[] {(int) minx, (int) miny});
            result.Add(new[] {(int) maxx, (int) maxy});

            return result;
        }


        private void DrawApproxBit(List<List<double[]>> obj)
        {
            var _approxBit = new Bitmap(_inputImage);
            foreach (var cloud in obj)
            {
                if (Correlation(cloud) >= DescCorrelation)
                {
                    var xmin = GetMinMax(cloud)[0][0];
                    var xmax = GetMinMax(cloud)[1][0];
                    var ymin = GetMinMax(cloud)[0][1];
                    var ymax = GetMinMax(cloud)[1][1];

                    var a = get_regression(cloud)[0];
                    var b = get_regression(cloud)[1];

                    if (Math.Abs(a) <= 1)
                    {
                        for (var i = xmin; i < xmax; i++)
                        {
                            var number = Convert.ToInt32(i*a + b);
                            if (number >= _inputImage.Height)
                            {
                                number = _inputImage.Height - 1;
                            }
                            _approxBit.SetPixel(i, number, Color.Red);
                        }
                    }
                    else if (Math.Abs(a) > 1 && Math.Abs(a) < 10)
                    {
                        for (int i = ymin; i <= ymax; i++)
                        {
                            int number = Convert.ToInt32(-b/a + i/a);
                            if (number >= _inputImage.Width)
                            {
                                number = _inputImage.Width - 1;
                            }
                            _approxBit.SetPixel(number, i, Color.Red);
                        }
                    }
                    else
                    {
                        for (int i = ymin; i < ymax; i++)
                        {
                            _approxBit.SetPixel(xmin, i, Color.Red);
                        }
                    }
                }
            }
            _pathBmp = @"C:\2\ellipse\" + I + "_a.bmp";
            _approxBit.Save(_pathBmp);
        }


        private Description MakeDescriptions(List<List<double[]>> obj)
        {
            var mainDescPath = @"C:\2\ellipse\" + I + "_c.txt";
            var mainDescFile = new StreamWriter(mainDescPath);
            foreach (var cloud in obj)
            {
                var regression = get_regression(cloud);
                mainDescFile.WriteLine(@"корреляция=" + Correlation(cloud) + " a=" + Math.Atan(regression[0])*57.3 + " b=" + regression[1]);

            }
            mainDescFile.Close();

            var differenceDescPath = @"C:\2\ellipse\" + I + "_d.txt";
            var differenceDescFile = new StreamWriter(differenceDescPath);
            differences.Add(new List<double>());
            for (var i = 1; i < obj.Count; i++)
            {
                var regressionNext = get_regression(obj[i]);
                var regressionPrev = get_regression(obj[i - 1]);
                differenceDescFile.WriteLine((Math.Atan(regressionNext[0]) - Math.Atan(regressionPrev[0]))*57.3);
                differences[differences.Count - 1].Add(
                    Math.Atan(regressionNext[0]) - Math.Atan(regressionPrev[0]));
            }
            differenceDescFile.Close();

            var desc = new Description(_pathBmp,
                differenceDescPath,
                mainDescPath, getSimpleList(obj), differences[differences.Count-1]);
            return desc;
        }


        private void CheckCreateDirectories()
        {
            if (Directory.Exists(DescPathToDesctiptors + FileName))
            {
                //MessageBox.Show(
                //    @"Папка с таким названием уже существует. Прекратите выполнение программы и измените название входного файла, иначе выходные файлы будут перезаписаны.");
            }
            else
                Directory.CreateDirectory(DescPathToDesctiptors + FileName);
        }



        public List<double[]> getSimpleList(List<List<double[]>> sourceList)
        {
            return sourceList.SelectMany(ar => ar).ToList();
        }


        public List<int[]> MinAndMaxes(List<double[]> cloud)
        {

            double minx = double.PositiveInfinity, miny = double.PositiveInfinity, maxx = double.NegativeInfinity, maxy = double.NegativeInfinity;
            foreach (var point in cloud)
            {
                if (minx > point[0])
                    minx = point[0];
                if (maxx < point[0])
                    maxx = point[0];

                if (miny > point[1])
                    miny = point[1];
                if (maxy < point[1])
                    maxy = point[1];
            }

            return new List<int[]> { new[] { (int)minx, (int)miny }, new[] { (int)maxx, (int)maxy } };
        }

        public void DrawOnlyOneObject(List<List<double[]>> obj)
        {
            var currObject = getSimpleList(obj);
            var a = MinAndMaxes(currObject);
            Bitmap bit = new Bitmap(a[1][0] - a[0][0] + 2, a[1][1] - a[0][1] + 2);
            foreach (var point in currObject)
            {
                bit.SetPixel((int)point[0] - a[0][0], (int)point[1] - a[0][1], Color.Black);
            }
            bit.Save(DescPathToDesctiptors + FileName + "\\" + I + "_o.bmp");
        }

        public Bitmap getOnlyOneBitmap(List<double[]> obj)
        {
            var a = MinAndMaxes(obj);
            Bitmap bit = new Bitmap(a[1][0] - a[0][0] + 2, a[1][1] - a[0][1] + 2);
            foreach (var point in obj)
            {
                bit.SetPixel((int)point[0] - a[0][0], (int)point[1] - a[0][1], Color.Black);
            }
            return bit;
        }

        public void ReadSettings(object sender, EventArgs e)
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
                        CannyHighTh = Single.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                    else if (line.Contains(bCannyLowTL))
                    {
                        CannyLowTL = Single.Parse(line.Substring(line.IndexOf(' ')+1));
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
                    else if (line.Contains(bOtherDifferenceBetweenTwoArrays))
                    {
                        OtherDifferenceBetweenTwoArrays = Int32.Parse(line.Substring(line.IndexOf(' ')+1));
                    }
                    else MessageBox.Show("error" + line);
                }
            }
        }




}
}