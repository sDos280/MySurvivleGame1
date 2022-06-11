using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace engine
{
    public class Tile
    {
        public static Vector2 tileSizeTexture = Vector2.One * 16;
        public static Vector2 tileSizeScreen = Vector2.One * 32;
        public static Texture2D texture = Raylib.LoadTexture("Assets/Tiles/grass_top.png");
        // the dict is for topleft rects in the textere 
        public static Dictionary<string, Vector2> textureTilesPositions = new Dictionary<string, Vector2>()
        {
            { "grass_top", new Vector2(0, 0) }
        };

        public static void drawTile(Camera camera, Vector2 pos, string tileType)
        {
            Raylib.DrawTexturePro(texture, 
                new Rectangle(textureTilesPositions[tileType].X * tileSizeScreen.X, textureTilesPositions[tileType].Y * tileSizeScreen.X, tileSizeTexture.X, tileSizeTexture.Y), 
                new Rectangle(camera.WorldPointToScreenPoint(pos).X, camera.WorldPointToScreenPoint(pos).Y, tileSizeScreen.X, tileSizeScreen.Y),
                Vector2.Zero,
                0,
                Color.WHITE);
        }
    }
}
