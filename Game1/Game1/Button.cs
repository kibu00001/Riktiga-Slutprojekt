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
    class Button : Sprite
    {
        public Vector2 size;

        public bool isclicked; // För att se när den har blivit klickad.

        public Button(Texture2D _texture, GraphicsDevice graphics, Vector2 _position)
        {
            texture = _texture;

            position = _position;

            size = new Vector2(graphics.Viewport.Width / 10, graphics.Viewport.Height / 20); // För att få Play button i stolek jämnfört med spel skärm storlek.
        }

        public void Update(MouseState mouse)
        {
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1); //Visra vart musen är.

            if (mouse.LeftButton == ButtonState.Pressed) isclicked = true; // Visar att musen har klickats på vänster.
        }
    }
}
