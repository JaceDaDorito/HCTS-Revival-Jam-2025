using Godot;
using System;

namespace HCTSTankGame;

public partial class CharacterMotor : CharacterBody2D
{
    [Export]
    public float Speed { get; set; } = 400f;
    [Export]
    public float RotationSpeed { get; set; } = 1.5f;

    public float _rotationDirection { get; set; }
    public float _moveDirection { get; set; } //relative to orientation

    public override void _PhysicsProcess(double delta)
    {
        Velocity = Transform.X * _moveDirection * Speed;
        Rotation += _rotationDirection * RotationSpeed * (float)delta;
        MoveAndSlide();
    }
}