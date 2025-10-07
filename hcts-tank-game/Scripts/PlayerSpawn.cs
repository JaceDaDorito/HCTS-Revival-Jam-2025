using Godot;
using System;


namespace HCTSTankGame;
public partial class PlayerSpawn : Node2D
{
	public override void _Ready()
	{
		Hide();
		Global.PlayerSpawnInstance = this;
		Global.GameManager.SpawnPlayer(); //Move this to wherever is necessary. Make sure Spawning the Player happens AFTER the spawn point is stored
	}
}
