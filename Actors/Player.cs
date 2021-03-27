using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JnRGame
{
    class Player : Actor
    {
        Texture2D texture;
        Vector2 velocity;
        KeyboardState keyState;
        KeyboardState prevKeyState;

        public Player(float _posX, float _posY)
        {
            position.X = _posX;
            position.Y = _posY;
            width = 8;
            height = 12;
            texture = GameContent.player_placeholder;
        }

        public override void Update()
        {
            // CONTROLS
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.A))
                velocity.X -= 1f;
            if (keyState.IsKeyDown(Keys.D))
                velocity.X += 1f;
            if (keyState.IsKeyDown(Keys.Space))
                velocity.Y -= 2f;

            // PLACEHOLDER ROOM TRANSITIONER
            if (keyState.IsKeyDown(Keys.V) && prevKeyState != keyState)
            {
                position.X += velocity.X * 32f;
                position.Y -= 32f;
                foreach (Room room in World.roomList)
                {
                    if (position.X > room.size.X && position.X < room.size.X + room.size.Width
                        && position.Y > room.size.Y && position.Y < room.size.Y + room.size.Height)
                        World.roomIndex = World.roomList.IndexOf(room);
                }
            }

            // Apply Gravity
            velocity.Y += 1f;

            // Move Player with Velocity & Collision Detection
            #region Velocity & Collision
                if (velocity.X > 0 && boundingBox.Right < World.roomList[World.roomIndex].size.Right)
                {
                    bool collision = false;
                    for (int i = 0; i < velocity.X; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (World.roomList[World.roomIndex].tileID[(boundingBox.Right - World.roomList[World.roomIndex].size.X) / World.tileSize,
                                ((boundingBox.Top + j) - World.roomList[World.roomIndex].size.Y) / World.tileSize] > 0)
                            { collision = true; j = height; }
                        }
                        if (collision == false)
                            position.X += 1f;
                    }
                }
                if (velocity.X < 0 && boundingBox.Left > World.roomList[World.roomIndex].size.Left)
                {
                    bool collision = false;
                    for (int i = 0; i > velocity.X; i--)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (World.roomList[World.roomIndex].tileID[((boundingBox.Left - 1) - World.roomList[World.roomIndex].size.X) / World.tileSize,
                                ((boundingBox.Top + j) - World.roomList[World.roomIndex].size.Y) / World.tileSize] > 0)
                            { collision = true; j = height; }
                        }
                        if (collision == false)
                            position.X -= 1f;
                    }
                }
                if (velocity.Y > 0 && boundingBox.Bottom < World.roomList[World.roomIndex].size.Bottom)
                {
                    bool collision = false;
                    for (int i = 0; i < velocity.Y; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (World.roomList[World.roomIndex].tileID[((boundingBox.Left + j) - World.roomList[World.roomIndex].size.X) / World.tileSize,
                                (boundingBox.Bottom - World.roomList[World.roomIndex].size.Y) / World.tileSize] > 0)
                            { collision = true; j = width; }
                        }
                        if (collision == false)
                            position.Y += 1f;
                    }
                }
                if (velocity.Y < 0 && boundingBox.Top > World.roomList[World.roomIndex].size.Top)
                {
                    bool collision = false;
                    for (int i = 0; i > velocity.Y; i--)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (World.roomList[World.roomIndex].tileID[((boundingBox.Left + j) - World.roomList[World.roomIndex].size.X) / World.tileSize,
                                ((boundingBox.Top - 1) - World.roomList[World.roomIndex].size.Y) / World.tileSize] > 0)
                            { collision = true; j = width; }
                        }
                        if (collision == false)
                            position.Y -= 1f;
                    }
                }
            #endregion

            // Remove Velocity
            velocity = new Vector2(0f, 0f);

            // CAMERA
            World.camera.X = (int)(position.X + (width / 2) - (World.camera.Width / 2));
            World.camera.Y = (int)(position.Y + (height / 2) - (World.camera.Height / 2));

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(texture, position - new Vector2(World.camera.X, World.camera.Y), Color.White);
        }
    }
}
