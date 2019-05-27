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
        bool hasjumped; // För att kolla om hero har hoppat och gravitation ska sättas in.

        public Hero(Texture2D _texture, Vector2 _position)
        {
            texture = _texture;

            position = _position;

            hasjumped = false;

            hp = 5;
        }

        public override void Update(GameTime gameTime)
        {
            position += velocity;

            velocity = Vector2.Zero; // Används för att inte drifta omkring när man har släppt key.

            Move(); // Använder metoden jag skrev för att gå.
        }


        public void Move()
        {
            KeyboardState currkey = Keyboard.GetState();

            if (currkey.IsKeyDown(Keys.A))
                velocity.X = -5f;

            else if (currkey.IsKeyDown(Keys.D))
                velocity.X = +5f;

            else velocity.X = 0;

            if(currkey.IsKeyDown(Keys.W) && hasjumped == false) // Gör så att man bara kan trycka W och hoppa om man inte är i luften eller har hoppat.
            {
                position.Y -= 300f;

                velocity.Y = -20f;

                hasjumped = true; // Gör så attman ser att man har hoppat.
            }

            if (hasjumped == true) // Om man har hoppat så sätts gravitation in.
            {
                float i = 1;
                velocity.Y += 10f * i;
            }

            if (position.Y + texture.Height >= 800) // Gör så att, om hero har nåt marken som jag har satt in som den ska vara,
                                                    // Så blir hoppat fel och hastigheten blir 0.
            {
                hasjumped = false;

                velocity.Y = 0f;
            }
            else hasjumped = true; // Om man inte har nåt marken så är hoppat sant, så att man inte kan hoppa i all evighet.
        }

    }
}
