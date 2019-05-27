using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Bullet : Sprite
    {
        public Bullet(Texture2D _texture)
        {
            texture = _texture;

            isvisible = true; // Bullet är synligt.

            hp = 1;
        }

        public void Update()
        {
            position += velocity;

            if (hp == 0) // Gör så att bullet inte är synlig när hp = 0.
                isvisible = false;
        }

    }
}
