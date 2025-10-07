using Godot;
using System;
using System.Runtime.CompilerServices;


namespace HCTSTankGame;

public partial class Global : Node
{
    public static Global Instance { get; private set; }
    public static GameManager GameManager { get; set; }

    public static PlayerMaster PlayerInstance { get; set; }
    public static PlayerSpawn PlayerSpawnInstance { get; set; }
    public override void _Ready()
    {
        Instance = this;
    }
}
