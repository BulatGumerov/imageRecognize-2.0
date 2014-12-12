using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CannyEdgeDetectionCSharp
{
    public partial class identifyForm : Form
    {
        public identifyForm()
        {
            InitializeComponent();
        }

        //private string _fileName;
        public Mainform main;
        private int iterator;
        private string text;

        private void identifyForm_Load(object sender, EventArgs e)
        {
            var lines = File.ReadAllLines(@"buttons.txt");
            foreach (var str in lines)
            {
                var but = new Button();
                text = str;
                AddButton(but);
                if (!Directory.Exists(@"2\" + text))
                {
                    Directory.CreateDirectory(@"2\" + text);
                }
            }
            main = Owner as Mainform;
            draw();
        }


        private List<int[]> MinAndMaxes(IEnumerable<List<double[]>> cloud)
        {

            double minx = double.PositiveInfinity, miny = double.PositiveInfinity, maxx = double.NegativeInfinity, maxy = double.NegativeInfinity;
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

            return new List<int[]> { new[] { (int)minx, (int)miny }, new[] { (int)maxx, (int)maxy } };
        }

        public void draw(string but)
        {

            List<List<double[]>> currObject = main.AllApproximObjects[iterator];
            var a = MinAndMaxes(currObject);
            Bitmap bit = new Bitmap(a[1][0] - a[0][0] + 2, a[1][1] - a[0][1] + 2);
            foreach (var nPoints in currObject)
            {
                foreach (var point in nPoints)
                {
                    bit.SetPixel((int)point[0] - a[0][0], (int)point[1] - a[0][1], Color.Black);
                }
            }
            pictureBox1.Image = bit;
            bit.Save(@"2\" + but + "\\" + iterator + ".bmp");
        }

        public void draw()
        {
            List<List<double[]>> currObject = main.AllApproximObjects[iterator];
            var a = MinAndMaxes(currObject);
            Bitmap bit = new Bitmap(a[1][0] - a[0][0] + 2, a[1][1] - a[0][1] + 2);
            foreach (var nPoints in currObject)
            {
                foreach (var point in nPoints)
                {
                    bit.SetPixel((int)point[0] - a[0][0], (int)point[1] - a[0][1], Color.Black);
                }
            }
            pictureBox1.Image = bit;
            //pictureBox2.Image = new Bitmap(main.Names[iterator][0]);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //pictureBox2.Image = new Bitmap(main.Names[iterator][0]);
                iterator++;
                draw();
            }
            catch (Exception)
            {
                MessageBox.Show("Это был последний элемент");
            }
        }



        private void button6_Click(object sender, EventArgs e)
        {
            if (main.AllApproximObjects.Count == iterator)
            {
                MessageBox.Show("Это был последний элемент");
                return;
            }
            text = Interaction.InputBox("Введите название нового класса?", @"Новый класс", "");
            Directory.CreateDirectory(@"2\" + text);
            AddButton(new Button());
            StreamWriter reader = new StreamWriter(@"buttons.txt", true);
            reader.Write(Environment.NewLine + text);
            reader.Close();
        }

        private void AddButton(Button but)
        {
            but.Text = text;
            but.Click += ClickBut;
            but.Size = button1.Size;
            but.Tag = text;
            flowLayoutPanel1.Controls.Add(but);
        }

        private void ClickBut(object sender, EventArgs e)
        {
            if (main.AllApproximObjects.Count == iterator)
            {
                MessageBox.Show("Это был последний элемент");
                return;
            }
            var value = ((Button)sender).Tag;
            if (File.Exists(@"2\" + value + "\\" + main.Names[iterator][2]))
            {
                File.Delete(@"2\" + value + "\\" + main.Names[iterator][2]);
            }
            //if (File.Exists(@"\2\" + value + "\\" + main.Names[iterator][3]))
            //{
            //    File.Delete(@"\2\" + value + "\\" + main.Names[iterator][3]);
            //}
            if (File.Exists(@"2\" + value + "\\" + main.Names[iterator][5]))
            {
                File.Delete(@"2\" + value + "\\" + main.Names[iterator][5]);
            }
            File.Copy(main.Names[iterator][0], @"2\" + value + "\\" + main.Names[iterator][2]);
            //File.Copy(main.Names[iterator][1], @"\2\" + value + "\\" + main.Names[iterator][3]);
            File.Copy(main.Names[iterator][4], @"2\" + value + "\\" + main.Names[iterator][5]);
            draw(value.ToString());
            iterator++;
            if (main.AllApproximObjects.Count == iterator)
            {
                MessageBox.Show("Это был последний элемент");
                return;
            }
            draw();
        }

        private List<string> ReadFolder()
        {
            var s = Directory.GetFiles(main.DescPathToDesctiptors + main._fileName,"*.bmp");
            return s.ToList();
        }
    }
}
