using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;


namespace HCTSTankGame;

public static partial class Global
{

	public readonly static Vector2 DEFAULT_RESOLUTION  = new Vector2(640, 360);

	//Controls
	public const string LEFT = "left";
	public const string RIGHT = "right";
	public const string FORWARD = "forward";
	public const string BACKWARD = "backward";
	public const string FIRE = "fire";

	public const string DEBUGNEXTLEVL = "nextlevel";

	public const string NULL_LEVEL_UID = "uid://dkjaiqaqe57qc";
}
