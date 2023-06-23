using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;

namespace ImageFilters
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        byte[,] ImageMatrix;
        byte[,] NewImageMatrix;
        int WindowSize;
        int T;
        int WS;
        int Type;
        int Wmax;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
        }

        private void btnZGraph_Click(object sender, EventArgs e)
        {
            // Make up some data points from the N, N log(N) functions
            int N = 40;
            double[] x_values = new double[N];
            double[] y_values_N = new double[N];
            double[] y_values_NLogN = new double[N];

            for (int i = 0; i < N; i++)
            {
                x_values[i] = i;
                y_values_N[i] = i;
                y_values_NLogN[i] = i * Math.Log(i);
            }

            //Create a graph and add two curves to it
            ZGraphForm ZGF = new ZGraphForm("Sample Graph", "N", "f(N)");
            ZGF.add_curve("f(N) = N", x_values, y_values_N,Color.Red);
            ZGF.add_curve("f(N) = N Log(N)", x_values, y_values_NLogN, Color.Blue);
            ZGF.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AlphaTrimFilter_Click(object sender, EventArgs e)
        {
            WindowSize = int.Parse(textBox1.Text);
            T = int.Parse(textBox2.Text);
            Type = int.Parse(textBox5.Text);
            NewImageMatrix = ImageOperations.AlphaTrimFilter(ImageMatrix, WindowSize, T, Type);
        }

        private void MedianFilter_Click(object sender, EventArgs e)
        {
            WS = int.Parse(textBox3.Text);
            Type = int.Parse(textBox4.Text);
            //int t1 = System.Environment.TickCount;
            NewImageMatrix = ImageOperations.MedianFilter(ImageMatrix, WS, Type);
            /*int t2 = System.Environment.TickCount;
            MessageBox.Show(t1.ToString());
            MessageBox.Show(t2.ToString());
            MessageBox.Show((t2 - t1).ToString());*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ImageOperations.DisplayImage(NewImageMatrix, pictureBox1);
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

    }
}