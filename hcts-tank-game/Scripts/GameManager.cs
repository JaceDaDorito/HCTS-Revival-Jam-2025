using Godot;
using System;


namespace HCTSTankGame;
public partial class GameManager : Node
{
	[Export]
	public PackedScene Player;

	[Export]
	public PackedScene Spawn;    

	public void SpawnPlayer()
	{
		GD.Print("Initializing Player");
		Global.PlayerInstance = Player.Instantiate<PlayerMaster>();

		//Should probably delay this to the scene with actual gameplay.
		AddChild(Global.PlayerInstance);
		Global.PlayerInstance.targetMotor.Position = Global.PlayerSpawnInstance.GlobalPosition;
		Global.PlayerInstance.targetMotor.Rotation = Global.PlayerSpawnInstance.GlobalRotation;
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
