using Godot;
using System;


namespace HCTSTankGame;
public partial class GameManager : Node
{
	[Export]
	public PackedScene Player;

	[Export]
	public PackedScene Spawn;

	private Node2D spawnPoint => Global.SceneInfoInstance.spawnPoint;


	public void SpawnPlayer()
	{
		if(Global.SceneInfoInstance.spawnPoint == null)
		{
			GD.PrintErr("Spawn Point not found, not spawning player.");
			return;
		}

		GD.Print("Initializing Player");
		Global.PlayerInstance = Player.Instantiate<PlayerMaster>();

		//Should probably delay this to the scene with actual gameplay.
		AddChild(Global.PlayerInstance);
		Global.PlayerInstance.TargetMotor.Position = spawnPoint.GlobalPosition;
		Global.PlayerInstance.TargetMotor.Rotation = spawnPoint.GlobalRotation;
	}

	//Called on starts
	public override void _Ready()
	{
		Global.GameManager = this;
	}

	//Called every frame
	public override void _Process(double delta)
	{
		
	}
}
