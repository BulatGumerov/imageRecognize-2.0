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

        public void draw(string but)
        {

            var currObject = Main.descList[iterator].SourceCircuit;
            var a = Main.MinAndMaxes(currObject);
            Bitmap bit = new Bitmap(a[1][0] - a[0][0] + 2, a[1][1] - a[0][1] + 2);
            foreach (var point in currObject)
            {
                    bit.SetPixel((int)point[0] - a[0][0], (int)point[1] - a[0][1], Color.Black);
            }
            pictureBox1.Image = bit;
            pictureBox2.Image = new Bitmap(Main.DescPathToDesctiptors+Main.FileName+"\\"+iterator+".bmp");
            bit.Save(Main.DescPathToLibrary + but + "\\" + iterator + ".bmp");
        }

        public void draw()
        {
            var currObject = Main.descList[iterator].SourceCircuit;
            var a = Main.MinAndMaxes(currObject);
            Bitmap bit = new Bitmap(a[1][0] - a[0][0] + 2, a[1][1] - a[0][1] + 2);
            foreach (var point in currObject)
            {
                    bit.SetPixel((int)point[0] - a[0][0], (int)point[1] - a[0][1], Color.Black);
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
            if (Main.descList.Count == iterator)
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

        private void ClickBut(object sender, EventArgs e)
        {
            if (Main.descList.Count == iterator)
            {
                MessageBox.Show("Это был последний элемент");
                return;
            }
            var value = ((Button)sender).Tag;
            var fromDescriptor = ReadFolder(Main.DescPathToDesctiptors);
            if (File.Exists(Main.DescPathToLibrary + Main.FileName + "\\" + fromDescriptor[iterator]))
            {
                File.Delete(Main.DescPathToLibrary + Main.FileName + "\\" + fromDescriptor[iterator]);
            }
            //if (File.Exists(@"\2\" + value + "\\" + main.Names[iterator][3]))
            //{
            //    File.Delete(@"\2\" + value + "\\" + main.Names[iterator][3]);
            //}
            ////if (File.Exists(main.DescPathToLibrary + value + "\\" + main.Names[iterator][5]))
            ////{
            ////    File.Delete(@"2\" + value + "\\" + main.Names[iterator][5]);
            ////}
            File.Copy(Main.DescPathToDesctiptors + Main.FileName + "\\" + fromDescriptor[iterator], Main.DescPathToLibrary + Main.FileName + "\\" + fromDescriptor[iterator]);
            //File.Copy(main.Names[iterator][1], @"\2\" + value + "\\" + main.Names[iterator][3]);
            //File.Copy(main.Names[iterator][4], @"2\" + value + "\\" + main.Names[iterator][5]);
            draw(value.ToString());
            iterator++;
            if (Main.descList.Count == iterator)
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
