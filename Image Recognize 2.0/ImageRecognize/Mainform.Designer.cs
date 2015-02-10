using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CannyEdgeDetectionCSharp
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.CannyEdges = new System.Windows.Forms.PictureBox();
            this.BtnCannyEdgeDetect = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.CannyEdges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CannyEdges
            // 
            this.CannyEdges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CannyEdges.Location = new System.Drawing.Point(294, 28);
            this.CannyEdges.Name = "CannyEdges";
            this.CannyEdges.Size = new System.Drawing.Size(282, 534);
            this.CannyEdges.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CannyEdges.TabIndex = 45;
            this.CannyEdges.TabStop = false;
            // 
            // BtnCannyEdgeDetect
            // 
            this.BtnCannyEdgeDetect.Location = new System.Drawing.Point(582, 28);
            this.BtnCannyEdgeDetect.Name = "BtnCannyEdgeDetect";
            this.BtnCannyEdgeDetect.Size = new System.Drawing.Size(88, 50);
            this.BtnCannyEdgeDetect.TabIndex = 48;
            this.BtnCannyEdgeDetect.Text = "Canny";
            this.BtnCannyEdgeDetect.UseVisualStyleBackColor = true;
            this.BtnCannyEdgeDetect.Click += new System.EventHandler(this.Step1Canny);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(453, 549);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Final Step1Canny Edges";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(582, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 49);
            this.button1.TabIndex = 63;
            this.button1.Text = "Circuit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Step2Circuit);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(582, 139);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 49);
            this.button2.TabIndex = 69;
            this.button2.Text = "Identify";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Step3Identify);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(6, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(282, 534);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 70;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 549);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Source";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(582, 194);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 49);
            this.button3.TabIndex = 72;
            this.button3.Text = "Comparsion";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Step4Comparsion);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(582, 513);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 49);
            this.button4.TabIndex = 73;
            this.button4.Text = "Settings";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Settings);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Open";
            this.toolStripLabel1.Click += new System.EventHandler(this.ClickOpen);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(677, 25);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 571);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CannyEdges);
            this.Controls.Add(this.BtnCannyEdgeDetect);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Распознавание изображения";
            this.Load += new System.EventHandler(this.ReadSettings);
            ((System.ComponentModel.ISupportInitialize)(this.CannyEdges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox CannyEdges;
        private Button BtnCannyEdgeDetect;
        private Label label7;
        private Button button1;
        private Button button2;
        private PictureBox pictureBox1;
        private Label label3;
        private Button button3;
        private OpenFileDialog openFileDialog1;
        private Button button4;
        private ToolStripLabel toolStripLabel1;
        private ToolStrip toolStrip1;
    }
}

