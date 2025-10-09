using Godot;
using System;
using System.Runtime.CompilerServices;


namespace HCTSTankGame;

public partial class Global : Node
{

	public readonly static Vector2 DEFAULT_RESOLUTION  = new Vector2(640, 360);

	public static Global Instance { get; private set; }
	public static GameManager GameManager { get; set; }

	public static PlayerMaster PlayerInstance { get; set; }
	public static SceneInfo SceneInfoInstance { get; set; }
	public override void _Ready()
	{
		Instance = this;
	}
}
