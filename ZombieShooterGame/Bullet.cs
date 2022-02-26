using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ZombieShooterGame
{
    internal class Bullet
    {
        private string direction;
        public string Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        private int speed = 20;
        private PictureBox bullet = new PictureBox();
        private Timer bulletTIme = new Timer();
        public int bulletTop { get; set; }
      
        public int bulletLeft { get; set; }
        public void MakeBullet(Form form)
        {
            bullet.BackColor = Color.White;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Top = bulletTop;
            bullet.Left = bulletLeft;
         
            bullet.BringToFront();
            form.Controls.Add(bullet);
            bulletTIme.Interval = speed;
            bulletTIme.Tick += new EventHandler(BulletTimeEvent);
            bulletTIme.Start();
         
        }
        private void BulletTimeEvent(object sender,EventArgs e)
        {
            if (Direction == "left")
            {
                bullet.Left -= speed;
            }
            if (Direction == "right")
            {
                bullet.Left += speed;
            }
            if (Direction == "up")
            {
                bullet.Top -= speed;
            }
            if (Direction == "down")
            {
                bullet.Top += speed;
            }
            if (bullet.Left < 10|| bullet.Left>860|| bullet.Top<10|| bullet.Top>600)
            {
                bulletTIme.Stop();
                bulletTIme.Dispose();
                bullet.Dispose();
                bulletTIme = null;
                bullet = null;
            }
        }

    }
}
