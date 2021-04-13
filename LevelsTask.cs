using System;
using System.Collections.Generic;
using System.Drawing;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();

		public static Vector GetGravityUp(Size size, Vector vector)
		{
			return new Vector(0, -300 / (size.Height - vector.Y + 300));
		}

		public static Vector GetGravityWhiteHole(Size size, Vector vector)
		{
			var toWhiteHole = new Vector(vector.X - 600, vector.Y - 200);
			return toWhiteHole.Normalize() * 140 * toWhiteHole.Length / (toWhiteHole.Length * toWhiteHole.Length + 1);
		}

		public static Vector GetGravityBlaskHole(Size size, Vector vector)
		{
			var blackHolePosition = new Vector((600 + 200) / 2, (500 + 200) / 2);
			var toBlackHole = blackHolePosition - vector;
			return new Vector(blackHolePosition.X - vector.X, blackHolePosition.Y - vector.Y).Normalize() *
				300 * toBlackHole.Length / (toBlackHole.Length * toBlackHole.Length + 1);
		}

		public static Vector GetGravitBlackAndWhiteHole(Size size, Vector vector)
		{
			var toWhiteHole = new Vector(vector.X - 600, vector.Y - 200);
			var blackHolePosition = new Vector((600 + 200) / 2, (500 + 200) / 2);
			var toBlackHole = blackHolePosition - vector;
			return (toWhiteHole.Normalize() * 140 * toWhiteHole.Length
			/ (toWhiteHole.Length * toWhiteHole.Length + 1) +
			new Vector(blackHolePosition.X - vector.X, blackHolePosition.Y - vector.Y).Normalize() *
				300 * toBlackHole.Length / (toBlackHole.Length * toBlackHole.Length + 1)) / 2;
		}

		public static IEnumerable<Level> CreateLevels()
		{
			var rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
			yield return new Level("Zero",
				rocket, new Vector(600, 200), (size, vector) => Vector.Zero,
				standardPhysics);

			yield return new Level("Heavy",
				rocket, new Vector(600, 200), (size, vector) => new Vector(0, 0.9),
				standardPhysics);

			yield return new Level("Up",
				rocket, new Vector(700, 500), (size, vector) => GetGravityUp(size, vector),
				standardPhysics);

			yield return new Level("WhiteHole",
				rocket, new Vector(600, 200), (size, vector) => GetGravityWhiteHole(size, vector),
				standardPhysics);

			yield return new Level("BlackHole",
				rocket, new Vector(600, 200), (size, vector) => GetGravityBlaskHole(size, vector),
				standardPhysics);

			yield return new Level("BlackAndWhite",
				rocket, new Vector(600, 200),
				(size, vector) => GetGravitBlackAndWhiteHole(size, vector),
				standardPhysics);
		}
	}
}