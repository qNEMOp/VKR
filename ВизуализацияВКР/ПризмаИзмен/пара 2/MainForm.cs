using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;


namespace пара_2
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			AnT.InitializeContexts();
		}

		void MainForm_Load(object sender, EventArgs e)
		{
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
			Gl.glEnable(Gl.GL_DEPTH_TEST);
			Gl.glClearColor(1.0f, 1.0f, 1.0f, 1);
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();
			Gl.glOrtho(-10.0, 10.0, -10.0, 10.0, -10.0, 10.0);
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();
			CalcAngles();
		}

		double a, b, x, y, z, r = 1.0, A11 = -3, A12 = -2, B11 = 0, B12 = 3, C11 = 3, C12 = -3, A21, A22, B21, B22, C21, C22;
		double z11, z12, z21, z22, z31, z32, Z1, Z2, Z3;

		double alphaA, alphaB, alphaC, betaA, betaB, betaC;
		double min = 0, max = 0, angle, angle1, r1, r2, r3;
		double section, center;
		int coefC1, coefC2, coefC3;

		double x1, x2, x3, x4, x5, x6, y1, y2, y3, y4, y5, y6, w1, w2, lambda;

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
			//FullDraw();
		}

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
			//FullDraw();
		}

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
			//FullDraw();
		}

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
			//FullDraw();
        }

        double[,] vertexHexagon;
		double zAxisChange;

		private void button1_Click(object sender, EventArgs e)
		{
			
		}

		void CalcAngles()
		{
			z11 = Convert.ToDouble(textBox1.Text);
			z12 = Convert.ToDouble(textBox2.Text);
			z21 = Convert.ToDouble(textBox3.Text);
			z22 = Convert.ToDouble(textBox4.Text);
			z31 = Convert.ToDouble(textBox5.Text);
			z32 = Convert.ToDouble(textBox6.Text);

			alphaA = -Math.Acos((z11 * (B11 - A11) + z12 * (B12 - A12)) / Math.Sqrt(z11 * z11 + z12 * z12) / Math.Sqrt(Math.Pow(B11 - A11, 2) + Math.Pow(B12 - A12, 2))) * 180 / Math.PI;
			betaA = Math.Acos((z11 * (C11 - A11) + z12 * (C12 - A12)) / Math.Sqrt(z11 * z11 + z12 * z12) / Math.Sqrt(Math.Pow(C11 - A11, 2) + Math.Pow(C12 - A12, 2))) * 180 / Math.PI;

			alphaB = -Math.Acos((z31 * (A11 - B11) + z32 * (A12 - B12)) / Math.Sqrt(z31 * z31 + z32 * z32) / Math.Sqrt(Math.Pow(A11 - B11, 2) + Math.Pow(A12 - B12, 2))) * 180 / Math.PI;
			betaB = Math.Acos((z31 * (C11 - B11) + z32 * (C12 - B12)) / Math.Sqrt(z31 * z31 + z32 * z32) / Math.Sqrt(Math.Pow(C11 - B11, 2) + Math.Pow(C12 - B12, 2))) * 180 / Math.PI;

			alphaC = -Math.Acos((z21 * (A11 - C11) + z22 * (A12 - C12)) / Math.Sqrt(z21 * z21 + z22 * z22) / Math.Sqrt(Math.Pow(A11 - C11, 2) + Math.Pow(A12 - C12, 2))) * 180 / Math.PI;
			betaC = Math.Acos((z21 * (B11 - C11) + z22 * (B12 - C12)) / Math.Sqrt(z21 * z21 + z22 * z22) / Math.Sqrt(Math.Pow(B11 - C11, 2) + Math.Pow(B12 - C12, 2))) * 180 / Math.PI;


			min = Math.Max(Math.Max(alphaA, alphaB), Math.Max(alphaB, alphaC));
			max = Math.Min(Math.Min(betaA, betaB), Math.Min(betaB, betaC));

			richTextBox1.Text = min.ToString() + '\n';
			richTextBox1.Text += max.ToString() + '\n';

			Z1 = Norma(z11, z12);
			Z2 = Norma(z21, z22);
			Z3 = Norma(z31, z32);
		}

		/*private void button3_Click(object sender, EventArgs e)
		{
			if (trackBar4.Value == 0)
				DeltaInTriangleBot();
			else if (trackBar4.Value == 100)
				DeltaInTriangleTop();
			else
			{
				richTextBox2.Clear();
				FindDelta();
			}
		}*/

		/*private void button2_Click_1(object sender, EventArgs e)
        {

        }*/

		void calc()
		{
			x = r * Math.Sin(Math.PI * b) * Math.Cos(2 * Math.PI * a);
			y = r * Math.Sin(Math.PI * b) * Math.Sin(2 * Math.PI * a);
			z = r * Math.Cos(Math.PI * b);

			if (angle < 0)
			{
				angle1 = angle * (-min);
			}
			else angle1 = angle * max;

			label3.Text = angle1.ToString();

			A21 = A11; A22 = A12; B21 = B11; B22 = B12; C21 = C11; C22 = C12;

			r1 = Norma(A21, A22);
			r2 = Norma(B21, B22);
			r3 = Norma(C21, C22);

			CalcTriangleTop();

			x1 = -3;
			x2 = 3;
			x3 = 0;
			x4 = r1 * Math.Cos(2 * Math.PI - Math.Acos(-3 / Math.Sqrt(13)) + angle1 * Math.PI / 180);
			x5 = r3 * Math.Cos(7 * Math.PI / 4 + angle1 * Math.PI / 180);
			x6 = r2 * Math.Cos(Math.PI / 2 + angle1 * Math.PI / 180);
			y1 = -2;
			y2 = -3;
			y3 = 3;
			y4 = r1 * Math.Sin(2 * Math.PI - Math.Acos(-3 / Math.Sqrt(13)) + angle1 * Math.PI / 180);
			y5 = r3 * Math.Sin(7 * Math.PI / 4 + angle1 * Math.PI / 180);
			y6 = r2 * Math.Sin(Math.PI / 2 + angle1 * Math.PI / 180);

			//zAxisChange = 10 * section - 5;
		}

		double Norma(double X, double Y)
		{
			return Math.Sqrt(X * X + Y * Y);
		}

		void TriangleBot()
		{
			Gl.glLineWidth(3);
			//Gl.glColor3f(0.0f, 0.0f, 0.0f);
			Gl.glColor3f(0.0f, 1.0f, 0.0f);
			Gl.glBegin(Gl.GL_LINE_LOOP);
			Gl.glVertex3d(A11, A12, -5.0);
			Gl.glVertex3d(B11, B12, -5.0);
			Gl.glVertex3d(C11, C12, -5.0);
			Gl.glEnd();

			Gl.glPointSize(7);
			Gl.glBegin(Gl.GL_POINTS);
			Gl.glVertex3d(A11, A12, -5.0);
			Gl.glVertex3d(B11, B12, -5.0);
			Gl.glVertex3d(C11, C12, -5.0);
			Gl.glEnd();
		}

		void CalcTriangleTop()
		{
			A21 = r1 * Math.Cos(2 * Math.PI - Math.Acos(-3 / Math.Sqrt(13)) + angle1 * Math.PI / 180);
			A22 = r1 * Math.Sin(2 * Math.PI - Math.Acos(-3 / Math.Sqrt(13)) + angle1 * Math.PI / 180);
			B21 = r2 * Math.Cos(Math.PI / 2 + angle1 * Math.PI / 180);
			B22 = r2 * Math.Sin(Math.PI / 2 + angle1 * Math.PI / 180);
			C21 = r3 * Math.Cos(7 * Math.PI / 4 + angle1 * Math.PI / 180);
			C22 = r3 * Math.Sin(7 * Math.PI / 4 + angle1 * Math.PI / 180);
		}

		void TriangleTop()
		{
			Gl.glLineWidth(3);
			Gl.glColor3f(0.0f, 1.0f, 0.0f);
			Gl.glBegin(Gl.GL_LINE_LOOP);
			Gl.glVertex3d(A21, A22, 5.0);
			Gl.glVertex3d(B21, B22, 5.0);
			Gl.glVertex3d(C21, C22, 5.0);
			Gl.glEnd();

			Gl.glPointSize(7);
			Gl.glBegin(Gl.GL_POINTS);
			Gl.glVertex3d(A21, A22, 5.0);
			Gl.glVertex3d(B21, B22, 5.0);
			Gl.glVertex3d(C21, C22, 5.0);
			Gl.glEnd();
		}

		void Edge()
		{
			Gl.glLineWidth(3);
			Gl.glColor3f(0.0f, 1.0f, 0.0f);
			Gl.glBegin(Gl.GL_LINES);
			Gl.glVertex3d(A11, A12, -5.0);
			Gl.glVertex3d(A21, A22, 5.0);
			Gl.glVertex3d(B11, B12, -5.0);
			Gl.glVertex3d(B21, B22, 5.0);
			Gl.glVertex3d(C11, C12, -5.0);
			Gl.glVertex3d(C21, C22, 5.0);
			Gl.glEnd();

			if (angle1 > 0)
			{
				Gl.glColor3f(0.0f, 1.0f, 0.0f);
				Gl.glBegin(Gl.GL_LINES);
				Gl.glVertex3d(A11, A12, -5.0);
				Gl.glVertex3d(B21, B22, 5.0);
				Gl.glVertex3d(B11, B12, -5.0);
				Gl.glVertex3d(C21, C22, 5.0);
				Gl.glVertex3d(C11, C12, -5.0);
				Gl.glVertex3d(A21, A22, 5.0);
				Gl.glEnd();
			}
			else if (angle1 < 0)
			{
				Gl.glColor3f(0.0f, 1.0f, 0.0f);
				Gl.glBegin(Gl.GL_LINES);
				Gl.glVertex3d(A11, A12, -5.0);
				Gl.glVertex3d(C21, C22, 5.0);
				Gl.glVertex3d(B11, B12, -5.0);
				Gl.glVertex3d(A21, A22, 5.0);
				Gl.glVertex3d(C11, C12, -5.0);
				Gl.glVertex3d(B21, B22, 5.0);
				Gl.glEnd();
			}
		}

		void vector()
		{
			Gl.glLineWidth(3);
			Gl.glColor3f(1.0f, 1.0f, 0.0f);
			Gl.glBegin(Gl.GL_LINES);
			Gl.glVertex3d(0.0, 0.0, zAxisChange);
			Gl.glVertex3d(15.0 * (z11), 15.0 * (z12), zAxisChange);
			Gl.glVertex3d(0.0, 0.0, zAxisChange);
			Gl.glVertex3d(15.0 * (z21), 15.0 * (z22), zAxisChange);
			Gl.glVertex3d(0.0, 0.0, zAxisChange);
			Gl.glVertex3d(15.0 * (z31), 15.0 * (z32), zAxisChange);
			Gl.glEnd();
		}


		void Calc_w()
		{
			if (center < 100)
			{
				lambda = center / 100.0;
				w1 = (1 - lambda) * vertexHexagon[0, 0] + (lambda) * vertexHexagon[1, 0];
				w2 = (1 - lambda) * vertexHexagon[0, 1] + (lambda) * vertexHexagon[1, 1];
			}
			else if (center >= 100 && center < 200)
			{
				lambda = (200 - center) / 100.0;
				w1 = lambda * vertexHexagon[1, 0] + (1 - lambda) * vertexHexagon[2, 0];
				w2 = lambda * vertexHexagon[1, 1] + (1 - lambda) * vertexHexagon[2, 1];
			}
			else if (center >= 200 && center < 300)
			{
				lambda = (300 - center) / 100.0;
				w1 = lambda * vertexHexagon[2, 0] + (1 - lambda) * vertexHexagon[3, 0];
				w2 = lambda * vertexHexagon[2, 1] + (1 - lambda) * vertexHexagon[3, 1];
			}
			else if (center >= 300 && center < 400)
			{
				lambda = (400 - center) / 100.0;
				w1 = lambda * vertexHexagon[3, 0] + (1 - lambda) * vertexHexagon[4, 0];
				w2 = lambda * vertexHexagon[3, 1] + (1 - lambda) * vertexHexagon[4, 1];
			}
			else if (center >= 400 && center < 500)
			{
				lambda = (500 - center) / 100.0;
				w1 = lambda * vertexHexagon[4, 0] + (1 - lambda) * vertexHexagon[5, 0];
				w2 = lambda * vertexHexagon[4, 1] + (1 - lambda) * vertexHexagon[5, 1];
			}
			else if (center >= 500 && center <= 600)
			{
				lambda = (600 - center) / 100.0;
				w1 = lambda * vertexHexagon[5, 0] + (1 - lambda) * vertexHexagon[0, 0];
				w2 = lambda * vertexHexagon[5, 1] + (1 - lambda) * vertexHexagon[0, 1];
			}
		}

		void VertexHexagon()
		{
			vertexHexagon = new double[6, 2];

			if (angle1 < 0)
			{
				vertexHexagon[0, 0] = x3 + section * (x4 - x3);
				vertexHexagon[0, 1] = y3 + section * (y4 - y3);
				vertexHexagon[1, 0] = x1 + section * (x4 - x1);
				vertexHexagon[1, 1] = y1 + section * (y4 - y1);
				vertexHexagon[2, 0] = x1 + section * (x5 - x1);
				vertexHexagon[2, 1] = y1 + section * (y5 - y1);
				vertexHexagon[3, 0] = x2 + section * (x5 - x2);
				vertexHexagon[3, 1] = y2 + section * (y5 - y2);
				vertexHexagon[4, 0] = x2 + section * (x6 - x2);
				vertexHexagon[4, 1] = y2 + section * (y6 - y2);
				vertexHexagon[5, 0] = x3 + section * (x6 - x3);
				vertexHexagon[5, 1] = y3 + section * (y6 - y3);
			}
			else
			{
				vertexHexagon[0, 0] = x1 + section * (x6 - x1);
				vertexHexagon[0, 1] = y1 + section * (y6 - y1);
				vertexHexagon[1, 0] = x1 + section * (x4 - x1);
				vertexHexagon[1, 1] = y1 + section * (y4 - y1);
				vertexHexagon[2, 0] = x2 + section * (x4 - x2);
				vertexHexagon[2, 1] = y2 + section * (y4 - y2);
				vertexHexagon[3, 0] = x2 + section * (x5 - x2);
				vertexHexagon[3, 1] = y2 + section * (y5 - y2);
				vertexHexagon[4, 0] = x3 + section * (x5 - x3);
				vertexHexagon[4, 1] = y3 + section * (y5 - y3);
				vertexHexagon[5, 0] = x3 + section * (x6 - x3);
				vertexHexagon[5, 1] = y3 + section * (y6 - y3);
			}
		}

		double[,] vertexHexagonN;
		void VertexHexagonN()
		{
			vertexHexagon = new double[6, 2];

			if (angle1 < 0)
			{
				vertexHexagon[0, 0] = x3 + axisZ * (x4 - x3);
				vertexHexagon[0, 1] = y3 + axisZ * (y4 - y3);
				vertexHexagon[1, 0] = x1 + axisZ * (x4 - x1);
				vertexHexagon[1, 1] = y1 + axisZ * (y4 - y1);
				vertexHexagon[2, 0] = x1 + axisZ * (x5 - x1);
				vertexHexagon[2, 1] = y1 + axisZ * (y5 - y1);
				vertexHexagon[3, 0] = x2 + axisZ * (x5 - x2);
				vertexHexagon[3, 1] = y2 + axisZ * (y5 - y2);
				vertexHexagon[4, 0] = x2 + axisZ * (x6 - x2);
				vertexHexagon[4, 1] = y2 + axisZ * (y6 - y2);
				vertexHexagon[5, 0] = x3 + axisZ * (x6 - x3);
				vertexHexagon[5, 1] = y3 + axisZ * (y6 - y3);
			}
			else
			{
				vertexHexagon[0, 0] = x1 + axisZ * (x6 - x1);
				vertexHexagon[0, 1] = y1 + axisZ * (y6 - y1);
				vertexHexagon[1, 0] = x1 + axisZ * (x4 - x1);
				vertexHexagon[1, 1] = y1 + axisZ * (y4 - y1);
				vertexHexagon[2, 0] = x2 + axisZ * (x4 - x2);
				vertexHexagon[2, 1] = y2 + axisZ * (y4 - y2);
				vertexHexagon[3, 0] = x2 + axisZ * (x5 - x2);
				vertexHexagon[3, 1] = y2 + axisZ * (y5 - y2);
				vertexHexagon[4, 0] = x3 + axisZ * (x5 - x3);
				vertexHexagon[4, 1] = y3 + axisZ * (y5 - y3);
				vertexHexagon[5, 0] = x3 + axisZ * (x6 - x3);
				vertexHexagon[5, 1] = y3 + axisZ * (y6 - y3);
			}
		}

		void hexagonN(double[,] vertexHexagon,double z)
		{
			Gl.glColor3f(1.0f, 0.0f, 0.0f);
			Gl.glLineWidth(3);

			Gl.glBegin(Gl.GL_LINE_LOOP);
			Gl.glVertex3d(vertexHexagon[0, 0], vertexHexagon[0, 1], z);
			Gl.glVertex3d(vertexHexagon[1, 0], vertexHexagon[1, 1], z);
			Gl.glVertex3d(vertexHexagon[2, 0], vertexHexagon[2, 1], z);
			Gl.glVertex3d(vertexHexagon[3, 0], vertexHexagon[3, 1], z);
			Gl.glVertex3d(vertexHexagon[4, 0], vertexHexagon[4, 1], z);
			Gl.glVertex3d(vertexHexagon[5, 0], vertexHexagon[5, 1], z);
			Gl.glEnd();
		}
		void hexagon()
		{
			Gl.glColor3f(0.0f, 0.0f, 0.0f);
			Gl.glLineWidth(8);

			Gl.glBegin(Gl.GL_LINE_LOOP);
			Gl.glVertex3d(vertexHexagon[0, 0], vertexHexagon[0, 1], 10 * axisZ - 5);
			Gl.glVertex3d(vertexHexagon[1, 0], vertexHexagon[1, 1], 10 * axisZ - 5);
			Gl.glVertex3d(vertexHexagon[2, 0], vertexHexagon[2, 1], 10 * axisZ - 5);
			Gl.glVertex3d(vertexHexagon[3, 0], vertexHexagon[3, 1], 10 * axisZ - 5);
			Gl.glVertex3d(vertexHexagon[4, 0], vertexHexagon[4, 1], 10 * axisZ - 5);
			Gl.glVertex3d(vertexHexagon[5, 0], vertexHexagon[5, 1], 10 * axisZ - 5);
			Gl.glEnd();
		}

		void DrawVertex()
		{
			for (int i = 0; i < 6; i++)
			{
				//float k = i % 2;
				Gl.glColor3f(0.0f, 0.0f, 1.0f);
				Gl.glBegin(Gl.GL_POINTS);
				Gl.glVertex3d(vertexHexagon[i, 0], vertexHexagon[i, 1], zAxisChange);
				Gl.glEnd();
			}
		}

		void DrawPointC(double coordX, double coordY)
		{
			Gl.glColor3f(0.0f, 0.0f, 0.0f);
			Gl.glBegin(Gl.GL_POINTS);
			Gl.glVertex3d(coordX, coordY, zAxisChange);
			Gl.glEnd();
		}

		void CalcPointC1(double aX, double aY, double bX, double bY, out double[] array)
		{
			array = new double[3];
			double lambda = ((vertexHexagon[4, 0] - aX) * (bY - aY) - (vertexHexagon[4, 1] - aY) * (bX - aX)) / (z11 * (bY - aY) - z12 * (bX - aX));
			array[0] = vertexHexagon[4, 0] + lambda * (-z11);
			array[1] = vertexHexagon[4, 1] + lambda * (-z12);
			array[2] = (array[0] - aX) / (bX - aX);
		}

		void CalcPointC2(double aX, double aY, double bX, double bY, out double[] array)
		{
			array = new double[3];
			double lambda = ((vertexHexagon[0, 0] - aX) * (bY - aY) - (vertexHexagon[0, 1] - aY) * (bX - aX)) / (z21 * (bY - aY) - z22 * (bX - aX));
			array[0] = vertexHexagon[0, 0] + lambda * (-z21);
			array[1] = vertexHexagon[0, 1] + lambda * (-z22);
			array[2] = (array[0] - aX) / (bX - aX);
		}

		void CalcPointC3(double aX, double aY, double bX, double bY, out double[] array)
		{
			array = new double[3];
			double lambda = ((vertexHexagon[2, 0] - aX) * (bY - aY) - (vertexHexagon[2, 1] - aY) * (bX - aX)) / (z31 * (bY - aY) - z32 * (bX - aX));
			array[0] = vertexHexagon[2, 0] + lambda * (-z31);
			array[1] = vertexHexagon[2, 1] + lambda * (-z32);
			array[2] = (array[0] - aX) / (bX - aX);
		}

		
		void LyambdaToZero()
		{
			lyambdaI = new double[9, 3];
			for (int i = 0; i < 9; i++)
			{
				lyambdaI[i, 0] = 1;
				lyambdaI[i, 1] = 1;
				lyambdaI[i, 2] = 1;
			}
			for (int i=0;i<9;i++)
            {
				if(listPoints[i][0]==vertexHexagon[1,0] && listPoints[i][1] == vertexHexagon[1, 1])
                {
					int j = i-1;
					do
					{
						j++;
						lyambdaI[j, 2] = 0;
					}
					while (listPoints[j][0] != vertexHexagon[3, 0] && listPoints[j][1] != vertexHexagon[3, 1]);
                }

				if (listPoints[i][0] == vertexHexagon[3, 0] && listPoints[i][1] == vertexHexagon[3, 1])
				{
					int j = i-1;
					do
					{
						j++;
						lyambdaI[j, 0] = 0;
					}
					while (listPoints[j][0] != vertexHexagon[5, 0] && listPoints[j][1] != vertexHexagon[5, 1]);
				}
				
				if (listPoints[i][0] == vertexHexagon[0, 0] && listPoints[i][1] == vertexHexagon[0, 1])
				{
					int j = i-1;
					do
					{
						j++;
						lyambdaI[j, 1] = 0;
					}
					while (listPoints[j][0] != vertexHexagon[1, 0] && listPoints[j][1] != vertexHexagon[1, 1]);
				}
			}

			/*if (listPoints[7][0] == vertexHexagon[5, 0] && listPoints[7][1] == vertexHexagon[5, 1])
			{
				lyambdaI[7, 1] = 0;
				lyambdaI[8, 1] = 0;
			}
			else*/ if (listPoints[8][0] == vertexHexagon[5, 0] && listPoints[8][1] == vertexHexagon[5, 1])
			{
				lyambdaI[8, 1] = 0;
			}
			if (coefC1 == 0)
				lyambdaI[0, 1] = 0;
			if (coefC3 == 0)
				lyambdaI[0, 1] = 0;
		}

		double[,] lyambdaI;
		void FindLyambdaLenght()
		{
			var intersctnPoint = new double[3, 2];

			for (int i = 0; i < 9; i++)
			{
				if(lyambdaI[i,0]!=0)
                {
					if(i<coefC1)
                    {
						/*intersctnPoint[0, 1] = ((z11 / z12 * listPoints[i][1] - listPoints[i][0]) * (vertexHexagon[4, 1] - vertexHexagon[5, 1]) - vertexHexagon[5, 1] * (vertexHexagon[4, 0] - vertexHexagon[5, 0])) / (z11 / z12 * (vertexHexagon[4, 1] - vertexHexagon[5, 1]) - vertexHexagon[4, 0] + vertexHexagon[5, 0]);
						intersctnPoint[0, 0] = z11 / z12 * (intersctnPoint[0, 1] - listPoints[i][1]) + listPoints[i][0];
						lyambdaI[i, 0] = Norma(intersctnPoint[0, 0] - listPoints[i][0], intersctnPoint[0, 1] - listPoints[i][1]) / Z1;*/
						lyambdaI[i, 0] = ((listPoints[i][1] - vertexHexagon[4, 1]) * (vertexHexagon[5, 0] - vertexHexagon[4, 0]) - (listPoints[i][0] - vertexHexagon[4, 0]) * (vertexHexagon[5, 1] - vertexHexagon[4, 1])) / (z11 * (vertexHexagon[5, 1] - vertexHexagon[4, 1]) - z12 * (vertexHexagon[5, 0] - vertexHexagon[4, 0]));
					}
					else
                    {
						/*intersctnPoint[0, 1] = ((z11 / z12 * listPoints[i][1] - listPoints[i][0]) * (vertexHexagon[3, 1] - vertexHexagon[4, 1]) - vertexHexagon[4, 1] * (vertexHexagon[3, 0] - vertexHexagon[4, 0])) / (z11 / z12 * (vertexHexagon[3, 1] - vertexHexagon[4, 1]) - vertexHexagon[3, 0] + vertexHexagon[4, 0]);
						intersctnPoint[0, 0] = z11 / z12 * (intersctnPoint[0, 1] - listPoints[i][1]) + listPoints[i][0];
						lyambdaI[i, 0] = Norma(intersctnPoint[0, 0] - listPoints[i][0], intersctnPoint[0, 1] - listPoints[i][1]) / Z1;*/
						lyambdaI[i, 0] = ((listPoints[i][1] - vertexHexagon[3, 1]) * (vertexHexagon[4, 0] - vertexHexagon[3, 0]) - (listPoints[i][0] - vertexHexagon[3, 0]) * (vertexHexagon[4, 1] - vertexHexagon[3, 1])) / (z11 * (vertexHexagon[4, 1] - vertexHexagon[3, 1]) - z12 * (vertexHexagon[4, 0] - vertexHexagon[3, 0]));
					}
                }
				if(lyambdaI[i, 1] != 0)
                {
					if(i<coefC2)
                    {
						/*intersctnPoint[1, 1] = ((z21 / z22 * listPoints[i][1] - listPoints[i][0]) * (vertexHexagon[0, 1] - vertexHexagon[1, 1]) - vertexHexagon[1, 1] * (vertexHexagon[0, 0] - vertexHexagon[1, 0])) / (z11 / z12 * (vertexHexagon[0, 1] - vertexHexagon[1, 1]) - vertexHexagon[0, 0] + vertexHexagon[1, 0]);
						intersctnPoint[1, 0] = z21 / z22 * (intersctnPoint[1, 1] - listPoints[i][1]) + listPoints[i][0];
						lyambdaI[i, 1] = Norma(intersctnPoint[1, 0] - listPoints[i][0], intersctnPoint[1, 1] - listPoints[i][1]) / Z2;*/
						lyambdaI[i, 1] = ((listPoints[i][1] - vertexHexagon[0, 1]) * (vertexHexagon[1, 0] - vertexHexagon[0, 0]) - (listPoints[i][0] - vertexHexagon[0, 0]) * (vertexHexagon[1, 1] - vertexHexagon[0, 1])) / (z21 * (vertexHexagon[1, 1] - vertexHexagon[0, 1]) - z22 * (vertexHexagon[1, 0] - vertexHexagon[0, 0]));
					}
                    else
                    {
						/*intersctnPoint[1, 1] = ((z21 / z22 * listPoints[i][1] - listPoints[i][0]) * (vertexHexagon[5, 1] - vertexHexagon[0, 1]) - vertexHexagon[0, 1] * (vertexHexagon[5, 0] - vertexHexagon[0, 0])) / (z11 / z12 * (vertexHexagon[5, 1] - vertexHexagon[0, 1]) - vertexHexagon[5, 0] + vertexHexagon[0, 0]);
						intersctnPoint[1, 0] = z21 / z22 * (intersctnPoint[1, 1] - listPoints[i][1]) + listPoints[i][0];
						lyambdaI[i, 1] = Norma(intersctnPoint[1, 0] - listPoints[i][0], intersctnPoint[1, 1] - listPoints[i][1]) / Z2;*/
						lyambdaI[i, 1] = ((listPoints[i][1] - vertexHexagon[5, 1]) * (vertexHexagon[0, 0] - vertexHexagon[5, 0]) - (listPoints[i][0] - vertexHexagon[5, 0]) * (vertexHexagon[0, 1] - vertexHexagon[5, 1])) / (z21 * (vertexHexagon[0, 1] - vertexHexagon[5, 1]) - z22 * (vertexHexagon[0, 0] - vertexHexagon[5, 0]));
					}
                }
				if (lyambdaI[i, 2] != 0)
                {
					if (coefC3 > 5 && i > 5 && i < coefC3)
					{
						lyambdaI[i, 2] = ((listPoints[i][1] - vertexHexagon[2, 1]) * (vertexHexagon[3, 0] - vertexHexagon[2, 0]) - (listPoints[i][0] - vertexHexagon[2, 0]) * (vertexHexagon[3, 1] - vertexHexagon[2, 1])) / (z31 * (vertexHexagon[3, 1] - vertexHexagon[2, 1]) - z32 * (vertexHexagon[3, 0] - vertexHexagon[2, 0]));
					}
					else if (coefC3 < 5 && i > 5)
					{
						lyambdaI[i, 2] = ((listPoints[i][1] - vertexHexagon[2, 1]) * (vertexHexagon[3, 0] - vertexHexagon[2, 0]) - (listPoints[i][0] - vertexHexagon[2, 0]) * (vertexHexagon[3, 1] - vertexHexagon[2, 1])) / (z31 * (vertexHexagon[3, 1] - vertexHexagon[2, 1]) - z32 * (vertexHexagon[3, 0] - vertexHexagon[2, 0]));
						//lyambdaI[i, 2] = ((listPoints[i][0] - vertexHexagon[2, 0]) * (vertexHexagon[3, 1] - vertexHexagon[2, 1]) - (listPoints[i][1] - vertexHexagon[2, 1]) * (vertexHexagon[3, 0] - vertexHexagon[2, 0])) / (z31 * (vertexHexagon[3, 1] - vertexHexagon[2, 1]) - z32 * (vertexHexagon[3, 0] - vertexHexagon[2, 0]));
					}
					else
					{
						lyambdaI[i, 2] = ((listPoints[i][1] - vertexHexagon[1, 1]) * (vertexHexagon[2, 0] - vertexHexagon[1, 0]) - (listPoints[i][0] - vertexHexagon[1, 0]) * (vertexHexagon[2, 1] - vertexHexagon[1, 1])) / (z31 * (vertexHexagon[2, 1] - vertexHexagon[1, 1]) - z32 * (vertexHexagon[2, 0] - vertexHexagon[1, 0]));
						//lyambdaI[i, 2] = ((listPoints[i][0] - vertexHexagon[1, 0]) * (vertexHexagon[2, 1] - vertexHexagon[1, 1]) - (listPoints[i][1] - vertexHexagon[1, 1]) * (vertexHexagon[2, 0] - vertexHexagon[1, 0])) / (z31 * (vertexHexagon[2, 1] - vertexHexagon[1, 1]) - z32 * (vertexHexagon[2, 0] - vertexHexagon[1, 0]));
					}
				}
			}
		}

		void FindDelta()
		{
			LyambdaToZero();
			FindLyambdaLenght();
			//richTextBox3.Text = "";
			var delta = new double[9];
			var g = 1;
			for (int i = 0; i < 9; i++)
			{
				double max1 = 0;
				int coef1 = 0;
				double max2 = 0;
				int coef2 = 0;

				for (int j = 0; j < 3; j++)
				{
					if (lyambdaI[i, j] > max1)
					{
						max1 = lyambdaI[i, j];
						coef1 = j;
					}
					if (lyambdaI[g, j] > max2)
					{
						max2 = lyambdaI[g, j];
						coef2 = j;
					}
				}

				if (coef1 == coef2)
				{
					delta[i] = Math.Min(max1, max2);
					//richTextBox3.Text += delta[i] + "\n";
					//richTextBox3.Text += "\n";
				}
				else
				{
					double alpha1, interPoint = 0;
					if (lyambdaI[i, 0] == 0 && lyambdaI[g, 0] == 0)
					{
						alpha1 = (lyambdaI[i, 2] - lyambdaI[i, 1]) / (lyambdaI[g, 1] - lyambdaI[i, 1] - lyambdaI[g, 2] + lyambdaI[i, 2]);
						interPoint = alpha1 * lyambdaI[g, 1] + (1 - alpha1) * lyambdaI[i, 1];
					}
					else if (lyambdaI[i, 1] == 0 && lyambdaI[g, 1] == 0)
					{
						alpha1 = (lyambdaI[i, 2] - lyambdaI[i, 0]) / (lyambdaI[g, 0] - lyambdaI[i, 0] - lyambdaI[g, 2] + lyambdaI[i, 2]);
						interPoint = alpha1 * lyambdaI[g, 0] + (1 - alpha1) * lyambdaI[i, 0];
					}
					else if (lyambdaI[i, 2] == 0 && lyambdaI[g, 2] == 0)
					{
						alpha1 = (lyambdaI[i, 1] - lyambdaI[i, 0]) / (lyambdaI[g, 0] - lyambdaI[i, 0] - lyambdaI[g, 1] + lyambdaI[i, 1]);
						interPoint = alpha1 * lyambdaI[g, 1] + (1 - alpha1) * lyambdaI[i, 1];
					}
					delta[i] = Math.Min(interPoint, Math.Min(max1, max2));
					//richTextBox3.Text += delta[i] + "\n";
				}

				if (g == 8)
					g = 0;
				else
					g++;
			}
			Delta.Add(MinElement(delta));
		}

		double MinElement(double[] mass)
		{
			double min = mass[0];
			for (int i = 0; i < mass.Length; i++)
			{
				if (min > mass[i])
					min = mass[i];
			}
			return min;
		}

		List<double> Delta = new List<double>();
		int indexMin=0;
		double MinDelta(List<double> list)
        {
			indexMin = 0;
			double min = list[0];
			for (int i = 0; i < list.Count; i++)
			{
				if (min > list[i])
				{
					min = list[i];
					indexMin = i;
				}
			}
			return min;
		}

		/*void DeltaInTriangleTop()
		{
			double a, b, c;
			double[,] intersctnPoint = new double[3, 2];
			double[] lyambdaInTriangle = new double[3];

			intersctnPoint[0, 0] = ((A22 - A21 - B22) * (C21 - B21) + B21 * (C22 - B22)) / (C22 - B22 - C21 + B21);
			intersctnPoint[0, 1] = intersctnPoint[0, 0] - A21 + A22;
			a = Norma(intersctnPoint[0, 0] - A21, intersctnPoint[0, 1] - A22) / Z1;

			intersctnPoint[1, 0] = ((C22 + C21 - B22) * (A21 - B21) + B21 * (A22 - B22)) / (A22 - B22 + A21 - B21);
			intersctnPoint[1, 1] = -intersctnPoint[1, 0] + C21 + C22;
			b = Norma(intersctnPoint[1, 0] - C21, intersctnPoint[1, 1] - C22) / Z2;

			intersctnPoint[2, 0] = B21;
			intersctnPoint[2, 1] = (B21 - A21) / (C21 - A21) * (C22 - A22) + A22;
			c = Norma(intersctnPoint[2, 0] - B21, intersctnPoint[2, 1] - B22) / Z3;

			lyambdaInTriangle[0] = b / (a + b) * a;
			lyambdaInTriangle[1] = c / (a + c) * a;
			lyambdaInTriangle[2] = c / (b + c) * b;

			Delta.Add(Math.Min(lyambdaInTriangle[0],Math.Min(lyambdaInTriangle[1], lyambdaInTriangle[2])));
		}*/

		void DeltaInTriangleTop()
		{
			double a, b, c;
			double[,] intersctnPoint = new double[3, 2];
			double[] lyambdaInTriangle = new double[3];

			//intersctnPoint[0, 1] = ((z11 / z12 * A22 - A21) * (B22 - C22) - C22 * (B21 - C21)) / (z11 / z12 * (B22 - C22) - B21 + C21);
			//intersctnPoint[0, 0] = z11 / z12 * (intersctnPoint[0, 1] - A22) + A21;
			a = ((A22 - B22) * (C21 - B21) - (A21 - B21) * (C22 - B22)) / (z11 * (C22 - B22) - z12 * (C21 - B21));

			//intersctnPoint[1, 1] = ((z11 / z12 * B22 - B21) * (A22 - C22) - C22 * (A21 - C21)) / (z11 / z12 * (A22 - C22) - A21 + C21);
			//intersctnPoint[1, 0] = z11 / z12 * (intersctnPoint[0, 1] - B22) + B21;
			b = ((C22 - A22) * (B21 - A21) - (C21 - A21) * (B22 - A22)) / (z21 * (B22 - A22) - z22 * (B21 - A21));

			//intersctnPoint[2, 0] = B21;
			//intersctnPoint[2, 1] = (B21 - A21) / (C21 - A21) * (C22 - A22) + A22;
			c = ((B22 - A22) * (C21 - A21) - (B21 - A21) * (C22 - A22)) / (z31 * (C22 - A22) - z32 * (C21 - A21));

			lyambdaInTriangle[0] = b / (a + b) * a;
			lyambdaInTriangle[1] = c / (a + c) * a;
			lyambdaInTriangle[2] = c / (b + c) * b;

			Delta.Add(Math.Min(lyambdaInTriangle[0], Math.Min(lyambdaInTriangle[1], lyambdaInTriangle[2])));
		}

		void DeltaInTriangleBot()
		{
			double a, b, c;
			double[] lyambdaInTriangle = new double[3];

			a = ((A12 - B12) * (C11 - B11) - (A11 - B11) * (C12 - B12)) / (z11 * (C12 - B12) - z12 * (C11 - B11));
			b = ((C12 - A12) * (B11 - A11) - (C11 - A11) * (B12 - A12)) / (z21 * (B12 - A12) - z22 * (B11 - A11));
			c = ((B12 - A12) * (C11 - A11) - (B11 - A11) * (C12 - A12)) / (z31 * (C12 - A12) - z32 * (C11 - A11));

			lyambdaInTriangle[0] = b / (a + b) * a;
			lyambdaInTriangle[1] = c / (a + c) * a;
			lyambdaInTriangle[2] = c / (b + c) * b;

			Delta.Add(Math.Min(lyambdaInTriangle[0], Math.Min(lyambdaInTriangle[1], lyambdaInTriangle[2])));
		}

		/*void DeltaInTriangleBot()
		{
			double a, b, c;
			double[,] intersctnPoint = new double[3, 2];
			double[] lyambdaInTriangle = new double[3];

			intersctnPoint[0, 0] = ((A12 - A11 - B12) * (C11 - B11) + B11 * (C12 - B12)) / (C12 - B12 - C11 + B11);
			intersctnPoint[0, 1] = intersctnPoint[0, 0] - A11 + A12;
			a = Norma(intersctnPoint[0, 0] - A11, intersctnPoint[0, 1] - A12) / Z1;

			intersctnPoint[1, 0] = ((C12 + C11 - B12) * (A11 - B11) + B11 * (A12 - B12)) / (A12 - B12 + A11 - B11);
			intersctnPoint[1, 1] = -intersctnPoint[1, 0] + C11 + C12;
			b = Norma(intersctnPoint[1, 0] - C11, intersctnPoint[1, 1] - C12) / Z2;

			intersctnPoint[2, 0] = B11;
			intersctnPoint[2, 1] = (B11 - A11) / (C11 - A11) * (C12 - A12) + A12;
			c = Norma(intersctnPoint[2, 0] - B11, intersctnPoint[2, 1] - B12) / Z3;

			lyambdaInTriangle[0] = b / (a + b) * a;
			lyambdaInTriangle[1] = c / (a + c) * a;
			lyambdaInTriangle[2] = c / (b + c) * b;
			*//*lyambdaInTriangle[0] = Math.Min(b / (a + b) * a, (1 - b / (a + b)) * b);
			lyambdaInTriangle[1] = Math.Min(c / (a + c) * a, (1 - c / (a + c)) * c);
			lyambdaInTriangle[2] = Math.Min(c / (b + c) * b, (1 - c / (b + c)) * c);*//*

			Delta.Add(Math.Min(lyambdaInTriangle[0], Math.Min(lyambdaInTriangle[1], lyambdaInTriangle[2])));
		}*/

		//void PointsC()
		//{
		//	double[] C1, C2, C3;
		//	int k = 5;
		//	for (var i = 0; i < 4; i++)
		//	{
		//		CalcPointC1(vertexHexagon[k, 0], vertexHexagon[k, 1], vertexHexagon[i, 0], vertexHexagon[i, 1], out C1);
		//		if (C1[2] >= 0 && C1[2] <= 1)
		//		{
		//			if (checkBox5.Checked)
		//				DrawPointC(C1[0], C1[1]);
		//			listPoints.Insert(i, new[] { C1[0], C1[1] });
		//			coefC1 = i;
		//		}
		//		if (k == 5) k = 0;
		//		else k++;
		//	}

		//	for (var i = 1; i < 5; i++)
		//	{
		//		CalcPointC2(vertexHexagon[i, 0], vertexHexagon[i, 1], vertexHexagon[i + 1, 0], vertexHexagon[i + 1, 1], out C2);
		//		if (C2[2] >= 0 && C2[2] <= 1)
		//		{
		//			if (checkBox5.Checked)
		//				DrawPointC(C2[0], C2[1]);
		//			listPoints.Insert(i + 2, new[] { C2[0], C2[1] });
		//			coefC2 = i + 2;
		//		}
		//	}

		//	k = 3;
		//	int k1 = 4;
		//	for (var i = 0; i < 2; i++)
		//	{
		//		CalcPointC3(vertexHexagon[k, 0], vertexHexagon[k, 1], vertexHexagon[k1, 0], vertexHexagon[k1, 1], out C3);
		//		if (C3[2] >= 0 && C3[2] <= 1)
		//		{
		//			if (checkBox5.Checked)
		//				DrawPointC(C3[0], C3[1]);
		//			listPoints.Insert(k1 + 2, new[] { C3[0], C3[1] });
		//			coefC3 = k1 + 2;
		//		}
		//		if (k == 5) k = 0;
		//		else k++;
		//		if (k1 == 5) k1 = 0;
		//		else k1++;
		//	}
		//	for (var i = 2; i < 4; i++)
		//	{
		//		CalcPointC3(vertexHexagon[k, 0], vertexHexagon[k, 1], vertexHexagon[k1, 0], vertexHexagon[k1, 1], out C3);
		//		if (C3[2] >= 0 && C3[2] <= 1)
		//		{
		//			if (checkBox5.Checked)
		//				DrawPointC(C3[0], C3[1]);
		//			listPoints.Insert(k1, new[] { C3[0], C3[1] });
		//			coefC3 = k1;
		//		}
		//		if (k == 5) k = 0;
		//		else k++;
		//		if (k1 == 5) k1 = 0;
		//		else k1++;
		//	}
		//}
		void PointsC()
		{
			double[] C1, C2, C3;

				int k = 5;
				for (var i = 0; i < 4; i++)
				{
					CalcPointC1(vertexHexagon[k, 0], vertexHexagon[k, 1], vertexHexagon[i, 0], vertexHexagon[i, 1], out C1);
					if (C1[2] >= 0 && C1[2] <= 1)
					{
						if (checkBox5.Checked)
							DrawPointC(C1[0], C1[1]);
						listPoints.Insert(i, new[] { C1[0], C1[1] });
						coefC1 = i;
					}
					if (k == 5) k = 0;
					else k++;
				}

				for (var i = 1; i < 5; i++)
				{
					CalcPointC2(vertexHexagon[i, 0], vertexHexagon[i, 1], vertexHexagon[i + 1, 0], vertexHexagon[i + 1, 1], out C2);
					if (C2[2] >= 0 && C2[2] <= 1)
					{
						if (checkBox5.Checked)
							DrawPointC(C2[0], C2[1]);
						listPoints.Insert(i + 2, new[] { C2[0], C2[1] });
						coefC2 = i + 2;
					}
				}

				k = 3;
				int k1 = 4;
            for (var i = 0; i < 2; i++)
            {
                CalcPointC3(vertexHexagon[k, 0], vertexHexagon[k, 1], vertexHexagon[k1, 0], vertexHexagon[k1, 1], out C3);
                if (C3[2] >= 0 && C3[2] <= 1)
                {
                    if (checkBox5.Checked)
                        DrawPointC(C3[0], C3[1]);
                    listPoints.Insert(k1 + 2, new[] { C3[0], C3[1] });
                    coefC3 = k1 + 2;
                }
                if (k == 5) k = 0;
                else k++;
                if (k1 == 5) k1 = 0;
                else k1++;
            }
			/*CalcPointC3(vertexHexagon[k, 0], vertexHexagon[k, 1], vertexHexagon[k1, 0], vertexHexagon[k1, 1], out C3);
			if (C3[2] >= 0 && C3[2] <= 1)
			{
				if (checkBox5.Checked)
					DrawPointC(C3[0], C3[1]);
				listPoints.Insert(8, new[] { C3[0], C3[1] });
				coefC3 = 8;
			}
			if (k == 5) k = 0;
			else k++;
			if (k1 == 5) k1 = 0;
			else k1++;*/

			for (var i = 2; i < 4; i++)
            {
                CalcPointC3(vertexHexagon[k, 0], vertexHexagon[k, 1], vertexHexagon[k1, 0], vertexHexagon[k1, 1], out C3);
                if (C3[2] >= 0 && C3[2] <= 1)
                {
                    if (checkBox5.Checked)
                        DrawPointC(C3[0], C3[1]);
                    listPoints.Insert(k1, new[] { C3[0], C3[1] });
                    coefC3 = k1;
                }
                if (k == 5) k = 0;
                else k++;
                if (k1 == 5) k1 = 0;
                else k1++;
            }
        }

		List<double[]> listPoints;

		void ListPoints()
		{
			listPoints = new List<double[]>();
			for (var i = 0; i < 6; i++)
			{
				listPoints.Add(new[] { vertexHexagon[i, 0], vertexHexagon[i, 1] });
			}
		}

		void AnTLoad(object sender, EventArgs e)
		{

		}
		private void trackBar4_Scroll(object sender, EventArgs e)
		{
			//FullDraw();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			//CalcMinDelta();
			double asdada = 0;
		}

		void CalcMinDelta()
		{
			double n = trackBar6.Value;
			label4.Text = (n+1).ToString();
			axisZ = 0;
			zAxisChange = -5;
			VH = new double[(int)n - 1][,];
			aZ = new double[(int)n - 1];
			DeltaInTriangleBot();
			axisZ += 1 / n;
			for (int i = 1; i < n; i++)
			{
				VertexHexagonN();
				VH[i-1] = vertexHexagon;
				aZ[i-1] = 10 * axisZ - 5;
				ListPoints();
				PointsC();

				FindDelta();

				axisZ += 1 / n;
				zAxisChange = 10 * axisZ - 5;
			}
			DeltaInTriangleTop();

			//VertexHexagonN();
			//hexagon();
			richTextBox2.Text = MinDelta(Delta).ToString() + "\n" + "\n";
			axisZ = indexMin * 1 / n;
			zAxisChange = 10 * axisZ - 5;
			if (checkBox1.Checked)
			{
				for (int i = 0; i < Delta.Count; i++)
				{
					richTextBox2.Text += Delta[i] + "\n";
				}
			}
			Delta.Clear();
		}


		double[][,] VH;
		double[] aZ;
		double axisZ;
		void Timer1Tick(object sender, EventArgs e)
		{
			FullDraw();
		}

		void FullDraw()
        {
			//CalcAngles();
			CalcMinDelta();

			a = Convert.ToDouble(trackBar1.Value) / 100;
			b = Convert.ToDouble(trackBar2.Value) / 100;
			angle = Convert.ToDouble(trackBar3.Value) / 100;
			//section = Convert.ToDouble(trackBar4.Value) / 100;
			//center = Convert.ToDouble(trackBar5.Value);
			label1.Text = " " + trackBar1.Value;
			label2.Text = " " + trackBar2.Value;

			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
			calc();
			CalcTriangleTop();


			Gl.glPushMatrix();
			Glu.gluLookAt(x, y, z, 0, 0, 0, 0, 0, 1);
			if (checkBox2.Checked)
			{
				TriangleBot();
				TriangleTop();
				Edge();
			}
			if (checkBox5.Checked)
				DrawVertex();

			for (int i = 0; i < aZ.Length; i++)
			{
				hexagonN(VH[i], aZ[i]);
			}

			VertexHexagonN();
			hexagon();
			/*double n = trackBar6.Value;
			axisZ = 0;
			zAxisChange = -5;

			DeltaInTriangleBot();
			for (int i = 0; i < n + 1; i++)
			{
				VertexHexagonN();
				hexagonN();
				ListPoints();
				PointsC();
				if (zAxisChange > -5 && zAxisChange < 5)
				{
					FindDelta();
				}

				axisZ += 1 / n;
				zAxisChange = 10 * axisZ - 5;
			}
			DeltaInTriangleTop();
			axisZ = indexMin * 1 / n;
			zAxisChange = 10 * axisZ - 5;
			VertexHexagonN();
			hexagon();
			richTextBox2.Text = MinDelta(Delta).ToString();
			Delta.Clear();*/
			//richTextBox2.Clear();
			//coordinates();

			if (checkBox3.Checked)
			{
				//hexagon();
			}
			//ListPoints();
			//PointsC();

			//DrawPoints();

			//CalcPointsC();
			//DrawPointsC();
			//CalcDelta();
			//CalcLyambda();

			Calc_w();
			Gl.glTranslated(w1, w2, 0);
			if (checkBox4.Checked)
			{
				vector();
			}
			Gl.glPopMatrix();


			AnT.Invalidate();
		}
	}
}