using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;

namespace engine
{
    public class Sprite
    {
        // the x,y position of the sprite is in the midle;
        //public Rectangle Properties; // x,y,w,h
        public Vector2 pos;
        public Vector2 size;
        Texture2D texture;
        Rectangle Source; // where from the texture to draw
        public float angle; // Rotation

        public Sprite(Rectangle _Properties, Rectangle _Source, float _angle, string texturePath)
        {
            pos = new Vector2(_Properties.x, _Properties.y);
            size = new Vector2(_Properties.width, _Properties.height);
            Source = _Source;
            angle = _angle;
            texture = Raylib.LoadTexture(texturePath);
        }
        public Sprite(Vector2 _pos, Vector2 _size, Rectangle _Source, float _angle, string texturePath)
        {
            pos = _pos;
            size = _size;
            Source = _Source;
            angle = _angle;
            texture = Raylib.LoadTexture(texturePath);
        }

        public void draw(Camera camera)
        {
            /*//Rectangle Dest = Properties;
            //Vector2 Origin = new Vector2(Properties.width * 0.5f, Properties.height * 0.5f); // off set draw texture
            //Raylib.DrawTexturePro(texture, Source, Dest, Origin, angle, Color.WHITE);*/
            Vector2 screenSpritePos = camera.WorldPointToScreenPoint(this.pos);
            Raylib.DrawTexturePro(texture, Source, new Rectangle(screenSpritePos.X, screenSpritePos.Y, size.X, size.Y), new Vector2(size.X * 0.5f, size.Y * 0.5f), angle, Color.WHITE);
        }
        public void consoleWorldPos()
        {
            Console.WriteLine(this.pos.ToString() + "p");
        }

        /*public void draw(Texture2D texture, Vector2 cameraPos, Vector2 screenSize)
        {
            //Rectangle Dest = Properties;
            //Vector2 Origin = new Vector2(Properties.width * 0.5f, Properties.height * 0.5f); // off set draw texture
            //Raylib.DrawTexturePro(texture, Source, Dest, Origin, angle, Color.WHITE);
            Rectangle Dest = Properties;
            //Dest.x = Dest.x - cameraPos.X + screenSize.X * 0.5f;
            Dest.x += -cameraPos.X + screenSize.X * 0.5f;
            //Dest.y = Dest.y - cameraPos.Y + screenSize.Y * 0.5f;
            Dest.y += cameraPos.Y + screenSize.Y * 0.5f;
            Raylib.DrawTexturePro(texture, Source, Dest, new Vector2(Properties.width * 0.5f, Properties.height * 0.5f), angle, Color.WHITE);
            //Raylib.DrawTexturePro(texture, Source, Properties, new Vector2(Properties.width * 0.5f, Properties.height * 0.5f), angle, Color.WHITE);
            //Raylib.DrawCircle((int)Properties.x, (int)Properties.y, 10, Color.RED);
        }*/
    }
}