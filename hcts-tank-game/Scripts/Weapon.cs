using Godot;
using System;

namespace HCTSTankGame;

//May need to abstract this to its own weapons class
//This is very prototype
public partial class Weapon : Node { 
	[Export]
	public Node2D muzzle;
	[Export]
	private int ammo;
	[Export]
	public PackedScene projectileType;

	private Node main;

	private bool ready = false;
	private bool infiniteAmmo = false;
	private int outgoingShotCount = 0;

    public override void _Ready()
    {
		outgoingShotCount = 0;
		infiniteAmmo = ammo < 1;
		main = GetTree().CurrentScene;
		ready = true;
    }
	
	public void Fire(Vector2 aimDirection)
	{
		if (!ready) return;

		if (outgoingShotCount >= ammo && !infiniteAmmo) return;

		outgoingShotCount++;
		ProjectileBase projectile = projectileType.Instantiate<ProjectileBase>();
		projectile.direction = aimDirection;
		projectile.Rotation = aimDirection.Angle();
		projectile.GlobalPosition = muzzle.GlobalPosition;
		projectile.onProjectileDestroyed += OnProjectileDestroy;
		main.CallDeferred(Node.MethodName.AddChild, projectile);
    }

	public void OnProjectileDestroy()
	{
		outgoingShotCount--;
	}
}
