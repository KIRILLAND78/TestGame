using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
	static class Assets
	{
		public static Texture2D chooseTexture { get; private set; }
		public static Texture2D chosenTexture { get; private set; }
		public static Texture2D pawnTexture { get; private set; }
		public static Texture2D pawnTexture_w { get; private set; }
		public static Texture2D boardTexture { get; private set; }
		public static Texture2D towerTexture { get; private set; }
		public static Texture2D towerTexture_w { get; private set; }
		public static Texture2D bishopTexture { get; private set; }
		public static Texture2D bishopTexture_w { get; private set; }
		public static Texture2D horseTexture { get; private set; }
		public static Texture2D horseTexture_w { get; private set; }
		public static Texture2D kingTexture { get; private set; }
		public static Texture2D kingTexture_w { get; private set; }
		public static Texture2D queenTexture { get; private set; }
		public static Texture2D queenTexture_w { get; private set; }



		public static SpriteFont Font { get; private set; }





		public static void Load(ContentManager Content)
		{
			// TODO: use this.Content to load your game content here
			pawnTexture = Content.Load<Texture2D>("import/ChessTextures/Pawn2_B");
			pawnTexture_w = Content.Load<Texture2D>("import/ChessTextures/Pawn2_W");
			towerTexture = Content.Load<Texture2D>("import/ChessTextures/Tower_B");
			towerTexture_w = Content.Load<Texture2D>("import/ChessTextures/Tower_W");
			bishopTexture = Content.Load<Texture2D>("import/ChessTextures/Bishop_B");
			bishopTexture_w = Content.Load<Texture2D>("import/ChessTextures/Bishop_W");
			horseTexture = Content.Load<Texture2D>("import/ChessTextures/Horse_B");
			horseTexture_w = Content.Load<Texture2D>("import/ChessTextures/Horse_W");
			kingTexture = Content.Load<Texture2D>("import/ChessTextures/King_B");
			kingTexture_w = Content.Load<Texture2D>("import/ChessTextures/King_W");
			queenTexture = Content.Load<Texture2D>("import/ChessTextures/Queen_B");
			queenTexture_w = Content.Load<Texture2D>("import/ChessTextures/Queen_W");
			chooseTexture = Content.Load<Texture2D>("import/Choose");
			boardTexture = Content.Load<Texture2D>("import/Board");
			chosenTexture = Content.Load<Texture2D>("import/Chosen");

			//Font = Content.Load<SpriteFont>("Font");
		}
	}
}