﻿using AvengersTheFallen.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvengersTheFallen
{
	public partial class Form1 : Form
	{
		private Avenger avenger;
        Map map;
        Boolean t;

		public Form1()
		{
            InitializeComponent();
            this.DoubleBuffered = true;
            timerGenerateObstacles.Interval = timerMapMove.Interval * 54;
            timerMapMove.Enabled = true;
            timerGenerateObstacles.Enabled = true;
            this.Height = 500;
            this.Width = 1000;
            t = false;
            avenger = new Avenger("Thor", new Point(this.Width / 2, this.Height - 130));
            avenger.Resize(1000, 500);
            map = new Map(this.Height, this.Width, avenger.Name);
        }

		private void Form1_Resize(object sender, EventArgs e)
		{
            this.Width = this.Height * 2;
			Invalidate(true);
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
            if (!t)
			    e.Graphics.Clear(Color.White);
            else
                e.Graphics.Clear(Color.Green);
            Pen pen = new Pen(Color.White);
            e.Graphics.DrawRectangle(pen, 0, 0, 1, 1);
            e.Graphics.ScaleTransform(((float)(this.Height * 2) / 1000.0F), ((float)(this.Height) / 500.0F));
            avenger.Draw(e.Graphics);
            map.Draw(e.Graphics);
            pen.Dispose();
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

			Invalidate(true);
		}

        private void TimerGenerateObstacles_Tick(object sender, EventArgs e)
        {
            map.AddObstacles();
            Invalidate();
        }

        private void TimerMapMove_Tick(object sender, EventArgs e)
        {
            map.moveObstacles();
            t = map.checkCollisionObstacle(avenger);
            Invalidate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timerGenerateObstacles.Enabled = !timerGenerateObstacles.Enabled;
            timerMapMove.Enabled = timerGenerateObstacles.Enabled;
        }
    }
}
