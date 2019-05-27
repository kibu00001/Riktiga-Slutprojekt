using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Score
    {
        public int score;
        private SpriteFont font;

        public Score(SpriteFont _font)
        {
            font = _font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, score.ToString(), new Vector2(10, 10), Color.White); // ToString för att göra så att det visas som en string.
        }
    }
}
