using Godot;
using System;

namespace HCTSTankGame;

public partial class Barrel : Sprite2D
{
	[Export]
	public Node2D muzzle; 
	public override void _Process(double delta)
	{
		LookAtMouse();
	}

	public void LookAtMouse()
	{
		Vector2 mousePos = GetViewport().GetMousePosition();
		LookAt(mousePos);
	}
}
