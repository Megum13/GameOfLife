using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
  

    public partial class Form1 : Form
    {
        private static int size = 7;
        private static bool[,] coordinates;

        //scroll
        private static int scrollSize = 14;
        private static int scrollButtonPixel = 35;


        private static int[,] coordinates3;//для переливаний
               
        private static Graphics g;
        private static int cycle = 0;
                
        private static int pWidth;
        private static int pHeight;
               

        private static int choice = 0;
        private List<Ant> ant = new List<Ant>();

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            size = trackBar1.Value;
        }

        
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDown1.Value;
        }

        #region B1 , S2345678, фрактал, поле зрения - 8 (1)
        private void button1_Click(object sender, EventArgs e)
        {
            B1S();
        }

        private void B1S()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width/size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);

            coordinates[30, 30] = true;
            choice = 1;

            timer1.Start();
        }

        private void B1SNextGeneration()
        {
            g.Clear(Color.White);

            bool[,] newCoordinates = new bool[pWidth, pHeight];

            for (int tX = 0; tX < pWidth; tX++)
            {
                for (int tY = 0; tY < pHeight; tY++)
                {
                    if (!coordinates[tX, tY] && PovCount8(tX, tY) == 1)//правила
                    {
                        newCoordinates[tX, tY] = true;
                    }
                    //else if (coordinates[tX, tY] && PovCount8(tX, tY) > 2 )
                    //{
                    //    newCoordinates[tX, tY] = false;
                    //}
                    else
                    {
                        newCoordinates[tX, tY] = coordinates[tX, tY];
                    }

                    if (coordinates[tX, tY])
                    {
                         g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                    }

                }
            }

            coordinates = newCoordinates;
            pictureBox1.Refresh();
            
        }

        #endregion

        #region B3 , S1345678, Оригинальная клетка, поле зрения - 8 (2)
        private void button2_Click(object sender, EventArgs e)
        {
            
            B3S1();
        }

        private void B3S1()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);


            for (int x = 0; x < pWidth; x++)
            {
                for (int y = 0; y < pHeight; y++)
                {
                    coordinates[x, y] = ran.Next(0,5) == 0;
                }
            }
            choice = 2;

            timer1.Start();
        }

        private void B3S1NextGeneration()
        {
            g.Clear(Color.White);

            bool[,] newCoordinates = new bool[pWidth, pHeight];

            for (int tX = 0; tX < pWidth; tX++)
            {
                for (int tY = 0; tY < pHeight; tY++)
                {

                    if (!coordinates[tX, tY] && PovCount8(tX, tY) == 3)//правила
                    {
                        newCoordinates[tX, tY] = true;
                    }
                    else if (coordinates[tX, tY] && PovCount8(tX, tY) < 2 || PovCount8(tX, tY) > 3)
                    {
                        newCoordinates[tX, tY] = false;
                    }
                    else
                    {
                        newCoordinates[tX, tY] = coordinates[tX, tY];
                    }

                    if (coordinates[tX, tY])
                    {
                        

                        g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                       
                    }
                }
            }

            coordinates = newCoordinates;
            pictureBox1.Refresh();

        }

        #endregion

        #region B3 , S1345678, Пульсар, поле зрения - 8 (3)
        private void button3_Click(object sender, EventArgs e)
        {

            B3S1U();
        }

        private void B3S1U()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);




            coordinates[30, 30] = true;
            coordinates[31, 30] = true;
            coordinates[31, 29] = true;
            coordinates[32, 30] = true;

            coordinates[36, 30] = true;
            coordinates[37, 30] = true;
            coordinates[37, 29] = true;
            coordinates[38, 30] = true;


            choice = 3;
            timer1.Start();
        }

        private void B3S1UNextGeneration()
        {
            g.Clear(Color.White);

            bool[,] newCoordinates = new bool[pWidth, pHeight];

            for (int tX = 0; tX < pWidth; tX++)
            {
                for (int tY = 0; tY < pHeight; tY++)
                {

                    if (!coordinates[tX, tY] && PovCount8(tX, tY) == 3)//правила
                    {
                        newCoordinates[tX, tY] = true;
                    }
                    else if (coordinates[tX, tY] && PovCount8(tX, tY) < 2 || PovCount8(tX, tY) > 3)
                    {
                        newCoordinates[tX, tY] = false;
                    }
                    else
                    {
                        newCoordinates[tX, tY] = coordinates[tX, tY];
                    }

                    if (coordinates[tX, tY])
                    {
                        g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates = newCoordinates;
            pictureBox1.Refresh();

        }

        #endregion

        #region B3 , S1345678, Глайдерная пушка, поле зрения - 8 (4)
        private void button4_Click(object sender, EventArgs e)
        {

            B3S1G();
        }

        private void B3S1G()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);




            coordinates[20, 20] = true;//куб
            coordinates[20, 21] = true;
            coordinates[21, 20] = true;
            coordinates[21, 21] = true;

            coordinates[33, 18] = true;
            coordinates[32, 18] = true;
            coordinates[31, 19] = true;
            coordinates[30, 20] = true;
            coordinates[30, 21] = true;
            coordinates[30, 22] = true;
            coordinates[31, 23] = true;
            coordinates[32, 24] = true;
            coordinates[33, 24] = true;

            coordinates[34, 21] = true;

            coordinates[36, 21] = true;
            coordinates[36, 20] = true;
            coordinates[36, 22] = true;
            coordinates[35, 19] = true;
            coordinates[35, 23] = true;
            coordinates[37, 21] = true;

            coordinates[40, 20] = true;
            coordinates[40, 19] = true;
            coordinates[40, 18] = true;
            coordinates[41, 20] = true;
            coordinates[41, 19] = true;
            coordinates[41, 18] = true;

            coordinates[42, 17] = true;
            coordinates[42, 21] = true;

            coordinates[44, 21] = true;
            coordinates[44, 22] = true;

            coordinates[44, 17] = true;
            coordinates[44, 16] = true;

            coordinates[54, 19] = true;
            coordinates[54, 20] = true;
            coordinates[55, 19] = true;
            coordinates[50, 20] = true;//куб

            choice = 4;
            timer1.Start();
        }

        private void B3S1GNextGeneration()
        {
            g.Clear(Color.White);

            bool[,] newCoordinates = new bool[pWidth, pHeight];

            for (int tX = 0; tX < pWidth; tX++)
            {
                for (int tY = 0; tY < pHeight; tY++)
                {

                    if (!coordinates[tX, tY] && PovCount8(tX, tY) == 3)//правила
                    {
                        newCoordinates[tX, tY] = true;
                    }
                    else if (coordinates[tX, tY] && PovCount8(tX, tY) < 2 || PovCount8(tX, tY) > 3)
                    {
                        newCoordinates[tX, tY] = false;
                    }
                    else
                    {
                        newCoordinates[tX, tY] = coordinates[tX, tY];
                    }

                    if (coordinates[tX, tY])
                    {
                        g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates = newCoordinates;
            pictureBox1.Refresh();

        }

        #endregion

        #region B3 , S1345678, Фрактал 2, поле зрения - 8 (5)
        private void button5_Click(object sender, EventArgs e)
        {

            B3S012345678();
        }

        private void B3S012345678()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);




            coordinates[30, 30] = true;
            coordinates[31, 30] = true;
            coordinates[32, 30] = true;
            coordinates[33, 30] = true;
            coordinates[34, 30] = true;

            coordinates[29, 29] = true;
            coordinates[30, 29] = true;
            coordinates[31, 29] = true;
            coordinates[32, 29] = true;
            coordinates[33, 29] = true;
            coordinates[34, 29] = true;
            coordinates[35, 29] = true;


            coordinates[27, 28] = true;
            coordinates[28, 28] = true;
            coordinates[29, 28] = true;
            coordinates[30, 28] = true;
            coordinates[31, 28] = true;
            coordinates[32, 28] = true;
            coordinates[33, 28] = true;
            coordinates[34, 28] = true;
            coordinates[35, 28] = true;
            coordinates[36, 28] = true;
            coordinates[37, 28] = true;

            coordinates[26, 27] = true;
            coordinates[27, 27] = true;
            coordinates[28, 27] = true;
            coordinates[29, 27] = true;
            coordinates[30, 27] = true;
            coordinates[31, 27] = true;
            coordinates[32, 27] = true;
            coordinates[33, 27] = true;
            coordinates[34, 27] = true;
            coordinates[35, 27] = true;
            coordinates[36, 27] = true;
            coordinates[37, 27] = true;
            coordinates[38, 27] = true;

            coordinates[27, 26] = true;
            coordinates[28, 26] = true;
            coordinates[29, 26] = true;
            coordinates[30, 26] = true;
            coordinates[31, 26] = true;
            coordinates[32, 26] = true;
            coordinates[33, 26] = true;
            coordinates[34, 26] = true;
            coordinates[35, 26] = true;
            coordinates[36, 26] = true;
            coordinates[37, 26] = true;

            coordinates[29, 25] = true;
            coordinates[30, 25] = true;
            coordinates[31, 25] = true;
            coordinates[32, 25] = true;
            coordinates[33, 25] = true;
            coordinates[34, 25] = true;
            coordinates[35, 25] = true;

            coordinates[30, 24] = true;
            coordinates[31, 24] = true;
            coordinates[32, 24] = true;
            coordinates[33, 24] = true;
            coordinates[34, 24] = true;


            choice = 5;
            timer1.Start();
        }

        private void B3S012345678NextGeneration()
        {
            g.Clear(Color.White);

            bool[,] newCoordinates = new bool[pWidth, pHeight];

            for (int tX = 0; tX < pWidth; tX++)
            {
                for (int tY = 0; tY < pHeight; tY++)
                {

                    if (!coordinates[tX, tY] && PovCount8(tX, tY) == 3)//правила
                    {
                        newCoordinates[tX, tY] = true;
                    }
                    //else if (coordinates[tX, tY] && PovCount8(tX, tY) < 2 || PovCount8(tX, tY) > 3)
                    //{
                    //    newCoordinates[tX, tY] = false;
                    //}
                    else
                    {
                        newCoordinates[tX, tY] = coordinates[tX, tY];
                    }

                    if (coordinates[tX, tY])
                    {
                        g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates = newCoordinates;
            pictureBox1.Refresh();

        }

        #endregion

        #region B5678 , S45678, Оригинальная клетка, поле зрения - 8 (6)
        private void button7_Click(object sender, EventArgs e)
        {
            B5678S45678();
        }

        private void B5678S45678()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);



            for (int x = 0; x < pWidth; x++)
            {
                for (int y = 0; y < pHeight; y++)
                {
                    coordinates[x, y] = ran.Next(0, 3) == 0;
                }
            }
            choice = 6;
            timer1.Start();
        }

        private void B5678S45678NextGeneration()
        {
            g.Clear(Color.White);

            bool[,] newCoordinates = new bool[pWidth, pHeight];

            for (int tX = 0; tX < pWidth; tX++)
            {
                for (int tY = 0; tY < pHeight; tY++)
                {

                    if (!coordinates[tX, tY] && PovCount8(tX, tY) >=5)//правила
                    {
                        newCoordinates[tX, tY] = true;
                    }
                    //else if (coordinates[tX, tY] && PovCount8(tX, tY) < 3)
                    //{
                    //    newCoordinates[tX, tY] = false;
                    //}
                    else
                    {
                        newCoordinates[tX, tY] = coordinates[tX, tY];
                    }

                    if (coordinates[tX, tY])
                    {
                        g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates = newCoordinates;
            pictureBox1.Refresh();

        }

        #endregion

        #region B1 , S2345678, фрактал, поле зрения - 4 (7)
        private void button8_Click(object sender, EventArgs e)
        {
            B1S4();
        }

        private void B1S4()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);

            coordinates[30, 30] = true;
            choice = 7;
            timer1.Start();
        }

        private void B1S4NextGeneration()
        {
            g.Clear(Color.White);

            bool[,] newCoordinates = new bool[pWidth, pHeight];

            for (int tX = 0; tX < pWidth; tX++)
            {
                for (int tY = 0; tY < pHeight; tY++)
                {

                    if (!coordinates[tX, tY] && PovCount4(tX, tY) == 1)//правила
                    {
                        newCoordinates[tX, tY] = true;
                    }
                    //else if (coordinates[tX, tY] && PovCount8(tX, tY) > 2 )
                    //{
                    //    newCoordinates[tX, tY] = false;
                    //}
                    else
                    {
                        newCoordinates[tX, tY] = coordinates[tX, tY];
                    }

                    if (coordinates[tX, tY])
                    {
                        g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates = newCoordinates;
            pictureBox1.Refresh();

        }

        #endregion 

        #region B1 , S2345678, фрактал, поле зрения - 3 (8)
        private void button9_Click(object sender, EventArgs e)
        {
            transfusion();
        }

        private void transfusion()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates3 = new int[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);

            for (int x = 0; x < pWidth; x++)
            {
                for (int y = 0; y < pHeight; y++)
                {
                    coordinates3[x, y] = ran.Next(1, 4);
                }
            }

            choice = 8;
            timer1.Start();
        }

        private void transfusionNextGeneration()
        {
            g.Clear(Color.White);

            int[,] newCoordinates = new int[pWidth, pHeight];

            for (int tX = 0; tX < pWidth; tX++)
            {
                for (int tY = 0; tY < pHeight; tY++)
                {
                    //если красную клетку окружают 3 или больше зеленых клеток то она зеленая
                    //если зеленую клетку окружают 3 или больше синих клеток то она синей
                    //если синюю клетку окружают 3 или больше красных клеток то она красный

                    //красный 1      зеленый 2     синий 3

                    int count1 = 0;//красный
                    int count2 = 0;//зеленый
                    int count3 = 0;//синий
                    int count = 0;

                    for (int Jx = -1; Jx < 2; Jx++)
                    {
                        for (int Jy = -1; Jy < 2; Jy++)
                        {
                            int col = (tX + Jx + pWidth) % pWidth;
                            int row = (tY + Jy + pHeight) % pHeight;

                            if (coordinates3[col, row] == 1 && !(col == tX && row == tY))
                            {
                                count1++;
                            }
                            if (coordinates3[col, row] == 2 && !(col == tX && row == tY))
                            {
                                count2++;
                            }

                            if (coordinates3[col, row] == 3 && !(col == tX && row == tY))
                            {
                                count3++;
                            }
                        }
                    }

                    

                    if (coordinates3[tX, tY] == 1 && count2 >= 3)//правила
                    {
                        newCoordinates[tX, tY] = 2;
                        
                    }

                    else if (coordinates3[tX, tY] == 2 && count3 >= 3)//правила
                    {
                        newCoordinates[tX, tY] = 3;
                    }

                    else if (coordinates3[tX, tY] == 3 && count1 >= 3)//правила
                    {
                        newCoordinates[tX, tY] = 1;
                    }
                    
                    else
                    {
                        newCoordinates[tX, tY] = coordinates3[tX, tY];
                    }
                    
                    if (coordinates3[tX, tY] == 1)
                    {
                        g.FillRectangle(Brushes.LightBlue, tX * size, tY * size, size, size);
                    }
                    else if (coordinates3[tX, tY] == 2)
                    {
                        g.FillRectangle(Brushes.SlateGray, tX * size, tY * size, size, size);
                    }
                    else if (coordinates3[tX, tY] == 3)
                    {
                        g.FillRectangle(Brushes.Aquamarine, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates3 = newCoordinates;
            pictureBox1.Refresh();

        }

        #endregion 

        #region B1 , S2345678, фрактал, поле зрения - 3 (9)
        private void button10_Click(object sender, EventArgs e)
        {
            transfusion2();
        }

        private void transfusion2()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates3 = new int[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);

            for (int x = 0; x < pWidth; x++)
            {
                for (int y = 0; y < pHeight; y++)
                {
                    coordinates3[x, y] = 1;
                }
            }

            for (int x = size*2; x < pWidth-size; x++)
            {
                for (int y = size * 2; y < pHeight-size; y++)
                {
                    coordinates3[x, y] = 2;
                }
            }

            for (int x = size * 4; x < pWidth ; x++)
            {
                for (int y = size * 4; y < pHeight; y++)
                {
                    coordinates3[x, y] = 3;
                }
            }

            choice = 8;
            timer1.Start();
        }

        private void transfusion2NextGeneration()
        {
            g.Clear(Color.White);

            int[,] newCoordinates = new int[pWidth, pHeight];

            for (int tX = 0; tX < pWidth; tX++)
            {
                for (int tY = 0; tY < pHeight; tY++)
                {
                    //если красную клетку окружают 3 или больше зеленых клеток то она зеленая
                    //если зеленую клетку окружают 3 или больше синих клеток то она синей
                    //если синюю клетку окружают 3 или больше красных клеток то она красный

                    //красный 1      зеленый 2     синий 3

                    int count1 = 0;//красный
                    int count2 = 0;//зеленый
                    int count3 = 0;//синий
                    int count = 0;

                    for (int Jx = -1; Jx < 2; Jx++)
                    {
                        for (int Jy = -1; Jy < 2; Jy++)
                        {
                            int col = (tX + Jx + pWidth) % pWidth;
                            int row = (tY + Jy + pHeight) % pHeight;

                            if (coordinates3[col, row] == 1 && !(col == tX && row == tY))
                            {
                                count1++;
                            }
                            if (coordinates3[col, row] == 2 && !(col == tX && row == tY))
                            {
                                count2++;
                            }

                            if (coordinates3[col, row] == 3 && !(col == tX && row == tY))
                            {
                                count3++;
                            }
                        }
                    }



                    if (coordinates3[tX, tY] == 1 && count2 >= 3)//правила
                    {
                        newCoordinates[tX, tY] = 2;

                    }

                    else if (coordinates3[tX, tY] == 2 && count3 >= 3)//правила
                    {
                        newCoordinates[tX, tY] = 3;
                    }

                    else if (coordinates3[tX, tY] == 3 && count1 >= 3)//правила
                    {
                        newCoordinates[tX, tY] = 1;
                    }

                    else
                    {
                        newCoordinates[tX, tY] = coordinates3[tX, tY];
                    }

                    if (coordinates3[tX, tY] == 1)
                    {
                        g.FillRectangle(Brushes.LightBlue, tX * size, tY * size, size, size);
                    }
                    else if (coordinates3[tX, tY] == 2)
                    {
                        g.FillRectangle(Brushes.SlateGray, tX * size, tY * size, size, size);
                    }
                    else if (coordinates3[tX, tY] == 3)
                    {
                        g.FillRectangle(Brushes.Aquamarine, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates3 = newCoordinates;
            pictureBox1.Refresh();

        }

        #endregion 

        #region ant, поле зрения - 1 (10)
        private void button11_Click(object sender, EventArgs e)
        {
            Ant();
        }

        private void Ant()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);


            ant.Add(new Ant {direction = 2, x = pWidth / 2, y = pHeight/2 });

            choice = 10;
            timer1.Start();
        }

        private void AntNextGeneration()
        {
            g.Clear(Color.White);

            bool[,] newCoordinates = new bool[pWidth, pHeight];

            int aCol = 0;
            int aRow = 0;
            foreach (var item in ant)
            {
                for (int tX = 0; tX < pWidth; tX++)
                {
                    for (int tY = 0; tY < pHeight; tY++)
                    {
                        //1 left
                        //2 up
                        //3 right
                        //4 down

                        aCol = (item.x + pWidth) % pWidth;
                        aRow = (item.y + pHeight) % pHeight;

                        if (aCol == tX && aRow == tY && coordinates[tX, tY])
                        {
                            if (item.direction == 1) item.direction = 4;
                            else if (item.direction == 2) item.direction = 1;
                            else if (item.direction == 3) item.direction = 2;
                            else if (item.direction == 4) item.direction = 3;
                        }

                        else if (aCol == tX && aRow == tY && !coordinates[tX, tY])
                        {

                            if (item.direction == 1) item.direction = 2;
                            else if (item.direction == 2) item.direction = 3;
                            else if (item.direction == 3) item.direction = 4;
                            else if (item.direction == 4) item.direction = 1;
                        }

                        if (aCol == tX && aRow == tY && coordinates[tX, tY]) newCoordinates[tX, tY] = false;
                        else if (aCol == tX && aRow == tY && !coordinates[tX, tY]) newCoordinates[tX, tY] = true;



                        else
                        {
                            newCoordinates[tX, tY] = coordinates[tX, tY];
                        }

                        if (coordinates[tX, tY])
                        {
                            g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                        }

                        if (aCol == tX && aRow == tY)
                        {
                            g.FillRectangle(Brushes.Red, tX * size, tY * size, size, size);
                        }
                    }
                }

                if (item.direction == 1) item.x = aCol - 1;
                else if (item.direction == 2) item.y =  aRow - 1;
                else if (item.direction == 3) item.x = aCol + 1;
                else if (item.direction == 4) item.y =  aRow + 1;

            coordinates = newCoordinates;
            pictureBox1.Refresh();
            }
        }

        #endregion

        #region Треугольник серпинсокго, поле зрения - 3 (11)
        private void button12_Click(object sender, EventArgs e)
        {

            Rule90();
        }

        private void Rule90()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);




            coordinates[pWidth / 2, 0] = true;


            choice = 11;
            timer1.Start();
        }

        private void Rule90NextGeneration()
        {
            g.Clear(Color.White);

            //   1   2   3   4   5   6   7   8
            //  111 110 101 100 011 010 001 000
            //   0   1   0   1   1   0   1   0 

            bool[,] newCoordinates = new bool[pWidth, pHeight+2];

            for (int tY = 0; tY < pHeight; tY++)
            {
                for (int tX = 0; tX < pWidth; tX++)
                {

                    int count = 0;
                    string test = "";

                    for (int Jx = -1; Jx < 2; Jx++)
                    {
                        int col = (tX + Jx + pWidth) % pWidth;

                        if (coordinates[col, tY])
                        {
                            test += "1";
                        }
                        else if (!coordinates[col, tY])
                        {
                            test += "0";
                        }

                    }
                  
                    if(test == "111")
                    {

                        newCoordinates[tX, tY + 1] = false;
                    }
                    else if (test == "110")
                    {
                        newCoordinates[tX, tY + 1] = true;
                    }
                     else if (test == "101")
                    {
                        newCoordinates[tX, tY + 1] = false;
                    }
                     else if (test == "100")
                    {
                        newCoordinates[tX, tY + 1] = true;
                    }
                     else if (test == "011")
                    {
                        newCoordinates[tX, tY + 1] = true;
                    }
                     else if (test == "010")
                    {
                        newCoordinates[tX, tY ] = true;
                        newCoordinates[tX, tY + 1] = false;
                    }
                     else if (test == "001")
                    {
                        newCoordinates[tX, tY + 1] = true;
                       
                    }
                     else if (test == "000")
                    {
                        newCoordinates[tX, tY + 1] = false;
                    }

                    //else
                    //{
                    //    newCoordinates[tX, tY] = coordinates[tX, tY];
                    //}

                    if (coordinates[tX, tY])
                    {
                        g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates = newCoordinates;

            
            pictureBox1.Refresh();

        }

        #endregion

        #region Правило 110, поле зрения - 3 (12)
        private void button13_Click(object sender, EventArgs e)
        {

            Rule110();
        }

        private void Rule110()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);




            coordinates[pWidth / 2, 0] = true;


            choice = 12;
            timer1.Start();
        }

        private void Rule110NextGeneration()
        {
            g.Clear(Color.White);

            //   1   2   3   4   5   6   7   8
            //  111	110	101	100	011	010	001	000
            //   0	 1	 1	 0	 1	 1	 1   0

            bool[,] newCoordinates = new bool[pWidth, pHeight + 2];

            for (int tY = 0; tY < pHeight; tY++)
            {
                for (int tX = 0; tX < pWidth; tX++)
                {

                    int count = 0;
                    string test = "";

                    for (int Jx = -1; Jx < 2; Jx++)
                    {
                        int col = (tX + Jx + pWidth) % pWidth;

                        if (coordinates[col, tY])
                        {
                            test += "1";
                        }
                        else if (!coordinates[col, tY])
                        {
                            test += "0";
                        }

                    }

                    if (test == "111")
                    {

                        newCoordinates[tX, tY + 1] = false;
                    }
                    else if (test == "110")
                    {
                        newCoordinates[tX, tY + 1] = true;
                    }
                    else if (test == "101")
                    {
                        newCoordinates[tX, tY + 1] = true;
                    }
                    else if (test == "100")
                    {
                        newCoordinates[tX, tY + 1] = false;
                    }
                    else if (test == "011")
                    {
                        newCoordinates[tX, tY + 1] = true;
                    }
                    else if (test == "010")
                    {
                        newCoordinates[tX, tY] = true;
                        newCoordinates[tX, tY + 1] = true;
                    }
                    else if (test == "001")
                    {
                        newCoordinates[tX, tY + 1] = true;

                    }
                    else if (test == "000")
                    {
                        newCoordinates[tX, tY + 1] = false;
                    }

                    //else
                    //{
                    //    newCoordinates[tX, tY] = coordinates[tX, tY];
                    //}

                    if (coordinates[tX, tY])
                    {
                        g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates = newCoordinates;

            
            pictureBox1.Refresh();

        }

        #endregion

        #region Треугольник серпинсокго другой, поле зрения - 3 (13)
        private void button14_Click(object sender, EventArgs e)
        {

            Rule161();
        }

        private void Rule161()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);




            coordinates[pWidth / 2, 0] = true;


            choice = 13;
            timer1.Start();
        }

        private void Rule161NextGeneration()
        {
            g.Clear(Color.Black);

            //   1   2   3   4   5   6   7   8
            //  111 110 101 100 011 010 001 000
            //   1	 0	 1	 0	 0	 0	 0	 1

            bool[,] newCoordinates = new bool[pWidth, pHeight + 2];

            for (int tY = 0; tY < pHeight; tY++)
            {
                for (int tX = 0; tX < pWidth; tX++)
                {

                    int count = 0;
                    string test = "";

                    for (int Jx = -1; Jx < 2; Jx++)
                    {
                        int col = (tX + Jx + pWidth) % pWidth;

                        if (coordinates[col, tY])
                        {
                            test += "1";
                        }
                        else if (!coordinates[col, tY])
                        {
                            test += "0";
                        }

                    }

                    if (test == "111")
                    {

                        newCoordinates[tX, tY + 1] = true;
                    }
                    else if (test == "110")
                    {
                        newCoordinates[tX, tY + 1] = false;
                    }
                    else if (test == "101")
                    {
                        newCoordinates[tX, tY + 1] = true;
                    }
                    else if (test == "100")
                    {
                        newCoordinates[tX, tY + 1] = false;
                    }
                    else if (test == "011")
                    {
                        newCoordinates[tX, tY + 1] = false;
                    }
                    else if (test == "010")
                    {
                        newCoordinates[tX, tY] = true;
                        newCoordinates[tX, tY + 1] = false;
                    }
                    else if (test == "001")
                    {
                        newCoordinates[tX, tY + 1] = false;

                    }
                    else if (test == "000")
                    {
                        newCoordinates[tX, tY + 1] = true;
                    }

                    //else
                    //{
                    //    newCoordinates[tX, tY] = coordinates[tX, tY];
                    //}

                    if (coordinates[tX, tY])
                    {
                        g.FillRectangle(Brushes.White, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates = newCoordinates;

            
            pictureBox1.Refresh();

        }

        #endregion

        #region Треугольник серпинсокго рандом, поле зрения - 3 (14)
        //РАНДОМ РАНДОМ РАНДОМ РАНДОМ РАНДОМ РАНДОМ РАНДОМ РАНДОМ РАНДОМ РАНДОМ РАНДОМ 
       
        private bool tes1;
        private bool tes2;
        private bool tes3;
        private bool tes4;
        private bool tes5;
        private bool tes6;
        private bool tes7;

        private void button15_Click(object sender, EventArgs e)
        {

            RuleRan();
        }

        private void RuleRan()
        {
            if (timer1.Enabled) return;

            pWidth = pictureBox1.Width / size;
            pHeight = pictureBox1.Height / size;

            coordinates = new bool[pWidth, pHeight];

            Random ran = new Random();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);

            tes1 = ran.Next(0, 2) == 0;
            tes2 = ran.Next(0, 2) == 0;
            tes3 = ran.Next(0, 2) == 0;
            tes4 = ran.Next(0, 2) == 0;
            tes5 = ran.Next(0, 2) == 0;
            tes6 = ran.Next(0, 2) == 0;
            tes7 = ran.Next(0, 2) == 0;


            coordinates[pWidth / 2, 0] = true;


            choice = 14;
            timer1.Start();
        }

        
        private void RuleRanNextGeneration()
        {
            g.Clear(Color.White);

            //   1   2   3   4   5   6   7   8
            //  111 110 101 100 011 010 001 000
            //   1	 0	 1	 0	 0	 0	 0	 1

            bool[,] newCoordinates = new bool[pWidth, pHeight + 2];
            
            for (int tY = 0; tY < pHeight; tY++)
            {
                for (int tX = 0; tX < pWidth; tX++)
                {

                    int count = 0;
                    string test = "";

                    for (int Jx = -1; Jx < 2; Jx++)
                    {
                        int col = (tX + Jx + pWidth) % pWidth;

                        if (coordinates[col, tY])
                        {
                            test += "1";
                        }
                        else if (!coordinates[col, tY])
                        {
                            test += "0";
                        }

                    }

                    if (test == "111")
                    {

                        newCoordinates[tX, tY + 1] = tes1;
                    }
                    else if (test == "110")
                    {
                        newCoordinates[tX, tY + 1] = tes2;
                    }
                    else if (test == "101")
                    {
                        newCoordinates[tX, tY + 1] = tes3;
                    }
                    else if (test == "100")
                    {
                        newCoordinates[tX, tY + 1] = tes4;
                    }
                    else if (test == "011")
                    {
                        newCoordinates[tX, tY + 1] = tes5;
                    }
                    else if (test == "010")
                    {
                        newCoordinates[tX, tY] = true;
                        newCoordinates[tX, tY + 1] = tes6;
                    }
                    else if (test == "001")
                    {
                        newCoordinates[tX, tY + 1] = tes7;

                    }
                    else if (test == "000")
                    {
                        newCoordinates[tX, tY + 1] = false;
                    }

                    //else
                    //{
                    //    newCoordinates[tX, tY] = coordinates[tX, tY];
                    //}

                    if (coordinates[tX, tY])
                    {
                        g.FillRectangle(Brushes.Black, tX * size, tY * size, size, size);
                    }
                }
            }

            coordinates = newCoordinates;


            pictureBox1.Refresh();

        }

        #endregion

        public int PovCount8(int x, int y)// поле зрения 8
        {
            int count = 0;

            for (int Jx = -1; Jx < 2; Jx++)
            {
                for (int Jy = -1; Jy < 2; Jy++)
                {
                    int col = (x + Jx + pWidth) % pWidth;
                    int row = (y + Jy + pHeight) % pHeight;

                    if (coordinates[col, row] && !(col == x && row == y))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

      
            
        

        public int PovCount4(int x, int y)// поле зрения 4 (лишние циклы, ваще похрен)
        {
            int count = 0;

            for (int Jx = -1; Jx < 2; Jx++)
            {
                for (int Jy = -1; Jy < 2; Jy++)
                {
                    int col = (x + Jx + pWidth) % pWidth;
                    int row = (y + Jy + pHeight) % pHeight;

                    if (coordinates[col, row] && !(col == x && row == y) && !(col == x-1 && row == y-1) && !(col == x+1 && row == y-1) && !(col == x+1 && row == y+1) && !(col == x-1 && row == y+1))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

      
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            cycle++;
            Text = "Game of Life | Клеточный автомат  " + cycle;
            if (choice == 1)
                B1SNextGeneration();
            else if (choice == 2)
                B3S1NextGeneration();
            else if (choice == 3)
                B3S1UNextGeneration();
            else if (choice == 4)
                B3S1GNextGeneration();
            else if (choice == 5)
                B3S012345678NextGeneration();
            else if (choice == 6)
                B5678S45678NextGeneration();
            else if (choice == 7)
                B1S4NextGeneration();
            else if (choice == 8)
                transfusionNextGeneration();
            else if (choice == 9)
                transfusion2NextGeneration();
            else if (choice == 10)
                AntNextGeneration();
            else if (choice == 11)
                Rule90NextGeneration();
            else if (choice == 12)
                Rule110NextGeneration();
            else if (choice == 13)
                Rule161NextGeneration();
            else if (choice == 14)
                RuleRanNextGeneration();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled) return;
            cycle = 0;
            ant.Clear();
            timer1.Stop();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)//рисовавние
        {
            try
            {
                

                if (choice == 14)
                {
                    if (e.Button == MouseButtons.Right)
                    {

                        coordinates[e.X / size, e.Y / size] = false;

                    }
                    else if (e.Button == MouseButtons.Left)
                    {
                        coordinates[e.X / size, e.Y / size] = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (choice == 10)
            {
                ant.Add(new Ant { direction = 2, x = e.X/size, y = e.Y/size });
            }
        }


    }
}

class Ant
{
    public int direction { get; set; }
    public int x { get; set; }
    public int y { get; set; }
}
