using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace JnRGame
{
    public class Room
    {
        public int locationX;
        public int locationY;
        public byte[,] tileID;
        public Rectangle size;
    }

    public class LevelData
    {
        public string name;
        public List<Room> roomList;
    }

    public static class World
    {
        static Texture2D texture = GameContent.tile_placeholder;
        public static int tileSize = 8;
        public static string name;
        public static List<Actor> actorList = new List<Actor>();
        public static List<Room> roomList;
        public static Rectangle camera;
        public static int roomIndex = 0;

        static World()
        {
            camera.Width = 320;
            camera.Height = 180;
        }

        public static void BuildLevel(string levelName)
        {
            // Get File Path of Level JSON and read it
            string gamePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string levelPath = gamePath + @"\Content\Levels\";
            string output = Path.Combine(levelPath, levelName + ".json");
            string jsonString = File.ReadAllText(output);

            // Deserialize jsonString into the LevelData Class
            LevelData levelData = JsonConvert.DeserializeObject<LevelData>(jsonString);

            // Convert levelData's Variables to Local Variables
            name = levelData.name;
            roomList = levelData.roomList;

            // Add Player to Level
            actorList.Add(new Player(16f, 16f));

            // Build all Rooms
            foreach (Room room in roomList)
            {
                // Reverse 2D Array Axis
                room.tileID = GameMath.Reverse2DArrayAxis(room.tileID);

                // Set Location of Room Rectangle
                room.size.X = room.locationX;
                room.size.Y = room.locationY;

                // Set Width and Height of Room Rectangle
                room.size.Width = room.tileID.GetLength(0) * tileSize;
                room.size.Height = room.tileID.GetLength(1) * tileSize;
            }
        }

        public static void Update()
        {

        }

        public static void Draw (SpriteBatch spriteBatch)
        {
            // Draw Each Room
            foreach (Room room in roomList)
            {
                for (int x = 0; x < room.size.Width / tileSize; x++)
                {
                    for (int y = 0; y < room.size.Height / tileSize; y++)
                    {
                        int posX = room.size.X + (x * tileSize);
                        int posY = room.size.Y + (y * tileSize);
                        if (room.tileID[x, y] > 0 && posX + tileSize > camera.Left && posY + tileSize > camera.Top
                            && posX < camera.Right && posY < camera.Bottom)
                        switch (room.tileID[x, y])
                        {
                            case 1:
                                spriteBatch.Draw(texture, new Vector2(posX, posY) - new Vector2(camera.X, camera.Y), Color.White);
                                break;
                        }
                    }
                }
            }
        }
    }
}
