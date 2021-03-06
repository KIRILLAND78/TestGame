
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;


namespace TestGame
{
    class EntityManager
	{	static int[,] field = new int[8,8];
		static List<Entity> entities = new List<Entity>();
		static List<Pawn> pawns = new List<Pawn>();
		static List<Tower> towers = new List<Tower>();
		static List<Bishop> bishops = new List<Bishop>();
		static List<Horse> horses = new List<Horse>();
		static List<Queen> queens = new List<Queen>();
		static List<King> kings = new List<King>();
		//public static IEnumerable<BlackHole> BlackHoles { get { return blackHoles; } }

		static bool isUpdating;
		static public List<Entity> addedEntities = new List<Entity>();

		public static int Count { get { return entities.Count; } }

		static public string text = "Black!";
		static public bool hodblack =true;

		public static void MoveDone()
        {
			hodblack = !hodblack;
			text=hodblack ? "Black!" : "White!";
        }

		/// <summary>
		/// Говорит, какая фигура расположена в данной позиции. Возвращает фигуру как класс.
		/// </summary>

		public static Entity FindEntity(int x, int y)
		{
			Entity trigger=null;
			entities.ForEach(Ent =>
			{
				if ((Ent.Position.X==x)&&(Ent.Position.Y == y))
                {
					trigger = Ent;
                }
			});
			//fix this code later
			return trigger;
		}

		/// <summary>
		/// Говорит, опасна ли эта клетка для короля. Возвращает bool.
		/// </summary>

		public static bool TileIsDangerous(int x, int y, bool black)
		{
			bool trigger = false;
			entities.ForEach(Ent =>
			{
				if (!(Ent is King)&&((Ent.black!=black)&&((Ent.CanGo(x,y)&&!(Ent is Pawn))|| (Ent.CanAttack(x, y) && (Ent is Pawn)))))
				{
					trigger = true;
				}
				if ((Ent is King)&&(Ent.black!=black))
                {
					if (((x==Ent.Position.X)|| (x == Ent.Position.X+64)|| (x == Ent.Position.X - 64))&& ((y == Ent.Position.Y) || (y == Ent.Position.Y+64) || (y == Ent.Position.Y-64))){
						trigger = true;
                    }

                }
			});
			//fix this code later
			return trigger;
		}

		/// <summary>
		/// Добавляет фигуру.
		/// </summary>

		public static void Add(Entity entity)
		{
			if (!isUpdating)
				AddEntity(entity);
			else
				addedEntities.Add(entity);
		}


		private static void AddEntity(Entity entity)
		{
			entities.Add(entity);
			if (entity is Pawn)
				pawns.Add(entity as Pawn);
			if (entity is Tower)
				towers.Add(entity as Tower);
			if (entity is Bishop)
				bishops.Add(entity as Bishop);
			if (entity is Horse)
				horses.Add(entity as Horse);
			if (entity is King)
				kings.Add(entity as King);
			if (entity is Queen)
				queens.Add(entity as Queen);
		}

		public static void Update()
		{
			isUpdating = true;
			foreach (var entity in entities)
			{
				entity.Update();
			}

			isUpdating = false;

			foreach (var entity in addedEntities)
				AddEntity(entity);

			addedEntities.Clear();

			entities = entities.Where(x => !x.IsExpired).ToList();
			pawns = pawns.Where(x => !x.IsExpired).ToList();
		}
		public static IEnumerable<Entity> GetNearbyEntities(Vector2 position, float radius)
		{
			return entities.Where(x => Vector2.DistanceSquared(position, x.Position) < radius * radius);
		}
		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (var entity in entities)
				entity.Draw(spriteBatch);
		}


	}
}
