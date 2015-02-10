namespace CannyEdgeDetectionCSharp
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button5 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.MinSegmentsCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Correlation = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LengthInsideObject = new System.Windows.Forms.TextBox();
            this.LengthBetweenObjects = new System.Windows.Forms.TextBox();
            this.PointsCountInsideSegment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TL = new System.Windows.Forms.TextBox();
            this.TH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PathToDesctiptors = new System.Windows.Forms.TextBox();
            this.PathToLibrary = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.MaskSize = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Sigma = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(157, 267);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 101;
            this.button5.Text = "Задать";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(156, 238);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 26);
            this.label11.TabIndex = 100;
            this.label11.Text = "Путь до библиотеки\r\nэталонов";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(159, 202);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 99;
            this.button4.Text = "Задать";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(156, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 13);
            this.label10.TabIndex = 98;
            this.label10.Text = "Путь до дескрипторов";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(233, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 97;
            this.label9.Text = "Число отрезков";
            // 
            // MinSegmentsCount
            // 
            this.MinSegmentsCount.Location = new System.Drawing.Point(236, 134);
            this.MinSegmentsCount.Name = "MinSegmentsCount";
            this.MinSegmentsCount.Size = new System.Drawing.Size(39, 20);
            this.MinSegmentsCount.TabIndex = 84;
            this.MinSegmentsCount.Text = "10";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(328, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 96;
            this.label8.Text = "Корреляция";
            // 
            // Correlation
            // 
            this.Correlation.Location = new System.Drawing.Point(331, 134);
            this.Correlation.Name = "Correlation";
            this.Correlation.Size = new System.Drawing.Size(43, 20);
            this.Correlation.TabIndex = 95;
            this.Correlation.Text = "0.3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(266, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 26);
            this.label6.TabIndex = 94;
            this.label6.Text = "Расстояние внутри \r\nобъекта";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(156, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 26);
            this.label5.TabIndex = 93;
            this.label5.Text = "Расстояние между \r\nобъектами";
            // 
            // LengthInsideObject
            // 
            this.LengthInsideObject.Location = new System.Drawing.Point(269, 70);
            this.LengthInsideObject.Name = "LengthInsideObject";
            this.LengthInsideObject.Size = new System.Drawing.Size(39, 20);
            this.LengthInsideObject.TabIndex = 92;
            this.LengthInsideObject.Text = "5";
            // 
            // LengthBetweenObjects
            // 
            this.LengthBetweenObjects.Location = new System.Drawing.Point(157, 70);
            this.LengthBetweenObjects.Name = "LengthBetweenObjects";
            this.LengthBetweenObjects.Size = new System.Drawing.Size(39, 20);
            this.LengthBetweenObjects.TabIndex = 91;
            this.LengthBetweenObjects.Text = "10";
            // 
            // PointsCountInsideSegment
            // 
            this.PointsCountInsideSegment.Location = new System.Drawing.Point(157, 134);
            this.PointsCountInsideSegment.Name = "PointsCountInsideSegment";
            this.PointsCountInsideSegment.Size = new System.Drawing.Size(34, 20);
            this.PointsCountInsideSegment.TabIndex = 90;
            this.PointsCountInsideSegment.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(154, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 26);
            this.label4.TabIndex = 89;
            this.label4.Text = "Число точек \r\nв отрезке";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 88;
            this.label2.Text = "Low TL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "High TH";
            // 
            // TL
            // 
            this.TL.Location = new System.Drawing.Point(65, 70);
            this.TL.Name = "TL";
            this.TL.Size = new System.Drawing.Size(41, 20);
            this.TL.TabIndex = 86;
            this.TL.Text = "240";
            // 
            // TH
            // 
            this.TH.Location = new System.Drawing.Point(12, 70);
            this.TH.Name = "TH";
            this.TH.Size = new System.Drawing.Size(38, 20);
            this.TH.TabIndex = 85;
            this.TH.Text = "250";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 102;
            this.label3.Text = "Canny";
            // 
            // PathToDesctiptors
            // 
            this.PathToDesctiptors.Location = new System.Drawing.Point(285, 202);
            this.PathToDesctiptors.Name = "PathToDesctiptors";
            this.PathToDesctiptors.Size = new System.Drawing.Size(100, 20);
            this.PathToDesctiptors.TabIndex = 103;
            // 
            // PathToLibrary
            // 
            this.PathToLibrary.Location = new System.Drawing.Point(285, 267);
            this.PathToLibrary.Name = "PathToLibrary";
            this.PathToLibrary.Size = new System.Drawing.Size(100, 20);
            this.PathToLibrary.TabIndex = 104;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(282, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 105;
            this.label7.Text = "Сейчас";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(282, 238);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 106;
            this.label12.Text = "Сейчас";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(154, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 13);
            this.label13.TabIndex = 107;
            this.label13.Text = "Descriptor getting";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(321, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 108;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 109;
            this.label14.Text = "Mask Size";
            // 
            // MaskSize
            // 
            this.MaskSize.Location = new System.Drawing.Point(12, 121);
            this.MaskSize.Name = "MaskSize";
            this.MaskSize.Size = new System.Drawing.Size(38, 20);
            this.MaskSize.TabIndex = 110;
            this.MaskSize.Text = "5";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(71, 105);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 13);
            this.label15.TabIndex = 111;
            this.label15.Text = "Sigma";
            // 
            // Sigma
            // 
            this.Sigma.Location = new System.Drawing.Point(74, 121);
            this.Sigma.Name = "Sigma";
            this.Sigma.Size = new System.Drawing.Size(38, 20);
            this.Sigma.TabIndex = 112;
            this.Sigma.Text = "1";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 171);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 113;
            this.label16.Text = "Other";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 202);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 115;
            this.checkBox1.Text = "Save circuit";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 267);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(38, 20);
            this.textBox1.TabIndex = 116;
            this.textBox1.Text = "10";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 238);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 26);
            this.label17.TabIndex = 117;
            this.label17.Text = "Разница между\r\nдескрипторами";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 339);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.Sigma);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.MaskSize);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.PathToLibrary);
            this.Controls.Add(this.PathToDesctiptors);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.MinSegmentsCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Correlation);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LengthInsideObject);
            this.Controls.Add(this.LengthBetweenObjects);
            this.Controls.Add(this.PointsCountInsideSegment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TL);
            this.Controls.Add(this.TH);
            this.Name = "Settings";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox MinSegmentsCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Correlation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox LengthInsideObject;
        private System.Windows.Forms.TextBox LengthBetweenObjects;
        private System.Windows.Forms.TextBox PointsCountInsideSegment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TL;
        private System.Windows.Forms.TextBox TH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PathToDesctiptors;
        private System.Windows.Forms.TextBox PathToLibrary;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox MaskSize;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox Sigma;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label17;
    }
}