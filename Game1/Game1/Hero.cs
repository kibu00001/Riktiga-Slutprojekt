using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Hero : Sprite
    {
        bool hasjumped;

        public Hero(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;
            position = _position;
            hasjumped = false;
        }

        public override void Update(GameTime gameTime)
        {
            position += velocity;

            velocity = Vector2.Zero;

            Move();
        }

        public void Move()
        {
            KeyboardState currkey = Keyboard.GetState();

            if (currkey.IsKeyDown(Keys.A))
                velocity.X = -5f;

            else if (currkey.IsKeyDown(Keys.D))
                velocity.X = +5f;

            else velocity.X = 0;

            if(currkey.IsKeyDown(Keys.Space) && hasjumped == false)
            {
                position.Y -= 150f;
                velocity.Y = -10f;
                hasjumped = true;
            }

            if (hasjumped == true)
            {
                float i = 1;
                velocity.Y += 4f * i;
            }

            if (position.Y + texture.Height >= 800)
            {
                hasjumped = false;
                velocity.Y = 0f;
            }
            else hasjumped = true;


        }

    }
}
