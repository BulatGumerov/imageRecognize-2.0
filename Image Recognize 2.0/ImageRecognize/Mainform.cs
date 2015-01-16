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


        private float CannyHighTh;
        private float CannyLowTL;
        private int CannyMaskSize;
        private float CannySigma;

        private int DescLengthBetweenObjects;
        private int DescLengthInsideObject;
        private int DescMinSegmentsCount;
        private double DescCorrelation;
        private int DescPointsCountInsideSegment;
        public string DescPathToDesctiptors;
        public string DescPathToLibrary;

        public List<List<double>> differences = new List<List<double>>();


        private Canny _cannyData;
        private List<double[]> _reducingList = new List<double[]>();
        private OpenFileDialog _ofd;
        private bool _endObj;
        private double[] _nextPoint = new double[3];
        private double[] _beginPoint = new double[2];
        private List<List<double[]>> _objectsMassive;
        public List<List<List<double[]>>> AllApproximObjects;
        private Bitmap _approxBit;
        private Bitmap _inputImage;
        public String _fileName;
        public int I;

        public List<List<double[]>> TempArray = new List<List<double[]>>();
        public string pathBmp;


        public List<double> libar1;
        public List<double> libar2;
        public List<double> inpAr;


        public List<double[]> resultArray;
        ///////////////////////////////////////////
        public string inputFileName;
        public string pathForCircuit;


        //private List<double[]> _approxNPoints;
        //private List<List<double[]>> _approxObject;

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
                _fileName = Path.GetFileNameWithoutExtension(_ofd.FileName);

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
                _cannyData = new Canny(_inputImage, CannyHighTh, CannyLowTL, CannyMaskSize, CannySigma);
                CannyEdges.Image = _cannyData.DisplayImage(_cannyData.EdgeMap);

                CheckCreateDirectories();
                pathForCircuit = DescPathToDesctiptors + _fileName+"\\circuit.bmp";
                new Bitmap(CannyEdges.Image).Save(@pathForCircuit);
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
                        _reducingList.Add(new[] { i, (double)j });
                }
            _beginPoint = GetMaxLength(_reducingList, ShapeCenter());


        }



        private void Step2Circuit(object sender, EventArgs e)
        {
            AllApproximObjects = new List<List<List<double[]>>>();
            _objectsMassive = new List<List<double[]>>();

            try
            {
                while (true)
                {
                    if (_reducingList.Count > DescPointsCountInsideSegment+1)
                    {
                        resultArray = new List<double[]>();
                        _nextPoint = _beginPoint;
                        var obj = GetOneObject();
                        if(obj!=null)
                        AllApproximObjects.Add(obj);
                    }
                    else
                    {
                        MessageBox.Show(@"Сканирование завершено");
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
            //var result = new List<Tuple<double, double, double, string>>();
            //inpAr = new List<double>();
            //if (iterat >= Names.Count)
            //{
            //    MessageBox.Show("Элементы кончились");
            //    return;
            //}
            //var q = new StreamReader(Names[iterat][4]);
            //comparsionImg = Names[iterat][0];
            //string doubles;
            //while ((doubles = q.ReadLine()) != null)
            //{
            //    inpAr.Add(Convert.ToDouble(doubles));
            //}

            //var directories = Directory.GetDirectories(@"2\");
            //foreach (var name in directories)
            //{
            //    var files = Directory.GetFiles(name + "\\", "*.txt");
            //    foreach (var file in files)
            //    {
            //        // MessageBox.Show(file);
            //        var mins = new List<double>();
            //        libar1 = new List<double>();
            //        libar2 = new List<double>();
            //        var t = new StreamReader(file);
            //        string dbls;
            //        while ((dbls = t.ReadLine()) != null)
            //        {
            //            libar1.Add(Convert.ToDouble(dbls));
            //            libar2.Add(Convert.ToDouble(dbls));
            //        }
            //        for (var i = 2; i < inpAr.Count; i++)
            //        {
            //            mins.Add(equalTwoArrays(new[] { inpAr[i - 2], inpAr[i - 1], inpAr[i] }));
            //        }
            //        var total = mins.Sum();

            //        mins = new List<double>();
            //        for (var i = 2; i < libar2.Count; i++)
            //        {
            //            mins.Add(equalTwoArraysRevert(new[] { libar2[i - 2], libar2[i - 1], libar2[i] }));
            //        }
            //        var total2 = mins.Sum();
            //        t.Close();
            //        result.Add(new Tuple<double, double, double, string>(total + total2, total, total2, file));
            //    }
            //}

            //if (result.Count == 0)
            //{
            //    MessageBox.Show(@"В библиотеке нет элементов");
            //    return;
            //}

            //result.Sort();
            //var txtFile = new StreamWriter(@"1\" + _fileName + "\\" + "outCopmare" + iterat + ".txt");
            //pathForComparsion = result[1].Item4;
            //pathForComparsion = pathForComparsion.Substring(0, pathForComparsion.Length - 4);
            //pathForComparsion += ".bmp";
            //pathForComparsionTxt = result[1].Item1;

            //pathForComparsion2 = result[2].Item4;
            //pathForComparsion2 = pathForComparsion2.Substring(0, pathForComparsion2.Length - 4);
            //pathForComparsion2 += ".bmp";
            //pathForComparsionTxt2 = result[2].Item1;

            //pathForComparsion3 = result[3].Item4;
            //pathForComparsion3 = pathForComparsion3.Substring(0, pathForComparsion3.Length - 4);
            //pathForComparsion3 += ".bmp";
            //pathForComparsionTxt3 = result[3].Item1;

            //draw();

            //foreach (var line in result)
            //{
            //    txtFile.WriteLine(line.Item1 + @" " + line.Item2 + @" " + line.Item3 + @" " + line.Item4);
            //}
            //txtFile.Close();
            //iterat++;



            var comparsion = new ComparsionForm() { Owner = this };
            comparsion.ShowDialog();
        }

        private void MakeObjectDescription()
        {
            
        }

        private void TestDraw()
        {
            var image = new Bitmap(_inputImage.Width, _inputImage.Height);
            foreach (var elem in _reducingList)
            {
                image.SetPixel((int) elem[0], (int) elem[1], Color.Black);
            }
            image.Save(@"C:\\1\\test1.bmp");

            image = new Bitmap(_inputImage.Width, _inputImage.Height);
            foreach (var elem in resultArray)
            {
                image.SetPixel((int) elem[0], (int) elem[1], Color.Black);
            }
            image.Save(@"C:\\1\\test2.bmp");

        }


        private List<List<double[]>> GetOneObject()
        {
            _objectsMassive.Clear();
            while (true)
            {
                while (resultArray.Count < DescPointsCountInsideSegment)
                {
                    if (_reducingList.Count < DescPointsCountInsideSegment + 1)
                    {
                        if (_objectsMassive.Count <= DescMinSegmentsCount)
                        {
                            _objectsMassive.Clear();
                            resultArray.Clear();
                            return null;
                        }
                        DrawApproxBit();
                        pathBmp = DescPathToDesctiptors + _fileName + "\\" + I + ".bmp";
                        _approxBit.Save(pathBmp);
                        I++;
                        return _objectsMassive;
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
                        if (_objectsMassive.Count <= DescMinSegmentsCount)
                        {
                            _objectsMassive.Clear();
                            _nextPoint = min;
                            resultArray.Clear();
                            continue;
                        }
                        DrawApproxBit();
                        MakeDescriptions();
                        I++;
                        return new List<List<double[]>>(_objectsMassive);
                    }
                }
                TempArray.Clear();
                _objectsMassive.Add(new List<double[]>());
                foreach (double[] t in resultArray)
                {
                    _objectsMassive[_objectsMassive.Count - 1].Add(t);
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



            var a = Math.Round((meanXy - meanX*meanY)/(meanSqrX - Math.Pow(meanX, 2)), 3);
            var b = Math.Round(meanY - (a*meanX), 3);

            if (double.IsNaN(a))
            {
                a = double.PositiveInfinity;
            }
            if (double.IsNaN(b))
            {
                b = double.PositiveInfinity;
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

            return upCor == 0 ? 1 : Math.Abs(Math.Round(upCor/Math.Sqrt(downCor1*downCor2), 3));
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


        private void DrawApproxBit()
        {
            _approxBit = new Bitmap(_inputImage);
            foreach (var cloud in _objectsMassive)
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
            pathBmp = DescPathToDesctiptors + _fileName + "\\" + I + ".bmp";
            _approxBit.Save(pathBmp);
        }


        private void MakeDescriptions()
        {
            var descFile = new StreamWriter(DescPathToDesctiptors + _fileName + "\\" + I + ".txt");
            foreach (var cloud in _objectsMassive)
            {
                var regression = get_regression(cloud);
                descFile.WriteLine(@"корреляция=" + Correlation(cloud) + " a=" + regression[0] + " b=" + regression[1]);
                
            }
            descFile.Close();

            var descDifference = new StreamWriter(DescPathToDesctiptors + _fileName + "\\" + I + "_d.txt");
            differences.Add(new List<double>());
            for (var i = 1; i < _objectsMassive.Count; i++)
            {
                var regressionNext = get_regression(_objectsMassive[i]);
                var regressionPrev = get_regression(_objectsMassive[i - 1]);
                descDifference.WriteLine(Math.Round(Math.Atan(regressionNext[0]) - Math.Atan(regressionPrev[0]), 3));
                differences[differences.Count - 1].Add(Math.Round(Math.Atan(regressionNext[0]) - Math.Atan(regressionPrev[0]), 3));
            }
            descDifference.Close();
        }



        private static double[] GetTotalMin(IEnumerable<List<double[]>> globalCloud)
        {
            double[] min = {0, 0, double.PositiveInfinity};
            foreach (var t in globalCloud)
            {
                for (var j = 0; j < t.Count; j++)
                {
                    if (t[j][2] < min[2])
                        min = t[j];
                    else j = t.Count;
                }
            }
            return min;
        }

        private void CheckCreateDirectories()
        {
            if (Directory.Exists(DescPathToDesctiptors + _fileName))
            {
                MessageBox.Show(
                    @"Папка с таким названием уже существует. Прекратите выполнение программы и измените название входного файла, иначе выходные файлы будут перезаписаны.");
            }
            else
                Directory.CreateDirectory(DescPathToDesctiptors + _fileName);
        }



        private List<int[]> GetMinMax(IEnumerable<List<double[]>> cloud)
        {

            double minx = double.PositiveInfinity,
                miny = double.PositiveInfinity,
                maxx = double.NegativeInfinity,
                maxy = double.NegativeInfinity;
            foreach (var nPoints in cloud)
            {
                foreach (var point in nPoints)
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
            }

            return new List<int[]> {new[] {(int) minx, (int) miny}, new[] {(int) maxx, (int) maxy}};
        }



        private double equalTwoArrays(double[] three)
        {
            var min = double.PositiveInfinity;
            for (int i = 2; i < libar1.Count; i++)
            {
                var number = Math.Abs(three[0] - libar1[i - 2]) + Math.Abs(three[1] - libar1[i - 1]) +
                             Math.Abs(three[2] - libar1[i]);
                if (min > number)
                {
                    min = number;
                }
            }
            return min;
        }

        private double equalTwoArraysRevert(double[] three)
        {
            var min = double.PositiveInfinity;
            for (int i = 2; i < inpAr.Count; i++)
            {
                var number = Math.Abs(three[0] - inpAr[i - 2]) + Math.Abs(three[1] - inpAr[i - 1]) +
                             Math.Abs(three[2] - inpAr[i]);
                if (min > number)
                {
                    min = number;
                }
            }
            return min;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var settings = new Settings(){ Owner = this };
            settings.ShowDialog();
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

            using (var sr = new StreamReader("Settings.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine();
                    if (line.Contains(bCannyHighTh))
                    {
                        CannyHighTh = float.Parse(line.Substring(line.IndexOf(' ')));
                    }
                    else if (line.Contains(bCannyLowTL))
                    {
                        CannyLowTL = float.Parse(line.Substring(line.IndexOf(' ')));
                    }
                    else if (line.Contains(bCannyMaskSize))
                    {
                        CannyMaskSize = int.Parse(line.Substring(line.IndexOf(' ')));
                    }
                    else if (line.Contains(bCannySigma))
                    {
                        CannySigma = float.Parse(line.Substring(line.IndexOf(' ')));
                    }
                    else if (line.Contains(bDescLengthBetweenObjects))
                    {
                        DescLengthBetweenObjects = int.Parse(line.Substring(line.IndexOf(' ')));
                    }
                    else if (line.Contains(bDescLengthInsideObject))
                    {
                        DescLengthInsideObject = int.Parse(line.Substring(line.IndexOf(' ')));
                    }
                    else if (line.Contains(bDescPointsCountInsideSegment))
                    {
                        DescPointsCountInsideSegment = int.Parse(line.Substring(line.IndexOf(' ')));
                    }
                    else if (line.Contains(bDescMinSegmentsCount))
                    {
                        DescMinSegmentsCount = int.Parse(line.Substring(line.IndexOf(' ')));
                    }
                    else if (line.Contains(bDescCorrelation))
                    {
                        DescCorrelation = double.Parse(line.Substring(line.IndexOf(' ')+2));
                    }
                    else if (line.Contains(bDescPathToDesctiptors))
                    {
                        DescPathToDesctiptors = line.Substring(line.IndexOf(' ')+2);
                    }
                    else if (line.Contains(bDescPathToLibrary))
                    {
                        DescPathToLibrary = line.Substring(line.IndexOf(' '));
                    }
                    else MessageBox.Show("error" + line);
                }
            }
        }




}
}