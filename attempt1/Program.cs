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
            Camera camera = new Camera(0, 0, width, height);
            Raylib.InitWindow(width, height, "Hello World");
            Random r = new Random();
            double seed = r.NextDouble();
            WorldMap worldMap = new WorldMap(0, new Vector2(width, height));
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                Vector2 mouseWorldPosition = camera.ScreenPointToWorldPoint(Raylib.GetMousePosition());
                Console.WriteLine(worldMap.getChunkPosition(mouseWorldPosition));
                camera.drawAxis();
                worldMap.DrawChunk(camera, worldMap.getChunkPosition(mouseWorldPosition));
                Raylib.DrawFPS(10, 10);
                Raylib.EndDrawing();
                //Console.WriteLine(Raylib.GetFPS());
            }

            Raylib.CloseWindow();
        }
    }
}