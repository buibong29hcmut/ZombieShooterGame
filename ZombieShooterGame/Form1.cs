using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombieShooterGame
{
    public partial class Form1 : Form
    {
        private bool goLeft, goRight, goUp, goDown, gameOver=false;
        string facing = "up";
        int playerHeath = 100;
        int speed = 10;
        int zoombieSpeed = 3;
        public int ammo = 10;
        int score=0;
        Random ranNum= new Random();
        List<PictureBox> zombieList = new List<PictureBox>();
        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

      

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (playerHeath > 1)
            {
                healthBar.Value = playerHeath;

            }
            else
            {
                gameOver = true;
                player.Image = Properties.Resources.dead;
                gameTimer.Stop();
            }
            if (healthBar.Value < 20)
            {
                healthBar.BackColor = Color.Red;
            }
            txtScore.Text = "Ammo" + ammo;
            killTxt.Text = "Kills" + score;
            if (goLeft == true&& player.Left > 0)
            {
                player.Left -= speed;
            }
            if (goRight == true && (player.Left+player.Width) <this.ClientSize.Width)
            {
                player.Left += speed;
            }
            if (goDown== true && (player.Top + player.Height) < this.ClientSize.Height)
            {
                player.Top += speed;
            }
            if (goUp==true&& player.Top >0)
            {
                player.Top -= speed;
            }
            foreach(Control control in this.Controls)
            {
                if(control is PictureBox && (string)control.Tag=="ammo")
                {
                    if (player.Bounds.IntersectsWith(control.Bounds))
                    {
                        this.Controls.Remove(control);
                        ((PictureBox)control).Dispose();
                        ammo += 5;
                    }
                }
                if (control is PictureBox && (string)control.Tag == "zombie")
                {

                    if (player.Bounds.IntersectsWith(control.Bounds))
                    {
                        playerHeath -= 1;
                    }


                    if (control.Left > player.Left)
                    {
                        control.Left -= zoombieSpeed;
                        ((PictureBox)control).Image = Properties.Resources.zleft;
                    }
                    if (control.Left < player.Left)
                    {
                        control.Left += zoombieSpeed;
                        ((PictureBox)control).Image = Properties.Resources.zright;
                    }
                    if (control.Top > player.Top)
                    {
                        control.Top -= zoombieSpeed;
                        ((PictureBox)control).Image = Properties.Resources.zup;
                    }
                    if (control.Top < player.Top)
                    {
                        control.Top += zoombieSpeed;
                        ((PictureBox)control).Image = Properties.Resources.zdown;
                    }

                }
                foreach (Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "bullet" && control is PictureBox && (string)control.Tag == "zombie")
                    {
                        if (control.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;

                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(control);
                            ((PictureBox)control).Dispose();
                            zombieList.Remove(((PictureBox)control));
                            MakeZombies();
                        }
                    }
                }

            }
          

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image=Properties.Resources.left;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.right;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.down;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
               
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
               
            }
            if (e.KeyCode ==Keys.Space&& ammo>0)
            {
                ammo--;
                 ShootBullet(facing);
                if (ammo < 1)
                {
                    DropAmo();
                }
            }
            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                MessageBox.Show("Game Over");
                RestartGame();
            }

        }

        private void KeyIsPress(object sender, KeyPressEventArgs e)
        {

        }
        private void ShootBullet(string direction)
        {
            Bullet bullet= new Bullet();
            bullet.Direction = direction;
            bullet.bulletLeft = player.Left + (player.Width / 2);
            bullet.bulletTop=player.Top+ (player.Height / 2);
         
            bullet.MakeBullet(this);
        }
        private void MakeZombies()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = ranNum.Next(0, 900);
            zombie.Top = ranNum.Next(0, 800);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombieList.Add(zombie);
            this.Controls.Add(zombie);
            player.BringToFront();

        }
        private void DropAmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode=PictureBoxSizeMode.AutoSize;
            ammo.Left= ranNum.Next(10, this.ClientSize.Width-ammo.Width);
            ammo.Top = ranNum.Next(60, this.ClientSize.Height - ammo.Height);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);
            ammo.BringToFront();
        }
        private void RestartGame()
        {
            player.Image = Properties.Resources.up;
            foreach(var picture in zombieList)
            {
                this.Controls.Remove(picture);
            }
            for(int i = 0; i < 3; i++)
            {
                MakeZombies();
            }
            goDown = false;
            goUp= false;
            goLeft = false;
            goRight = false;

            playerHeath = 100;
            score = 0;
            ammo = 10;

            gameTimer.Start();

        }
    }
}
