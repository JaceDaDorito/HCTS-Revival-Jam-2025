using Godot;
using HCTSTankGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Master : Node
{
    [Export]
    protected CharacterMotor _targetMotor;
    public CharacterMotor TargetMotor
    {
        get { return _targetMotor; }
    }

    [Export]
    protected Camera2D _camera;

    public Camera2D Camera
    {
        get { return _camera; }
    }

    protected float _rotationDirection;
    protected float _moveDirection; //relative to orientation

    public delegate void OnMasterDeath();
    public event OnMasterDeath onMasterDeath;

    public virtual void SetTargetMotorDirections()
    {
        this._targetMotor._rotationDirection = _rotationDirection;
        this._targetMotor._moveDirection = _moveDirection;
    }

    public override void _PhysicsProcess(double delta)
    {
        SetTargetMotorDirections();
    }

    public virtual void Death()
    {
        this._targetMotor?.LockMovementAndHide();
        onMasterDeath?.Invoke();
    }
}