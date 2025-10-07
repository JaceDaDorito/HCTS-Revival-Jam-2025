using Godot;
using System;

namespace HCTSTankGame;

public partial class PlayerMaster : Node
{
    [Export]
    public CharacterMotor targetMotor;

    private float _rotationDirection;
    private float _moveDirection; //relative to orientation
    public void GetPlayerInput() {
        _rotationDirection = Input.GetAxis("left", "right");
        _moveDirection = Input.GetAxis("backward", "forward");
    }

    public void SetTargetMotorDirections()
    {
        targetMotor._rotationDirection = _rotationDirection;
        targetMotor._moveDirection = _moveDirection;
    }

    public override void _PhysicsProcess(double delta)
    {
        GetPlayerInput();
        SetTargetMotorDirections();
    }

}