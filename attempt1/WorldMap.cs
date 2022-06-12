using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using engine;

namespace engine
{
    public class WorldMap
    {
        public readonly double seed;
        public static Vector2 tilesInChunk = new Vector2(4, 4);
        public readonly Vector2 chunkInWorldSpace;
        public readonly Vector2 chunksInScreenSpace;
        public static float noiseScale = 0.001f;

        Dictionary<string, int[,]> dataMap = new Dictionary<string, int[,]>(); // ("5;3"), {{"grass", "grass", "air"},{"grass", "grass", "air"},{"grass", "grass", "air"}}
        public WorldMap(double _seed, Vector2 screenSize)
        {
            seed = _seed;
            chunkInWorldSpace = Tile.tileSizeScreen * tilesInChunk;
            chunksInScreenSpace = screenSize / chunkInWorldSpace;
        }

        public int[,] generateChunk(Vector2 chunkPos)
        {
            int[,] data = new int[(int)tilesInChunk.X, (int)tilesInChunk.Y];
            // we start to iterat from TopLeft then right then down
            for (int y = 0; y < tilesInChunk.Y; y++)
            {
                for (int x = 0; x < tilesInChunk.X; x++)
                {
                    //Vector2 tileTopLeftPos = new Vector2(chunkPos.X * chunkInWorldSpace.X + x * Tile.tileSizeScreen.X, (chunkPos.Y+1) * chunkInWorldSpace.Y - y * Tile.tileSizeScreen.Y); // הצאנק הראשון ברביע הראשון שהכי קרוב לצירים הוא 0,0
                    Vector2 tileTopLeftPos = this.getTopLeftPointPositionChunkPosition(chunkPos); // הצאנק הראשון ברביע הראשון שהכי קרוב לצירים הוא 0,0
                    tileTopLeftPos = tileTopLeftPos + new Vector2(x * Tile.tileSizeScreen.X, -y * Tile.tileSizeScreen.Y);
                    float height = Raymath.Remap((float)Perlin.perlin(tileTopLeftPos.X * noiseScale, 0, seed, 999999999), 0, 1, -400, 400);
                    //Console.WriteLine(tileTopLeftPos);
                    if (tileTopLeftPos.Y+Tile.tileSizeScreen.Y < height)
                    {
                        data[y, x] = 2;
                    }
                    else if (tileTopLeftPos.Y < height)
                    {
                        data[y, x] = 1;
                    }
                    else
                    {
                        data[y, x] = 0;
                    }
                    
                }
            }
            return data;
        }

        public Vector2 getChunkPositionFromPoint(Vector2 pos)
        {
            return new Vector2((int)MathF.Ceiling(pos.X / this.chunkInWorldSpace.X) - 1, (int)MathF.Floor(pos.Y / this.chunkInWorldSpace.Y));
        }

        public Vector2 getTopLeftPointPositionChunkPosition(Vector2 chunkpos)
        {
            return new Vector2((int)MathF.Ceiling(chunkpos.X * this.chunkInWorldSpace.X), (int)MathF.Floor((chunkpos.Y+1) * this.chunkInWorldSpace.Y));
        }

        public Vector2 getTilePositionFromPoint(Vector2 pos)
        {
            return new Vector2((int)MathF.Ceiling(pos.X / Tile.tileSizeScreen.X) - 1, (int)MathF.Floor(pos.Y / Tile.tileSizeScreen.Y));
        }

        public void updateChunks(Camera camera)
        {
            //Vector2 cameraChunk = this.getChunkPosition(camera.pos);
            for (int y = 0; y < this.chunksInScreenSpace.Y+2; y++)
            {
                for (int x = 0; x < this.chunksInScreenSpace.X+2; x++)
                {
                    Vector2 thisChunkUpdatePositionWorld = new Vector2(camera.pos.X + (x-(this.chunksInScreenSpace.X + 2)*0.5f) * chunkInWorldSpace.X, camera.pos.Y + (((this.chunksInScreenSpace.Y + 2)*0.5f)- y) * chunkInWorldSpace.Y);
                    Vector2 thisChunkUpdatePositionChunkBase = this.getChunkPositionFromPoint(thisChunkUpdatePositionWorld);
                    if (dataMap.ContainsKey($"{thisChunkUpdatePositionChunkBase.Y};{thisChunkUpdatePositionChunkBase.X}"))
                    {
                        this.DrawChunk(camera, thisChunkUpdatePositionChunkBase);
                    }
                    else
                    {
                        dataMap.Add($"{thisChunkUpdatePositionChunkBase.Y};{thisChunkUpdatePositionChunkBase.X}", this.generateChunk(thisChunkUpdatePositionChunkBase));
                    }
                }
            }
        }

        public void DrawChunk(Camera camera, Vector2 chunkPos)
        {
            // we start to iterat from TopLeft then right then down
            for (int y = 0; y < tilesInChunk.Y; y++)
            {
                for (int x = 0; x < tilesInChunk.X; x++)
                { 
                    if (Tile.textureTilesPositions.ContainsKey(Tile.tilesIntToTypes[dataMap[$"{chunkPos.Y};{chunkPos.X}"][y, x]]))
                    {
                        Vector2 tileTopLeftPos = new Vector2(chunkPos.X * chunkInWorldSpace.X + x * Tile.tileSizeScreen.X, (chunkPos.Y + 1) * chunkInWorldSpace.Y - y * Tile.tileSizeScreen.Y);
                        Tile.drawTilePro(camera, tileTopLeftPos, Tile.tilesIntToTypes[dataMap[$"{chunkPos.Y};{chunkPos.X}"][y, x]]);
                    }
                }
            }
        }
    }
}
