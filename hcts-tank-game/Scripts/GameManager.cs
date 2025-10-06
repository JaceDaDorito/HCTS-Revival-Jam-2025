using Godot;
using System;


namespace HCTSTankGame;
public partial class GameManager : Node
{
    [Export]
    public PackedScene Player;

    [Export]
    public PackedScene Spawn;    

    //Called on starts
    public override void _Ready()
    {
        GD.Print("Initializing Player");
        PlayerMaster player = Player.Instantiate<PlayerMaster>();

        //Should probably delay this to the scene with actual gameplay.
        GetTree().CurrentScene.AddChild(player);
        //player.Position = new Vector2(0f, 0f);
    }

    //Called every frame
    public override void _Process(double delta)
    {
        
    }
}