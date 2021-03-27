using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JnRGame
{
    public class Actor
    {
        public Vector2 position;
        public int width;
        public int height;
        public Rectangle boundingBox
        {
            get
            {
                return new Rectangle(position.ToPoint(), new Point(width, height));
            }
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
