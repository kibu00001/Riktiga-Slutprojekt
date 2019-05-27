using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Hero
        Hero hero;

        //Bullet
        List<Bullet> bullet = new List<Bullet>();
        KeyboardState pastkey;

        //Enemy
        List<Enemy> enemy = new List<Enemy>();
        Random random = new Random();

        //Score
        private Score score;

        //Button
        Button buttonplay;

        //Pause
        bool pause = false;

        //Menu
        enum GameState
        {
            MainMenu,
            Playing,
            GameOver
        }
        GameState CurrentGameState = GameState.MainMenu; // När spelet startas så börjar det på MainMenu.

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 800; //För att få spel skärm storlek.
            graphics.PreferredBackBufferWidth = 1500;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            IsMouseVisible = true; // Gör så att man kan se vart musen är.

            hero = new Hero(Content.Load<Texture2D>("state1"),
                   new Vector2(1, graphics.PreferredBackBufferHeight - Content.Load<Texture2D>("state1").Height));

            score = new Score(Content.Load<SpriteFont>("Font"));

            buttonplay = new Button(Content.Load<Texture2D>("button"), graphics.GraphicsDevice, 
                         new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2));
        }

        protected override void UnloadContent()
        {

        }


        //Enemy
        public void LoadEnemies()
        {
            int randy = random.Next(400, // För att få en random vart i Y-axeln enemy ska spawna.
                graphics.PreferredBackBufferHeight - Content.Load<Texture2D>("enemy").Height);

            if(spawn >= 0.5) // Om spawn, som räknas i sekunder, är 0.5 så blir det omsatt som 0 igen och en enemy skickas ut.
            {
                spawn = 0;
                if (enemy.Count <= 15) // Om det finns mer än 15 enemy så sänds inga ut.
                    enemy.Add(new Enemy(Content.Load<Texture2D>("enemy"), new Vector2(graphics.PreferredBackBufferWidth, randy)));
            }

            for(int i = 0; i < enemy.Count; i++)
                if(!enemy[i].isvisible) // Om en enemy inte är synlig längre, så blir den i och sen raderas den och i-- gör så i = 0, alltså inget.
            {
                    enemy.RemoveAt(i);
                    i--;
                    score.score++; // Varje gång en enemy raderas så får score +1.
            }
        }


        //Bullet
        public void UpdateBullet()
        {
            foreach(Bullet bullets in bullet)
            {
                bullets.position += bullets.velocity; // Bullet hastighet.

                if (bullets.position.X >= graphics.PreferredBackBufferWidth - bullets.texture.Width)
                    bullets.isvisible = false; // Om bullet inte rör något och går utanför Width jag satte så blir den osynlig.
            }

            for(int i = 0; i < bullet.Count; i++)
            {
                if (!bullet[i].isvisible) // Samma som enemy, att bullet tas bort när den inte är synlig.
                {
                    bullet.RemoveAt(i);
                    i--;
                }
            }
        }
        public void Shoot()
        {
                Bullet newbullet = new Bullet(Content.Load<Texture2D>("bullet")); // Gör att ett namn blir en bild/sprite.

                newbullet.velocity.X = newbullet.velocity.X + 5f; // När bullet skjuts så får den en hastighet.

                newbullet.position = new Vector2(hero.position.X + newbullet.velocity.X, // Sätter att position är vid hero.
                    hero.position.Y + (hero.texture.Height /2) - (newbullet.texture.Height/2));

                newbullet.isvisible = true; // Gör så att bullet är synlig.

                if (bullet.Count() <= 5) // Om det redan finns mer än 5 bullets på skärmen så skjuts inga mer.
                    bullet.Add(newbullet);
        }


        //Highscore
        public void WriteHighscore()
        {
            StreamWriter sw = new StreamWriter("Highscore.txt", true);

            sw.WriteLine(score.score.ToString()); // Gör score till en string och skriver ut den till en .txt fil som finns eller skapas när hero.hp = 0.

            sw.Close();
        }


        float spawn = 0; // För att få en timer för enemy spawn och sånt.
        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            if (CurrentGameState == GameState.MainMenu)
                pause = true; // När spelet startas är det i MainMenu och är i pause lägge.
            if (CurrentGameState == GameState.GameOver)
                Exit();

            //Menu
            switch (CurrentGameState)
            {
                case GameState.MainMenu:

                    if (buttonplay.isclicked == true) CurrentGameState = GameState.Playing; // Om button är klickad så blir GameState till playing.
                    buttonplay.Update(mouse);

                    break;

                case GameState.Playing:

                    pause = false; // Medans GameState är i playing så är spelet inte i pause.
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) || hero.hp == 0) CurrentGameState = GameState.GameOver;
                    break;

                case GameState.GameOver:
                    WriteHighscore();
                    break;
            }

            if (pause == false) // Om spelet inte är i pause så laddas allt i denna text upp.
            {
                //Enemy
                spawn += (float)gameTime.ElapsedGameTime.TotalSeconds; // Gör en timer för enemy.

                foreach (Enemy enemies in enemy) // För varje enemy så skrivs koden som är i enemy.Update.
                    enemies.Update(graphics.GraphicsDevice);

                LoadEnemies(); // Använder metoden som är i LoadEnemies.

                // IsTouchingLeft?
                foreach (Enemy enemies in enemy) // En kod som är som Intersect som kollar om enemy rörs på vänster sidan av hero.
                    if (hero.rectangle.Right > enemies.rectangle.Left &&
                        hero.rectangle.Left < enemies.rectangle.Left &&
                        hero.rectangle.Bottom > enemies.rectangle.Top &&
                        hero.rectangle.Top < enemies.rectangle.Bottom)
                    {
                        enemies.hp--; // Enemy förlorar ett hp om den rörs.
                        hero.hp--; // Hero förlorar ett hp om den rörs.
                    }

                //Hero
                if (hero.position.X <= 0)
                    hero.position.X = 1; // Gör så att hero inte kan gå utanför X = 0.

                if (hero.position.X >= graphics.PreferredBackBufferWidth - hero.texture.Width) 
                    hero.position.X = graphics.PreferredBackBufferWidth - hero.texture.Width - 1; // Gör så att hero inte kan gå utanför Width på spel skärmen.

                hero.Update(gameTime); // Använder hero.Update koden som skrevs i Hero.

                //Bullet
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && pastkey.IsKeyUp(Keys.Space))
                    Shoot(); // Gör så att om space blev nertryckt och är uppe igen so används koden i metoden Shoot.

                pastkey = Keyboard.GetState(); // För att se om en key är uppe igen.

                UpdateBullet(); // Använder koden i metoden UpdateBullet för hela spelet.

                foreach (Bullet bullets in bullet)
                    bullets.Update(); // Använder koden som skrivs i Bullet.Update för alla bullets som görs.

                foreach (Bullet bullets in bullet) // För varje bullet som görs u bullet listan.
                {
                    foreach (Enemy enemies in enemy) // För varje enemies som görs i listan enemy.
                    {
                        if (bullets.rectangle.Intersects(enemies.rectangle)) // Om bullet rör enemies rectangle så förlorar båda ett hp.
                        {
                            bullets.hp--;
                            enemies.hp--;
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //Menu Draw
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    buttonplay.Draw(spriteBatch); // När screen är i MainMenu så ritas button upp.
                    break;

                case GameState.Playing:

                    break;
            }

            if (pause == false) // Om spelet inte är i pause.
            {
                hero.Draw(spriteBatch);

                foreach (Enemy enemies in enemy) // Ritar ut spriten för varje enemy som skapas.
                    enemies.Draw(spriteBatch);

                foreach (Bullet bullets in bullet) // Riar ut spriten för varje bullet som skapas.
                    bullets.Draw(spriteBatch);

                score.Draw(spriteBatch);
            }

                spriteBatch.End();

                base.Draw(gameTime);
        }
    }
}