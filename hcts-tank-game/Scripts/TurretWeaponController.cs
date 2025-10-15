using Godot;
using HCTSTankGame;
using HCTSTankGame.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;


namespace HCTSTankGame;
public partial class TurretWeaponController : Sprite2D
{
	[Export]
	public Weapon weapon;
	[Export]
	public Godot.Timer timer;
	[Export]
	public float[] angleList;
	[Export]
	public float rotationSpeed = 1.5f;
	[Export]
	public float cooldown = 2f;

	private Node main;
	private float nextAngle;
	private int angleID;

	private int _index = 0;
	private int index
	{
		get { return _index; }
		set { _index = value % angleList.Length; }
	}
	private bool rotating = false;
	private float lerpValue = 0;
	private float easeValue = 0;
	private Vector2 _aimDirection => Vector2.FromAngle(GlobalRotation); 

	public override void _Ready()
	{
		timer.Timeout += Timer_Timeout;
		timer.WaitTime = cooldown;

		nextAngle = angleList[angleID];
		Rotate(Mathf.DegToRad(angleList[index]));
	}

	public override void _Process(double delta)
	{
		if (rotating)
		{
			easeValue += (float)delta * rotationSpeed;
			lerpValue = MathUtils.easeInSine(easeValue);
			lerpValue = Mathf.Clamp(lerpValue, 0, 1);

			float initRotation = Mathf.DegToRad(angleList[index]);
			float finalRotation = Mathf.DegToRad(angleList[(index + 1) % angleList.Length]);

			Rotation = Mathf.LerpAngle(initRotation,
				finalRotation,
				lerpValue);

			GD.Print("In Progress Rotation: " + Rotation);

			if(lerpValue >= 1) {
				rotating = false;
				index++;

				GD.Print("Rotation: " + initRotation + " " + finalRotation);
			}
		}
	}
	private void Timer_Timeout()
	{
		//fire on timer
		GD.Print(_aimDirection);
		weapon.Fire(_aimDirection);

		lerpValue = 0;
		easeValue = 0;
		
		rotating = true;

		timer.Start();
	}
}
