using System;
using System.Numerics;
using Raylib_cs;
using engine;

namespace Main
{
    static class Program
    {
        public static void Main()
        {

            int width = 800;
            int height = 600;
            float dt = 0;
            Camera camera = new Camera(0, 0, width, height);
            Raylib.InitWindow(width, height, "Hello World");
            Random r = new Random();
            double seed = r.NextDouble();
            WorldMap worldMap = new WorldMap(0, width, height);
            while (!Raylib.WindowShouldClose())
            {
                dt = Raylib.GetFrameTime();
                if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_D))
                {
                    camera.pos.X += dt * 500;
                }
                if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_A))
                {
                    camera.pos.X -= dt * 500;
                }
                if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_W))
                {
                    camera.pos.Y += dt * 500;
                }
                if (Raylib.IsKeyDown(Raylib_cs.KeyboardKey.KEY_S))
                {
                    camera.pos.Y -= dt * 500;
                }
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                camera.drawAxis();
                worldMap.updateChunks(camera);
                //Console.WriteLine(worldMap.getTilePositionInChumk(camera.ScreenPointToWorldPoint(Raylib.GetMousePosition())));
                Raylib.DrawFPS(10, 10);
                Raylib.EndDrawing();
                //Console.WriteLine(Raylib.GetFPS());
            }

            Raylib.CloseWindow();
        }
    }
}