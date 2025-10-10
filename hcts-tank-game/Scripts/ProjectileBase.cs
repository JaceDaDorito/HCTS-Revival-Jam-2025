using Godot;
using System;

namespace HCTSTankGame;

//May need to abstract this to its own weapons class
public partial class ProjectileBase : Sprite2D
{
    [Export]
    public float lifetime = 5f;

    public Vector2 direction;

    public delegate void OnProjectileDestroyed();
    public event OnProjectileDestroyed onProjectileDestroyed;

    private double timer;

    public override void _Ready()
    {
        timer = lifetime;
    }
    public override void _Process(double delta)
    {
        timer -= delta;
        
        if(timer < 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        onProjectileDestroyed.Invoke();
        QueueFree();
    }


}
