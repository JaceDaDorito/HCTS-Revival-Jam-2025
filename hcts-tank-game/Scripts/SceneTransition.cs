using Godot;
using System;

namespace HCTSTankGame;

public partial class SceneTransition : CanvasLayer
{
    public static SceneTransition Instance { get; private set; }

    [Export]
    private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        Instance = this;
    }
    public async void ChangeScene(String target)
    {
        animationPlayer.Play("dissolve");
        await ToSignal(animationPlayer, "animation_finished");
        GetTree().ChangeSceneToFile(target);
        animationPlayer.PlayBackwards("dissolve");
    }
}