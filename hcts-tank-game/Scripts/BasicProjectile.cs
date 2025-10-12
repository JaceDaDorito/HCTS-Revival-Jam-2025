using Godot;
using System;

namespace HCTSTankGame;

//May need to abstract this to its own weapons class
public partial class BasicProjectile : ProjectileBase
{

	[Export]
	public float speed = 10f;
	public override void _PhysicsProcess(double delta)
	{
		Translate(direction * (float)(speed* delta));
		Vector2 motion = direction * (float)(speed * delta);

		KinematicCollision2D kc2D = MoveAndCollide(motion);
		if (kc2D != null)
		{
			Collide(kc2D);
		};
	}

	public override void OnAreaEntered(Rid areaRid, Area2D area, long areaShapeIndex, long localShapeIndex)
	{
		
	}

	public virtual void Collide(KinematicCollision2D kc2D)
	{
		Vector2 normal = kc2D.GetNormal();

		if (normal.Dot(direction) >= 0) return;

		//why
		direction = direction.Bounce(normal);
		Rotation = direction.Angle();
		GD.Print("Direction: " + direction + " Normal: " + normal + " DOT: " + normal.Dot(direction));
	}
}
