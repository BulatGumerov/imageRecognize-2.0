namespace CannyEdgeDetectionCSharp
{
    partial class Mainform
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.CNMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectFullImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CannyEdges = new System.Windows.Forms.PictureBox();
            this.TxtTH = new System.Windows.Forms.TextBox();
            this.TxtTL = new System.Windows.Forms.TextBox();
            this.BtnCannyEdgeDetect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.CNMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CannyEdges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Open";
            this.toolStripLabel1.Click += new System.EventHandler(this.ClickOpen);
            // 
            // CNMenuStrip
            // 
            this.CNMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFullImageToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.CNMenuStrip.Name = "CNMenuStrip";
            this.CNMenuStrip.Size = new System.Drawing.Size(164, 48);
            // 
            // selectFullImageToolStripMenuItem
            // 
            this.selectFullImageToolStripMenuItem.Name = "selectFullImageToolStripMenuItem";
            this.selectFullImageToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.selectFullImageToolStripMenuItem.Text = "Select Full Image";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.closeToolStripMenuItem.Text = "Close";
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
            // TxtTH
            // 
            this.TxtTH.Location = new System.Drawing.Point(46, 581);
            this.TxtTH.Name = "TxtTH";
            this.TxtTH.Size = new System.Drawing.Size(38, 20);
            this.TxtTH.TabIndex = 46;
            this.TxtTH.Text = "250";
            // 
            // TxtTL
            // 
            this.TxtTL.Location = new System.Drawing.Point(99, 581);
            this.TxtTL.Name = "TxtTL";
            this.TxtTL.Size = new System.Drawing.Size(41, 20);
            this.TxtTL.TabIndex = 47;
            this.TxtTL.Text = "240";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 565);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "High TH";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 565);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Low TL";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(481, 549);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 565);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "N";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 581);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(34, 20);
            this.textBox1.TabIndex = 66;
            this.textBox1.Text = "10";
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
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(294, 581);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(39, 20);
            this.textBox2.TabIndex = 73;
            this.textBox2.Text = "7";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(415, 581);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(39, 20);
            this.textBox3.TabIndex = 74;
            this.textBox3.Text = "3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(291, 565);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 75;
            this.label5.Text = "Между объектами";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(412, 565);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 76;
            this.label6.Text = "Внутри объекта";
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 605);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CannyEdges);
            this.Controls.Add(this.TxtTL);
            this.Controls.Add(this.BtnCannyEdgeDetect);
            this.Controls.Add(this.TxtTH);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Распознавание изображения";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.CNMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CannyEdges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ContextMenuStrip CNMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem selectFullImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.PictureBox CannyEdges;
        private System.Windows.Forms.TextBox TxtTH;
        private System.Windows.Forms.TextBox TxtTL;
        private System.Windows.Forms.Button BtnCannyEdgeDetect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

