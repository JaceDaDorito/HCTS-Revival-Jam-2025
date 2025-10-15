using Godot;
using System;

namespace HCTSTankGame;

public partial class PlayerMaster : Master
{
	public void GetPlayerInput() {
		_rotationDirection = Input.GetAxis(Global.LEFT, Global.RIGHT);
		_moveDirection = Input.GetAxis(Global.BACKWARD, Global.FORWARD);
	}

	public override void _PhysicsProcess(double delta)
	{
		GetPlayerInput();
		SetTargetMotorDirections();
	}
}
