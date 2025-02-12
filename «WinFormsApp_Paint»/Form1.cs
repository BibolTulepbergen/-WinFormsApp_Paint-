﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _WinFormsApp_Paint_
{
    public partial class Form1 : Form
    {
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        ToolStripLabel infoLabel1;
        Timer timer;

        Color color = Color.Black; //Создаем переменную типа Color присваиваем ей черный цвет.
        bool isPressed = false; //логическая переменная понадобиться для опеределения когда можно рисовать на panel
        Point CurrentPoint; //Текущая точка ресунка.
        Point PrevPoint; //Это начальная точка рисунка.
        Graphics g; //Создаем графический элемент.
        ColorDialog colorDialog = new ColorDialog(); //диалоговое окно для выбора цвета.
        public Form1()
        {
            InitializeComponent();

            
            infoLabel = new ToolStripLabel();
            infoLabel.Text = "Текущие дата и время";
            dateLabel = new ToolStripLabel();
            timeLabel = new ToolStripLabel();
            infoLabel1 = new ToolStripLabel();
            infoLabel1.Text = "Менің атым Бибол,Тегім Тулепберген, әкем аты Бақберген.";

            
            statusStrip1.Items.Add(infoLabel1);
            statusStrip1.Items.Add(dateLabel);
            statusStrip1.Items.Add(timeLabel);

            timer = new Timer() { Interval = 1000 };
            timer.Tick += timer1_Tick;
            timer.Start();
            label2.BackColor = Color.Black; //По умолчанию для пера задан черный цвет, поэтому мы зададим такой же фон для label2
            g = panel1.CreateGraphics(); //Создаем область для работы с графикой на элементе panel
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) //Если окно закрылось с OK, то меняем цвет для пера и фона label2
            {
                color = colorDialog.Color; //меняем цвет для пера
                label2.BackColor = colorDialog.Color; //меняем цвет для Фона label2
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                PrevPoint = CurrentPoint;
                CurrentPoint = e.Location;
                my_Pen();
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            CurrentPoint = e.Location;
        }


        private void my_Pen()
        {
            Pen pen = new Pen(color, (float)numericUpDown1.Value); //Создаем перо, задаем ему цвет и толщину.
            g.DrawLine(pen, CurrentPoint, PrevPoint); //Соединияем точки линиями
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToLongDateString();
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
