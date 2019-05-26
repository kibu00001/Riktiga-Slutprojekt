using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Enemy : Sprite
    {
        public bool isvisible;
        Random random = new Random();
        int randx, randy;

        public Enemy(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;
            isvisible = true;

            randx = random.Next(-4, -1);
            randy = random.Next(-4, 4);

            velocity = new Vector2(randx, randy);
        }

        public void Update(GraphicsDevice graphics)
        {
            position += velocity;

            if (position.Y <= 500 || position.Y >= graphics.Viewport.Height - texture.Height)
                velocity.Y = -velocity.Y;

            if (position.X < 0 - texture.Width)
                isvisible = false;

        }

    }
}
