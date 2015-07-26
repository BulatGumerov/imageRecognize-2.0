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

        private static Mainform Main;
        private int iterator;

        private void identifyForm_Load(object sender, EventArgs e)
        {
            var lines = File.ReadAllLines(@"buttons.txt");
            Main = Owner as Mainform;
            foreach (var str in lines)
            {
                var but = new Button();
                AddButton(but, str);
                if (!Directory.Exists(Main.DescPathToLibrary + str))
                {
                    Directory.CreateDirectory(Main.DescPathToLibrary + str);
                }
            }
            draw();
        }


        public void draw()
        {
            var currObject = Main.DescList[iterator].SourceCircuit;
            var bit = Main.GetBitmap(currObject);
            var minP = Main.GetMinPoint(currObject);
            foreach (var point in currObject)
            {
                    bit.SetPixel(point.X - minP.X, point.Y - minP.Y, Color.Black);
            }
            pictureBox1.Image = bit;
            pictureBox2.Image = new Bitmap(Main.DescPathToDesctiptors + Main.FileName + "\\" + iterator +"_b" + ".bmp");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
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
            if (Main.DescList.Count == iterator)
            {
                MessageBox.Show("Это был последний элемент");
                return;
            }
            string newText = Interaction.InputBox("Введите название нового класса?", @"Новый класс", "");
            Directory.CreateDirectory(Main.DescPathToLibrary + newText);
            AddButton(new Button(), newText);
            StreamWriter reader = new StreamWriter(@"buttons.txt", true);
            reader.Write(Environment.NewLine + newText);
            reader.Close();
        }

        private void AddButton(Button but, string text)
        {
            but.Text = text;
            but.Click += ClickBut;
            but.Size = button1.Size;
            but.Tag = text;
            flowLayoutPanel1.Controls.Add(but);
        }

        private static int GetIterator(string path)
        {
            var dir = Directory.GetFiles(Main.DescPathToLibrary + path);
            return (dir.Length/4);
        }

        private void ClickBut(object sender, EventArgs e)
        {
            if (Main.DescList.Count == iterator)
            {
                MessageBox.Show("Это был последний элемент");
                return;
            }
            var buttonName = ((Button)sender).Tag.ToString();
            var fromDescriptor = ReadFolder(Main.DescPathToDesctiptors);
            if (File.Exists(Main.DescPathToLibrary + Main.FileName + "\\" + fromDescriptor[iterator]))
            {
                File.Delete(Main.DescPathToLibrary + Main.FileName + "\\" + fromDescriptor[iterator]);
            }
            var lastIter = GetIterator(buttonName);
            File.Copy(Main.DescPathToDesctiptors + Main.FileName + "\\" + iterator + ".txt", Main.DescPathToLibrary + buttonName + "\\" + lastIter + ".txt", true);
            File.Copy(Main.DescPathToDesctiptors + Main.FileName + "\\" + iterator + "_d.txt", Main.DescPathToLibrary + buttonName + "\\" + lastIter + "_d.txt", true);
            File.Copy(Main.DescPathToDesctiptors + Main.FileName + "\\" + iterator + "_b.bmp", Main.DescPathToLibrary + buttonName + "\\" + lastIter + "_b.bmp", true);
            File.Copy(Main.DescPathToDesctiptors + Main.FileName + "\\" + iterator + "_o.bmp", Main.DescPathToLibrary + buttonName + "\\" + lastIter + "_o.bmp", true);
            draw();
            iterator++;
            if (Main.DescList.Count == iterator)
            {
                MessageBox.Show("Это был последний элемент");
                return;
            }
            draw();
        }

        private List<string> ReadFolder(string path)
        {
            var s = Directory.GetFiles(path + Main.FileName, "*.bmp");
            return s.ToList();
        }
    }
}
