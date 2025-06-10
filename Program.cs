/*
Pong demo in Raylib
By PixelRunner
*/
using Raylib_cs;

namespace RaylibGame1;

class Program
{
    const int WIDTH = 800;
    const int HEIGHT = 480;
    
    [STAThread]
    public static void Main()
    {
        Raylib.InitWindow(WIDTH, HEIGHT, "Pong");

        Raylib.SetTargetFPS(60);

        int p1Score = 0;
        int p2Score = 0;

        int gameState = 0;
        int frameCounter = 0;

        // ball
        Ball ball = new Ball(WIDTH / 2, HEIGHT / 2, 1, 1);
        Rectangle ballRect;

        // paddle p1
        Paddle p1 = new Paddle(20, HEIGHT / 2, 2, KeyboardKey.W, KeyboardKey.S);
        Rectangle p1Rect;

        // paddle p2
        Paddle p2 = new Paddle(WIDTH - 20, HEIGHT / 2, 2, KeyboardKey.Up, KeyboardKey.Down);
        Rectangle p2Rect;

        while (!Raylib.WindowShouldClose())
        {
            // === UPDATE ===

            switch(gameState)
            {
                case 0: // splash screen
                    frameCounter++;
                    if (frameCounter >= 240)
                    {
                        gameState++;
                        frameCounter = 0;
                    }
                    break;
                case 1: // main menu
                    p1Score = 0;
                    p2Score = 0;

                    if (Raylib.GetKeyPressed() != 0)
                    {
                        gameState++;
                        frameCounter = 0;
                    }
                    break;
                case 2: // game

                    ball.Update();
                    p1.Update();
                    p2.Update();

                    

                    // prevent paddles from leaving the screen
                    if (p1.PosY <= 0)
                    {
                        p1.PosY = 0;
                    }
                    else if (p1.PosY + 40 >= HEIGHT)
                    {
                        p1.PosY = HEIGHT - 40;
                    }

                    if (p2.PosY <= 0)
                    {
                        p2.PosY = 0;
                    }
                    else if (p2.PosY + 40 >= HEIGHT)
                    {
                        p2.PosY = HEIGHT - 40;
                    }

                    ballRect = new Rectangle((float)ball.PosX + ball.SpeedX * ball.DirX, (float)ball.PosY + ball.SpeedY * ball.DirY, 10f, 10f);
                    p1Rect = new Rectangle((float)p1.PosX, (float)p1.PosY, 10f, 40f);
                    p2Rect = new Rectangle((float)p2.PosX, (float)p2.PosY, 10f, 40f);

                    // change ball direction if hitting the top or bottom of screen
                    if (ball.PosY <= 0 || ball.PosY + 10 >= HEIGHT)
                    {
                        ball.ChangeDirY();
                    }

                    // change ball direction if hitting a paddle
                    if (Raylib.CheckCollisionRecs(ballRect, p1Rect))
                    {
                        ball.ChangeDirX();
                        ball.SpeedX++;
                    }

                    if (Raylib.CheckCollisionRecs(ballRect, p2Rect))
                    {
                        ball.ChangeDirX();
                        ball.SpeedX++;
                    }

                    // reset game and add to a player's score if ball hits respective wall
                    if (ball.PosX <= 0)
                    {
                        ball.PosX = WIDTH / 2;
                        ball.PosY = HEIGHT / 2;
                        p2Score++;
                        ball.SpeedX = 1;
                    }

                    else if (ball.PosX + 10 >= WIDTH)
                    {
                        ball.PosX = WIDTH / 2;
                        ball.PosY = HEIGHT / 2;
                        p1Score++;
                        ball.SpeedX = 1;
                    }

                    // end the game if one of the player's scores hits 6
                    if (p1Score >= 6 || p2Score >= 6)
                    {
                        gameState++;
                        frameCounter = 0;
                    }

                    break;

                case 3: // game over
                    if (Raylib.GetKeyPressed() != 0)
                    {
                        gameState = 1;
                        frameCounter = 0;
                    }
                    break;
            }


            // === RENDER ===
            Raylib.BeginDrawing();
            //Raylib.ClearBackground(new Color(0, 0, 0, 16));

            


            switch (gameState)
            {
                case 0: // splash screen
                    Raylib.DrawRectangle(0, 0, WIDTH, HEIGHT, new Color(0, 0, 0, 16));

                    if (frameCounter <= 120)
                    {
                        Raylib.DrawText("PixelRunner Presents...", WIDTH / 2 - 130, HEIGHT / 2 - 10, 20, Color.White);
                    } 
                    break;
                case 1: // main menu
                    Raylib.ClearBackground(new Color(0, 0, 0, 16));

                    Raylib.DrawText("Raylib Pong", WIDTH / 2 - 150, HEIGHT / 2 - 25, 50, Color.White);
                    Raylib.DrawText("Press any key to play...", 20, HEIGHT - 50, 20, new Color(0, 255, 0));
                    break;
                case 2: // game
                    Raylib.DrawRectangle(0, 0, WIDTH, HEIGHT, new Color(0, 0, 0, 64));

                    frameCounter++;
                    if (frameCounter <= 120)
                    {
                        Raylib.DrawText("P1: Use W/S to move\nP2: Use Arrow Keys to move", WIDTH / 2 - 130, HEIGHT / 2 - 10, 20, new Color(0, 255, 0));
                    }

                    ball.Render(ball.PosX, ball.PosY);
                    p1.Render(p1.PosX, p1.PosY);
                    p2.Render(p2.PosX, p2.PosY);

                    Raylib.DrawText(Convert.ToString(p1Score), 40, 40, 40, Color.White);
                    Raylib.DrawText(Convert.ToString(p2Score), WIDTH - 100, 40, 40, Color.White);
                    break;
                case 3: // game over
                    Raylib.ClearBackground(new Color(0, 0, 0, 16));
                    string winTxt;

                    if (p1Score > p2Score)
                    {
                        winTxt = $"Player 1 wins by {p1Score - p2Score} points!";
                    } 
                    else
                    {
                        winTxt = $"Player 2 wins by {p2Score - p1Score} points!";
                    }

                    Raylib.DrawText(winTxt, 20, HEIGHT / 2 - 25, 50, Color.White);
                    Raylib.DrawText("Press any key to continue...", 20, HEIGHT - 50, 20, new Color(0, 255, 0));
                    break;
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}