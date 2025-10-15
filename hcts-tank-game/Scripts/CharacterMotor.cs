using Godot;
using System;

namespace HCTSTankGame;

public partial class CharacterMotor : CharacterBody2D
{
    [Export]
    public float Speed { get; set; }
    [Export]
    public float RotationSpeed { get; set; } = 1.5f;

    public float _rotationDirection { get; set; }
    public float _moveDirection { get; set; } //relative to orientation

    private bool lockedMovement = false;

    public void LockMovementAndHide()
    {
        lockedMovement = true;
        Velocity = Vector2.Zero;
        this.Hide();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (lockedMovement) return;

        Velocity = Transform.X * _moveDirection * Speed;
        Rotation += _rotationDirection * RotationSpeed * (float)delta;
        MoveAndSlide();
    }
}