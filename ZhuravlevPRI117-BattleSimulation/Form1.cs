using System;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using System.Linq;
using static ZhuravlevPRI117_BattleSimulation.HelpEnums;

namespace ZhuravlevPRI117_BattleSimulation
{
    public partial class Form1 : Form
    {
        //объект сражения
        private Battle Battle = null;
        //общее время сцены
        float GlobalTime = 0;
        //массив с данными о точках обзора
        private float[,] CameraPosition = new float[5, 7];

        public Form1()
        {
            InitializeComponent();
            mainScene.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, mainScene.Width, mainScene.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Glu.gluPerspective(45, (float)mainScene.Width / (float)mainScene.Height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);

            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glLineWidth(1.0f);

            //Установка значений для поворота и переноса точки обзора
            CameraPosition[0, 0] = 0;
            CameraPosition[0, 1] = 2;
            CameraPosition[0, 2] = -45;
            CameraPosition[0, 3] = -45;
            CameraPosition[0, 4] = 1;
            CameraPosition[0, 5] = 0;
            CameraPosition[0, 6] = 0;

            CameraPosition[1, 0] = 0;
            CameraPosition[1, 1] = 0;
            CameraPosition[1, 2] = -50;
            CameraPosition[1, 3] = -80;
            CameraPosition[1, 4] = 1;
            CameraPosition[1, 5] = 0;
            CameraPosition[1, 6] = 0;

            CameraPosition[2, 0] = 0;
            CameraPosition[2, 1] = 0;
            CameraPosition[2, 2] = -45;
            CameraPosition[2, 3] = 0;
            CameraPosition[2, 4] = 1;
            CameraPosition[2, 5] = 0;
            CameraPosition[2, 6] = 0;

            CameraPosition[3, 0] = 0;
            CameraPosition[3, 1] = 3;
            CameraPosition[3, 2] = -55;
            CameraPosition[3, 3] = -90;
            CameraPosition[3, 4] = 0;
            CameraPosition[3, 5] = 0;
            CameraPosition[3, 6] = 1;

            CameraPosition[4, 0] = 0;
            CameraPosition[4, 1] = 3;
            CameraPosition[4, 2] = -55;
            CameraPosition[4, 3] = 90;
            CameraPosition[4, 4] = 0;
            CameraPosition[4, 5] = 0;
            CameraPosition[4, 6] = 1;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GlobalTime += (float)timer1.Interval / 1000;
            label1.Text = Math.Round(GlobalTime, 2).ToString();
            Draw();
            label12.Text = Battle?.AliveReds?.Length.ToString();
            label13.Text = Battle?.AliveBlues?.Length.ToString();
            label14.Text = Battle?.AllUnits?.Where(x => x.Team == Team.Red).Sum(x => x.Health).ToString();
            label15.Text = Battle?.AllUnits?.Where(x => x.Team == Team.Blue).Sum(x => x.Health).ToString();
        }

        private void Draw()
        {

            Gl.glClearColor(255, 255, 255, 1);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glColor3d(0, 0, 0);
            Gl.glPushMatrix();

            //определение точки обзора
            int camera = comboBox1.SelectedIndex;
            Gl.glTranslated(CameraPosition[camera, 0], CameraPosition[camera, 1], CameraPosition[camera, 2]);
            if (camera > 2) 
            {
                //операции последовательного поворота системы координат по разным осям
                Gl.glRotated(CameraPosition[camera, 3], CameraPosition[camera, 4], CameraPosition[camera, 5], CameraPosition[camera, 6]);
                Gl.glRotated(60 * CameraPosition[camera, 3]/90, 0, 1, 0);
            }
            else
                Gl.glRotated(CameraPosition[camera, 3], CameraPosition[camera, 4], CameraPosition[camera, 5], CameraPosition[camera, 6]);

            Gl.glPushMatrix();

            DrawMatrix(34,50);

            if (Battle != null)
            {
                //Расчет логики для Юнитов
                int ? result = Battle.Calculate(GlobalTime);
                if (result == 0)
                {
                    //обработка победы красной армии
                    timer1.Stop();
                    label6.Visible = true;
                    label6.Text = "RED WINS!";
                    label6.ForeColor = System.Drawing.Color.Red;
                }
                if (result == 1)
                {
                    //обработка победы синей армии
                    timer1.Stop();
                    label6.Visible = true;
                    label6.Text = "BLUE WINS!";
                    label6.ForeColor = System.Drawing.Color.Blue;
                }
                //Расчет логики для стрел
                Battle.CalculateArrows(GlobalTime);
            }

            Gl.glPopMatrix();
            Gl.glPopMatrix();

            Gl.glFlush();
            mainScene.Invalidate();
        }

        private void DrawMatrix(int x, int y)
        {
            //Gl.glBegin(Gl.GL_LINES);

            //for (int ax = 0; ax < y + 1; ax++)
            //{
            //    Gl.glVertex3d(ax - 25, -14, 0);
            //    Gl.glVertex3d(ax - 25, x - 14, 0);
            //}

            //for (int bx = 0; bx < x + 1; bx++)
            //{
            //    Gl.glVertex3d(-25, bx - 14, 0);
            //    Gl.glVertex3d(y - 25, bx - 14, 0);
            //}

            //Gl.glEnd(); 


            //рисование плоскости поля сражения 
            Gl.glColor3f(0, 1, 0);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(-28, -16, -0.5);
            Gl.glVertex3d(-28, 16, -0.5);
            Gl.glVertex3d(28 , 16, -0.5);
            Gl.glVertex3d(28, -16, -0.5);

            Gl.glEnd();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label6.Visible = false;
            timer1.Start();
            label1.Visible = true;
            GlobalTime = 0;
            button2.Visible = true;
            //Создание объекта сражения 
            Battle = new Battle(20 - trackBar1.Value, 20 - trackBar2.Value, comboBox3.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), GlobalTime);
            Battle.StartBattle();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = "Red Warriors: " + trackBar1.Value.ToString();
            label3.Text = "Red Archers: " + (20-trackBar1.Value).ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label5.Text = "Blue Warriors: " + trackBar2.Value.ToString();
            label4.Text = "Blue Archers: " + (20 - trackBar2.Value).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            button2.Visible = false;
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            button2.Visible = true;
            button3.Visible = false;
        }
    }
}
