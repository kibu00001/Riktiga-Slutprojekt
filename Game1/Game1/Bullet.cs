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
        public bool isvisible;

        public Bullet(Texture2D _texture)
        {
            texture = _texture;
            isvisible = false;

        }

        public void Update()
        {
            position += velocity;
        }

    }
}
