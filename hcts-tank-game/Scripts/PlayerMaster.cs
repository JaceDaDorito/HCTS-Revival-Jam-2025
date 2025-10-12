using Godot;
using System;

namespace HCTSTankGame;

public partial class PlayerMaster : Node
{
	[Export]
	private CharacterMotor _targetMotor;
	public CharacterMotor TargetMotor
	{
		get { return _targetMotor; }
	}
	
	[Export]
	private Camera2D _camera;

	public Camera2D Camera
	{
		get { return _camera; }
	}

	private float _rotationDirection;
	private float _moveDirection; //relative to orientation
	public void GetPlayerInput() {
		_rotationDirection = Input.GetAxis(Global.LEFT, Global.RIGHT);
		_moveDirection = Input.GetAxis(Global.BACKWARD, Global.FORWARD);
	}

	public void SetTargetMotorDirections()
	{
		this._targetMotor._rotationDirection = _rotationDirection;
		this._targetMotor._moveDirection = _moveDirection;
	}

	public override void _PhysicsProcess(double delta)
	{
		GetPlayerInput();
		SetTargetMotorDirections();
	}

}
