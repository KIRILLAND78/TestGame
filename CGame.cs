using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
    public class CGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        Player myplayer = new Player();
        public CGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Assets.Load(Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            EntityManager.Add(new Pawn(new Vector2(20, 20)));
            EntityManager.Add(new Pawn(new Vector2(60, 60)));
            EntityManager.Add(new Pawn(new Vector2(123, 123)));//добавление существ на изи.
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            EntityManager.Update();//Kiri: вызываем update у EManager. Он передает update() всем остальным сущ-вам.
            myplayer.update();
            myplayer.updatepos(0,0,64);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            EntityManager.Draw(_spriteBatch);
            _spriteBatch.Draw(Assets.chooseTexture, new Vector2(myplayer.posx, myplayer.posy), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
