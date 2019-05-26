using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Hero hero;
        List<Bullet> bullet = new List<Bullet>();
        List<Enemy> enemy = new List<Enemy>();
        Random random = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 800; //För att få skärm storlek.
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

            hero = new Hero(Content.Load<Texture2D>("state1"),
                   new Vector2(1, graphics.PreferredBackBufferHeight - Content.Load<Texture2D>("state1").Height));

        }

        public void LoadEnemies()
        {
            int randy = random.Next(graphics.PreferredBackBufferHeight / 2,
                graphics.PreferredBackBufferHeight - Content.Load<Texture2D>("enemy").Height);

            if(spawn >= 1)
            {
                spawn = 0;
                if (enemy.Count < 10)
                    enemy.Add(new Enemy(Content.Load<Texture2D>("enemy"), new Vector2(graphics.PreferredBackBufferWidth, randy)));
            }

            for(int i = 0; i < enemy.Count; i++)
                if(!enemy[i].isvisible)
            {
                    enemy.RemoveAt(i);
                    i--;
            }
        }

        public void UpdateBullet()
        {
            foreach(Bullet bullets in bullet)
            {
                if (bullets.position.X >= graphics.PreferredBackBufferWidth - bullets.texture.Width)
                    bullets.isvisible = false;
            }

            for(int i = 0; i < bullet.Count; i++)
            {
                if (!bullet[i].isvisible)
                {
                    bullet.RemoveAt(i);
                    i--;
                }
            }
        }

        public void shoot()
        {
            Bullet newbullet = new Bullet(Content.Load<Texture2D>("bullet"));
            newbullet = new Vector2(hero.position,hero.position.Y);
            newbullet.position = hero.position + newbullet.velocity * 5;
        }

        protected override void UnloadContent()
        {

        }

        float spawn = 0;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Enemy enemies in enemy)
                enemies.Update(graphics.GraphicsDevice);


            if (hero.position.X <= 0)
                hero.position.X = 1;

            if (hero.position.X >= graphics.PreferredBackBufferWidth - hero.texture.Width)
                hero.position.X = graphics.PreferredBackBufferWidth - hero.texture.Width - 1;

            hero.Update(gameTime);

            LoadEnemies();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            hero.Draw(spriteBatch);

            foreach (Enemy enemies in enemy)
                enemies.Draw(spriteBatch);

            foreach (Bullet bullets in bullet)
                bullets.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
