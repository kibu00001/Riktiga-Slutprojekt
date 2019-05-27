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
        Random random = new Random();

        int randx, randy;

        public Enemy(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;

            position = _position;

            isvisible = true;

            randx = random.Next(-10, -5); // Random hastighet som enemy kan få.

            randy = random.Next(-4, 4); // Random hastighet enemy kan få att studsa på.

            velocity = new Vector2(randx, randy);

            hp = 1;
        }

        public void Update(GraphicsDevice graphics)
        {
            position += velocity;

            if (position.Y <= 400 || position.Y >= graphics.Viewport.Height - texture.Height) // Om enemy nudar marken eller när Y = 400 så vänds dem om.
                velocity.Y = -velocity.Y;

            if (position.X < 0 - texture.Width) // Om Fiende går utanför X = 0 så försvinner dem.
                isvisible = false;

            if (hp == 0) // Enemy blir osynlig när hp = 0.
                isvisible = false;
        }

    }
}
