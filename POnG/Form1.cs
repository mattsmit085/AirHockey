using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace POnG
{
    public partial class Form1 : Form
    {
        int paddle1X = 10;
        int paddle1Y = 160;
        int player1Score = 0;

        int paddle2X = 530;
        int paddle2Y = 160;
        int player2Score = 0;

        int playerturn = 1;

        int paddleWidth = 40;
        int paddleHeight = 40;
        int paddleSpeed = 8;

        int ballX = 295;
        int ballY = 195;
        int ballXSpeed = 6;
        int ballYSpeed = 6;
        int ballWidth = 25;
        int ballHeight = 25;


        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool dDown = false;
        bool aDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        Font screenFont = new Font("Consolas", 12);

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ballstart();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }

        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            if (wDown == true && paddle1Y > 0)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }

            if (aDown == true && paddle1X > 5)
            {
                paddle1X -= paddleSpeed;
            }

            if (dDown == true && paddle1X < this.Width / 2 - 40)
            {
                paddle1X += paddleSpeed;
            }


            if (upArrowDown == true && paddle2Y > 0)
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
            }

            if (leftArrowDown == true && paddle2X > this.Width / 2)
            {
                paddle2X -= paddleSpeed;
            }

            if (rightArrowDown == true && paddle2X < this.Width - paddleWidth)
            {
                paddle2X += paddleSpeed;
            }

            if (ballY < 0 || ballY > this.Height - ballHeight)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }

            if (ballX < 0 || ballX > this.Width - ballWidth)
            {
                ballXSpeed *= -1;
            }
            //create Rectangles of objects on screen to be used for collision detection 
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth + 5, paddleHeight + 5);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth + 5, paddleHeight + 5);
            Rectangle ballRec = new Rectangle(ballX + 5, ballY, ballWidth, ballHeight);
            Rectangle goal1Rec = new Rectangle(0, 128, 5, 100);
            Rectangle goal2Rec = new Rectangle(this.Width - 5, 128, 5, 100);

            //check if ball hits either paddle. If it does change the direction 
            //and place the ball in front of the paddle hit 
            if (player1Rec.IntersectsWith(ballRec))
            {
                SoundPlayer bonkPlayer = new SoundPlayer(Properties.Resources.bonk);
                bonkPlayer.Play();

                if (ballXSpeed == 0)
                {
                    ballYSpeed = 6;
                    ballXSpeed = 6;
                }
                if (ballXSpeed < 0)
                {
                    ballYSpeed--;
                    ballXSpeed--;
                }
                else
                {
                    ballXSpeed++;
                }
                if (ballYSpeed < 0)
                {
                    ballYSpeed--;
                }
                else
                {
                    ballYSpeed++;
                }

                if (ballXSpeed == 0)
                { ballmove(); }

                else if (ballX < paddle1X)
                {
                    ballXSpeed *= -1;
                    ballX = paddle1X - paddleWidth - 10;
                }
                else
                {
                    ballXSpeed *= -1;
                    ballX = paddle1X + paddleWidth + 10;
                }
            }
            else if (player2Rec.IntersectsWith(ballRec))
            {
                if (ballXSpeed == 0)
                {
                    ballYSpeed = 6;
                    ballXSpeed = 6;
                }
                if (ballXSpeed < 0)
                {
                    ballYSpeed--;
                    ballXSpeed--;
                }
                else
                {
                    ballXSpeed++;
                }
                if (ballYSpeed < 0)
                {
                    ballYSpeed--;
                }
                else
                {
                    ballYSpeed++;
                }

                SoundPlayer bonkPlayer = new SoundPlayer(Properties.Resources.bonk);
                bonkPlayer.Play();
                if (ballXSpeed == 0)
                { ballmove(); }

                else if (ballX > paddle2X)
                {
                    ballXSpeed *= -1;
                    ballX = paddle2X + paddleWidth + 10;
                }
                else
                {
                    ballXSpeed *= -1;
                    ballX = paddle2X - paddleWidth - 10;
                }

            }

            if (ballRec.IntersectsWith(goal1Rec))
            {

                player2Score++;

                p2ScoreLabel.Text = $"{player2Score}";



                ballX = 295;
                ballY = 195;

                paddle1X = 10;
                paddle1Y = 160;

                paddle2X = 530;
                paddle2Y = 160;

                ballstart();
            }
            else if (ballRec.IntersectsWith(goal2Rec))
            {
                player1Score++;

                p1ScoreLabel.Text = $"{player1Score}";

                paddle1X = 10;
                paddle1Y = 160;

                paddle2X = 530;
                paddle2Y = 160;
                ballstart();
            }

            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                gameoverLabel.Visible = true;
                Thread.Sleep(500);
                Refresh();
                gameoverLabel.Visible = false;
                Thread.Sleep(500);
                Refresh();
                gameoverLabel.Visible = true;
                Thread.Sleep(500);
                Refresh();
                gameoverLabel.Visible = false;
                Thread.Sleep(500);
                Refresh();
                gameoverLabel.Text = "PLAYER 1 WINS!";
                gameoverLabel.Visible = true;
                Refresh();
                SoundPlayer gameoverPlayer = new SoundPlayer(Properties.Resources.GameOver);
                gameoverPlayer.Play();
            }
            if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                gameoverLabel.Visible = true;
                Thread.Sleep(500);
                Refresh();
                gameoverLabel.Visible = false;
                Thread.Sleep(500);
                Refresh();
                gameoverLabel.Visible = true;
                Thread.Sleep(500);
                Refresh();
                gameoverLabel.Visible = false;
                Thread.Sleep(500);
                Refresh();
                gameoverLabel.Text = "PLAYER 2 WINS!";
                gameoverLabel.Visible = true;
                Refresh();
                SoundPlayer gameoverPlayer = new SoundPlayer(Properties.Resources.GameOver);
                gameoverPlayer.Play();
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(whiteBrush, ballX, ballY, ballWidth, ballHeight);
            e.Graphics.FillRectangle(blackBrush, 0, 128, 5, 100);
            e.Graphics.FillRectangle(blackBrush, this.Width - 5, 128, 5, 100);
            e.Graphics.FillEllipse(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillEllipse(greenBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);


        }

        public void ballstart()
        {
            ballXSpeed = 0;
            ballYSpeed = 0;

            ballX = this.Width / 2 - 5;
            ballY = this.Height / 2 - 5;
        }
        public void ballmove()
        {
            ballXSpeed = 6;
            ballYSpeed = 6;
        }

        private void gameoverLabel_Click(object sender, EventArgs e)
        {

        }

        private void fpsTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}

