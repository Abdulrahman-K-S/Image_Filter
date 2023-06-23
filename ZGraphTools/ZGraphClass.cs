using System;
using System.Collections.Generic;
using System.Text;
using ZedGraph;
using System.Drawing;
using System.Windows.Forms;

namespace ZGraphTools
{
    class ZGraphClass
    {
        ZedGraphControl _zedObject;
        GraphPane myPane;

        Random ClrRnd;


        public ZGraphClass(ZedGraphControl ZedObject, string GraphTitle, string XAxisTitle, string YAxisTitle)
        {
            _zedObject = ZedObject;
            ClrRnd = new Random(System.Environment.TickCount);

            myPane = _zedObject.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = GraphTitle;
            myPane.XAxis.Title.Text = XAxisTitle;
            myPane.YAxis.Title.Text = YAxisTitle;
                     
        }
      
        public void DrawGraph(Form DrawingForm)
        {
            _zedObject.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            _zedObject.Size = new Size(DrawingForm.ClientRectangle.Width - 20, DrawingForm.ClientRectangle.Height - 20);
        }

        public void AddNewCurve(string CurveName, double[] CurveData_X, double[] CurveData_Y)
        {
            // Make up some data points from the Sine function
            int NumofPoints = CurveData_X.Length;
            PointPairList list = new PointPairList();

            for (int x = 0; x < NumofPoints; x++)
            {
                list.Add(CurveData_X[x], CurveData_Y[x]);
            }

            Color Clr = Color.FromArgb(ClrRnd.Next(255), ClrRnd.Next(255), ClrRnd.Next(255));
            
            // Generate a random-colored curve with circle symbols, and CurveName in the legend
            LineItem myCurve = myPane.AddCurve(CurveName, list, Clr,SymbolType.Circle);
            
            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);

            // Fill the pane background with a color gradient
            myPane.Fill = new Fill(Color.White, Color.Pink, 45F);

            // Calculate the Axis Scale Ranges
            _zedObject.AxisChange();
        }
    }
}
