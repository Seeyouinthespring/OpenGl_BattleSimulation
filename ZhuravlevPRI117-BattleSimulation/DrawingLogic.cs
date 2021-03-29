using Tao.OpenGl;

namespace ZhuravlevPRI117_BattleSimulation
{
    public static class DrawingLogic
    {
        //метод рисования моделей объектов
        public static void DrawElements() 
        {
            // рисование красной пирамиды вершиной вниз
            Gl.glNewList(1, Gl.GL_COMPILE);
            Gl.glColor3f(1, 0, 0);

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0.5, 0.5f, 2);
            Gl.glVertex3d(0.5f, -0.5f, 2);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0.5, -0.5f, 2);
            Gl.glVertex3d(-0.5f, -0.5f, 2);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(-0.5, -0.5f, 2);
            Gl.glVertex3d(-0.5f, 0.5f, 2);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(-0.5, 0.5f, 2);
            Gl.glVertex3d(0.5f, 0.5f, 2);
            Gl.glEnd();

            Gl.glEndList();


            // рисование синей пирамиды вершиной вниз
            Gl.glNewList(2, Gl.GL_COMPILE);
            Gl.glColor3f(0, 0, 1);

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0.5, 0.5f, 2);
            Gl.glVertex3d(0.5f, -0.5f, 2);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0.5, -0.5f, 2);
            Gl.glVertex3d(-0.5f, -0.5f, 2);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(-0.5, -0.5f, 2);
            Gl.glVertex3d(-0.5f, 0.5f, 2);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(-0.5, 0.5f, 2);
            Gl.glVertex3d(0.5f, 0.5f, 2);
            Gl.glEnd();

            Gl.glEndList();


            // рисование красной пирамиды вершиной вверх
            Gl.glNewList(3, Gl.GL_COMPILE);
            Gl.glColor3f(1, 0, 0);

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 2);
            Gl.glVertex3d(0.5, 0.5f, 0);
            Gl.glVertex3d(0.5f, -0.5f, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 2);
            Gl.glVertex3d(0.5, -0.5f, 0);
            Gl.glVertex3d(-0.5f, -0.5f, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 2);
            Gl.glVertex3d(-0.5, -0.5f, 0);
            Gl.glVertex3d(-0.5f, 0.5f, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 2);
            Gl.glVertex3d(-0.5, 0.5f, 0);
            Gl.glVertex3d(0.5f, 0.5f, 0);
            Gl.glEnd();

            Gl.glEndList();


            // рисование синей пирамиды вершиной вверх
            Gl.glNewList(4, Gl.GL_COMPILE);
            Gl.glColor3f(0, 0, 1);

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 2);
            Gl.glVertex3d(0.5, 0.5f, 0);
            Gl.glVertex3d(0.5f, -0.5f, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 2);
            Gl.glVertex3d(0.5, -0.5f, 0);
            Gl.glVertex3d(-0.5f, -0.5f, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 2);
            Gl.glVertex3d(-0.5, -0.5f, 0);
            Gl.glVertex3d(-0.5f, 0.5f, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 2);
            Gl.glVertex3d(-0.5, 0.5f, 0);
            Gl.glVertex3d(0.5f, 0.5f, 0);
            Gl.glEnd();

            Gl.glEndList();


            // рисование красной стрелы направленной вправо
            Gl.glNewList(5, Gl.GL_COMPILE);
            Gl.glColor3f(1, 0, 0);

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(-0.5f, 0, 0.3f);
            Gl.glVertex3d(-0.5f, 0.3f, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(-0.5, 0.3f, 0);
            Gl.glVertex3d(-0.5f, 0, -0.3f);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(-0.5, 0, -0.3f);
            Gl.glVertex3d(-0.5f, -0.3f, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(-0.5f, -0.3f, 0);
            Gl.glVertex3d(-0.5f, 0, 0.3f);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 0.1f, 0);
            Gl.glVertex3d(-2.5f, 0.1f, 0);
            Gl.glVertex3d(-2.5f, -0.1f, 0);
            Gl.glVertex3d(0, -0.1f, 0);
            Gl.glEnd();

            Gl.glEndList();


            // рисование синей стрелы направленной влево
            Gl.glNewList(6, Gl.GL_COMPILE);
            Gl.glColor3f(0, 0, 1);

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0.5f, 0, 0.3f);
            Gl.glVertex3d(0.5f, 0.3f, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0.5, 0.3f, 0);
            Gl.glVertex3d(0.5f, 0, -0.3f);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0.5, 0, -0.3f);
            Gl.glVertex3d(0.5f, -0.3f, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex3d(0, 0, 0);
            Gl.glVertex3d(0.5f, -0.3f, 0);
            Gl.glVertex3d(0.5f, 0, 0.3f);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex3d(0, 0.1f, 0);
            Gl.glVertex3d(2.5f, 0.1f, 0);
            Gl.glVertex3d(2.5f, -0.1f, 0);
            Gl.glVertex3d(0, -0.1f, 0);
            Gl.glEnd();

            Gl.glEndList();
        }

    }
}
