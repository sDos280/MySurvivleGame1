using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;


namespace engine
{
    public class Camera
    {
        public static int ticks = 5;
        public Vector2 pos;
        public Vector2 screenSize;

        public Camera(float x, float y, float w, float h)
        {
            pos = new Vector2(x, y);
            screenSize = new Vector2(w, h);
        }

        public void drawAxis()
        {
            Raylib.DrawLineV(this.WorldPointToScreenPoint(0, 10000), this.WorldPointToScreenPoint(0, -10000), Color.BLACK);
            Raylib.DrawLineV(this.WorldPointToScreenPoint(-10000, 0), this.WorldPointToScreenPoint(10000, 0), Color.BLACK);
        }

        public void MoveToPlayer(Vector2 playerPos, float dt)
        {
            Vector2 offset = (playerPos - this.pos);
            Console.WriteLine();
            if (!(offset.X > -1 && offset.X < 1))
            {
                this.pos.X += offset.X * dt;
            }
            if (!(offset.Y > -1 && offset.Y < 1))
            {
                this.pos.Y += offset.Y * dt;
            }

        }

        public Vector2 WorldPointToScreenPoint(Vector2 pos)
        {
            return new Vector2(pos.X - this.pos.X + screenSize.X * 0.5f, -pos.Y + this.pos.Y + screenSize.Y * 0.5f);
        }

        public Vector2 WorldPointToScreenPoint(float posx, float posy)
        {
            return new Vector2(posx - this.pos.X + screenSize.X * 0.5f, -posy + this.pos.Y + screenSize.Y * 0.5f);
        }

        public Vector2 ScreenPointToWorldPoint(Vector2 pos)
        {
            return new Vector2(pos.X + this.pos.X - screenSize.X * 0.5f, this.pos.Y - pos.Y + screenSize.Y * 0.5f);
        }

        public Vector2 ScreenPointToWorldPoint(float posx, float posy)
        {
            return new Vector2(posx + this.pos.X - screenSize.X * 0.5f, this.pos.Y - posy + screenSize.Y * 0.5f);
        }
    }
}