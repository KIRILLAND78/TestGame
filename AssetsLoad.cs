using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
	static class Assets
	{
		public static Texture2D chooseTexture { get; private set; }
		public static Texture2D pawnTexture { get; private set; }



		public static SpriteFont Font { get; private set; }





		public static void Load(ContentManager Content)
		{
			// TODO: use this.Content to load your game content here
			pawnTexture = Content.Load<Texture2D>("import/pawn");
			chooseTexture = Content.Load<Texture2D>("import/Choose");

			//Font = Content.Load<SpriteFont>("Font");
		}
	}
}