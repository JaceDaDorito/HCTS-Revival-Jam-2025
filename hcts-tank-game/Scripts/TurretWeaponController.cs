using Godot;
using HCTSTankGame;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

public partial class TurretWeaponController : Sprite2D
{
    [Export]
    public Weapon weapon;
    [Export]
    public Godot.Timer timer;

    private Node main;
    private float rotationSpeed = 1.5f;
    private List<float> angleList = new List<float>();
    private float nextAngle;
    private int angleID;

    private Vector2 _aimDirection => Vector2.FromAngle(GlobalRotation); 

    public override void _Ready()
    {
        timer.Timeout += Timer_Timeout;
        angleList.Add(45);
        angleList.Add(135);
        angleList.Add(225);
        angleList.Add(315);

        angleID = 0;
        nextAngle = angleList[angleID];
        Rotate(Mathf.DegToRad(45));
    }

    public override void _PhysicsProcess(double delta)
    {
        /* This shit isn't how you do it
        float rot1 = Rotation;
        float rot2 = Rotation + (float)(Math.PI / 2);
        double timeAlongLerp = timer.TimeLeft - (timeLowThreshold * 2);

        //slow rotation
        //Rotation += ((float)(1 * delta));

        if (timeLowThreshold < timer.TimeLeft && timer.TimeLeft < timeHighThreshold)
        {
            float interpolation = (float)(timeHighThreshold * (1 - timeAlongLerp) + (timeLowThreshold * timeAlongLerp));
            
        }
        */

        /* this shit doesn't work either, probably just ask jace
        if (Mathf.RadToDeg(Rotation) != nextAngle)
        {
            Rotation += rotationSpeed * (float)delta;
            GD.Print(Rotation);
        }
        else
        {
            //if its close enough, force lock position
            GD.Print("Lock");
            Rotation = Mathf.DegToRad(nextAngle);
        }
        */
    }

    private void Timer_Timeout()
    {
        //fire on timer
        GD.Print(_aimDirection);
        weapon.Fire(_aimDirection);

        //Activate Rotation
        Rotate(Mathf.DegToRad(90));
        if(angleID >= angleList.Count - 1)
        {
            angleID = 0;
        }
        else
        {
            angleID += 1;
        }

        nextAngle = angleList[angleID];
        //GD.Print(nextAngle);

        //restart timer
        timer.Start();
    }
}
