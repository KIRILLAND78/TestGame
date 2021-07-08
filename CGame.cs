using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
    public class CGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public CGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 768;
            _graphics.PreferredBackBufferHeight = 512;
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Assets.Load(Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            EntityManager.Add(new Pawn(new Vector2(0, 0)));
            EntityManager.Add(new Pawn(new Vector2(64, 64)));
            EntityManager.Add(new Pawn(new Vector2(128, 128)));//добавление существ на изи.
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            EntityManager.Update();//Kiri: вызываем update у EManager. Он передает update() всем остальным сущ-вам.
            Player.update();
            Player.updatepos(0,0,64);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.DarkGray);
            _spriteBatch.Begin();



            _spriteBatch.Draw(Assets.boardTexture, new Vector2(0,0), null, Color.White, 0, new Vector2(0, 0), 1f, 0, 0);
            //Рисуем доску


            EntityManager.Draw(_spriteBatch);//Kiri: вызываем Draw у EManager. Он передает draw() всем остальным сущ-вам.
            _spriteBatch.Draw(Assets.chooseTexture, new Vector2(Player.posx, Player.posy), Color.White);//Kiri: Рисуем квадрат
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
