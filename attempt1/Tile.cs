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
        public static Texture2D texture = Raylib.LoadTexture("Assets/Tiles/tileset.png");
        // the dict is for topleft rects in the textere 
        public static Dictionary<string, Vector2> textureTilesPositionsString = new Dictionary<string, Vector2>()
        {
            { "grass_top", new Vector2(549, 264) },
            { "rock", new Vector2(437, 136) },
            { "rock2", new Vector2(495, 136) }
        };
        public static Dictionary<int, Vector2> textureTilesPositionsInt = new Dictionary<int, Vector2>()
        {
            { 1, new Vector2(549, 264) },
            { 2, new Vector2(437, 135) },
            { 3, new Vector2(495, 136) }
        };
        public static Dictionary<string, int> tilesTypesToInt = new Dictionary<string, int>()
        {
            { "air", 0 },
            { "grass_top", 1 },
            { "rock", 2 },
            { "rock2", 3 }
        };
        public static Dictionary<int, string> tilesIntToTypes = new Dictionary<int, string>()
        {
            { 0, "air"},
            { 1, "grass_top"},
            { 2, "rock"},
            { 3, "rock2"}
        };

        public static void drawTilePro(Camera camera, Vector2 pos, string tileType)
        {
            Raylib.DrawTexturePro(texture, 
                new Rectangle(textureTilesPositionsString[tileType].X , textureTilesPositionsString[tileType].Y , tileSizeTexture.X, tileSizeTexture.Y), 
                new Rectangle(camera.WorldPointToScreenPoint(pos).X, camera.WorldPointToScreenPoint(pos).Y, tileSizeScreen.X, tileSizeScreen.Y),
                Vector2.Zero,
                0,
                Color.WHITE);
        }
        public static void drawTilePro(Camera camera, Vector2 pos, int tileType)
        {
            Raylib.DrawTexturePro(texture,
                new Rectangle(textureTilesPositionsString[Tile.tilesIntToTypes[tileType]].X, textureTilesPositionsString[Tile.tilesIntToTypes[tileType]].Y, tileSizeTexture.X, tileSizeTexture.Y),
                new Rectangle(camera.WorldPointToScreenPoint(pos).X, camera.WorldPointToScreenPoint(pos).Y, tileSizeScreen.X, tileSizeScreen.Y),
                Vector2.Zero,
                0,
                Color.WHITE);
        }
    }
}
