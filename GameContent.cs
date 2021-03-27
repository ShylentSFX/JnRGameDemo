using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JnRGame
{
    public static class GameContent
    {
        public static ContentManager content;
        public static Texture2D player_placeholder;
        public static Texture2D tile_placeholder;

        public static void Load()
        {
            player_placeholder = content.Load<Texture2D>("textures/player_placeholder");
            tile_placeholder = content.Load<Texture2D>("textures/tile_placeholder");
        }
    }
}
