using Godot;
using System;

namespace HCTSTankGame;

//May need to abstract this to its own weapons class
public partial class ProjectileBase : CharacterBody2D
{
	[Export]
	public Area2D area2d;
	[Export]
	public float lifetime = 5f;
	[Export]
	public Master owner;

	public Vector2 direction;

	public delegate void OnProjectileDestroyed();
	public event OnProjectileDestroyed onProjectileDestroyed;

	private double timer;

	public override void _Ready()
	{
		area2d.AreaShapeEntered += OnAreaEntered;
		timer = lifetime;
	}

	public override void _Process(double delta)
	{
		timer -= delta;
		
		if(timer < 0)
		{
			Destroy();
		}
	}

	public virtual void Destroy()
	{
		onProjectileDestroyed.Invoke();
		QueueFree();
	}

	public virtual void OnAreaEntered(Rid areaRid, Area2D area, long areaShapeIndex, long localShapeIndex)
	{
		
	}

}
