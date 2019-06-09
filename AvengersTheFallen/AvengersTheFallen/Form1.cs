﻿using AvengersTheFallen.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvengersTheFallen
{
    public partial class Form1 : Form
	{
		private Avenger avenger;
        Map map;
        public static Random r;

		public Form1()
		{
            InitializeComponent();
            this.DoubleBuffered = true;
            r = new Random();
            timerGenerateObstacles.Interval = timerMapMove.Interval * 54;
            timerMapMove.Enabled = true;
            timerGenerateObstacles.Enabled = true;
            this.Height = 750;
            this.Width = 1500;
            panel1.Location = new Point(0, 0);
            panel1.Height = 500;
            panel1.Width = 1000;
            avenger = new Avenger("Thor", new Point(1000 / 2, 500 - 90));
            map = new Map(500, 1000, avenger.Name);
            TimerGenerateObstacles_Tick(null, null);
        }

		private void Form1_Resize(object sender, EventArgs e)
		{
            this.Width = this.Height * 2;
			panel1.Invalidate(true);
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.A)
			{
				avenger.Move("Left");
			}
			if (e.KeyData == Keys.D) 
			{
				avenger.Move("Right");
			}
            panel1.Invalidate(true);
        }

        private void TimerGenerateObstacles_Tick(object sender, EventArgs e)
        {
            map.AddObstacles();
            Invalidate(true);
        }

        private void TimerMapMove_Tick(object sender, EventArgs e)
        {
            map.moveObstacles();
            Boolean t = map.checkCollisionObstacle(avenger);
            if (t) { }
            //avenger takes damage
            panel1.Invalidate(true);
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.ScaleTransform((float)(panel1.Width / 1000), ((float)(panel1.Height) / 500));
            avenger.Draw(e.Graphics);
            map.Draw(e.Graphics);
        }
    }
}
