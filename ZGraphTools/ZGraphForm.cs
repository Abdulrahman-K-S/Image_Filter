using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;


namespace ZGraphTools
{
    public partial class ZGraphForm : Form
    {
        ZGraphClass z;
        GraphPane myPane1;
        PointPairList spl,spl_imposter;
        List<LineItem> lineItems = new List<LineItem>();
        List<BarItem> barItems = new List<BarItem>();
        
        int curveCnt = 0;
        public ZGraphForm(string GraphTitle, string XAxisTitle, string YAxisTitle)
        {
            InitializeComponent();
            
            myPane1 = zg1.GraphPane;
           // myPane2 = zg2.GraphPane;
            myPane1.CurveList.Clear();
            
            curveCnt = 0;
            // Set the titles and axis labels
            myPane1.Title.Text = GraphTitle;
            myPane1.XAxis.Title.Text = XAxisTitle;
            myPane1.YAxis.Title.Text = YAxisTitle;
            // myPane2.CurveList.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CreateGraph(zg1);
            //SetSize();
        }

    

        private void CreateGraph(ZedGraphControl zgc)
		{

            z = new ZGraphClass(zg1, "scores", "% of error", "Frequency of error");
            curveCnt = 0;
		}

        public void add_bar(string bar_name, double[] x, double[] y, int clr, Color c)
        {
            // z.AddNewCurve(curve_name, x, y);
            spl = new PointPairList(x, y);
            BarItem tmpBarItem = myPane1.AddBar(bar_name, spl, c);
            
            barItems.Add(tmpBarItem);
            curveCnt++;
            zg1.AxisChange();
            zg1.Invalidate();
            zg1.Refresh();

        }

        public void add_curve(string curve_name,double[]x,double[]y, Color c)
        {
           // z.AddNewCurve(curve_name, x, y);
            spl = new PointPairList(x, y);
            LineItem tmpLI = myPane1.AddCurve(curve_name, spl, c, SymbolType.None);

            //tmpLI.Line.Fill = new Fill(c, Color.FromArgb(100, 100, 0, 0), 100F);//100 100 0 0 
            lineItems.Add(tmpLI); 
            
            curveCnt++;
            zg1.AxisChange();
            zg1.Invalidate();
            zg1.Refresh();

        }
        public void add_points(string name, double[] x, double[] y, Color c, SymbolType symbol)
        {
            // z.AddNewCurve(curve_name, x, y);
            spl = new PointPairList(x, y);
            LineItem tmpLI = myPane1.AddCurve(name, spl, c, symbol);
                        
            lineItems.Add(tmpLI);

            curveCnt++;
            zg1.AxisChange();
            zg1.Invalidate();
            zg1.Refresh();

        }
        public void add_log10_curve(string curve_name, double[] x, double[] y, int clr, Color c)
        {
            // z.AddNewCurve(curve_name, x, y);
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = i;
            }
            spl = new PointPairList(x, y);
            LineItem tmpLI = myPane1.AddCurve(curve_name, spl, c, SymbolType.None);
            tmpLI.Line.Fill = new Fill(c, Color.FromArgb(100, 100, 0, clr), 100F);//100 100 0 0 
            lineItems.Add(tmpLI); 
            
            curveCnt++;
            zg1.AxisChange();
            zg1.Invalidate();
            zg1.Refresh();

        }
        public void draw()
        {
           // z.DrawGraph(this);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void SetSize()
        {
            zg1.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            zg1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }

        private void zg1_Load(object sender, EventArgs e)
        {

        }
    }
}