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
	public RayCast2D aimBeam;
	[Export]
	public float rotationSpeed = 1.5f;
	[Export]
	public float cooldown = 2f;

	private Node main;
	private Vector2 playerLocation;
	[Export]
	public Line2D raycast;

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
		//if you're close enough to the tank
        if((GlobalPosition - playerLocation).Length() < 220)
		{
			//look at player
			LookAt(playerLocation);

			//set global rotation to 0 every frame so it doesn't mess up the detection
			raycast.GlobalRotation = 0;
			aimBeam.GlobalRotation = 0;

			aimBeam.TargetPosition = playerLocation - GlobalPosition;
            raycast.SetPointPosition(1, playerLocation - GlobalPosition);
            if (!aimBeam.IsColliding() && !visible)
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
			else if (!aimBeam.IsColliding() && readyToFire)
			{
				weapon.Fire(_aimDirection);
				readyToFire = false;
				timer.Start(cooldown);
			}
			//if back into cover, revert to not visible
			else if (aimBeam.IsColliding())
			{
				visible = false;
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
