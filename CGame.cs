using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{

    public class CGame : Game
    {

        SpriteFont textBlock;
        
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public string Text;
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
        {//так, сделал
            
            Assets.Load(Content);
            textBlock = Content.Load<SpriteFont>("NewSpriteFont");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            EntityManager.Add(new Tower(new Vector2(0, 0), true));
            EntityManager.Add(new Tower(new Vector2(448, 0), true));
            EntityManager.Add(new Horse(new Vector2(64, 0), true));
            EntityManager.Add(new Horse(new Vector2(384, 0), true));
            EntityManager.Add(new Bishop(new Vector2(128, 0), true));
            EntityManager.Add(new Bishop(new Vector2(320, 0), true));
            EntityManager.Add(new Queen(new Vector2(192, 0), true));

            //вернуть короля!!!(но потом)
            //EntityManager.Add(new King(new Vector2(256, 0), true));

            for (int i=0; i <= 7; i++) 
            { 
                EntityManager.Add(new Pawn(new Vector2(i*64, 64), true));//добавление существ на изи.
            }//Черные
            EntityManager.Add(new Tower(new Vector2(448, 448), false));
            EntityManager.Add(new Tower(new Vector2(0, 448), false));
            EntityManager.Add(new Horse(new Vector2(64, 448), false));
            EntityManager.Add(new Horse(new Vector2(384, 448), false));
            EntityManager.Add(new Bishop(new Vector2(128, 448), false));
            EntityManager.Add(new Bishop(new Vector2(320, 448), false));
            EntityManager.Add(new Queen(new Vector2(192, 448), false));
            EntityManager.Add(new King(new Vector2(256, 448), false));
            for (int i = 0; i <= 7; i++)
            {
                EntityManager.Add(new Pawn(new Vector2(i * 64, 384), false));//добавление существ на изи.


                                                            }//Белые
            EntityManager.addedEntities.ForEach(Pawn=> {
            if (Pawn.Position.Y > 128)
                {
                    Pawn.black = false;


                }
            }
                
                );
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            EntityManager.Update();//Kiri: вызываем update у EManager. Он передает update() всем остальным сущ-вам.
            Player.update();
            Player.updatepos(0,0,64);
            Text = "Black move!";

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

            //рисуем текст 
            
            Vector2 position = new Microsoft.Xna.Framework.Vector2(625, 150); // position
            Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(80, 80, 80);// color grey
            _spriteBatch.DrawString(textBlock, Text, position, color); // draw text
            _spriteBatch.End();
            base.Draw(gameTime);

            
           
        }
    }
}
