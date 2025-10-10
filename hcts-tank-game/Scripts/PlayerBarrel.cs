using Godot;
using System;

namespace HCTSTankGame;

//May need to abstract this to its own weapons class
//This is very prototype
public partial class PlayerBarrel : Sprite2D
{
	[Export]
	public Node2D muzzle;
	[Export]
	private int amountOfProjectiles;
	[Export]
	public PackedScene projectileType;

	private Node main;

	private bool ready = false;
	private int outgoingShotCount;

	private Vector2 _aimDirection;

	public Vector2 AimDirection
	{
		get { return _aimDirection; }
	}

    public override void _Ready()
    {
		outgoingShotCount = 0;
		main = GetTree().CurrentScene;
		ready = true;
    }
    public override void _Process(double delta)
	{
		LookAtMouse();
	}

	public void LookAtMouse()
	{
        _aimDirection = GetGlobalMousePosition();
		LookAt(_aimDirection);
	}

	public void Fire()
	{
		if (!ready) return;

		if (outgoingShotCount >= amountOfProjectiles) return;

		outgoingShotCount++;
		ProjectileBase projectile = projectileType.Instantiate<ProjectileBase>();
		projectile.direction = AimDirection;
		projectile.Rotation = AimDirection.Angle();
		projectile.GlobalPosition = muzzle.GlobalPosition;
		projectile.ZIndex = ZIndex - 1;
		main.CallDeferred("AddChild", projectile);
    }

	public void OnProjectileDestroy()
	{

	}
}
