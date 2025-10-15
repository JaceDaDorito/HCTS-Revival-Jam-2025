using Godot;
using System;

namespace HCTSTankGame;

//May need to abstract this to its own weapons class
public partial class BasicProjectile : ProjectileBase
{

	[Export]
	public float speed = 10f;
	[Export]
	public int ricochets = 1;

	private bool safe = true;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 motion = direction * (float)(speed * delta);
		Translate(motion);

		KinematicCollision2D kc2D = MoveAndCollide(motion);
		if (kc2D != null)
		{
			Collide(kc2D);
		};
	}

	public override void OnAreaEntered(Rid areaRid, Area2D area, long areaShapeIndex, long localShapeIndex)
	{
		
		bool areaIsInEntityPrecise = area.GetCollisionLayerValue((int)Global.LAYERS.EntityPrecise);

		if (areaIsInEntityPrecise)
		{
			Master master = area.GetOwner<Master>();
			GD.Print("Safe: " + safe + " Is Owner: " + (owner == master) +
				" Owner: " + (owner != null) + " Hit Master: " + (master != null));
			if (safe && owner == master)
			{
				return;
			}

			master.Death();
			Destroy();
		}
		else
		{
			Destroy();
		}
	}

	public virtual void Collide(KinematicCollision2D kc2D)
	{
		ricochets--;
		safe = false;
		if (ricochets < 0) Destroy();

		Vector2 normal = kc2D.GetNormal();

		if (normal.Dot(direction) >= 0) return;

		direction = direction.Bounce(normal);
		Rotation = direction.Angle();
		GD.Print("Direction: " + direction + " Normal: " + normal + " DOT: " + normal.Dot(direction));
	}
}
