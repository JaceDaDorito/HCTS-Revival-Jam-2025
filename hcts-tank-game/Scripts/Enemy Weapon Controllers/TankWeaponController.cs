using Godot;
using HCTSTankGame;
using HCTSTankGame.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;


namespace HCTSTankGame;
public partial class TankWeaponController : Sprite2D
{
	[Export]
	public Weapon weapon;
	[Export]
	public Godot.Timer timer;
	[Export]
	public RayCast2D lineOfSight;
	[Export]
	public float rotationSpeed = 2.5f;
	[Export]
	public float cooldown = 2f;
	[Export]
	public bool leadsShots = false;
	[Export]
	public bool knowsGeometry = false;

	private Node main;
	private Vector2 playerLocation;
	private Vector2 playerVelocity;
	private float projectileSpeed;
	[Export]
	public Line2D raycastVisual;

    private bool visible = false;
	private bool readyToFire = true;

	private float lerpValue = 0;
	private float easeValue = 0;
	private Vector2 _aimDirection => Vector2.FromAngle(GlobalRotation); 

	public override void _Ready()
	{
		timer.Timeout += Timer_Timeout;
		timer.WaitTime = cooldown;
    }

	public override void _Process(double delta)
	{
		playerLocation = GameManager.Instance.PlayerInstance.TargetMotor.GlobalPosition;
		playerVelocity = GameManager.Instance.PlayerInstance.TargetMotor.Velocity;

		Vector2 target = Vector2.Zero;
		Vector2 targetDirection = Vector2.Zero;
		//if you're close enough to the tank
        if ((GlobalPosition - playerLocation).Length() < 220)
		{
			//set targeting config
			if(leadsShots)
			{
				target = playerLocation + (playerVelocity * 20 * (float)delta);
				//LookAt(target);
			}
			else 
			{
				target = playerLocation;
                //LookAt(playerLocation);
            }

			targetDirection = target - GlobalPosition;
			//set the aim beam's global rotation to 0 every frame so it doesn't mess up the detection
			raycastVisual.GlobalRotation = 0;
			lineOfSight.GlobalRotation = 0;

			//aimbeam uses LoS on actual player location.
			lineOfSight.TargetPosition = playerLocation - GlobalPosition;
            raycastVisual.SetPointPosition(1, target - GlobalPosition);
            if (!lineOfSight.IsColliding() && !visible)
			{
				visible = true;
				//force a shorter cooldown to prevent instant shots if ready to fire
				if (readyToFire)
				{
					readyToFire = false;
					timer.Start(0.5);
				}
			}
			//if ready to fire, shoot
			else if (!lineOfSight.IsColliding() && readyToFire)
			{
				weapon.Fire(_aimDirection);
				readyToFire = false;
				timer.Start(cooldown);
			}
			//if back into cover, revert to not visible
			else if (lineOfSight.IsColliding())
			{
				visible = false;
			}

			//separately, if visible, rotate towards target.
			if (visible)
			{
				Rotation = Mathf.RotateToward(GlobalRotation, targetDirection.Angle(), rotationSpeed * (float)delta);
				//Rotate((targetDirection.Angle() - GlobalRotation) * rotationSpeed * (float)delta);
            }
		}
		//otherwise don't do anything
	}

	private void Timer_Timeout()
	{
		//reset readyToFire
		readyToFire = true;
	}
}
