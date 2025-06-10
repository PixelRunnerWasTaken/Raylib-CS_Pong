using Raylib_cs;

namespace RaylibGame1
{
    internal class Paddle
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Acceleration { get; set; }
        public KeyboardKey UpKey { get; set; }
        public KeyboardKey DownKey { get; set; }
        public int Speed = 0;

        public Paddle(int posX, int posY, int acceleration, KeyboardKey upKey, KeyboardKey downKey)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Acceleration = acceleration;
            this.UpKey = upKey;
            this.DownKey = downKey;
        }

        public void Update()
        {
            PosY += Speed;



            if (Speed > 0)
            {
                Speed -= Acceleration / 2;
            }

            if (Speed < 0)
            {
                Speed += Acceleration / 2;
            }


            if (Raylib.IsKeyDown(UpKey))
            {
                Speed -= Acceleration;
            }

            if (Raylib.IsKeyDown(DownKey))
            {
                Speed += Acceleration;
            }

        }

        public void Render(int x, int y)
        {
            Raylib.DrawRectangle(x, y, 10, 40, Color.White);
        }
    }
}
