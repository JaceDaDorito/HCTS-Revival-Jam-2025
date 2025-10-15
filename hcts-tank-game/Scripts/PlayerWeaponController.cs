using Godot;
using System;

namespace HCTSTankGame;

//May need to abstract this to its own weapons class
//This is very prototype
public partial class PlayerWeaponController : Sprite2D
{
    [Export]
    public Weapon weapon;

    private Node main;

    private Vector2 _aimPoint;

    private Vector2 _aimDirection => (_aimPoint - GlobalPosition).Normalized();
    public override void _PhysicsProcess(double delta)
    {
        LookAtMouse();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsAction(Global.FIRE) & @event.IsPressed())
        {
            GD.Print(_aimDirection);
            weapon.Fire(_aimDirection);
        }
    }

    public void LookAtMouse()
    {
        _aimPoint = GetGlobalMousePosition();
        LookAt(_aimPoint);
    }
}
