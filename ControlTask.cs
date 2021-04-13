using System;

namespace func_rocket
{
	public class ControlTask
	{
		public static double GetAngelBetweenVectors(Vector vector1, Vector vector2)
		{
			return Math.Acos((vector1.X * vector2.X + vector1.Y * vector2.Y) / (vector1.Length * vector2.Length));
		}

		public static Turn ControlRocket(Rocket rocket, Vector target)
		{
			var rocketTarget = rocket.Location - target;
			var vectorDirection = new Vector(1, 0).Rotate(rocket.Direction);
			var vectorMoving = (vectorDirection + rocket.Velocity.Normalize());
			var angleMovingToTarget = GetAngelBetweenVectors(vectorMoving, rocketTarget);

			if (angleMovingToTarget <= 1e-2)
				return Turn.None;

			var vectorRight = vectorMoving.Rotate(angleMovingToTarget / 2);
			var vectorLeft = vectorMoving.Rotate(-angleMovingToTarget / 2);
			var angleRight = GetAngelBetweenVectors(vectorRight, rocketTarget);
			var angleLeft = GetAngelBetweenVectors(vectorLeft, rocketTarget);

			return angleRight < angleLeft ? Turn.Left : Turn.Right;
		}
	}
}