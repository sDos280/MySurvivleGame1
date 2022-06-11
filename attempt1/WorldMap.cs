using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engine;

namespace engine
{
    public class WorldMap
    {
        public readonly double seed;
        public readonly Vector2 tilesInChunk = new Vector2(4, 4);
        public readonly Vector2 chunkInWorldSpace;
        public readonly Vector2 chunksInScreenSpace;
        Dictionary<string, string[,]> dataMap; // ("5;3"), {{"grass", "grass", "air"},{"grass", "grass", "air"},{"grass", "grass", "air"}}
        public WorldMap(double _seed, Vector2 screenSize)
        {
            seed = _seed;
            chunkInWorldSpace = Tile.tileSizeScreen * tilesInChunk;
            chunksInScreenSpace = screenSize / chunkInWorldSpace;
        }

        public string[,] generateChunk(Vector2 chunkPos)
        {
            string[,] data = new string[(int)tilesInChunk.X, (int)tilesInChunk.Y];
            // we start to iterat from TopLeft then right then down
            for (int y = 0; y < tilesInChunk.Y; y++)
            {
                for (int x = 0; x < tilesInChunk.X; x++)
                {
                    Vector2 tileTopLeftPos = new Vector2(chunkPos.X * chunkInWorldSpace.X + x * Tile.tileSizeScreen.X, (chunkPos.Y+1) * chunkInWorldSpace.Y - y * Tile.tileSizeScreen.Y); // הצאנק הראשון ברביע הראשון שהכי קרוב לצירים הוא 0,0
                    data[x, y] = "grass_top";
                }
            }
            return data;
        }

        public Vector2 getChunkPosition(Vector2 pos)
        {
            return new Vector2((int)MathF.Ceiling(pos.X / this.chunkInWorldSpace.X) - 1, (int)MathF.Floor(pos.Y / this.chunkInWorldSpace.Y));
        }

        public Vector2 getTilePosition(Vector2 pos)
        {
            return new Vector2((int)MathF.Ceiling(pos.X / Tile.tileSizeScreen.X) - 1, (int)MathF.Floor(pos.Y / Tile.tileSizeScreen.Y));
        }

        public void DrawChunk(Camera camera, Vector2 chunkPos)
        {
            // we start to iterat from TopLeft then right then down
            for (int y = 0; y < tilesInChunk.Y; y++)
            {
                for (int x = 0; x < tilesInChunk.X; x++)
                {
                    Vector2 tileTopLeftPos = new Vector2(chunkPos.X * chunkInWorldSpace.X + x * Tile.tileSizeScreen.X, (chunkPos.Y + 1) * chunkInWorldSpace.Y - y * Tile.tileSizeScreen.Y);
                    Tile.drawTile(camera, tileTopLeftPos, "grass_top");
                }
            }
        }
    }
}
