using Raylib_cs;

namespace RaylibGame1
{
    class Ball
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }

        // Direction
        public int DirX = 1;
        public int DirY = 1;

        public Ball(int posX, int posY, int speedX, int speedY)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.SpeedX = speedX;
            this.SpeedY = speedY;
        }

        public void Update()
        {
            PosX += SpeedX * DirX;
            PosY += SpeedY * DirY;
        }

        public void Render(int x, int y)
        {
            Raylib.DrawRectangle(x, y, 10, 10, Color.White);
        }

        // reverse x direction
        public void ChangeDirX()
        {
            DirX *= -1;
        }

        // reverse y direction
        public void ChangeDirY()
        {
            DirY *= -1;
        }
    }
}
