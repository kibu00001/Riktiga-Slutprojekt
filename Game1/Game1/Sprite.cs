using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Sprite // En klass Hero, Enemy, Bullet ärver av.
    {
        public Texture2D texture; // Vilken bild som skickas ut.

        public Vector2 position; // Vart allt ska vara.

        public Vector2 velocity; // Hastigheten

        public int hp; // Hp så att något inte blir synligt och tas bort efter hp = 0.

        public bool isvisible; // Detta kollar om något fortfarande ska vara synligt.

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); // Gör så att alla som ärver har en rectangle att anropa och att alla redan har den position och Width/Height bestämt.
            }
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

    }
}
